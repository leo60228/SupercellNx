﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using Common;
using UltimateOrb;

namespace Cpu64 {
	public abstract partial class BaseCpu {
		public readonly IKernel Kernel;
		public ulong PC, SP;
		public readonly ulong[] X = new ulong[32];
		public readonly Vector128<float>[] V = new Vector128<float>[32];

		public byte Exclusive8;
		public ushort Exclusive16;
		public uint Exclusive32;
		public ulong Exclusive64;

		public ulong NZCV {
			get => (NZCV_N << 31) | (NZCV_Z << 30) | (NZCV_C << 29) | (NZCV_V << 28);
			set {
				NZCV_N = (value >> 31) & 1;
				NZCV_Z = (value >> 30) & 1;
				NZCV_C = (value >> 29) & 1;
				NZCV_V = (value >> 28) & 1;
			}
		}
		public ulong NZCV_N, NZCV_Z, NZCV_C, NZCV_V;

		public ulong TlsBase;
		
		protected BaseCpu(IKernel kernel) => Kernel = kernel;
		public abstract void Run(ulong pc, ulong sp, bool one = false);

		public void Log(string message) => Kernel.Log(message);
		public void LogError(string message) =>
			Kernel.LogExclusive(() => {
				Kernel.Log(message);
				Environment.Exit(1);
			});

		public void DebugRegs() {
			Kernel.LogExclusive(() => {
				Log("========================");
				Log($"NZCV {NZCV_N}{NZCV_Z}{NZCV_C}{NZCV_V}");
				Log($"PC == {Kernel.MapAddress(PC)}    SP == 0x{SP:X}");
				for(var i = 0; i < 31; ++i)
					Log($"X{i} == {Kernel.MapAddress(X[i])}");
				Log("========================");
			});
		}
		
		public static T SignExt<T>(ulong value, int size) {
			if(typeof(T) == typeof(long))
				return (T) (object) ((value & (1UL << (size - 1))) != 0 ? (long) value - (1L << size) : (long) value);
			if(typeof(T) == typeof(int))
				return (T) (object) ((value & (1UL << (size - 1))) != 0 ? (int) value - (1 << size) : (int) value);
			throw new NotImplementedException($"Unknown return for SignExt: {typeof(T)}");
		}

		protected static ulong MakeWMask(uint n, uint imms, uint immr, long m, int immediate) =>
			MakeMasks(n, imms, immr, (int) m, immediate != 0).Item1;
		protected static ulong MakeTMask(uint n, uint imms, uint immr, long m, int immediate) =>
			MakeMasks(n, imms, immr, (int) m, immediate != 0).Item2;

		static int HighestSetBit(ulong v, int bits) {
			for(var i = bits - 1; i >= 0; --i)
				if((v & (1UL << i)) != 0)
					return i;
			return -1;
		}
		static ulong ZeroExtend(ulong v, int bits) => v & Ones(bits);
		static ulong Ones(int bits) => Enumerable.Range(0, bits).Select(i => 1UL << i).Aggregate((a, b) => a | b);
		static ulong Replicate(ulong v, int bits, int start, int rep, int ext) {
			var repval = (v >> start) & Ones(rep);
			var times = ext / rep;
			var val = 0UL;
			for(var i = 0; i < times; ++i)
				val = (val << rep) | repval;
			return v | (val << start);
		}
		static ulong RollRight(ulong v, int size, int rotate) => ((v << (size - rotate)) | (v >> rotate)) & Ones(size);
		static (ulong, ulong) MakeMasks(uint n, uint imms, uint immr, int m, bool immediate) {
			var len = HighestSetBit((n << 6) | (imms ^ 0b111111U), 7);
			Debug.Assert(len > 0);
			Debug.Assert(m >= 1 << len);

			var levels = ZeroExtend(Ones(len), 6);
			Debug.Assert(!immediate || (imms & levels) != levels);

			var S = imms & levels;
			var R = immr & levels;

			var diff = (S - R) & 0b111111;
			var esize = 1 << len;
			var d = diff & Ones(len);

			var welem = ZeroExtend(Ones((int) (S + 1)), esize);
			var telem = ZeroExtend(Ones((int) (d + 1)), esize);

			var wmask = Replicate(RollRight(welem, esize, (int) R), esize, 0, esize, m);
			var tmask = Replicate(telem, esize, 0, esize, m);
			return (wmask, tmask);
		}

		public uint ReverseBits(uint v) {
			var x = 0U;
			for(var i = 0; i < 32; ++i)
				x |= ((v >> i) & 1) << (31 - i);
			return x;
		}

		public ulong ReverseBits(ulong v) {
			var x = 0UL;
			for(var i = 0; i < 64; ++i)
				x |= ((v >> i) & 1) << (63 - i);
			return x;
		}

		public uint CountLeadingZeros(uint v) {
			for(var i = 0; i < 32; ++i)
				if(((v >> (31 - i)) & 1) == 1)
					return (uint) i;
			return 32;
		}

		public ulong CountLeadingZeros(ulong v) {
			for(var i = 0; i < 64; ++i)
				if(((v >> (63 - i)) & 1) == 1)
					return (uint) i;
			return 64;
		}

		public uint AddWithCarrySetNzcv(uint operand1, uint operand2, uint carryIn) {
			unchecked {
				var usum = operand1 + operand2 + carryIn;
				var ssum = (int) operand1 + (int) operand2 + (int) carryIn;
				var result = usum;
				var n = result >> 31;
				var z = result == 0 ? 1U : 0;
				var c = (uint) ((((ulong) operand1 + operand2 + carryIn) >> 32) & 1);
				var v = operand1 >> 31 == operand2 >> 31 && usum >> 31 != operand1 >> 31 ? 1U : 0;
				NZCV = (n << 31) | (z << 30) | (c << 29) | (v << 28);
				//$"{operand1:X} + {operand2:X} + {carryIn} -> {usum:X} {n}{z}{c}{v}".Debug();
				return usum;
			}
		}

		public ulong AddWithCarrySetNzcv(ulong operand1, ulong operand2, ulong carryIn) {
			unchecked {
				var usum = operand1 + operand2 + carryIn;
				var ssum = (long) operand1 + (long) operand2 + (long) carryIn;
				var result = usum;
				var n = result >> 63;
				var z = result == 0 ? 1U : 0;
				var c = (uint) ((((UInt128) operand1 + operand2 + carryIn) >> 64) & 1);
				var v = operand1 >> 63 == operand2 >> 63 && usum >> 63 != operand1 >> 63 ? 1U : 0;
				NZCV = (n << 31) | (z << 30) | (c << 29) | (v << 28);
				//$"{operand1:X} + {operand2:X} + {carryIn} -> {usum:X} {n}{z}{c}{v}".Debug();
				return usum;
			}
		}

		public void FloatCompare(float operand1, float operand2) {
			if(float.IsNaN(operand1) || float.IsNaN(operand2))
				NZCV = 0b0011UL << 28;
			else if(operand1 == operand2)
				NZCV = 0b0110UL << 28;
			else if(operand1 < operand2)
				NZCV = 0b1000UL << 28;
			else
				NZCV = 0b0010UL << 28;
		}
		
		public void FloatCompare(double operand1, double operand2) {
			if(double.IsNaN(operand1) || double.IsNaN(operand2))
				NZCV = 0b0011UL << 28;
			else if(operand1 == operand2)
				NZCV = 0b0110UL << 28;
			else if(operand1 < operand2)
				NZCV = 0b1000UL << 28;
			else
				NZCV = 0b0010UL << 28;
		}
		
		public void Svc(uint svc) => Kernel.Svc((int) svc);

		public ulong SR(uint op0, uint op1, uint crn, uint crm, uint op2) {
			var reg = ((0b10 | op0) << 14) | (op1 << 11) | (crn << 7) | (crm << 3) | op2;
			switch(reg) {
				case 0b11_011_0000_0000_001: // CtrEl0
					return 0x8444c004;
				case 0b11_011_0100_0100_000: // FPCR
					return 0;
				case 0b11_011_0100_0100_001: // FPSR
					return 0;
				case 0b11_011_1101_0000_011: // TPIDR
					return TlsBase;
				case 0b11_011_1110_0000_001: // CntpctEl0
					return 0;
				default:
					throw new NotSupportedException($"Unknown SR: S{op0|2}_{op1}_{crn}_{crm}_{op2}");
			}
		}

		public void SR(uint op0, uint op1, uint crn, uint crm, uint op2, ulong value) {
			var reg = ((0b10 | op0) << 14) | (op1 << 11) | (crn << 7) | (crm << 3) | op2;
			switch(reg) {
				case 0b11_011_0100_0100_000: // FPCR
					break;
				case 0b11_011_0100_0100_001: // FPSR
					break;
				case 0b11_011_1101_0000_010: // TPIDR_EL0
					break;
				default:
					throw new NotSupportedException($"Unknown SR: S{op0|2}_{op1}_{crn}_{crm}_{op2}");
			}
		}

		protected static unsafe OutT Bitcast<InT, OutT>(InT value) {
			var ov = Activator.CreateInstance<OutT>();
			switch(value) {
				case uint v:
					switch(ov) {
						case float _: return (OutT) (object) *(float*) &v;
						default: throw new NotImplementedException();
					}
				case ulong v:
					switch(ov) {
						case double _: return (OutT) (object) *(double*) &v;
						default: throw new NotImplementedException();
					}
				case float v:
					switch(ov) {
						case uint _: return (OutT) (object) *(uint*) &v;
						case int _: return (OutT) (object) *(int*) &v;
						default: throw new NotImplementedException();
					}
				case double v:
					switch(ov) {
						case ulong _: return (OutT) (object) *(ulong*) &v;
						case long _: return (OutT) (object) *(long*) &v;
						default: throw new NotImplementedException();
					}
				default: throw new NotImplementedException();
			}
		}

		protected Vector128<float> Insert<ElementT>(Vector128<float> vec, uint index, ElementT value)
			where ElementT : struct => vec.As<float, ElementT>().WithElement((int) index, value).As<ElementT, float>();

		public Vector128<float> VectorCountBits(Vector128<float> vec, long elems) {
			var ret = Vector128<byte>.Zero;
			var ivec = vec.As<float, byte>();
			for(var i = 0; i < elems; ++i)
				ret = ret.WithElement(i, (byte) Popcnt.PopCount(ivec.GetElement(i)));
			return ret.As<byte, float>();
		}

		public ulong VectorSumUnsigned(Vector128<float> vec, long esize, long count) {
			switch(esize) {
				case 8: {
					var bvec = vec.As<float, byte>();
					var sum = 0UL;
					for(var i = 0; i < count; ++i)
						sum += bvec.GetElement(i);
					return sum;
				}
				default: throw new NotSupportedException($"Unknown size for VectorSumUnsigned: {esize}");
			}
		}

		public Vector128<float> VectorExtract(Vector128<float> _a, Vector128<float> _b, uint Q, uint _index) {
			var index = (int) _index;
			var a = _a.As<float, byte>();
			var b = _b.As<float, byte>();
			
			var r = new Vector128<byte>();
			var count = Q == 0 ? 8 : 16;

			if(count == 8) {
				for(var i = index; i < 8; ++i)
					r = r.WithElement(i - index, a.GetElement(i));
				var offset = 8 - index;
				for(var i = offset; i < 8; ++i)
					r = r.WithElement(i, a.GetElement(i - offset));
			} else {
				for(var i = index; i < 16; ++i)
					r = r.WithElement(i - index, a.GetElement(i));
				var offset = 16 - index;
				for(var i = offset; i < 16; ++i)
					r = r.WithElement(i, a.GetElement(i - offset));
			}
			
			return r.As<byte, float>();
		}

		public uint FloatToFixed32(float fvalue, int fbits) {
			return unchecked((uint) (int) MathF.Round(fvalue * (1 << fbits)));
		}

		public ulong FloatToFixed64(float fvalue, int fbits) {
			return unchecked((ulong) (long) MathF.Round(fvalue * (1 << fbits)));
		}

		public uint FloatToFixed32(double fvalue, int fbits) {
			return unchecked((uint) (int) Math.Round(fvalue * (1 << fbits)));
		}

		public ulong FloatToFixed64(double fvalue, int fbits) {
			return unchecked((ulong) (long) Math.Round(fvalue * (1 << fbits)));
		}

		public unsafe byte CompareAndSwap(uint* ptr, uint value, uint comparand) =>
			unchecked(Interlocked.CompareExchange(ref *(int*) ptr, (int) value, (int) comparand) == (int) comparand ? (byte) 0 : (byte) 1);

		public unsafe byte CompareAndSwap(ulong* ptr, ulong value, ulong comparand) =>
			unchecked(Interlocked.CompareExchange(ref *(long*) ptr, (long) value, (long) comparand) == (long) comparand ? (byte) 0 : (byte) 1);
	}
}