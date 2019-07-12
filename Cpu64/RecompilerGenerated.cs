using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#if FULLSIGIL
using Sigil;
using Label = Sigil.Label;
#else
using SigilLite;
using Label = SigilLite.Label;
#endif
using UltimateOrb;
// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable RedundantCast
// ReSharper disable UnusedVariable
#pragma warning disable 162, 219

namespace Cpu64 {
	public partial class Recompiler {
		public bool Recompile(uint inst, ulong pc) {
			unchecked {
				/* ADCS */
				if((inst & 0x7FE0FC00U) == 0x3A000000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallAddWithCarrySetNzcv((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]), (RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), (RuntimeValue<byte>) (NZCV_CR))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]), (RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm]), (RuntimeValue<byte>) (NZCV_CR)));
					}
					return true;
				}
				/* ADD-extended-register */
				if((inst & 0x7FE00000U) == 0x0B200000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var imm = (inst >> 10) & 0x7U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r1 = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var r2 = (string) (((byte) ((((byte) ((((ulong) (option)) & ((ulong) (0x3))))) == (0x3)) ? 1U : 0U) != 0) ? ("X") : ("W"));
					var extend = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ((string) ((option) switch { 0x0 => "UXTB", 0x1 => "UXTH", 0x2 => "LSL", 0x3 => "UXTX", 0x4 => "SXTB", 0x5 => "SXTH", 0x6 => "SXTW", _ => "SXTX" })) : ((string) ((option) switch { 0x0 => "UXTB", 0x1 => "UXTH", 0x2 => "UXTW", 0x3 => "LSL", 0x4 => "SXTB", 0x5 => "SXTH", 0x6 => "SXTW", _ => "SXTX" })));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var m = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn]))) + (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((option) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFF))))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFF))))), 0x4 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<byte>) ((RuntimeValue<byte>) (m)), 8))))), 0x5 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (m)), 16))))), _ => (RuntimeValue<uint>) (m) })).ShiftLeft(imm)))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn]))) + (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((option) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFF))))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFF))))), 0x4 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<byte>) ((RuntimeValue<byte>) (m)), 8))))), 0x5 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (m)), 16))))), _ => (RuntimeValue<uint>) (m) })).ShiftLeft(imm)))));
					} else {
						// Runtime else!
						if(((byte) ((((byte) ((((ulong) (option)) & ((ulong) (0x3))))) == (0x3)) ? 1U : 0U)) != 0) {
							// Runtime if!
							if(rd == 31)
								SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).ShiftLeft(imm))));
							else
								XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).ShiftLeft(imm))));
						} else {
							// Runtime else!
							var m = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))).Store();
							// Runtime let!
							if(rd == 31)
								SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((option) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFF))))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFF))))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFFFFFF))))), 0x4 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<byte>) ((RuntimeValue<byte>) (m)), 8))))), 0x5 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (m)), 16))))), 0x6 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((RuntimeValue<uint>) (m)), 32))))), _ => (RuntimeValue<ulong>) (m) })).ShiftLeft(imm))));
							else
								XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((option) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFF))))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFF))))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFFFFFF))))), 0x4 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<byte>) ((RuntimeValue<byte>) (m)), 8))))), 0x5 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (m)), 16))))), 0x6 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((RuntimeValue<uint>) (m)), 32))))), _ => (RuntimeValue<ulong>) (m) })).ShiftLeft(imm))));
						}
					}
					return true;
				}
				/* ADD-immediate */
				if((inst & 0x7F800000U) == 0x11000000U) {
					var size = (inst >> 31) & 0x1U;
					var sh = (inst >> 22) & 0x1U;
					var imm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shift = (long) (((byte) (((sh) == (0x0)) ? 1U : 0U) != 0) ? (0x0) : (0xC));
					var simm = (uint) (((uint) ((uint) (imm))) << (int) (shift));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn]))) + (RuntimeValue<uint>) (RuntimeValue<uint>) (simm)));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn]))) + (RuntimeValue<uint>) (RuntimeValue<uint>) (simm)));
					} else {
						// Runtime else!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<uint>) (simm));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<uint>) (simm));
					}
					return true;
				}
				/* ADD-shifted-register */
				if((inst & 0x7F200000U) == 0x0B000000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var b = ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rm) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rm]))).Store();
						// Runtime let!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn]))) + (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((RuntimeValue<uint>) (b))).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => throw new NotImplementedException() }))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn]))) + (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((RuntimeValue<uint>) (b))).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => throw new NotImplementedException() }))));
					} else {
						// Runtime else!
						var b = ((RuntimeValue<ulong>) ((rm) == 31 ? SPR : XR[(int) rm])).Store();
						// Runtime let!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (b))).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => throw new NotImplementedException() })));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (b))).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => throw new NotImplementedException() })));
					}
					return true;
				}
				/* ADDS-immediate */
				if((inst & 0x7F800000U) == 0x31000000U) {
					var size = (inst >> 31) & 0x1U;
					var sh = (inst >> 22) & 0x1U;
					var imm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shift = (long) (((byte) (((sh) == (0x0)) ? 1U : 0U) != 0) ? (0x0) : (0xC));
					var simm = (uint) (((uint) ((uint) (imm))) << (int) (shift));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallAddWithCarrySetNzcv((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn])), simm, 0x0)));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]), simm, 0x0));
					}
					return true;
				}
				/* ADDS-shifted-register */
				if((inst & 0x7F200000U) == 0x2B000000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var b = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallAddWithCarrySetNzcv((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]), (RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<uint>) ((RuntimeValue<uint>) (((b).ShiftLeft((RuntimeValue<uint>) (32 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) }), 0x0)));
					} else {
						// Runtime else!
						var b = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]), (RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((b).ShiftLeft((RuntimeValue<uint>) (64 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) }), 0x0));
					}
					return true;
				}
				/* ADRP */
				if((inst & 0x9F000000U) == 0x90000000U) {
					var immlo = (inst >> 29) & 0x3U;
					var immhi = (inst >> 5) & 0x7FFFFU;
					var rd = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>((ulong) ((ulong) (((ulong) (ulong) (((ulong) (((ulong) ((ushort) ((ushort) (0x0)))) << 0)) | ((ulong) (((ulong) (immlo)) << 12)))) | ((ulong) (((ulong) (immhi)) << 14)))), 33));
					var addr = (ulong) ((ulong) ((ulong) ((ulong) (((ulong) (((ulong) ((ushort) ((ushort) (0x0)))) << 0)) | ((ulong) (((ulong) ((ulong) ((ulong) ((ulong) (((ulong) (pc)) >> (int) (0xC)))))) << 12))))) + (ulong) (imm));
					XR[(int) rd] = addr;
					return true;
				}
				/* AND-immediate */
				if((inst & 0x7F800000U) == 0x12000000U) {
					var size = (inst >> 31) & 0x1U;
					var up = (inst >> 22) & 0x1U;
					var immr = (inst >> 16) & 0x3FU;
					var imms = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (ulong) (MakeWMask(up, imms, immr, (long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x20) : (0x40)), 0x1));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) & ((RuntimeValue<uint>) ((uint) ((uint) (imm)))))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) & ((RuntimeValue<uint>) ((uint) ((uint) (imm)))))));
					} else {
						// Runtime else!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) & ((RuntimeValue<ulong>) (imm))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) & ((RuntimeValue<ulong>) (imm))));
					}
					return true;
				}
				/* AND-shifted-register */
				if((inst & 0x7F200000U) == 0x0A000000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var b = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) & ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<uint>) ((RuntimeValue<uint>) (((b).ShiftLeft((RuntimeValue<uint>) (32 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) }))))));
					} else {
						// Runtime else!
						var b = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) & ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((b).ShiftLeft((RuntimeValue<uint>) (64 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) })))));
					}
					return true;
				}
				/* ANDS-shifted-register */
				if((inst & 0x7F200000U) == 0x6A000000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var b = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						var result = ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) & ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<uint>) ((RuntimeValue<uint>) (((b).ShiftLeft((RuntimeValue<uint>) (32 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) })))))).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) (result);
						// Runtime let!
						NZCV_NR = (RuntimeValue<uint>) ((result).ShiftRight(0x1F));
						// Runtime let!
						NZCV_ZR = (RuntimeValue<byte>) ((result) == (0x0));
						// Runtime let!
						NZCV_CR = 0x0;
						// Runtime let!
						NZCV_VR = 0x0;
					} else {
						// Runtime else!
						var b = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						var result = ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) & ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((b).ShiftLeft((RuntimeValue<uint>) (64 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) })))))).Store();
						// Runtime let!
						XR[(int) rd] = result;
						// Runtime let!
						NZCV_NR = (RuntimeValue<ulong>) ((result).ShiftRight(0x3F));
						// Runtime let!
						NZCV_ZR = (RuntimeValue<byte>) ((result) == (0x0));
						// Runtime let!
						NZCV_CR = 0x0;
						// Runtime let!
						NZCV_VR = 0x0;
					}
					return true;
				}
				/* ANDS-immediate */
				if((inst & 0x7F800000U) == 0x72000000U) {
					var size = (inst >> 31) & 0x1U;
					var up = (inst >> 22) & 0x1U;
					var immr = (inst >> 16) & 0x3FU;
					var imms = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (ulong) (MakeWMask(up, imms, immr, (long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x20) : (0x40)), 0x1));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var result = ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) & ((RuntimeValue<ulong>) (imm))))).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) (result);
						// Runtime let!
						NZCV_NR = (RuntimeValue<uint>) ((result).ShiftRight(0x1F));
						// Runtime let!
						NZCV_ZR = (RuntimeValue<byte>) ((result) == (0x0));
						// Runtime let!
						NZCV_CR = 0x0;
						// Runtime let!
						NZCV_VR = 0x0;
					} else {
						// Runtime else!
						var result = ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) & ((RuntimeValue<ulong>) (imm))))).Store();
						// Runtime let!
						XR[(int) rd] = result;
						// Runtime let!
						NZCV_NR = (RuntimeValue<ulong>) ((result).ShiftRight(0x3F));
						// Runtime let!
						NZCV_ZR = (RuntimeValue<byte>) ((result) == (0x0));
						// Runtime let!
						NZCV_CR = 0x0;
						// Runtime let!
						NZCV_VR = 0x0;
					}
					return true;
				}
				/* ASRV */
				if((inst & 0x7FE0FC00U) == 0x1AC02800U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])))).ShiftRight((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]))))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])))).ShiftRight((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])))));
					}
					return true;
				}
				/* B */
				if((inst & 0xFC000000U) == 0x14000000U) {
					var imm = (inst >> 0) & 0x3FFFFFFU;
					var addr = (ulong) ((ulong) ((ulong) (pc)) + (ulong) ((long) (SignExt<long>((uint) (((uint) ((uint) (imm))) << (int) (0x2)), 28))));
					Branch(addr);
					return true;
				}
				/* B.cond */
				if((inst & 0xFF000010U) == 0x54000000U) {
					var imm = (inst >> 5) & 0x7FFFFU;
					var cond = (inst >> 0) & 0xFU;
					var addr = (ulong) ((ulong) ((ulong) (pc)) + (ulong) ((long) (SignExt<long>((uint) (((uint) ((uint) (imm))) << (int) (0x2)), 21))));
					var condstr = (string) ((cond) switch { 0x0 => "EQ", 0x1 => "NE", 0x2 => "CS", 0x3 => "CC", 0x4 => "MI", 0x5 => "PL", 0x6 => "VS", 0x7 => "VC", 0x8 => "HI", 0x9 => "LS", 0xA => "GE", 0xB => "LT", 0xC => "GT", 0xD => "LE", _ => "AL" });
					// Runtime block!
					var result = ((RuntimeValue<byte>) (((byte) ((cond) >> (int) (0x1))) switch { 0x0 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_ZR)), 0x1 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR)), 0x2 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_NR)), 0x3 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_VR)), 0x4 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), 0x5 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR)))), 0x6 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR))))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), _ => (RuntimeValue<byte>) (0x1) })).Store();
					// Runtime block!
					Label temp_0 = Ilg.DefineLabel(), temp_1 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((byte) ((((byte) ((byte) ((((ulong) (cond)) & ((ulong) (0x1)))))) & ((byte) ((byte) (((cond) != (0xF)) ? 1U : 0U)))))) != 0 ? ((RuntimeValue<byte>) (!(result))) : (result))) == 0, temp_1);
					Branch(addr);
					Branch(temp_0);
					Label(temp_1);
					Branch(pc + 4);
					Label(temp_0);
					return true;
				}
				/* BFM */
				if((inst & 0x7F800000U) == 0x33000000U) {
					var size = (inst >> 31) & 0x1U;
					var N = (inst >> 22) & 0x1U;
					var immr = (inst >> 16) & 0x3FU;
					var imms = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						// Runtime block!
						var dst = ((RuntimeValue<uint>) ((rd) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rd])).Store();
						// Runtime block!
						var src = ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).Store();
						var wmask = (uint) ((uint) ((ulong) (MakeWMask(N, imms, immr, 0x20, 0x0))));
						var tmask = (uint) ((uint) ((ulong) (MakeTMask(N, imms, immr, 0x20, 0x0))));
						// Runtime block!
						var bot = ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) (dst)) & ((RuntimeValue<uint>) ((uint) (~(wmask)))))))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) (((src).ShiftLeft((RuntimeValue<uint>) (32 - (immr)))) | ((src).ShiftRight((RuntimeValue<uint>) (immr)))))) & ((RuntimeValue<uint>) (wmask))))))))).Store();
						// Runtime block!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) (dst)) & ((RuntimeValue<uint>) ((uint) (~(tmask)))))))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) (bot)) & ((RuntimeValue<uint>) (tmask)))))))));
					} else {
						// Runtime else!
						// Runtime block!
						var dst = ((RuntimeValue<ulong>) ((rd) == 31 ? 0UL : XR[(int) rd])).Store();
						// Runtime block!
						var src = ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Store();
						var wmask = (ulong) (MakeWMask(N, imms, immr, 0x40, 0x0));
						var tmask = (ulong) (MakeTMask(N, imms, immr, 0x40, 0x0));
						// Runtime block!
						var bot = ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (dst)) & ((RuntimeValue<ulong>) ((ulong) (~(wmask)))))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((src).ShiftLeft((RuntimeValue<uint>) (64 - (immr)))) | ((src).ShiftRight((RuntimeValue<uint>) (immr)))))) & ((RuntimeValue<ulong>) (wmask))))))))).Store();
						// Runtime block!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (dst)) & ((RuntimeValue<ulong>) ((ulong) (~(tmask)))))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (bot)) & ((RuntimeValue<ulong>) (tmask))))))));
					}
					return true;
				}
				/* BIC */
				if((inst & 0x7F200000U) == 0x0A200000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var b = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) & ((RuntimeValue<uint>) ((RuntimeValue<uint>) (~((RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<uint>) ((RuntimeValue<uint>) (((b).ShiftLeft((RuntimeValue<uint>) (32 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) }))))))));
					} else {
						// Runtime else!
						var b = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) & ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (~((RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((b).ShiftLeft((RuntimeValue<uint>) (64 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) })))))));
					}
					return true;
				}
				/* BL */
				if((inst & 0xFC000000U) == 0x94000000U) {
					var imm = (inst >> 0) & 0x3FFFFFFU;
					var offset = (long) (SignExt<long>((uint) (((uint) ((uint) (imm))) << (int) (0x2)), 28));
					var addr = (ulong) ((ulong) ((ulong) (pc)) + (ulong) (offset));
					// Runtime block!
					XR[(int) 0x1E] = (ulong) ((ulong) ((ulong) (pc)) + (ulong) (0x4));
					// Runtime block!
					Branch(addr);
					return true;
				}
				/* BLR */
				if((inst & 0xFFFFFC1FU) == 0xD63F0000U) {
					var rn = (inst >> 5) & 0x1FU;
					// Runtime block!
					XR[(int) 0x1E] = (ulong) ((ulong) ((ulong) (pc)) + (ulong) (0x4));
					// Runtime block!
					Branch((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]));
					return true;
				}
				/* BR */
				if((inst & 0xFFFFFC1FU) == 0xD61F0000U) {
					var rn = (inst >> 5) & 0x1FU;
					Branch((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]));
					return true;
				}
				/* CASP */
				if((inst & 0xBFE0FC00U) == 0x08207C00U) {
					var size = (inst >> 30) & 0x1U;
					var rs = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var rs2 = (ulong) ((ulong) (rs) + (ulong) (0x1));
					var rt2 = (ulong) ((ulong) (rt) + (ulong) (0x1));
					throw new NotImplementedException();
					return true;
				}
				/* CASPA */
				if((inst & 0xBFE0FC00U) == 0x08607C00U) {
					var size = (inst >> 30) & 0x1U;
					var rs = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var rs2 = (ulong) ((ulong) (rs) + (ulong) (0x1));
					var rt2 = (ulong) ((ulong) (rt) + (ulong) (0x1));
					throw new NotImplementedException();
					return true;
				}
				/* CASPAL */
				if((inst & 0xBFE0FC00U) == 0x0860FC00U) {
					var size = (inst >> 30) & 0x1U;
					var rs = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var rs2 = (ulong) ((ulong) (rs) + (ulong) (0x1));
					var rt2 = (ulong) ((ulong) (rt) + (ulong) (0x1));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						// Runtime block!
						var cl = ((RuntimeValue<uint>) ((rs) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rs])).Store();
						// Runtime block!
						var ch = ((RuntimeValue<uint>) (((ulong) ((ulong) (rs) + (ulong) (0x1))) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) (ulong) ((ulong) (rs) + (ulong) (0x1))])).Store();
						// Runtime block!
						var nl = ((RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt])).Store();
						// Runtime block!
						var nh = ((RuntimeValue<uint>) (((ulong) ((ulong) (rt) + (ulong) (0x1))) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) (ulong) ((ulong) (rt) + (ulong) (0x1))])).Store();
						// Runtime block!
						var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
						// Runtime block!
						var data = ((RuntimeValue<ulong>) (((RuntimePointer<ulong>) (address)).Value)).Store();
						// Runtime block!
						Label temp_2 = Ilg.DefineLabel(), temp_3 = Ilg.DefineLabel();
						BranchIf(((RuntimeValue<byte>) ((data) == ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (ch))).ShiftLeft(0x20)))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (cl))))))))) == 0, temp_3);
						((RuntimePointer<ulong>) (address)).Value = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (nh))).ShiftLeft(0x20)))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (nl))))));
						Branch(temp_2);
						Label(temp_3);
						Label(temp_2);
						// Runtime block!
						XR[(int) rs] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (data)));
						// Runtime block!
						XR[(int) (ulong) ((ulong) (rs) + (ulong) (0x1))] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<ulong>) ((data).ShiftRight(0x20)))));
					} else {
						// Runtime else!
						// Runtime block!
						var cl = ((RuntimeValue<ulong>) ((rs) == 31 ? 0UL : XR[(int) rs])).Store();
						// Runtime block!
						var ch = ((RuntimeValue<ulong>) (((ulong) ((ulong) (rs) + (ulong) (0x1))) == 31 ? 0UL : XR[(int) (ulong) ((ulong) (rs) + (ulong) (0x1))])).Store();
						// Runtime block!
						var nl = ((RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt])).Store();
						// Runtime block!
						var nh = ((RuntimeValue<ulong>) (((ulong) ((ulong) (rt) + (ulong) (0x1))) == 31 ? 0UL : XR[(int) (ulong) ((ulong) (rt) + (ulong) (0x1))])).Store();
						// Runtime block!
						var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
						// Runtime block!
						var dl = ((RuntimeValue<ulong>) (((RuntimePointer<ulong>) (address)).Value)).Store();
						// Runtime block!
						var dh = ((RuntimeValue<ulong>) (((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value)).Store();
						// Runtime block!
						Label temp_4 = Ilg.DefineLabel(), temp_5 = Ilg.DefineLabel();
						BranchIf(((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) ((dl) == (cl)))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) ((dh) == (ch))))))) == 0, temp_5);
						// Runtime block!
						((RuntimePointer<ulong>) (address)).Value = nl;
						// Runtime block!
						((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value = nh;
						Branch(temp_4);
						Label(temp_5);
						Label(temp_4);
						// Runtime block!
						XR[(int) rs] = dl;
						// Runtime block!
						XR[(int) (ulong) ((ulong) (rs) + (ulong) (0x1))] = dh;
					}
					return true;
				}
				/* CASPL */
				if((inst & 0xBFE0FC00U) == 0x0820FC00U) {
					var size = (inst >> 30) & 0x1U;
					var rs = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var rs2 = (ulong) ((ulong) (rs) + (ulong) (0x1));
					var rt2 = (ulong) ((ulong) (rt) + (ulong) (0x1));
					throw new NotImplementedException();
					return true;
				}
				/* CBNZ */
				if((inst & 0x7F000000U) == 0x35000000U) {
					var size = (inst >> 31) & 0x1U;
					var imm = (inst >> 5) & 0x7FFFFU;
					var rs = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var addr = (ulong) ((ulong) ((ulong) (pc)) + (ulong) ((long) (SignExt<long>((uint) ((uint) ((uint) ((imm) << (int) (0x2)))), 21))));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						Label temp_6 = Ilg.DefineLabel(), temp_7 = Ilg.DefineLabel();
						BranchIf(((RuntimeValue<byte>) (((RuntimeValue<uint>) ((rs) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rs])) != ((uint) ((uint) (0x0))))) == 0, temp_7);
						Branch(addr);
						Branch(temp_6);
						Label(temp_7);
						Branch(pc + 4);
						Label(temp_6);
					} else {
						// Runtime else!
						Label temp_8 = Ilg.DefineLabel(), temp_9 = Ilg.DefineLabel();
						BranchIf(((RuntimeValue<byte>) (((RuntimeValue<ulong>) ((rs) == 31 ? 0UL : XR[(int) rs])) != ((ulong) ((ulong) (0x0))))) == 0, temp_9);
						Branch(addr);
						Branch(temp_8);
						Label(temp_9);
						Branch(pc + 4);
						Label(temp_8);
					}
					return true;
				}
				/* CBZ */
				if((inst & 0x7F000000U) == 0x34000000U) {
					var size = (inst >> 31) & 0x1U;
					var imm = (inst >> 5) & 0x7FFFFU;
					var rs = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var addr = (ulong) ((ulong) ((ulong) (pc)) + (ulong) ((long) (SignExt<long>((uint) ((uint) ((uint) ((imm) << (int) (0x2)))), 21))));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						Label temp_10 = Ilg.DefineLabel(), temp_11 = Ilg.DefineLabel();
						BranchIf(((RuntimeValue<byte>) (((RuntimeValue<uint>) ((rs) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rs])) == ((uint) ((uint) (0x0))))) == 0, temp_11);
						Branch(addr);
						Branch(temp_10);
						Label(temp_11);
						Branch(pc + 4);
						Label(temp_10);
					} else {
						// Runtime else!
						Label temp_12 = Ilg.DefineLabel(), temp_13 = Ilg.DefineLabel();
						BranchIf(((RuntimeValue<byte>) (((RuntimeValue<ulong>) ((rs) == 31 ? 0UL : XR[(int) rs])) == ((ulong) ((ulong) (0x0))))) == 0, temp_13);
						Branch(addr);
						Branch(temp_12);
						Label(temp_13);
						Branch(pc + 4);
						Label(temp_12);
					}
					return true;
				}
				/* CCMN-immediate */
				if((inst & 0x7FE00C10U) == 0x3A400800U) {
					var size = (inst >> 31) & 0x1U;
					var imm = (inst >> 16) & 0x1FU;
					var cond = (inst >> 12) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var nzcv = (inst >> 0) & 0xFU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var condstr = (string) ((cond) switch { 0x0 => "EQ", 0x1 => "NE", 0x2 => "CS", 0x3 => "CC", 0x4 => "MI", 0x5 => "PL", 0x6 => "VS", 0x7 => "VC", 0x8 => "HI", 0x9 => "LS", 0xA => "GE", 0xB => "LT", 0xC => "GT", 0xD => "LE", _ => "AL" });
					var result = ((RuntimeValue<byte>) (((byte) ((cond) >> (int) (0x1))) switch { 0x0 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_ZR)), 0x1 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR)), 0x2 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_NR)), 0x3 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_VR)), 0x4 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), 0x5 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR)))), 0x6 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR))))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), _ => (RuntimeValue<byte>) (0x1) })).Store();
					// Runtime let!
					Label temp_14 = Ilg.DefineLabel(), temp_15 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((byte) ((((byte) ((byte) ((((ulong) (cond)) & ((ulong) (0x1)))))) & ((byte) ((byte) (((cond) != (0xF)) ? 1U : 0U)))))) != 0 ? ((RuntimeValue<byte>) (!(result))) : (result))) == 0, temp_15);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) 0x1F] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallAddWithCarrySetNzcv((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]), (uint) ((uint) (imm)), 0x0)));
					} else {
						// Runtime else!
						XR[(int) 0x1F] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]), (ulong) ((ulong) (imm)), 0x0));
					}
					Branch(temp_14);
					Label(temp_15);
					NZCVR = (ulong) (((ulong) ((ulong) (nzcv))) << (int) (0x1C));
					Label(temp_14);
					return true;
				}
				/* CCMP-immediate */
				if((inst & 0x7FE00C10U) == 0x7A400800U) {
					var size = (inst >> 31) & 0x1U;
					var imm = (inst >> 16) & 0x1FU;
					var cond = (inst >> 12) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var nzcv = (inst >> 0) & 0xFU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var condstr = (string) ((cond) switch { 0x0 => "EQ", 0x1 => "NE", 0x2 => "CS", 0x3 => "CC", 0x4 => "MI", 0x5 => "PL", 0x6 => "VS", 0x7 => "VC", 0x8 => "HI", 0x9 => "LS", 0xA => "GE", 0xB => "LT", 0xC => "GT", 0xD => "LE", _ => "AL" });
					var result = ((RuntimeValue<byte>) (((byte) ((cond) >> (int) (0x1))) switch { 0x0 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_ZR)), 0x1 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR)), 0x2 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_NR)), 0x3 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_VR)), 0x4 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), 0x5 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR)))), 0x6 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR))))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), _ => (RuntimeValue<byte>) (0x1) })).Store();
					// Runtime let!
					Label temp_16 = Ilg.DefineLabel(), temp_17 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((byte) ((((byte) ((byte) ((((ulong) (cond)) & ((ulong) (0x1)))))) & ((byte) ((byte) (((cond) != (0xF)) ? 1U : 0U)))))) != 0 ? ((RuntimeValue<byte>) (!(result))) : (result))) == 0, temp_17);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) 0x1F] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallAddWithCarrySetNzcv((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]), (uint) (~((uint) ((uint) (imm)))), 0x1)));
					} else {
						// Runtime else!
						XR[(int) 0x1F] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]), (ulong) (~((ulong) ((ulong) (imm)))), 0x1));
					}
					Branch(temp_16);
					Label(temp_17);
					NZCVR = (ulong) (((ulong) ((ulong) (nzcv))) << (int) (0x1C));
					Label(temp_16);
					return true;
				}
				/* CCMP-register */
				if((inst & 0x7FE00C10U) == 0x7A400000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var cond = (inst >> 12) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var nzcv = (inst >> 0) & 0xFU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var condstr = (string) ((cond) switch { 0x0 => "EQ", 0x1 => "NE", 0x2 => "CS", 0x3 => "CC", 0x4 => "MI", 0x5 => "PL", 0x6 => "VS", 0x7 => "VC", 0x8 => "HI", 0x9 => "LS", 0xA => "GE", 0xB => "LT", 0xC => "GT", 0xD => "LE", _ => "AL" });
					var result = ((RuntimeValue<byte>) (((byte) ((cond) >> (int) (0x1))) switch { 0x0 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_ZR)), 0x1 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR)), 0x2 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_NR)), 0x3 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_VR)), 0x4 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), 0x5 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR)))), 0x6 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR))))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), _ => (RuntimeValue<byte>) (0x1) })).Store();
					// Runtime let!
					Label temp_18 = Ilg.DefineLabel(), temp_19 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((byte) ((((byte) ((byte) ((((ulong) (cond)) & ((ulong) (0x1)))))) & ((byte) ((byte) (((cond) != (0xF)) ? 1U : 0U)))))) != 0 ? ((RuntimeValue<byte>) (!(result))) : (result))) == 0, temp_19);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) 0x1F] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallAddWithCarrySetNzcv((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]), (RuntimeValue<uint>) (~((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]))), 0x1)));
					} else {
						// Runtime else!
						XR[(int) 0x1F] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]), (RuntimeValue<ulong>) (~((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm]))), 0x1));
					}
					Branch(temp_18);
					Label(temp_19);
					NZCVR = (ulong) (((ulong) ((ulong) (nzcv))) << (int) (0x1C));
					Label(temp_18);
					return true;
				}
				/* CLREX */
				if((inst & 0xFFFFF0FFU) == 0xD503305FU) {
					var crm = (inst >> 8) & 0xFU;
					return true;
				}
				/* CLZ */
				if((inst & 0x7FFFFC00U) == 0x5AC01000U) {
					var size = (inst >> 31) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallCountLeadingZeros((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (CallCountLeadingZeros((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])));
					}
					return true;
				}
				/* CNT */
				if((inst & 0xBF3FFC00U) == 0x0E205800U) {
					var Q = (inst >> 30) & 0x1U;
					var size = (inst >> 22) & 0x3U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var t = (string) (((byte) ((byte) (((byte) (((byte) (Q)) << 0)) | ((byte) (((byte) (size)) << 1))))) switch { 0x0 => "8B", 0x1 => "16B", _ => throw new NotImplementedException() });
					VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (CallVectorCountBits((RuntimeValue<Vector128<float>>) (VR[(int) (rn)]), (long) (((byte) ((byte) (((byte) (((byte) (Q)) << 0)) | ((byte) (((byte) (size)) << 1))))) switch { 0x0 => 0x8, _ => 0x10 })));
					return true;
				}
				/* CSEL */
				if((inst & 0x7FE00C00U) == 0x1A800000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var cond = (inst >> 12) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var condstr = (string) ((cond) switch { 0x0 => "EQ", 0x1 => "NE", 0x2 => "CS", 0x3 => "CC", 0x4 => "MI", 0x5 => "PL", 0x6 => "VS", 0x7 => "VC", 0x8 => "HI", 0x9 => "LS", 0xA => "GE", 0xB => "LT", 0xC => "GT", 0xD => "LE", _ => "AL" });
					var result = ((RuntimeValue<byte>) (((byte) ((cond) >> (int) (0x1))) switch { 0x0 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_ZR)), 0x1 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR)), 0x2 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_NR)), 0x3 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_VR)), 0x4 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), 0x5 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR)))), 0x6 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR))))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), _ => (RuntimeValue<byte>) (0x1) })).Store();
					// Runtime let!
					Label temp_20 = Ilg.DefineLabel(), temp_21 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((byte) ((((byte) ((byte) ((((ulong) (cond)) & ((ulong) (0x1)))))) & ((byte) ((byte) (((cond) != (0xF)) ? 1U : 0U)))))) != 0 ? ((RuntimeValue<byte>) (!(result))) : (result))) == 0, temp_21);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]);
					}
					Branch(temp_20);
					Label(temp_21);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm]);
					}
					Label(temp_20);
					return true;
				}
				/* CSINC */
				if((inst & 0x7FE00C00U) == 0x1A800400U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var cond = (inst >> 12) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var condstr = (string) ((cond) switch { 0x0 => "EQ", 0x1 => "NE", 0x2 => "CS", 0x3 => "CC", 0x4 => "MI", 0x5 => "PL", 0x6 => "VS", 0x7 => "VC", 0x8 => "HI", 0x9 => "LS", 0xA => "GE", 0xB => "LT", 0xC => "GT", 0xD => "LE", _ => "AL" });
					var result = ((RuntimeValue<byte>) (((byte) ((cond) >> (int) (0x1))) switch { 0x0 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_ZR)), 0x1 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR)), 0x2 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_NR)), 0x3 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_VR)), 0x4 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), 0x5 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR)))), 0x6 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR))))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), _ => (RuntimeValue<byte>) (0x1) })).Store();
					// Runtime let!
					Label temp_22 = Ilg.DefineLabel(), temp_23 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((byte) ((((byte) ((byte) ((((ulong) (cond)) & ((ulong) (0x1)))))) & ((byte) ((byte) (((cond) != (0xF)) ? 1U : 0U)))))) != 0 ? ((RuntimeValue<byte>) (!(result))) : (result))) == 0, temp_23);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]);
					}
					Branch(temp_22);
					Label(temp_23);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])) + (RuntimeValue<uint>) (RuntimeValue<uint>) ((uint) ((uint) (0x1)))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x1));
					}
					Label(temp_22);
					return true;
				}
				/* CSINV */
				if((inst & 0x7FE00C00U) == 0x5A800000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var cond = (inst >> 12) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var condstr = (string) ((cond) switch { 0x0 => "EQ", 0x1 => "NE", 0x2 => "CS", 0x3 => "CC", 0x4 => "MI", 0x5 => "PL", 0x6 => "VS", 0x7 => "VC", 0x8 => "HI", 0x9 => "LS", 0xA => "GE", 0xB => "LT", 0xC => "GT", 0xD => "LE", _ => "AL" });
					var result = ((RuntimeValue<byte>) (((byte) ((cond) >> (int) (0x1))) switch { 0x0 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_ZR)), 0x1 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR)), 0x2 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_NR)), 0x3 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_VR)), 0x4 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), 0x5 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR)))), 0x6 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR))))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), _ => (RuntimeValue<byte>) (0x1) })).Store();
					// Runtime let!
					Label temp_24 = Ilg.DefineLabel(), temp_25 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((byte) ((((byte) ((byte) ((((ulong) (cond)) & ((ulong) (0x1)))))) & ((byte) ((byte) (((cond) != (0xF)) ? 1U : 0U)))))) != 0 ? ((RuntimeValue<byte>) (!(result))) : (result))) == 0, temp_25);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]);
					}
					Branch(temp_24);
					Label(temp_25);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (~((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (~((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])));
					}
					Label(temp_24);
					return true;
				}
				/* CSNEG */
				if((inst & 0x7FE00C00U) == 0x5A800400U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var cond = (inst >> 12) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var condstr = (string) ((cond) switch { 0x0 => "EQ", 0x1 => "NE", 0x2 => "CS", 0x3 => "CC", 0x4 => "MI", 0x5 => "PL", 0x6 => "VS", 0x7 => "VC", 0x8 => "HI", 0x9 => "LS", 0xA => "GE", 0xB => "LT", 0xC => "GT", 0xD => "LE", _ => "AL" });
					var result = ((RuntimeValue<byte>) (((byte) ((cond) >> (int) (0x1))) switch { 0x0 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_ZR)), 0x1 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR)), 0x2 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_NR)), 0x3 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_VR)), 0x4 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), 0x5 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR)))), 0x6 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR))))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), _ => (RuntimeValue<byte>) (0x1) })).Store();
					// Runtime let!
					Label temp_26 = Ilg.DefineLabel(), temp_27 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((byte) ((((byte) ((byte) ((((ulong) (cond)) & ((ulong) (0x1)))))) & ((byte) ((byte) (((cond) != (0xF)) ? 1U : 0U)))))) != 0 ? ((RuntimeValue<byte>) (!(result))) : (result))) == 0, temp_27);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]);
					}
					Branch(temp_26);
					Label(temp_27);
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (-((RuntimeValue<int>) ((RuntimeValue<int>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]))))))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (-((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])))))));
					}
					Label(temp_26);
					return true;
				}
				/* DMB */
				if((inst & 0xFFFFF0FFU) == 0xD50330BFU) {
					var m = (inst >> 8) & 0xFU;
					var option = (string) ((m) switch { 0xF => "SY", 0xE => "ST", 0xD => "LD", 0xB => "ISH", 0xA => "ISHST", 0x9 => "ISHLD", 0x7 => "NSH", 0x6 => "NSHST", 0x5 => "NSHLD", 0x3 => "OSH", 0x2 => "OSHST", _ => "OSHLD" });
					return true;
				}
				/* DSB */
				if((inst & 0xFFFFF0FFU) == 0xD503309FU) {
					var crm = (inst >> 8) & 0xFU;
					var option = (string) ((crm) switch { 0xF => "SY", 0xE => "ST", 0xD => "LD", 0xB => "ISH", 0xA => "ISHST", 0x9 => "ISHLD", 0x7 => "NSH", 0x6 => "NSHST", 0x5 => "NSHLD", 0x3 => "OSH", 0x2 => "OSHST", _ => "OSHLD" });
					return true;
				}
				/* DUP-general */
				if((inst & 0xBFE0FC00U) == 0x0E000C00U) {
					var Q = (inst >> 30) & 0x1U;
					var imm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var size = ((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0xF))))) == (0x0)) ? 1U : 0U) != 0) ? throw new NotImplementedException() : ((long) (((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0xF))))) == (0x8)) ? 1U : 0U) != 0) ? (0x40) : (0x20)));
					var r = (string) (((byte) (((size) == (0x40)) ? 1U : 0U) != 0) ? ("X") : ("W"));
					var T = ((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0xF))))) == (0x0)) ? 1U : 0U) != 0) ? throw new NotImplementedException() : ((string) (((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x1))))) == (0x1)) ? 1U : 0U) != 0) ? ((string) ((Q != 0) ? ("16B") : ("8B"))) : ((string) (((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x3))))) == (0x2)) ? 1U : 0U) != 0) ? ((string) ((Q != 0) ? ("8H") : ("4H"))) : ((string) (((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x7))))) == (0x4)) ? 1U : 0U) != 0) ? ((string) ((Q != 0) ? ("4S") : ("2S"))) : ((string) ((Q != 0) ? ("2D") : throw new NotImplementedException()))))))));
					var src = ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Store();
					// Runtime let!
					VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x1))))) == (0x1)) ? 1U : 0U)) != 0 ? ((RuntimeValue<Vector128<float>>) ((Q) != 0 ? ((RuntimeValue<Vector128<float>>) (((RuntimeValue<byte>) ((RuntimeValue<byte>) ((RuntimeValue<byte>) (src)))).CreateVector())) : ((RuntimeValue<Vector128<float>>) ((RuntimeValue<Vector128<float>>) (((RuntimeValue<byte>) ((RuntimeValue<byte>) ((RuntimeValue<byte>) (src)))).CreateVector()))))) : ((RuntimeValue<Vector128<float>>) (((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x3))))) == (0x2)) ? 1U : 0U)) != 0 ? ((RuntimeValue<Vector128<float>>) ((Q) != 0 ? ((RuntimeValue<Vector128<float>>) (((RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (src)))).CreateVector())) : ((RuntimeValue<Vector128<float>>) ((RuntimeValue<Vector128<float>>) (((RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (src)))).CreateVector()))))) : ((RuntimeValue<Vector128<float>>) (((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x7))))) == (0x4)) ? 1U : 0U)) != 0 ? ((RuntimeValue<Vector128<float>>) ((Q) != 0 ? ((RuntimeValue<Vector128<float>>) (((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (src)))).CreateVector())) : ((RuntimeValue<Vector128<float>>) ((RuntimeValue<Vector128<float>>) (((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (src)))).CreateVector()))))) : ((RuntimeValue<Vector128<float>>) ((Q) != 0 ? ((RuntimeValue<Vector128<float>>) (((RuntimeValue<ulong>) (src)).CreateVector())) : throw new NotImplementedException())))))));
					return true;
				}
				/* EON-shifted-register */
				if((inst & 0x7F200000U) == 0x4A200000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var b = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) ^ ((RuntimeValue<uint>) ((RuntimeValue<uint>) (~((RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<uint>) ((RuntimeValue<uint>) (((b).ShiftLeft((RuntimeValue<uint>) (32 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) }))))))));
					} else {
						// Runtime else!
						var b = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) ^ ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (~((RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((b).ShiftLeft((RuntimeValue<uint>) (64 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) })))))));
					}
					return true;
				}
				/* EOR-immediate */
				if((inst & 0x7F800000U) == 0x52000000U) {
					var size = (inst >> 31) & 0x1U;
					var up = (inst >> 22) & 0x1U;
					var immr = (inst >> 16) & 0x3FU;
					var imms = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (ulong) (MakeWMask(up, imms, immr, (long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x20) : (0x40)), 0x1));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) ^ ((RuntimeValue<uint>) ((uint) ((uint) (imm)))))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) ^ ((RuntimeValue<uint>) ((uint) ((uint) (imm)))))));
					} else {
						// Runtime else!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) ^ ((RuntimeValue<ulong>) (imm))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) ^ ((RuntimeValue<ulong>) (imm))));
					}
					return true;
				}
				/* EOR-shifted-register */
				if((inst & 0x7F200000U) == 0x4A000000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var b = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) ^ ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<uint>) ((RuntimeValue<uint>) (((b).ShiftLeft((RuntimeValue<uint>) (32 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) }))))));
					} else {
						// Runtime else!
						var b = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) ^ ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((b).ShiftLeft((RuntimeValue<uint>) (64 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) })))));
					}
					return true;
				}
				/* EXT */
				if((inst & 0xBFE08400U) == 0x2E000000U) {
					var Q = (inst >> 30) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var index = (inst >> 11) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var ts = (string) ((Q != 0) ? ("16B") : ("8B"));
					VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (CallVectorExtract((RuntimeValue<Vector128<float>>) (VR[(int) (rn)]), (RuntimeValue<Vector128<float>>) (VR[(int) (rm)]), Q, index));
					return true;
				}
				/* EXTR */
				if((inst & 0x7FA00000U) == 0x13800000U) {
					var size = (inst >> 31) & 0x1U;
					var o = (inst >> 22) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var lsb = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).ShiftLeft((ulong) ((ulong) (0x20) - (ulong) (lsb)))))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).ShiftRight(lsb)))))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).ShiftLeft((ulong) ((ulong) (0x40) - (ulong) (lsb)))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).ShiftRight(lsb))))));
					}
					return true;
				}
				/* FADD-scalar */
				if((inst & 0xFF20FC00U) == 0x1E202800U) {
					var type = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch(type) {
						case 0x3: {
							VHR[(int) (rd)] = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) (VHR[(int) (rn)])) + (RuntimeValue<float>) ((RuntimeValue<float>) (VHR[(int) (rm)])))));
							break;
						}
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) (VSR[(int) (rn)])) + (RuntimeValue<float>) ((RuntimeValue<float>) (VSR[(int) (rm)])));
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rn)])) + (RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rm)])));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FADD-vector */
				if((inst & 0xBFA0FC00U) == 0x0E20D400U) {
					var Q = (inst >> 30) & 0x1U;
					var size = (inst >> 22) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var ts = (string) (((byte) ((byte) (((byte) (((byte) (Q)) << 0)) | ((byte) (((byte) (size)) << 1))))) switch { 0x0 => "2S", 0x1 => "4S", 0x3 => "2D", _ => throw new NotImplementedException() });
					switch((byte) ((byte) (((byte) (((byte) (Q)) << 0)) | ((byte) (((byte) (size)) << 1))))) {
						case 0x0: {
							VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) ((RuntimeValue<Vector128<float>>) (((RuntimeValue<Vector128<float>>) (VR[(int) (rn)])) + ((RuntimeValue<Vector128<float>>) (VR[(int) (rm)]))));
							break;
						}
						case 0x1: {
							VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (((RuntimeValue<Vector128<float>>) (VR[(int) (rn)])) + ((RuntimeValue<Vector128<float>>) (VR[(int) (rm)])));
							break;
						}
						case 0x3: {
							VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) ((RuntimeValue<Vector128<float>>) ((RuntimeValue<Vector128<double>>) ((RuntimeValue<Vector128<float>>) (VR[(int) (rn)])) + (RuntimeValue<Vector128<double>>) ((RuntimeValue<Vector128<float>>) (VR[(int) (rm)]))));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FADDP-scalar */
				if((inst & 0xFFBFFC00U) == 0x7E30D800U) {
					var size = (inst >> 22) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("S") : ("D"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) (VR[(int) (rn)].Element<float>(0x0))) + (RuntimeValue<float>) ((RuntimeValue<float>) (VR[(int) (rn)].Element<float>(0x1))));
					} else {
						// Runtime else!
						VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) (VR[(int) (rn)].Element<double>(0x0))) + (RuntimeValue<double>) ((RuntimeValue<double>) (VR[(int) (rn)].Element<double>(0x1))));
					}
					return true;
				}
				/* FADDP-vector */
				if((inst & 0xBFA0FC00U) == 0x2E20D400U) {
					var Q = (inst >> 30) & 0x1U;
					var size = (inst >> 22) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var t = (string) (((byte) ((byte) (((byte) (((byte) (Q)) << 0)) | ((byte) (((byte) (size)) << 1))))) switch { 0x0 => "2S", 0x1 => "4S", 0x3 => "2D", _ => throw new NotImplementedException() });
					switch((byte) ((byte) (((byte) (((byte) (Q)) << 0)) | ((byte) (((byte) (size)) << 1))))) {
						case 0x0: {
							var a = ((RuntimeValue<float>) (VR[(int) (rn)].Element<float>(0x0))).Store();
							// Runtime let!
							var b = ((RuntimeValue<float>) (VR[(int) (rn)].Element<float>(0x1))).Store();
							// Runtime let!
							var c = ((RuntimeValue<float>) (VR[(int) (rm)].Element<float>(0x0))).Store();
							// Runtime let!
							var d = ((RuntimeValue<float>) (VR[(int) (rm)].Element<float>(0x1))).Store();
							// Runtime let!
							VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (((RuntimeValue<float>) ((float) ((float) (0x0)))).CreateVector());
							// Runtime let!
							VR[(int) (rd)] = VR[(int) (rd)].Insert(0x0, (RuntimeValue<float>) ((RuntimeValue<float>) (a) + (RuntimeValue<float>) (b)));
							// Runtime let!
							VR[(int) (rd)] = VR[(int) (rd)].Insert(0x1, (RuntimeValue<float>) ((RuntimeValue<float>) (c) + (RuntimeValue<float>) (d)));
							break;
						}
						case 0x1: {
							var a = ((RuntimeValue<float>) (VR[(int) (rn)].Element<float>(0x0))).Store();
							// Runtime let!
							var b = ((RuntimeValue<float>) (VR[(int) (rn)].Element<float>(0x1))).Store();
							// Runtime let!
							var c = ((RuntimeValue<float>) (VR[(int) (rn)].Element<float>(0x2))).Store();
							// Runtime let!
							var d = ((RuntimeValue<float>) (VR[(int) (rn)].Element<float>(0x3))).Store();
							// Runtime let!
							var e = ((RuntimeValue<float>) (VR[(int) (rm)].Element<float>(0x0))).Store();
							// Runtime let!
							var f = ((RuntimeValue<float>) (VR[(int) (rm)].Element<float>(0x1))).Store();
							// Runtime let!
							var g = ((RuntimeValue<float>) (VR[(int) (rm)].Element<float>(0x2))).Store();
							// Runtime let!
							var h = ((RuntimeValue<float>) (VR[(int) (rm)].Element<float>(0x3))).Store();
							// Runtime let!
							VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (((RuntimeValue<float>) ((float) ((float) (0x0)))).CreateVector());
							// Runtime let!
							VR[(int) (rd)] = VR[(int) (rd)].Insert(0x0, (RuntimeValue<float>) ((RuntimeValue<float>) (a) + (RuntimeValue<float>) (b)));
							// Runtime let!
							VR[(int) (rd)] = VR[(int) (rd)].Insert(0x1, (RuntimeValue<float>) ((RuntimeValue<float>) (c) + (RuntimeValue<float>) (d)));
							// Runtime let!
							VR[(int) (rd)] = VR[(int) (rd)].Insert(0x2, (RuntimeValue<float>) ((RuntimeValue<float>) (e) + (RuntimeValue<float>) (f)));
							// Runtime let!
							VR[(int) (rd)] = VR[(int) (rd)].Insert(0x3, (RuntimeValue<float>) ((RuntimeValue<float>) (g) + (RuntimeValue<float>) (h)));
							break;
						}
						case 0x3: {
							var a = ((RuntimeValue<double>) (VR[(int) (rn)].Element<double>(0x0))).Store();
							// Runtime let!
							var b = ((RuntimeValue<double>) (VR[(int) (rn)].Element<double>(0x1))).Store();
							// Runtime let!
							var c = ((RuntimeValue<double>) (VR[(int) (rm)].Element<double>(0x0))).Store();
							// Runtime let!
							var d = ((RuntimeValue<double>) (VR[(int) (rm)].Element<double>(0x1))).Store();
							// Runtime let!
							VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (((RuntimeValue<float>) ((float) ((float) (0x0)))).CreateVector());
							// Runtime let!
							VR[(int) (rd)] = VR[(int) (rd)].Insert(0x0, (RuntimeValue<double>) ((RuntimeValue<double>) (a) + (RuntimeValue<double>) (b)));
							// Runtime let!
							VR[(int) (rd)] = VR[(int) (rd)].Insert(0x1, (RuntimeValue<double>) ((RuntimeValue<double>) (c) + (RuntimeValue<double>) (d)));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FCCMP */
				if((inst & 0xFF200C10U) == 0x1E200400U) {
					var type = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var cond = (inst >> 12) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var nzcv = (inst >> 0) & 0xFU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					var condstr = (string) ((cond) switch { 0x0 => "EQ", 0x1 => "NE", 0x2 => "CS", 0x3 => "CC", 0x4 => "MI", 0x5 => "PL", 0x6 => "VS", 0x7 => "VC", 0x8 => "HI", 0x9 => "LS", 0xA => "GE", 0xB => "LT", 0xC => "GT", 0xD => "LE", _ => "AL" });
					var result = ((RuntimeValue<byte>) (((byte) ((cond) >> (int) (0x1))) switch { 0x0 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_ZR)), 0x1 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR)), 0x2 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_NR)), 0x3 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_VR)), 0x4 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), 0x5 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR)))), 0x6 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR))))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), _ => (RuntimeValue<byte>) (0x1) })).Store();
					// Runtime let!
					Label temp_28 = Ilg.DefineLabel(), temp_29 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((byte) ((((byte) ((byte) ((((ulong) (cond)) & ((ulong) (0x1)))))) & ((byte) ((byte) (((cond) != (0xF)) ? 1U : 0U)))))) != 0 ? ((RuntimeValue<byte>) (!(result))) : (result))) == 0, temp_29);
					switch(type) {
						case 0x0: {
							CallFloatCompare((RuntimeValue<float>) (VSR[(int) (rn)]), (RuntimeValue<float>) (VSR[(int) (rm)]));
							break;
						}
						case 0x1: {
							CallFloatCompare((RuntimeValue<double>) (VDR[(int) (rn)]), (RuntimeValue<double>) (VDR[(int) (rm)]));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					Branch(temp_28);
					Label(temp_29);
					NZCVR = (ulong) (((ulong) ((ulong) (nzcv))) << (int) (0x1C));
					Label(temp_28);
					return true;
				}
				/* FCMP */
				if((inst & 0xFF20FC17U) == 0x1E202000U) {
					var type = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var opc = (inst >> 3) & 0x1U;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					var zero = (string) (((byte) (((opc) == (0x1)) ? 1U : 0U) != 0) ? ("/0") : (""));
					switch(type) {
						case 0x0: {
							CallFloatCompare((RuntimeValue<float>) (VSR[(int) (rn)]), (RuntimeValue<float>) (((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0 ? ((float) ((float) (0x0))) : ((RuntimeValue<float>) (VSR[(int) (rm)]))));
							break;
						}
						case 0x1: {
							CallFloatCompare((RuntimeValue<double>) (VDR[(int) (rn)]), (RuntimeValue<double>) (((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0 ? ((double) ((double) (0x0))) : ((RuntimeValue<double>) (VDR[(int) (rm)]))));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FCSEL */
				if((inst & 0xFF200C00U) == 0x1E200C00U) {
					var type = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var cond = (inst >> 12) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					var condstr = (string) ((cond) switch { 0x0 => "EQ", 0x1 => "NE", 0x2 => "CS", 0x3 => "CC", 0x4 => "MI", 0x5 => "PL", 0x6 => "VS", 0x7 => "VC", 0x8 => "HI", 0x9 => "LS", 0xA => "GE", 0xB => "LT", 0xC => "GT", 0xD => "LE", _ => "AL" });
					var result = ((RuntimeValue<byte>) (((byte) ((cond) >> (int) (0x1))) switch { 0x0 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_ZR)), 0x1 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR)), 0x2 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_NR)), 0x3 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_VR)), 0x4 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (NZCV_CR))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), 0x5 => (RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR)))), 0x6 => (RuntimeValue<byte>) ((RuntimeValue<byte>) ((((RuntimeValue<byte>) ((RuntimeValue<byte>) (((RuntimeValue<byte>) (NZCV_NR)) == ((RuntimeValue<byte>) (NZCV_VR))))) & ((RuntimeValue<byte>) ((RuntimeValue<byte>) (!((RuntimeValue<byte>) (NZCV_ZR)))))))), _ => (RuntimeValue<byte>) (0x1) })).Store();
					// Runtime let!
					Label temp_30 = Ilg.DefineLabel(), temp_31 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((byte) ((((byte) ((byte) ((((ulong) (cond)) & ((ulong) (0x1)))))) & ((byte) ((byte) (((cond) != (0xF)) ? 1U : 0U)))))) != 0 ? ((RuntimeValue<byte>) (!(result))) : (result))) == 0, temp_31);
					switch(type) {
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) (VSR[(int) (rn)]);
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) (VDR[(int) (rn)]);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					Branch(temp_30);
					Label(temp_31);
					switch(type) {
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) (VSR[(int) (rm)]);
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) (VDR[(int) (rm)]);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					Label(temp_30);
					return true;
				}
				/* FCVT */
				if((inst & 0xFF3E7C00U) == 0x1E224000U) {
					var type = (inst >> 22) & 0x3U;
					var opc = (inst >> 15) & 0x3U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r1 = "";
					var r2 = "";
					var tf = (byte) ((byte) (((byte) (((byte) (opc)) << 0)) | ((byte) (((byte) (type)) << 2))));
					switch(tf) {
						case 0xC: {
							r1 = "S";
							r2 = "H";
							break;
						}
						case 0xD: {
							r1 = "D";
							r2 = "H";
							break;
						}
						case 0x3: {
							r1 = "H";
							r2 = "S";
							break;
						}
						case 0x1: {
							r1 = "D";
							r2 = "S";
							break;
						}
						case 0x7: {
							r1 = "H";
							r2 = "D";
							break;
						}
						case 0x4: {
							r1 = "S";
							r2 = "D";
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					switch(tf) {
						case 0xC: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) (VHR[(int) (rn)])));
							break;
						}
						case 0xD: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<float>) (VHR[(int) (rn)])));
							break;
						}
						case 0x3: {
							VHR[(int) (rd)] = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<float>) (VSR[(int) (rn)])));
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<float>) (VSR[(int) (rn)])));
							break;
						}
						case 0x7: {
							VHR[(int) (rd)] = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<double>) (VDR[(int) (rn)])));
							break;
						}
						case 0x4: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<double>) (VDR[(int) (rn)])));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FCVTZS-scalar-fixedpoint */
				if((inst & 0x7F3F0000U) == 0x1E180000U) {
					var size = (inst >> 31) & 0x1U;
					var type = (inst >> 22) & 0x3U;
					var scale = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var fbits = (ulong) ((ulong) (0x40) - (ulong) (scale));
					var r1 = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var r2 = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch((byte) ((byte) (((byte) (((byte) (type)) << 0)) | ((byte) (((byte) (size)) << 2))))) {
						case 0x0: {
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallFloatToFixed32((RuntimeValue<float>) (VSR[(int) (rn)]), fbits)));
							break;
						}
						case 0x4: {
							XR[(int) rd] = (RuntimeValue<ulong>) (CallFloatToFixed64((RuntimeValue<float>) (VSR[(int) (rn)]), fbits));
							break;
						}
						case 0x1: {
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallFloatToFixed32((RuntimeValue<double>) (VDR[(int) (rn)]), fbits)));
							break;
						}
						case 0x5: {
							XR[(int) rd] = (RuntimeValue<ulong>) (CallFloatToFixed64((RuntimeValue<double>) (VDR[(int) (rn)]), fbits));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FCVTZS-scalar-integer */
				if((inst & 0x7F3FFC00U) == 0x1E380000U) {
					var size = (inst >> 31) & 0x1U;
					var type = (inst >> 22) & 0x3U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var st = (byte) ((byte) (((byte) (((byte) (type)) << 0)) | ((byte) (((byte) (size)) << 2))));
					var r1 = "";
					var r2 = "";
					switch(st) {
						case 0x3: {
							r1 = "W";
							r2 = "H";
							break;
						}
						case 0x7: {
							r1 = "X";
							r2 = "H";
							break;
						}
						case 0x0: {
							r1 = "W";
							r2 = "S";
							break;
						}
						case 0x4: {
							r1 = "X";
							r2 = "S";
							break;
						}
						case 0x1: {
							r1 = "W";
							r2 = "D";
							break;
						}
						case 0x5: {
							r1 = "X";
							r2 = "D";
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					switch(st) {
						case 0x0: {
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) ((RuntimeValue<int>) ((RuntimeValue<float>) (VSR[(int) (rn)]))))));
							break;
						}
						case 0x4: {
							XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<float>) (VSR[(int) (rn)])))));
							break;
						}
						case 0x1: {
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) ((RuntimeValue<int>) ((RuntimeValue<double>) (VDR[(int) (rn)]))))));
							break;
						}
						case 0x5: {
							XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<double>) (VDR[(int) (rn)])))));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FCVTZU-scalar-fixedpoint */
				if((inst & 0x7F3F0000U) == 0x1E190000U) {
					var size = (inst >> 31) & 0x1U;
					var type = (inst >> 22) & 0x3U;
					var scale = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var fbits = (ulong) ((ulong) (0x40) - (ulong) (scale));
					var r1 = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var r2 = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch((byte) ((byte) (((byte) (((byte) (type)) << 0)) | ((byte) (((byte) (size)) << 2))))) {
						case 0x0: {
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallFloatToFixed32((RuntimeValue<float>) (VSR[(int) (rn)]), fbits)));
							break;
						}
						case 0x4: {
							XR[(int) rd] = (RuntimeValue<ulong>) (CallFloatToFixed64((RuntimeValue<float>) (VSR[(int) (rn)]), fbits));
							break;
						}
						case 0x1: {
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallFloatToFixed32((RuntimeValue<double>) (VDR[(int) (rn)]), fbits)));
							break;
						}
						case 0x5: {
							XR[(int) rd] = (RuntimeValue<ulong>) (CallFloatToFixed64((RuntimeValue<double>) (VDR[(int) (rn)]), fbits));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FCVTZU-scalar-integer */
				if((inst & 0x7F3FFC00U) == 0x1E390000U) {
					var size = (inst >> 31) & 0x1U;
					var type = (inst >> 22) & 0x3U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var st = (byte) ((byte) (((byte) (((byte) (type)) << 0)) | ((byte) (((byte) (size)) << 2))));
					var r1 = "";
					var r2 = "";
					switch(st) {
						case 0x3: {
							r1 = "W";
							r2 = "H";
							break;
						}
						case 0x7: {
							r1 = "X";
							r2 = "H";
							break;
						}
						case 0x0: {
							r1 = "W";
							r2 = "S";
							break;
						}
						case 0x4: {
							r1 = "X";
							r2 = "S";
							break;
						}
						case 0x1: {
							r1 = "W";
							r2 = "D";
							break;
						}
						case 0x5: {
							r1 = "X";
							r2 = "D";
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					switch(st) {
						case 0x0: {
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<float>) (VSR[(int) (rn)]))));
							break;
						}
						case 0x4: {
							XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<float>) (VSR[(int) (rn)])));
							break;
						}
						case 0x1: {
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<double>) (VDR[(int) (rn)]))));
							break;
						}
						case 0x5: {
							XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<double>) (VDR[(int) (rn)])));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FDIV-scalar */
				if((inst & 0xFF20FC00U) == 0x1E201800U) {
					var type = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch(type) {
						case 0x3: {
							throw new NotImplementedException();
							break;
						}
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) (VSR[(int) (rn)])) / (RuntimeValue<float>) ((RuntimeValue<float>) (VSR[(int) (rm)])));
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rn)])) / (RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rm)])));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FMAXNM-scalar */
				if((inst & 0xFF20FC00U) == 0x1E206800U) {
					var type = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch(type) {
						case 0x0: {
							var a = ((RuntimeValue<float>) (VSR[(int) (rn)])).Store();
							// Runtime let!
							var b = ((RuntimeValue<float>) (VSR[(int) (rm)])).Store();
							// Runtime let!
							VSR[(int) (rd)] = (RuntimeValue<float>) (Ternary<byte, float>((RuntimeValue<byte>) ((RuntimeValue<byte>) ((a) > (b))), a, b));
							break;
						}
						case 0x1: {
							var a = ((RuntimeValue<double>) (VDR[(int) (rn)])).Store();
							// Runtime let!
							var b = ((RuntimeValue<double>) (VDR[(int) (rm)])).Store();
							// Runtime let!
							VDR[(int) (rd)] = (RuntimeValue<double>) (Ternary<byte, double>((RuntimeValue<byte>) ((RuntimeValue<byte>) ((a) > (b))), a, b));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FMINNM-scalar */
				if((inst & 0xFF20FC00U) == 0x1E207800U) {
					var type = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch(type) {
						case 0x0: {
							var a = ((RuntimeValue<float>) (VSR[(int) (rn)])).Store();
							// Runtime let!
							var b = ((RuntimeValue<float>) (VSR[(int) (rm)])).Store();
							// Runtime let!
							VSR[(int) (rd)] = (RuntimeValue<float>) (Ternary<byte, float>((RuntimeValue<byte>) ((RuntimeValue<byte>) ((a) < (b))), a, b));
							break;
						}
						case 0x1: {
							var a = ((RuntimeValue<double>) (VDR[(int) (rn)])).Store();
							// Runtime let!
							var b = ((RuntimeValue<double>) (VDR[(int) (rm)])).Store();
							// Runtime let!
							VDR[(int) (rd)] = (RuntimeValue<double>) (Ternary<byte, double>((RuntimeValue<byte>) ((RuntimeValue<byte>) ((a) < (b))), a, b));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FMOV-general */
				if((inst & 0x7F36FC00U) == 0x1E260000U) {
					var sf = (inst >> 31) & 0x1U;
					var type = (inst >> 22) & 0x3U;
					var mode = (inst >> 19) & 0x1U;
					var ropc = (inst >> 16) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var opc = (byte) ((byte) (((byte) (((byte) (ropc)) << 0)) | ((byte) (((byte) ((byte) ((byte) (0x3)))) << 1))));
					var tf = (byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) (opc)) << 0)) | ((byte) (((byte) ((byte) ((byte) (mode)))) << 3)))) | ((byte) (((byte) (type)) << 5)))) | ((byte) (((byte) (sf)) << 7))));
					var r1 = "";
					var r2 = "";
					switch(tf) {
						case 0x66: {
							r1 = "W";
							r2 = "H";
							break;
						}
						case 0xE6: {
							r1 = "X";
							r2 = "H";
							break;
						}
						case 0x67: {
							r1 = "H";
							r2 = "W";
							break;
						}
						case 0x7: {
							r1 = "S";
							r2 = "W";
							break;
						}
						case 0x6: {
							r1 = "W";
							r2 = "S";
							break;
						}
						case 0xE7: {
							r1 = "H";
							r2 = "X";
							break;
						}
						case 0xA7: {
							r1 = "D";
							r2 = "X";
							break;
						}
						case 0xCF: {
							r1 = "V";
							r2 = "X";
							break;
						}
						case 0xCE: {
							r1 = "X";
							r2 = "V";
							break;
						}
						case 0xA6: {
							r1 = "X";
							r2 = "D";
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					var index1 = (string) (((byte) (((r1) == ("V")) ? 1U : 0U) != 0) ? (".D[1]") : (""));
					var index2 = (string) (((byte) (((r2) == ("V")) ? 1U : 0U) != 0) ? (".D[1]") : (""));
					switch(tf) {
						case 0x66: {
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<float>) (VHR[(int) (rn)]))));
							break;
						}
						case 0xE6: {
							XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<float>) (VHR[(int) (rn)])));
							break;
						}
						case 0x67: {
							VHR[(int) (rd)] = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])));
							break;
						}
						case 0x7: {
							VSR[(int) (rd)] = (RuntimeValue<float>) (((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).Bitcast<float>());
							break;
						}
						case 0x6: {
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<float>) (VSR[(int) (rn)])).Bitcast<uint>()));
							break;
						}
						case 0xE7: {
							VHR[(int) (rd)] = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])));
							break;
						}
						case 0xA7: {
							VDR[(int) (rd)] = (RuntimeValue<double>) (((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Bitcast<double>());
							break;
						}
						case 0xA6: {
							XR[(int) rd] = (RuntimeValue<ulong>) (((RuntimeValue<double>) (VDR[(int) (rn)])).Bitcast<ulong>());
							break;
						}
						case 0xCE: {
							XR[(int) rd] = (RuntimeValue<ulong>) (((RuntimeValue<double>) (VDR[(int) ((byte) ((((ulong) ((byte) ((rn) << (int) (0x1)))) | ((ulong) (0x1)))))])).Bitcast<ulong>());
							break;
						}
						case 0xCF: {
							VDR[(int) ((byte) ((((ulong) ((byte) ((rd) << (int) (0x1)))) | ((ulong) (0x1)))))] = (RuntimeValue<double>) (((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Bitcast<double>());
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FMOV-scalar-immediate */
				if((inst & 0xFF201FE0U) == 0x1E201000U) {
					var type = (inst >> 22) & 0x3U;
					var imm = (inst >> 13) & 0xFFU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					var sv = (float) (Bitcast<uint, float>((uint) ((uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (((uint) ((uint) ((uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (uint) (((uint) (((uint) ((byte) ((byte) (0x0)))) << 0)) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 1)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 2)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 3)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 4)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 5)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 6)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 7)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 8)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 9)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 10)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 11)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 12)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 13)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 14)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 15)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 16)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 17)))) | ((uint) (((uint) ((byte) ((byte) (0x0)))) << 18)))))) << 0)) | ((uint) (((uint) ((byte) ((byte) ((byte) ((((ulong) (imm)) & ((ulong) (0xF)))))))) << 19)))) | ((uint) (((uint) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x4)))) & ((ulong) (0x3)))))))) << 23)))) | ((uint) (((uint) ((byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 0)) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 1)))) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 2)))) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 3)))) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 4)))))) << 25)))) | ((uint) (((uint) ((byte) (((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1))))) != 0 ? 0U : 1U))) << 30)))) | ((uint) (((uint) ((byte) ((byte) ((byte) ((imm) >> (int) (0x7)))))) << 31))))));
					switch(type) {
						case 0x0: {
							VSR[(int) (rd)] = sv;
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (double) (Bitcast<ulong, double>((ulong) ((ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (((ulong) ((ulong) ((ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 0)) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 1)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 2)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 3)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 4)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 5)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 6)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 7)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 8)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 9)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 10)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 11)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 12)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 13)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 14)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 15)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 16)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 17)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 18)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 19)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 20)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 21)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 22)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 23)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 24)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 25)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 26)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 27)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 28)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 29)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 30)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 31)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 32)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 33)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 34)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 35)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 36)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 37)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 38)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 39)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 40)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 41)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 42)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 43)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 44)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 45)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 46)))) | ((ulong) (((ulong) ((byte) ((byte) (0x0)))) << 47)))))) << 0)) | ((ulong) (((ulong) ((byte) ((byte) ((byte) ((((ulong) (imm)) & ((ulong) (0xF)))))))) << 48)))) | ((ulong) (((ulong) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x4)))) & ((ulong) (0x3)))))))) << 52)))) | ((ulong) (((ulong) ((byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 0)) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 1)))) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 2)))) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 3)))) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 4)))) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 5)))) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 6)))) | ((byte) (((byte) ((byte) ((byte) ((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1)))))))) << 7)))))) << 54)))) | ((ulong) (((ulong) ((byte) (((byte) ((((ulong) ((byte) ((imm) >> (int) (0x6)))) & ((ulong) (0x1))))) != 0 ? 0U : 1U))) << 62)))) | ((ulong) (((ulong) ((byte) ((byte) ((byte) ((imm) >> (int) (0x7)))))) << 63))))));
							break;
						}
					}
					return true;
				}
				/* FMUL-scalar */
				if((inst & 0xFF20FC00U) == 0x1E200800U) {
					var type = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch(type) {
						case 0x3: {
							throw new NotImplementedException();
							break;
						}
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) (VSR[(int) (rn)])) * (RuntimeValue<float>) ((RuntimeValue<float>) (VSR[(int) (rm)])));
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rn)])) * (RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rm)])));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FMUL-vector */
				if((inst & 0xBFA0FC00U) == 0x2E20DC00U) {
					var Q = (inst >> 30) & 0x1U;
					var size = (inst >> 22) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var ts = (string) (((byte) ((byte) (((byte) (((byte) (Q)) << 0)) | ((byte) (((byte) (size)) << 1))))) switch { 0x0 => "2S", 0x1 => "4S", 0x3 => "2D", _ => throw new NotImplementedException() });
					switch((byte) ((byte) (((byte) (((byte) (Q)) << 0)) | ((byte) (((byte) (size)) << 1))))) {
						case 0x0: {
							VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) ((RuntimeValue<Vector128<float>>) (((RuntimeValue<Vector128<float>>) (VR[(int) (rn)])) * ((RuntimeValue<Vector128<float>>) (VR[(int) (rm)]))));
							break;
						}
						case 0x1: {
							VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (((RuntimeValue<Vector128<float>>) (VR[(int) (rn)])) * ((RuntimeValue<Vector128<float>>) (VR[(int) (rm)])));
							break;
						}
						case 0x3: {
							VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) ((RuntimeValue<Vector128<float>>) ((RuntimeValue<Vector128<double>>) ((RuntimeValue<Vector128<float>>) (VR[(int) (rn)])) * (RuntimeValue<Vector128<double>>) ((RuntimeValue<Vector128<float>>) (VR[(int) (rm)]))));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FNEG */
				if((inst & 0xFF3FFC00U) == 0x1E214000U) {
					var type = (inst >> 22) & 0x3U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch(type) {
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) (-((RuntimeValue<float>) (VSR[(int) (rn)])));
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) (-((RuntimeValue<double>) (VDR[(int) (rn)])));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FNMUL-scalar */
				if((inst & 0xFF20FC00U) == 0x1E208800U) {
					var type = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch(type) {
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) (-((RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) (VSR[(int) (rn)])) * (RuntimeValue<float>) ((RuntimeValue<float>) (VSR[(int) (rm)])))));
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) (-((RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rn)])) * (RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rm)])))));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FSQRT-scalar */
				if((inst & 0xFF3FFC00U) == 0x1E21C000U) {
					var type = (inst >> 22) & 0x3U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch(type) {
						case 0x3: {
							throw new NotImplementedException();
							break;
						}
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<double>) ((RuntimeValue<float>) (VSR[(int) (rn)]))).Sqrt());
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rn)]))).Sqrt());
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* FSUB-scalar */
				if((inst & 0xFF20FC00U) == 0x1E203800U) {
					var type = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((type) switch { 0x3 => "H", 0x0 => "S", 0x1 => "D", _ => throw new NotImplementedException() });
					switch(type) {
						case 0x3: {
							throw new NotImplementedException();
							break;
						}
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) (VSR[(int) (rn)])) - (RuntimeValue<float>) ((RuntimeValue<float>) (VSR[(int) (rm)])));
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rn)])) - (RuntimeValue<double>) ((RuntimeValue<double>) (VDR[(int) (rm)])));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* INS-general */
				if((inst & 0xFFE0FC00U) == 0x4E001C00U) {
					var imm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var ts = "";
					var index = (uint) ((uint) (0x0));
					var r = "W";
					if(((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x1))))) == (0x1)) ? 1U : 0U)) != 0) {
						ts = "B";
						index = (byte) ((imm) >> (int) (0x1));
					} else {
						if(((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x2))))) == (0x2)) ? 1U : 0U)) != 0) {
							ts = "H";
							index = (byte) ((imm) >> (int) (0x2));
						} else {
							if(((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x4))))) == (0x4)) ? 1U : 0U)) != 0) {
								ts = "S";
								index = (byte) ((imm) >> (int) (0x3));
							} else {
								ts = "D";
								index = (byte) ((imm) >> (int) (0x4));
								r = "X";
							}
						}
					}
					if(((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x1))))) == (0x1)) ? 1U : 0U)) != 0) {
						VR[(int) (rd)] = VR[(int) (rd)].Insert(index, (RuntimeValue<byte>) ((RuntimeValue<byte>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))));
					} else {
						if(((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x2))))) == (0x2)) ? 1U : 0U)) != 0) {
							VR[(int) (rd)] = VR[(int) (rd)].Insert(index, (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))));
						} else {
							if(((byte) ((((byte) ((((ulong) (imm)) & ((ulong) (0x4))))) == (0x4)) ? 1U : 0U)) != 0) {
								VR[(int) (rd)] = VR[(int) (rd)].Insert(index, (RuntimeValue<float>) (((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).Bitcast<float>()));
							} else {
								VR[(int) (rd)] = VR[(int) (rd)].Insert(index, (RuntimeValue<double>) (((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Bitcast<double>()));
							}
						}
					}
					return true;
				}
				/* INS-vector */
				if((inst & 0xFFE08400U) == 0x6E000400U) {
					var imm5 = (inst >> 16) & 0x1FU;
					var imm4 = (inst >> 11) & 0xFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var ts = "";
					var index1 = (uint) ((uint) (0x0));
					var index2 = (uint) ((uint) (0x0));
					if(((byte) ((((byte) ((((ulong) (imm5)) & ((ulong) (0x1))))) == (0x1)) ? 1U : 0U)) != 0) {
						ts = "B";
						index1 = (byte) ((imm5) >> (int) (0x1));
						index2 = imm4;
					} else {
						if(((byte) ((((byte) ((((ulong) (imm5)) & ((ulong) (0x2))))) == (0x2)) ? 1U : 0U)) != 0) {
							ts = "H";
							index1 = (byte) ((imm5) >> (int) (0x2));
							index2 = (byte) ((imm4) >> (int) (0x1));
						} else {
							if(((byte) ((((byte) ((((ulong) (imm5)) & ((ulong) (0x4))))) == (0x4)) ? 1U : 0U)) != 0) {
								ts = "S";
								index1 = (byte) ((imm5) >> (int) (0x3));
								index2 = (byte) ((imm4) >> (int) (0x2));
							} else {
								ts = "D";
								index1 = (byte) ((imm5) >> (int) (0x4));
								index2 = (byte) ((imm4) >> (int) (0x3));
							}
						}
					}
					if(((byte) ((((byte) ((((ulong) (imm5)) & ((ulong) (0x1))))) == (0x1)) ? 1U : 0U)) != 0) {
						VR[(int) (rd)] = VR[(int) (rd)].Insert(index1, (RuntimeValue<byte>) (VR[(int) (rn)].Element<byte>(index2)));
					} else {
						if(((byte) ((((byte) ((((ulong) (imm5)) & ((ulong) (0x2))))) == (0x2)) ? 1U : 0U)) != 0) {
							VR[(int) (rd)] = VR[(int) (rd)].Insert(index1, (RuntimeValue<ushort>) (VR[(int) (rn)].Element<ushort>(index2)));
						} else {
							if(((byte) ((((byte) ((((ulong) (imm5)) & ((ulong) (0x4))))) == (0x4)) ? 1U : 0U)) != 0) {
								VR[(int) (rd)] = VR[(int) (rd)].Insert(index1, (RuntimeValue<float>) (VR[(int) (rn)].Element<float>(index2)));
							} else {
								VR[(int) (rd)] = VR[(int) (rd)].Insert(index1, (RuntimeValue<double>) (VR[(int) (rn)].Element<double>(index2)));
							}
						}
					}
					return true;
				}
				/* LDAR */
				if((inst & 0xBFFFFC00U) == 0x88DFFC00U) {
					var size = (inst >> 30) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
						// Runtime let!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimePointer<uint>) (address)).Value));
					} else {
						// Runtime else!
						var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
						// Runtime let!
						XR[(int) rt] = (RuntimeValue<ulong>) (((RuntimePointer<ulong>) (address)).Value);
					}
					return true;
				}
				/* LDARB */
				if((inst & 0xFFFFFC00U) == 0x08DFFC00U) {
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<byte>) (((RuntimePointer<byte>) (address)).Value)));
					return true;
				}
				/* LDARH */
				if((inst & 0xFFFFFC00U) == 0x48DFFC00U) {
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ushort>) (((RuntimePointer<ushort>) (address)).Value)));
					return true;
				}
				/* LDAXB */
				if((inst & 0xBFFFFC00U) == 0x885FFC00U) {
					var size = (inst >> 30) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (Exclusive32R = ((RuntimePointer<uint>) (address)).Value));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) (Exclusive64R = ((RuntimePointer<ulong>) (address)).Value);
					}
					return true;
				}
				/* LDAXRB */
				if((inst & 0xFFFFFC00U) == 0x085FFC00U) {
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<byte>) (Exclusive8R = ((RuntimePointer<byte>) (address)).Value)));
					return true;
				}
				/* LDAXRH */
				if((inst & 0xFFFFFC00U) == 0x485FFC00U) {
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ushort>) (Exclusive16R = ((RuntimePointer<ushort>) (address)).Value)));
					return true;
				}
				/* LDP-immediate-postindex */
				if((inst & 0x7FC00000U) == 0x28C00000U) {
					var size = (inst >> 31) & 0x1U;
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) ((long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x2) : (0x3))));
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						// Runtime block!
						XR[(int) rt1] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimePointer<uint>) (address)).Value));
						// Runtime block!
						XR[(int) rt2] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value));
					} else {
						// Runtime else!
						// Runtime block!
						XR[(int) rt1] = (RuntimeValue<ulong>) (((RuntimePointer<ulong>) (address)).Value);
						// Runtime block!
						XR[(int) rt2] = (RuntimeValue<ulong>) (((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value);
					}
					// Runtime let!
					if(rn == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					else
						XR[(int) rn] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					return true;
				}
				/* LDP-immediate-signed-offset */
				if((inst & 0x7FC00000U) == 0x29400000U) {
					var size = (inst >> 31) & 0x1U;
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) ((long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x2) : (0x3))));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						// Runtime block!
						XR[(int) rt1] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimePointer<uint>) (address)).Value));
						// Runtime block!
						XR[(int) rt2] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value));
					} else {
						// Runtime else!
						// Runtime block!
						XR[(int) rt1] = (RuntimeValue<ulong>) (((RuntimePointer<ulong>) (address)).Value);
						// Runtime block!
						XR[(int) rt2] = (RuntimeValue<ulong>) (((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value);
					}
					return true;
				}
				/* LDP-simd-postindex */
				if((inst & 0x3FC00000U) == 0x2CC00000U) {
					var opc = (inst >> 30) & 0x3U;
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var r = (string) ((opc) switch { 0x0 => "S", 0x1 => "D", _ => "Q" });
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) ((long) ((opc) switch { 0x0 => 0x2, 0x1 => 0x3, _ => 0x4 })));
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					switch(opc) {
						case 0x0: {
							// Runtime block!
							VSR[(int) (rt1)] = (RuntimeValue<float>) (((RuntimePointer<float>) (address)).Value);
							// Runtime block!
							VSR[(int) (rt2)] = (RuntimeValue<float>) (((RuntimePointer<float>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value);
							break;
						}
						case 0x1: {
							// Runtime block!
							VDR[(int) (rt1)] = (RuntimeValue<double>) (((RuntimePointer<double>) (address)).Value);
							// Runtime block!
							VDR[(int) (rt2)] = (RuntimeValue<double>) (((RuntimePointer<double>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value);
							break;
						}
						default: {
							// Runtime block!
							VR[(int) (rt1)] = (RuntimeValue<Vector128<float>>) (((RuntimePointer<Vector128<float>>) (address)).Value);
							// Runtime block!
							VR[(int) (rt2)] = (RuntimeValue<Vector128<float>>) (((RuntimePointer<Vector128<float>>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x10)))).Value);
							break;
						}
					}
					// Runtime let!
					if(rn == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					else
						XR[(int) rn] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					return true;
				}
				/* LDP-simd-signed-offset */
				if((inst & 0x3FC00000U) == 0x2D400000U) {
					var opc = (inst >> 30) & 0x3U;
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var r = (string) ((opc) switch { 0x0 => "S", 0x1 => "D", _ => "Q" });
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) ((long) ((opc) switch { 0x0 => 0x2, 0x1 => 0x3, _ => 0x4 })));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					switch(opc) {
						case 0x0: {
							// Runtime block!
							VSR[(int) (rt1)] = (RuntimeValue<float>) (((RuntimePointer<float>) (address)).Value);
							// Runtime block!
							VSR[(int) (rt2)] = (RuntimeValue<float>) (((RuntimePointer<float>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value);
							break;
						}
						case 0x1: {
							// Runtime block!
							VDR[(int) (rt1)] = (RuntimeValue<double>) (((RuntimePointer<double>) (address)).Value);
							// Runtime block!
							VDR[(int) (rt2)] = (RuntimeValue<double>) (((RuntimePointer<double>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value);
							break;
						}
						default: {
							// Runtime block!
							VR[(int) (rt1)] = (RuntimeValue<Vector128<float>>) (((RuntimePointer<Vector128<float>>) (address)).Value);
							// Runtime block!
							VR[(int) (rt2)] = (RuntimeValue<Vector128<float>>) (((RuntimePointer<Vector128<float>>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x10)))).Value);
							break;
						}
					}
					return true;
				}
				/* LDPSW-immediate-signed-offset */
				if((inst & 0xFFC00000U) == 0x69400000U) {
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) (0x2));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					XR[(int) rt1] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) (((RuntimePointer<uint>) (address)).Value), 32))));
					// Runtime let!
					XR[(int) rt2] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) (((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value), 32))));
					return true;
				}
				/* LDR-immediate-preindex */
				if((inst & 0xBFE00C00U) == 0xB8400C00U) {
					var size = (inst >> 30) & 0x1U;
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (long) (SignExt<long>(rawimm, 9));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm))).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimePointer<uint>) (address)).Value));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (((RuntimePointer<ulong>) (address)).Value);
					}
					// Runtime let!
					if(rn == 31)
						SPR = address;
					else
						XR[(int) rn] = address;
					return true;
				}
				/* LDR-immediate-postindex */
				if((inst & 0xBFE00C00U) == 0xB8400400U) {
					var size = (inst >> 30) & 0x1U;
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (long) (SignExt<long>(rawimm, 9));
					// Runtime block!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]))).Value));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]))).Value);
					}
					// Runtime block!
					if(rn == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					else
						XR[(int) rn] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					return true;
				}
				/* LDR-immediate-unsigned-offset */
				if((inst & 0xBFC00000U) == 0xB9400000U) {
					var size = (inst >> 30) & 0x1U;
					var rawimm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (ushort) ((rawimm) << (int) ((long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x2) : (0x3))));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value);
					}
					return true;
				}
				/* LDR-simd-immediate-postindex */
				if((inst & 0x3F600C00U) == 0x3C400400U) {
					var size = (inst >> 30) & 0x3U;
					var opc = (inst >> 23) & 0x1U;
					var imm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var simm = (long) (SignExt<long>(imm, 9));
					var r = (string) (((byte) ((byte) (((byte) (((byte) (opc)) << 0)) | ((byte) (((byte) (size)) << 1))))) switch { 0x0 => "B", 0x2 => "H", 0x4 => "S", 0x6 => "D", 0x1 => "Q", _ => throw new NotImplementedException() });
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					switch((byte) ((byte) (((byte) (((byte) (opc)) << 0)) | ((byte) (((byte) (size)) << 1))))) {
						case 0x0: {
							VBR[(int) (rt)] = (RuntimeValue<byte>) (((RuntimePointer<byte>) (address)).Value);
							break;
						}
						case 0x2: {
							VHR[(int) (rt)] = (RuntimeValue<ushort>) (((RuntimePointer<ushort>) (address)).Value);
							break;
						}
						case 0x4: {
							VSR[(int) (rt)] = (RuntimeValue<float>) (((RuntimePointer<float>) (address)).Value);
							break;
						}
						case 0x6: {
							VDR[(int) (rt)] = (RuntimeValue<double>) (((RuntimePointer<double>) (address)).Value);
							break;
						}
						case 0x1: {
							VR[(int) (rt)] = (RuntimeValue<Vector128<float>>) (((RuntimePointer<Vector128<float>>) (address)).Value);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					// Runtime let!
					if(rn == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					else
						XR[(int) rn] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					return true;
				}
				/* LDR-simd-immediate-unsigned-offset */
				if((inst & 0x3F400000U) == 0x3D400000U) {
					var size = (inst >> 30) & 0x3U;
					var ropc = (inst >> 23) & 0x1U;
					var rawimm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var opc = (byte) ((byte) (((byte) (((byte) ((byte) ((byte) (0x1)))) << 0)) | ((byte) (((byte) (ropc)) << 1))));
					var m = (byte) ((byte) (((byte) (((byte) (opc)) << 0)) | ((byte) (((byte) (size)) << 2))));
					var r = (string) ((m) switch { 0x1 => "B", 0x5 => "H", 0x9 => "S", 0xD => "D", _ => "Q" });
					var imm = (uint) (((uint) ((uint) (rawimm))) << (int) ((long) ((m) switch { 0x1 => 0x0, 0x5 => 0x1, 0x9 => 0x2, 0xD => 0x3, _ => 0x4 })));
					switch(m) {
						case 0x1: {
							VBR[(int) (rt)] = (RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<uint>) (imm)))).Value);
							break;
						}
						case 0x5: {
							VHR[(int) (rt)] = (RuntimeValue<ushort>) (((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<uint>) (imm)))).Value);
							break;
						}
						case 0x9: {
							VSR[(int) (rt)] = (RuntimeValue<float>) (((RuntimePointer<float>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<uint>) (imm)))).Value);
							break;
						}
						case 0xD: {
							VDR[(int) (rt)] = (RuntimeValue<double>) (((RuntimePointer<double>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<uint>) (imm)))).Value);
							break;
						}
						default: {
							VR[(int) (rt)] = (RuntimeValue<Vector128<float>>) (((RuntimePointer<Vector128<float>>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<uint>) (imm)))).Value);
							break;
						}
					}
					return true;
				}
				/* LDR-simd-register */
				if((inst & 0x3F600C00U) == 0x3C600800U) {
					var size = (inst >> 30) & 0x3U;
					var opc = (inst >> 23) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var scale = (inst >> 12) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r1 = (string) (((byte) ((((byte) ((byte) (((size) == (0x0)) ? 1U : 0U))) & ((byte) ((byte) (((opc) == (0x1)) ? 1U : 0U))))) != 0) ? ("Q") : ((string) ((size) switch { 0x0 => "B", 0x1 => "H", 0x2 => "S", 0x3 => "D", _ => throw new NotImplementedException() })));
					var r2 = (string) (((byte) ((((ulong) (option)) & ((ulong) (0x1)))) != 0) ? ("X") : ("W"));
					var extend = (string) ((option) switch { 0x2 => "UXTW", 0x3 => "LSL", 0x6 => "SXTW", 0x7 => "SXTX", _ => throw new NotImplementedException() });
					var amount = (ulong) ((ulong) (scale) * (ulong) ((long) (((byte) ((((byte) ((byte) (((size) == (0x0)) ? 1U : 0U))) & ((byte) ((byte) (((opc) == (0x1)) ? 1U : 0U))))) != 0) ? (0x4) : ((long) ((size) switch { 0x0 => 0x1, 0x1 => 0x1, 0x2 => 0x2, 0x3 => 0x3, _ => throw new NotImplementedException() })))));
					var offset = ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((option) switch { 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))), 0x3 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])), 0x6 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))), 0x7 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])), _ => throw new NotImplementedException() })).ShiftLeft(amount))).Store();
					// Runtime let!
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset))).Store();
					// Runtime let!
					switch((byte) ((byte) (((byte) (((byte) (opc)) << 0)) | ((byte) (((byte) (size)) << 1))))) {
						case 0x0: {
							VBR[(int) (rt)] = (RuntimeValue<byte>) (((RuntimePointer<byte>) (address)).Value);
							break;
						}
						case 0x4: {
							VSR[(int) (rt)] = (RuntimeValue<float>) (((RuntimePointer<float>) (address)).Value);
							break;
						}
						case 0x6: {
							VDR[(int) (rt)] = (RuntimeValue<double>) (((RuntimePointer<double>) (address)).Value);
							break;
						}
						case 0x1: {
							VR[(int) (rt)] = (RuntimeValue<Vector128<float>>) (((RuntimePointer<Vector128<float>>) (address)).Value);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* LDR-register */
				if((inst & 0xBFE00C00U) == 0xB8600800U) {
					var size = (inst >> 30) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var scale = (inst >> 12) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r1 = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var r2 = (string) (((byte) ((((ulong) (option)) & ((ulong) (0x1)))) != 0) ? ("X") : ("W"));
					var amount = (long) (((byte) (((scale) == (0x0)) ? 1U : 0U) != 0) ? (0x0) : ((long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x2) : (0x3))));
					var extend = (string) ((option) switch { 0x2 => "UXTW", 0x6 => "SXTW", 0x7 => "SXTX", _ => "LSL" });
					var offset = ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) (((byte) (((option) == (0x6)) ? 1U : 0U)) != 0 ? ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))) : ((RuntimeValue<ulong>) (((byte) ((((ulong) (option)) & ((ulong) (0x1))))) != 0 ? ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])) : ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))))))).ShiftLeft(amount))).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) (((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value);
					}
					return true;
				}
				/* LDRB-immediate-postindex */
				if((inst & 0xFFE00C00U) == 0x38400400U) {
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					// Runtime block!
					XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]))).Value))));
					// Runtime block!
					if(rn == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					else
						XR[(int) rn] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					return true;
				}
				/* LDRB-immediate-preindex */
				if((inst & 0xFFE00C00U) == 0x38400C00U) {
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm))).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<byte>) (((RuntimePointer<byte>) (address)).Value))));
					// Runtime let!
					if(rn == 31)
						SPR = address;
					else
						XR[(int) rn] = address;
					return true;
				}
				/* LDRB-immediate-unsigned-offset */
				if((inst & 0xFFC00000U) == 0x39400000U) {
					var imm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value)));
					return true;
				}
				/* LDRB-register */
				if((inst & 0xFFE00C00U) == 0x38600800U) {
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var amount = (inst >> 12) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) ((((ulong) (option)) & ((ulong) (0x1)))) != 0) ? ("X") : ("W"));
					var str = (string) ((option) switch { 0x2 => "UXTW", 0x3 => "LSL", 0x6 => "SXTW", 0x7 => "SXTX", _ => throw new NotImplementedException() });
					var offset = ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) (((byte) (((option) == (0x6)) ? 1U : 0U)) != 0 ? ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))) : ((RuntimeValue<ulong>) (((byte) ((((ulong) (option)) & ((ulong) (0x1))))) != 0 ? ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])) : ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))))))).ShiftLeft(amount))).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value));
					return true;
				}
				/* LDRH-immediate-postindex */
				if((inst & 0xFFE00C00U) == 0x78400400U) {
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ushort>) (((RuntimePointer<ushort>) (address)).Value)));
					// Runtime let!
					if(rn == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					else
						XR[(int) rn] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					return true;
				}
				/* LDRH-immediate-preindex */
				if((inst & 0xFFE00C00U) == 0x78400C00U) {
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm))).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ushort>) (((RuntimePointer<ushort>) (address)).Value)));
					// Runtime let!
					if(rn == 31)
						SPR = address;
					else
						XR[(int) rn] = address;
					return true;
				}
				/* LDRH-immediate-unsigned-offset */
				if((inst & 0xFFC00000U) == 0x79400000U) {
					var rawimm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (ushort) ((rawimm) << (int) (0x1));
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ushort>) (((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value)));
					return true;
				}
				/* LDRH-register */
				if((inst & 0xFFE00C00U) == 0x78600800U) {
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var amount = (inst >> 12) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) ((((ulong) (option)) & ((ulong) (0x1)))) != 0) ? ("X") : ("W"));
					var str = (string) ((option) switch { 0x2 => "UXTW", 0x3 => "LSL", 0x6 => "SXTW", 0x7 => "SXTX", _ => throw new NotImplementedException() });
					var offset = ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) (((byte) (((option) == (0x6)) ? 1U : 0U)) != 0 ? ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))) : ((RuntimeValue<ulong>) (((byte) ((((ulong) (option)) & ((ulong) (0x1))))) != 0 ? ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])) : ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))))))).ShiftLeft(amount))).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<ushort>) (((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value));
					return true;
				}
				/* LDRSB-immediate-postindex */
				if((inst & 0xFFA00C00U) == 0x38800400U) {
					var opc = (inst >> 22) & 0x1U;
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					var r = (string) (((byte) (((opc) == (0x1)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					if(((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<byte>) (((RuntimePointer<byte>) (address)).Value), 8)))));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<byte>) (((RuntimePointer<byte>) (address)).Value), 8))));
					}
					// Runtime let!
					if(rn == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					else
						XR[(int) rn] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					return true;
				}
				/* LDRSB-immediate-preindex */
				if((inst & 0xFFA00C00U) == 0x38800C00U) {
					var opc = (inst >> 22) & 0x1U;
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					var r = (string) (((byte) (((opc) == (0x1)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm))).Store();
					// Runtime let!
					if(((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<byte>) (((RuntimePointer<byte>) (address)).Value), 8)))));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<byte>) (((RuntimePointer<byte>) (address)).Value), 8))));
					}
					// Runtime let!
					if(rn == 31)
						SPR = address;
					else
						XR[(int) rn] = address;
					return true;
				}
				/* LDRSB-immediate-unsigned-offset */
				if((inst & 0xFF800000U) == 0x39800000U) {
					var opc = (inst >> 22) & 0x1U;
					var imm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((opc) == (0x1)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value), 8)))));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value), 8))));
					}
					return true;
				}
				/* LDRSB-register */
				if((inst & 0xFFA00C00U) == 0x38A00800U) {
					var opc = (inst >> 22) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var amount = (inst >> 12) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((opc) == (0x0)) ? 1U : 0U) != 0) ? ("X") : ("W"));
					var str = (string) ((option) switch { 0x2 => "UXTW", 0x3 => "LSL", 0x6 => "SXTW", 0x7 => "SXTX", _ => throw new NotImplementedException() });
					var offset = ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) (((byte) (((option) == (0x6)) ? 1U : 0U)) != 0 ? ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))) : ((RuntimeValue<ulong>) (((byte) ((((ulong) (option)) & ((ulong) (0x1))))) != 0 ? ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])) : ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))))))).ShiftLeft(amount))).Store();
					// Runtime let!
					if(((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value), 8)))));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value), 8))));
					}
					return true;
				}
				/* LDRSH-immediate-postindex */
				if((inst & 0xFFA00C00U) == 0x78800400U) {
					var opc = (inst >> 22) & 0x1U;
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					var r = (string) (((byte) (((opc) == (0x1)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					if(((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<ushort>) (((RuntimePointer<ushort>) (address)).Value), 16)))));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<ushort>) (((RuntimePointer<ushort>) (address)).Value), 16))));
					}
					// Runtime let!
					if(rn == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					else
						XR[(int) rn] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					return true;
				}
				/* LDRSH-immediate-preindex */
				if((inst & 0xFFA00C00U) == 0x78800C00U) {
					var opc = (inst >> 22) & 0x1U;
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					var r = (string) (((byte) (((opc) == (0x1)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm))).Store();
					// Runtime let!
					if(((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<ushort>) (((RuntimePointer<ushort>) (address)).Value), 16)))));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<ushort>) (((RuntimePointer<ushort>) (address)).Value), 16))));
					}
					// Runtime let!
					if(rn == 31)
						SPR = address;
					else
						XR[(int) rn] = address;
					return true;
				}
				/* LDRSH-immediate-unsigned-offset */
				if((inst & 0xFF800000U) == 0x79800000U) {
					var opc = (inst >> 22) & 0x1U;
					var rawimm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((opc) == (0x1)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (ushort) ((rawimm) << (int) (0x1));
					if(((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<ushort>) (((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value), 16)))));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<ushort>) (((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value), 16))));
					}
					return true;
				}
				/* LDRSH-register */
				if((inst & 0xFFA00C00U) == 0x78A00800U) {
					var opc = (inst >> 22) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var amount = (inst >> 12) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((opc) == (0x0)) ? 1U : 0U) != 0) ? ("X") : ("W"));
					var str = (string) ((option) switch { 0x2 => "UXTW", 0x3 => "LSL", 0x6 => "SXTW", 0x7 => "SXTX", _ => throw new NotImplementedException() });
					var offset = ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) (((byte) (((option) == (0x6)) ? 1U : 0U)) != 0 ? ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))) : ((RuntimeValue<ulong>) (((byte) ((((ulong) (option)) & ((ulong) (0x1))))) != 0 ? ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])) : ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))))))).ShiftLeft(amount))).Store();
					// Runtime let!
					if(((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<ushort>) (((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value), 16)))));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<ushort>) (((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value), 16))));
					}
					return true;
				}
				/* LDRSW-immediate-postindex */
				if((inst & 0xFFE00C00U) == 0xB8800400U) {
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) (((RuntimePointer<uint>) (address)).Value), 32))));
					// Runtime let!
					if(rn == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					else
						XR[(int) rn] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm));
					return true;
				}
				/* LDRSW-immediate-preindex */
				if((inst & 0xFFE00C00U) == 0xB8800C00U) {
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm))).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) (((RuntimePointer<uint>) (address)).Value), 32))));
					// Runtime let!
					if(rn == 31)
						SPR = address;
					else
						XR[(int) rn] = address;
					return true;
				}
				/* LDRSW-immediate-unsigned-offset */
				if((inst & 0xFFC00000U) == 0xB9800000U) {
					var rawimm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (ushort) ((rawimm) << (int) (0x2));
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) (((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value), 32))));
					return true;
				}
				/* LDRSW-register */
				if((inst & 0xFFE00C00U) == 0xB8A00800U) {
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var scale = (inst >> 12) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) ((((ulong) (option)) & ((ulong) (0x1)))) != 0) ? ("X") : ("W"));
					var amount = (long) (((byte) (((scale) == (0x0)) ? 1U : 0U) != 0) ? (0x0) : (0x2));
					var extend = (string) ((option) switch { 0x2 => "UXTW", 0x6 => "SXTW", 0x7 => "SXTX", _ => "LSL" });
					var offset = ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) (((byte) (((option) == (0x6)) ? 1U : 0U)) != 0 ? ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))) : ((RuntimeValue<ulong>) (((byte) ((((ulong) (option)) & ((ulong) (0x1))))) != 0 ? ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])) : ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))))))).ShiftLeft(amount))).Store();
					// Runtime let!
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) (((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value), 32))));
					return true;
				}
				/* LDUR */
				if((inst & 0xBFE00C00U) == 0xB8400000U) {
					var size = (inst >> 30) & 0x1U;
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (long) (SignExt<long>(rawimm, 9));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm)))).Value));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm)))).Value);
					}
					return true;
				}
				/* LDURB */
				if((inst & 0xFFE00C00U) == 0x38400000U) {
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm)))).Value)));
					return true;
				}
				/* LDURH */
				if((inst & 0xFFE00C00U) == 0x78400000U) {
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ushort>) (((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm)))).Value)));
					return true;
				}
				/* LDURSB */
				if((inst & 0xFFA00C00U) == 0x38800000U) {
					var opc = (inst >> 22) & 0x1U;
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((opc) == (0x1)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (long) (SignExt<long>(rawimm, 9));
					if(((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm)))).Value), 8)))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<byte>) (((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm)))).Value), 8))));
					}
					return true;
				}
				/* LDURSH */
				if((inst & 0xFFA00C00U) == 0x78800000U) {
					var opc = (inst >> 22) & 0x1U;
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((opc) == (0x1)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (long) (SignExt<long>(rawimm, 9));
					if(((byte) (((opc) == (0x1)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<ushort>) (((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm)))).Value), 16)))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<ushort>) (((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm)))).Value), 16))));
					}
					return true;
				}
				/* LDURSW */
				if((inst & 0xFFE00C00U) == 0xB8800000U) {
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (long) (SignExt<long>(rawimm, 9));
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) (((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm)))).Value), 32))));
					return true;
				}
				/* LDUR-simd */
				if((inst & 0x3F600C00U) == 0x3C400000U) {
					var size = (inst >> 30) & 0x3U;
					var opc = (inst >> 23) & 0x1U;
					var rawimm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) ((byte) (((byte) (((byte) (opc)) << 0)) | ((byte) (((byte) (size)) << 1))))) switch { 0x0 => "B", 0x2 => "H", 0x4 => "S", 0x6 => "D", 0x1 => "Q", _ => throw new NotImplementedException() });
					var imm = (long) (SignExt<long>(rawimm, 9));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (imm))).Store();
					// Runtime let!
					switch((byte) ((byte) (((byte) (((byte) (opc)) << 0)) | ((byte) (((byte) (size)) << 1))))) {
						case 0x0: {
							VBR[(int) (rt)] = (RuntimeValue<byte>) (((RuntimePointer<byte>) (address)).Value);
							break;
						}
						case 0x4: {
							VSR[(int) (rt)] = (RuntimeValue<float>) (((RuntimePointer<float>) (address)).Value);
							break;
						}
						case 0x6: {
							VDR[(int) (rt)] = (RuntimeValue<double>) (((RuntimePointer<double>) (address)).Value);
							break;
						}
						case 0x1: {
							VR[(int) (rt)] = (RuntimeValue<Vector128<float>>) (((RuntimePointer<Vector128<float>>) (address)).Value);
							break;
						}
					}
					return true;
				}
				/* LDXR */
				if((inst & 0xBFFFFC00U) == 0x885F7C00U) {
					var size = (inst >> 30) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rt] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (Exclusive32R = ((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]))).Value));
					} else {
						// Runtime else!
						XR[(int) rt] = (RuntimeValue<ulong>) (Exclusive64R = ((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]))).Value);
					}
					return true;
				}
				/* LDXRB */
				if((inst & 0xFFFFFC00U) == 0x085F7C00U) {
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<byte>) (Exclusive8R = ((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]))).Value)));
					return true;
				}
				/* LDXRH */
				if((inst & 0xFFFFFC00U) == 0x485F7C00U) {
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					XR[(int) rt] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ushort>) (Exclusive16R = ((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]))).Value)));
					return true;
				}
				/* LSL-register */
				if((inst & 0x7FE0FC00U) == 0x1AC02000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).ShiftLeft((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).ShiftLeft((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])));
					}
					return true;
				}
				/* LSRV */
				if((inst & 0x7FE0FC00U) == 0x1AC02400U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).ShiftRight((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).ShiftRight((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])));
					}
					return true;
				}
				/* MADD */
				if((inst & 0x7FE08000U) == 0x1B000000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var ra = (inst >> 10) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])) * (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))) + (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((ra) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) ra]))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])) * (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])))) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((ra) == 31 ? 0UL : XR[(int) ra])));
					}
					return true;
				}
				/* MOVI-32bit */
				if((inst & 0xBFF89C00U) == 0x0F000400U) {
					var Q = (inst >> 30) & 0x1U;
					var a = (inst >> 18) & 0x1U;
					var b = (inst >> 17) & 0x1U;
					var c = (inst >> 16) & 0x1U;
					var cmode = (inst >> 13) & 0x3U;
					var d = (inst >> 9) & 0x1U;
					var e = (inst >> 8) & 0x1U;
					var f = (inst >> 7) & 0x1U;
					var g = (inst >> 6) & 0x1U;
					var h = (inst >> 5) & 0x1U;
					var rd = (inst >> 0) & 0x1FU;
					var t = (string) ((Q != 0) ? ("4S") : ("2S"));
					var amount = (long) ((cmode) switch { 0x0 => 0x0, 0x1 => 0x8, 0x2 => 0x10, 0x3 => 0x18, _ => throw new NotImplementedException() });
					var imm = (byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) (h)) << 0)) | ((byte) (((byte) (g)) << 1)))) | ((byte) (((byte) (f)) << 2)))) | ((byte) (((byte) (e)) << 3)))) | ((byte) (((byte) (d)) << 4)))) | ((byte) (((byte) (c)) << 5)))) | ((byte) (((byte) (b)) << 6)))) | ((byte) (((byte) (a)) << 7))));
					var avec = ((RuntimeValue<Vector128<float>>) (((RuntimeValue<float>) ((float) (Bitcast<uint, float>((uint) (((uint) ((uint) (imm))) << (int) (amount)))))).CreateVector())).Store();
					// Runtime let!
					if((Q) != 0) {
						// Runtime if!
						VR[(int) (rd)] = avec;
					} else {
						// Runtime else!
						VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (avec);
					}
					return true;
				}
				/* MOVI-Vx.2D */
				if((inst & 0xFFF8FC00U) == 0x6F00E400U) {
					var a = (inst >> 18) & 0x1U;
					var b = (inst >> 17) & 0x1U;
					var c = (inst >> 16) & 0x1U;
					var d = (inst >> 9) & 0x1U;
					var e = (inst >> 8) & 0x1U;
					var f = (inst >> 7) & 0x1U;
					var g = (inst >> 6) & 0x1U;
					var h = (inst >> 5) & 0x1U;
					var rd = (inst >> 0) & 0x1FU;
					var imm = (ulong) ((ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (ulong) (((ulong) (((ulong) ((byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) (h)) << 0)) | ((byte) (((byte) (h)) << 1)))) | ((byte) (((byte) (h)) << 2)))) | ((byte) (((byte) (h)) << 3)))) | ((byte) (((byte) (h)) << 4)))) | ((byte) (((byte) (h)) << 5)))) | ((byte) (((byte) (h)) << 6)))) | ((byte) (((byte) (h)) << 7)))))) << 0)) | ((ulong) (((ulong) ((byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) (g)) << 0)) | ((byte) (((byte) (g)) << 1)))) | ((byte) (((byte) (g)) << 2)))) | ((byte) (((byte) (g)) << 3)))) | ((byte) (((byte) (g)) << 4)))) | ((byte) (((byte) (g)) << 5)))) | ((byte) (((byte) (g)) << 6)))) | ((byte) (((byte) (g)) << 7)))))) << 8)))) | ((ulong) (((ulong) ((byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) (f)) << 0)) | ((byte) (((byte) (f)) << 1)))) | ((byte) (((byte) (f)) << 2)))) | ((byte) (((byte) (f)) << 3)))) | ((byte) (((byte) (f)) << 4)))) | ((byte) (((byte) (f)) << 5)))) | ((byte) (((byte) (f)) << 6)))) | ((byte) (((byte) (f)) << 7)))))) << 16)))) | ((ulong) (((ulong) ((byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) (e)) << 0)) | ((byte) (((byte) (e)) << 1)))) | ((byte) (((byte) (e)) << 2)))) | ((byte) (((byte) (e)) << 3)))) | ((byte) (((byte) (e)) << 4)))) | ((byte) (((byte) (e)) << 5)))) | ((byte) (((byte) (e)) << 6)))) | ((byte) (((byte) (e)) << 7)))))) << 24)))) | ((ulong) (((ulong) ((byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) (d)) << 0)) | ((byte) (((byte) (d)) << 1)))) | ((byte) (((byte) (d)) << 2)))) | ((byte) (((byte) (d)) << 3)))) | ((byte) (((byte) (d)) << 4)))) | ((byte) (((byte) (d)) << 5)))) | ((byte) (((byte) (d)) << 6)))) | ((byte) (((byte) (d)) << 7)))))) << 32)))) | ((ulong) (((ulong) ((byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) (c)) << 0)) | ((byte) (((byte) (c)) << 1)))) | ((byte) (((byte) (c)) << 2)))) | ((byte) (((byte) (c)) << 3)))) | ((byte) (((byte) (c)) << 4)))) | ((byte) (((byte) (c)) << 5)))) | ((byte) (((byte) (c)) << 6)))) | ((byte) (((byte) (c)) << 7)))))) << 40)))) | ((ulong) (((ulong) ((byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) (b)) << 0)) | ((byte) (((byte) (b)) << 1)))) | ((byte) (((byte) (b)) << 2)))) | ((byte) (((byte) (b)) << 3)))) | ((byte) (((byte) (b)) << 4)))) | ((byte) (((byte) (b)) << 5)))) | ((byte) (((byte) (b)) << 6)))) | ((byte) (((byte) (b)) << 7)))))) << 48)))) | ((ulong) (((ulong) ((byte) ((byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (byte) (((byte) (((byte) (a)) << 0)) | ((byte) (((byte) (a)) << 1)))) | ((byte) (((byte) (a)) << 2)))) | ((byte) (((byte) (a)) << 3)))) | ((byte) (((byte) (a)) << 4)))) | ((byte) (((byte) (a)) << 5)))) | ((byte) (((byte) (a)) << 6)))) | ((byte) (((byte) (a)) << 7)))))) << 56))));
					VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (((RuntimeValue<ulong>) (imm)).CreateVector());
					return true;
				}
				/* MOVK */
				if((inst & 0x7F800000U) == 0x72800000U) {
					var size = (inst >> 31) & 0x1U;
					var hw = (inst >> 21) & 0x3U;
					var imm = (inst >> 5) & 0xFFFFU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shift = (byte) ((hw) << (int) (0x4));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rd) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rd]))) & ((RuntimeValue<uint>) ((uint) ((((uint) ((uint) ((uint) (-0x1)))) ^ ((uint) ((uint) (((uint) ((uint) (0xFFFF))) << (int) (shift)))))))))))) | ((RuntimeValue<uint>) ((uint) (((uint) ((uint) (imm))) << (int) (shift)))))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rd) == 31 ? 0UL : XR[(int) rd]))) & ((RuntimeValue<ulong>) ((ulong) ((((ulong) ((ulong) ((ulong) (-0x1)))) ^ ((ulong) ((ulong) (((ulong) ((ulong) (0xFFFF))) << (int) (shift)))))))))))) | ((RuntimeValue<ulong>) ((ulong) (((ulong) ((ulong) (imm))) << (int) (shift))))));
					}
					return true;
				}
				/* MOVN */
				if((inst & 0x7F800000U) == 0x12800000U) {
					var size = (inst >> 31) & 0x1U;
					var hw = (inst >> 21) & 0x3U;
					var imm = (inst >> 5) & 0xFFFFU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shift = (byte) ((hw) << (int) (0x4));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((uint) (~((uint) (((uint) ((uint) (imm))) << (int) (shift)))));
					} else {
						// Runtime else!
						XR[(int) rd] = (ulong) (~((ulong) (((ulong) ((ulong) (imm))) << (int) (shift))));
					}
					return true;
				}
				/* MOVZ */
				if((inst & 0x7F800000U) == 0x52800000U) {
					var size = (inst >> 31) & 0x1U;
					var hw = (inst >> 21) & 0x3U;
					var imm = (inst >> 5) & 0xFFFFU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shift = (byte) ((hw) << (int) (0x4));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((uint) (((uint) ((uint) (imm))) << (int) (shift)));
					} else {
						// Runtime else!
						XR[(int) rd] = (ulong) (((ulong) ((ulong) (imm))) << (int) (shift));
					}
					return true;
				}
				/* MRS */
				if((inst & 0xFFF00000U) == 0xD5300000U) {
					var op0 = (inst >> 19) & 0x1U;
					var op1 = (inst >> 16) & 0x7U;
					var cn = (inst >> 12) & 0xFU;
					var cm = (inst >> 8) & 0xFU;
					var op2 = (inst >> 5) & 0x7U;
					var rt = (inst >> 0) & 0x1FU;
					XR[(int) rt] = (RuntimeValue<ulong>) (CallSR(op0, op1, cn, cm, op2));
					return true;
				}
				/* MSR-register */
				if((inst & 0xFFF00000U) == 0xD5100000U) {
					var op0 = (inst >> 19) & 0x1U;
					var op1 = (inst >> 16) & 0x7U;
					var cn = (inst >> 12) & 0xFU;
					var cm = (inst >> 8) & 0xFU;
					var op2 = (inst >> 5) & 0x7U;
					var rt = (inst >> 0) & 0x1FU;
					CallSR(op0, op1, cn, cm, op2, (RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt]));
					return true;
				}
				/* MSUB */
				if((inst & 0x7FE08000U) == 0x1B008000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var ra = (inst >> 10) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((ra) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) ra])) - (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])) * (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]))))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((ra) == 31 ? 0UL : XR[(int) ra])) - (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])) * (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])))));
					}
					return true;
				}
				/* ORN-shifted-register */
				if((inst & 0x7F200000U) == 0x2A200000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var b = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) (~((RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<uint>) ((RuntimeValue<uint>) (((b).ShiftLeft((RuntimeValue<uint>) (32 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) }))))))));
					} else {
						// Runtime else!
						var b = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (~((RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((b).ShiftLeft((RuntimeValue<uint>) (64 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) })))))));
					}
					return true;
				}
				/* ORR-immediate */
				if((inst & 0x7F800000U) == 0x32000000U) {
					var size = (inst >> 31) & 0x1U;
					var up = (inst >> 22) & 0x1U;
					var immr = (inst >> 16) & 0x3FU;
					var imms = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (ulong) (MakeWMask(up, imms, immr, (long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x20) : (0x40)), 0x1));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) | ((RuntimeValue<uint>) ((uint) ((uint) (imm)))))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) | ((RuntimeValue<uint>) ((uint) ((uint) (imm)))))));
					} else {
						// Runtime else!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) | ((RuntimeValue<ulong>) (imm))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) | ((RuntimeValue<ulong>) (imm))));
					}
					return true;
				}
				/* ORR-shifted-register */
				if((inst & 0x7F200000U) == 0x2A000000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var b = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<uint>) ((RuntimeValue<uint>) (((b).ShiftLeft((RuntimeValue<uint>) (32 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) }))))));
					} else {
						// Runtime else!
						var b = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((b).ShiftLeft((RuntimeValue<uint>) (64 - (imm)))) | ((b).ShiftRight((RuntimeValue<uint>) (imm))))) })))));
					}
					return true;
				}
				/* ORR-simd-register */
				if((inst & 0xBFE0FC00U) == 0x0EA01C00U) {
					var q = (inst >> 30) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var t = (string) (((byte) (((q) == (0x0)) ? 1U : 0U) != 0) ? ("8B") : ("16B"));
					if(((byte) (((rm) == (rn)) ? 1U : 0U)) != 0) {
						// Runtime if!
						VR[(int) (rd)] = (RuntimeValue<Vector128<float>>) (VR[(int) (rn)]);
					} else {
						throw new NotImplementedException();
					}
					return true;
				}
				/* PRFM-immediate */
				if((inst & 0xFFC00000U) == 0xF9800000U) {
					var imm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var imm5 = (inst >> 0) & 0x1FU;
					var pimm = (ulong) ((ulong) (imm) * (ulong) (0x8));
					return true;
				}
				/* RBIT */
				if((inst & 0x7FFFFC00U) == 0x5AC00000U) {
					var size = (inst >> 31) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallReverseBits((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (CallReverseBits((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])));
					}
					return true;
				}
				/* RET */
				if((inst & 0xFFFFFC1FU) == 0xD65F0000U) {
					var rn = (inst >> 5) & 0x1FU;
					Branch((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]));
					return true;
				}
				/* REV */
				if((inst & 0x7FFFF800U) == 0x5AC00800U) {
					var size = (inst >> 31) & 0x1U;
					var opc = (inst >> 10) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					switch((byte) ((byte) (((byte) (((byte) (opc)) << 0)) | ((byte) (((byte) (size)) << 1))))) {
						case 0x0: {
							var x = ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).Store();
							// Runtime let!
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((((RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (x)) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x18)))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((x).ShiftRight(0x8)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x10))))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((x).ShiftRight(0x10)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x8))))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((x).ShiftRight(0x18)))) & ((RuntimeValue<ulong>) (0xFF)))))))));
							break;
						}
						case 0x3: {
							var x = ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Store();
							// Runtime let!
							XR[(int) rd] = (RuntimeValue<ulong>) ((((((((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (x)) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x38)))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x8)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x30))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x10)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x28))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x18)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x20))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x20)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x18))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x28)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x10))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x30)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x8))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x38)))) & ((RuntimeValue<ulong>) (0xFF))))))));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* REV16 */
				if((inst & 0x7FFFFC00U) == 0x5AC00400U) {
					var size = (inst >> 31) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var x = ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((((RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (x)) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x8)))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((x).ShiftRight(0x8)))) & ((RuntimeValue<ulong>) (0xFF))))))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((x).ShiftRight(0x10)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x18))))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((x).ShiftRight(0x18)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x10)))))));
					} else {
						// Runtime else!
						var x = ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((((((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (x)) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x8)))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x8)))) & ((RuntimeValue<ulong>) (0xFF))))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x10)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x18))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x18)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x10))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x20)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x28))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x28)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x20))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x30)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x38))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((x).ShiftRight(0x38)))) & ((RuntimeValue<ulong>) (0xFF))))).ShiftLeft(0x30))))));
					}
					return true;
				}
				/* RORV */
				if((inst & 0x7FE0FC00U) == 0x1AC02C00U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).ShiftLeft((RuntimeValue<uint>) (32 - ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]))))) | (((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).ShiftRight((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]))))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).ShiftLeft((RuntimeValue<uint>) (64 - ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm]))))) | (((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).ShiftRight((RuntimeValue<uint>) ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])))));
					}
					return true;
				}
				/* SBCS */
				if((inst & 0x7FE0FC00U) == 0x7A000000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallAddWithCarrySetNzcv((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]), (RuntimeValue<uint>) (~((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]))), (RuntimeValue<byte>) (NZCV_CR))));
					} else {
						// Runtime else!
						XR[(int) rd] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn]), (RuntimeValue<ulong>) (~((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm]))), (RuntimeValue<byte>) (NZCV_CR)));
					}
					return true;
				}
				/* SBFM */
				if((inst & 0x7F800000U) == 0x13000000U) {
					var size = (inst >> 31) & 0x1U;
					var N = (inst >> 22) & 0x1U;
					var immr = (inst >> 16) & 0x3FU;
					var imms = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						// Runtime block!
						var src = ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).Store();
						var wmask = (uint) ((uint) ((ulong) (MakeWMask(N, imms, immr, 0x20, 0x0))));
						var tmask = (uint) ((uint) ((ulong) (MakeTMask(N, imms, immr, 0x20, 0x0))));
						// Runtime block!
						var bot = ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) (((src).ShiftLeft((RuntimeValue<uint>) (32 - (immr)))) | ((src).ShiftRight((RuntimeValue<uint>) (immr)))))) & ((RuntimeValue<uint>) (wmask))))).Store();
						// Runtime block!
						var top = ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((uint) ((uint) (0x0))) - (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((src).ShiftRight(imms)))) & ((RuntimeValue<ulong>) (0x1))))))).Store();
						// Runtime block!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) (top)) & ((RuntimeValue<uint>) ((uint) (~(tmask)))))))) | ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) (bot)) & ((RuntimeValue<uint>) (tmask)))))))));
					} else {
						// Runtime else!
						// Runtime block!
						var src = ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Store();
						var wmask = (ulong) (MakeWMask(N, imms, immr, 0x40, 0x0));
						var tmask = (ulong) (MakeTMask(N, imms, immr, 0x40, 0x0));
						// Runtime block!
						var bot = ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((src).ShiftLeft((RuntimeValue<uint>) (64 - (immr)))) | ((src).ShiftRight((RuntimeValue<uint>) (immr)))))) & ((RuntimeValue<ulong>) (wmask))))).Store();
						// Runtime block!
						var top = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((ulong) ((ulong) (0x0))) - (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((src).ShiftRight(imms)))) & ((RuntimeValue<ulong>) (0x1))))))).Store();
						// Runtime block!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (top)) & ((RuntimeValue<ulong>) ((ulong) (~(tmask)))))))) | ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (bot)) & ((RuntimeValue<ulong>) (tmask))))))));
					}
					return true;
				}
				/* SCVTF-scalar-integer */
				if((inst & 0x7F3FFC00U) == 0x1E220000U) {
					var size = (inst >> 31) & 0x1U;
					var type = (inst >> 22) & 0x3U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var st = (byte) ((byte) (((byte) (((byte) (type)) << 0)) | ((byte) (((byte) (size)) << 2))));
					var r1 = "";
					var r2 = "";
					switch(st) {
						case 0x3: {
							r1 = "H";
							r2 = "W";
							break;
						}
						case 0x0: {
							r1 = "S";
							r2 = "W";
							break;
						}
						case 0x1: {
							r1 = "D";
							r2 = "W";
							break;
						}
						case 0x7: {
							r1 = "H";
							r2 = "X";
							break;
						}
						case 0x4: {
							r1 = "S";
							r2 = "X";
							break;
						}
						case 0x5: {
							r1 = "D";
							r2 = "X";
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					switch(st) {
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<int>) ((RuntimeValue<int>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])))));
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<int>) ((RuntimeValue<int>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])))));
							break;
						}
						case 0x4: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])))));
							break;
						}
						case 0x5: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])))));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* SCVTF-vector-integer */
				if((inst & 0xFFBFFC00U) == 0x5E21D800U) {
					var size = (inst >> 22) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("S") : ("D"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<int>) (((RuntimeValue<float>) (VSR[(int) (rn)])).Bitcast<int>())));
					} else {
						// Runtime else!
						VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<long>) (((RuntimeValue<double>) (VDR[(int) (rn)])).Bitcast<long>())));
					}
					return true;
				}
				/* SDIV */
				if((inst & 0x7FE0FC00U) == 0x1AC00C00U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var operand2 = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (Ternary<byte, uint>((RuntimeValue<byte>) ((RuntimeValue<byte>) ((operand2) == (0x0))), (uint) ((uint) (0x0)), (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<int>) ((RuntimeValue<int>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])))))) / (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<int>) ((RuntimeValue<int>) (operand2)))))))))));
					} else {
						// Runtime else!
						var operand2 = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (Ternary<byte, ulong>((RuntimeValue<byte>) ((RuntimeValue<byte>) ((operand2) == (0x0))), (ulong) ((ulong) (0x0)), (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])))))) / (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<long>) ((RuntimeValue<long>) (operand2))))))))));
					}
					return true;
				}
				/* SMADDL */
				if((inst & 0xFFE08000U) == 0x9B200000U) {
					var rm = (inst >> 16) & 0x1FU;
					var ra = (inst >> 10) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) ((RuntimeValue<long>) (RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<ulong>) ((ra) == 31 ? 0UL : XR[(int) ra])))) + (RuntimeValue<long>) (RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<long>) (RuntimeValue<long>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn]), 32))) * (RuntimeValue<long>) (RuntimeValue<long>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))))));
					return true;
				}
				/* SMULH */
				if((inst & 0xFFE0FC00U) == 0x9B407C00U) {
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<Int128>) (((RuntimeValue<Int128>) ((RuntimeValue<Int128>) (RuntimeValue<Int128>) ((RuntimeValue<Int128>) ((RuntimeValue<Int128>) ((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])))))) * (RuntimeValue<Int128>) (RuntimeValue<Int128>) ((RuntimeValue<Int128>) ((RuntimeValue<Int128>) ((RuntimeValue<long>) ((RuntimeValue<long>) ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])))))))).ShiftRight(0x40))))));
					return true;
				}
				/* STLR */
				if((inst & 0xBFFFFC00U) == 0x889FFC00U) {
					var size = (inst >> 30) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]))).Value = (RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt]);
					} else {
						// Runtime else!
						((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]))).Value = (RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt]);
					}
					return true;
				}
				/* STLRB */
				if((inst & 0xFFFFFC00U) == 0x089FFC00U) {
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					((RuntimePointer<byte>) (address)).Value = (RuntimeValue<byte>) ((RuntimeValue<byte>) ((RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt])));
					return true;
				}
				/* STLRH */
				if((inst & 0xFFFFFC00U) == 0x489FFC00U) {
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					((RuntimePointer<ushort>) (address)).Value = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt])));
					return true;
				}
				/* STLXR */
				if((inst & 0xBFE0FC00U) == 0x8800FC00U) {
					var size = (inst >> 30) & 0x1U;
					var rs = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						((RuntimePointer<uint>) (address)).Value = (RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt]);
					} else {
						// Runtime else!
						((RuntimePointer<ulong>) (address)).Value = (RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt]);
					}
					// Runtime let!
					XR[(int) rs] = (RuntimeValue<ulong>) (RuntimeValue<uint>) (0x0);
					return true;
				}
				/* STLXRB */
				if((inst & 0xFFE0FC00U) == 0x0800FC00U) {
					var rs = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					((RuntimePointer<byte>) (address)).Value = (RuntimeValue<byte>) ((RuntimeValue<byte>) ((RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt])));
					// Runtime let!
					XR[(int) rs] = (RuntimeValue<ulong>) (RuntimeValue<uint>) (0x0);
					return true;
				}
				/* STP-postindex */
				if((inst & 0x7FC00000U) == 0x28800000U) {
					var size = (inst >> 31) & 0x1U;
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rd = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) ((long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x2) : (0x3))));
					var address = ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						// Runtime block!
						((RuntimePointer<uint>) (address)).Value = (RuntimeValue<uint>) ((rt1) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt1]);
						// Runtime block!
						((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value = (RuntimeValue<uint>) ((rt2) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt2]);
					} else {
						// Runtime else!
						// Runtime block!
						((RuntimePointer<ulong>) (address)).Value = (RuntimeValue<ulong>) ((rt1) == 31 ? 0UL : XR[(int) rt1]);
						// Runtime block!
						((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value = (RuntimeValue<ulong>) ((rt2) == 31 ? 0UL : XR[(int) rt2]);
					}
					// Runtime let!
					if(rd == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					else
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					return true;
				}
				/* STP-preindex */
				if((inst & 0x7FC00000U) == 0x29800000U) {
					var size = (inst >> 31) & 0x1U;
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rd = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) ((long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x2) : (0x3))));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						// Runtime block!
						((RuntimePointer<uint>) (address)).Value = (RuntimeValue<uint>) ((rt1) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt1]);
						// Runtime block!
						((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value = (RuntimeValue<uint>) ((rt2) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt2]);
					} else {
						// Runtime else!
						// Runtime block!
						((RuntimePointer<ulong>) (address)).Value = (RuntimeValue<ulong>) ((rt1) == 31 ? 0UL : XR[(int) rt1]);
						// Runtime block!
						((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value = (RuntimeValue<ulong>) ((rt2) == 31 ? 0UL : XR[(int) rt2]);
					}
					// Runtime let!
					if(rd == 31)
						SPR = address;
					else
						XR[(int) rd] = address;
					return true;
				}
				/* STP-signed-offset */
				if((inst & 0x7FC00000U) == 0x29000000U) {
					var size = (inst >> 31) & 0x1U;
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rd = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) ((long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x2) : (0x3))));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						// Runtime block!
						((RuntimePointer<uint>) (address)).Value = (RuntimeValue<uint>) ((rt1) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt1]);
						// Runtime block!
						((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value = (RuntimeValue<uint>) ((rt2) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt2]);
					} else {
						// Runtime else!
						// Runtime block!
						((RuntimePointer<ulong>) (address)).Value = (RuntimeValue<ulong>) ((rt1) == 31 ? 0UL : XR[(int) rt1]);
						// Runtime block!
						((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value = (RuntimeValue<ulong>) ((rt2) == 31 ? 0UL : XR[(int) rt2]);
					}
					return true;
				}
				/* STP-simd-postindex */
				if((inst & 0x3FC00000U) == 0x2C800000U) {
					var opc = (inst >> 30) & 0x3U;
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rd = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var r = (string) ((opc) switch { 0x0 => "S", 0x1 => "D", 0x2 => "Q", _ => throw new NotImplementedException() });
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) ((long) ((opc) switch { 0x0 => 0x2, 0x1 => 0x3, 0x2 => 0x4, _ => throw new NotImplementedException() })));
					var address = ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])).Store();
					// Runtime let!
					switch(opc) {
						case 0x0: {
							// Runtime block!
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VSR[(int) (rt1)]);
							// Runtime block!
							((RuntimePointer<float>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value = (RuntimeValue<float>) (VSR[(int) (rt2)]);
							break;
						}
						case 0x1: {
							// Runtime block!
							((RuntimePointer<double>) (address)).Value = (RuntimeValue<double>) (VDR[(int) (rt1)]);
							// Runtime block!
							((RuntimePointer<double>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value = (RuntimeValue<double>) (VDR[(int) (rt2)]);
							break;
						}
						case 0x2: {
							// Runtime block!
							((RuntimePointer<Vector128<float>>) (address)).Value = (RuntimeValue<Vector128<float>>) (VR[(int) (rt1)]);
							// Runtime block!
							((RuntimePointer<Vector128<float>>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x10)))).Value = (RuntimeValue<Vector128<float>>) (VR[(int) (rt2)]);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					// Runtime let!
					if(rd == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					else
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					return true;
				}
				/* STP-simd-preindex */
				if((inst & 0x3FC00000U) == 0x2D800000U) {
					var opc = (inst >> 30) & 0x3U;
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rd = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var r = (string) ((opc) switch { 0x0 => "S", 0x1 => "D", 0x2 => "Q", _ => throw new NotImplementedException() });
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) ((long) ((opc) switch { 0x0 => 0x2, 0x1 => 0x3, 0x2 => 0x4, _ => throw new NotImplementedException() })));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					switch(opc) {
						case 0x0: {
							// Runtime block!
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VSR[(int) (rt1)]);
							// Runtime block!
							((RuntimePointer<float>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value = (RuntimeValue<float>) (VSR[(int) (rt2)]);
							break;
						}
						case 0x1: {
							// Runtime block!
							((RuntimePointer<double>) (address)).Value = (RuntimeValue<double>) (VDR[(int) (rt1)]);
							// Runtime block!
							((RuntimePointer<double>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value = (RuntimeValue<double>) (VDR[(int) (rt2)]);
							break;
						}
						case 0x2: {
							// Runtime block!
							((RuntimePointer<Vector128<float>>) (address)).Value = (RuntimeValue<Vector128<float>>) (VR[(int) (rt1)]);
							// Runtime block!
							((RuntimePointer<Vector128<float>>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x10)))).Value = (RuntimeValue<Vector128<float>>) (VR[(int) (rt2)]);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					// Runtime let!
					if(rd == 31)
						SPR = address;
					else
						XR[(int) rd] = address;
					return true;
				}
				/* STP-simd-signed-offset */
				if((inst & 0x3FC00000U) == 0x2D000000U) {
					var opc = (inst >> 30) & 0x3U;
					var imm = (inst >> 15) & 0x7FU;
					var rt2 = (inst >> 10) & 0x1FU;
					var rd = (inst >> 5) & 0x1FU;
					var rt1 = (inst >> 0) & 0x1FU;
					var r = (string) ((opc) switch { 0x0 => "S", 0x1 => "D", 0x2 => "Q", _ => throw new NotImplementedException() });
					var simm = (long) (((long) (SignExt<long>(imm, 7))) << (int) ((long) ((opc) switch { 0x0 => 0x2, 0x1 => 0x3, 0x2 => 0x4, _ => throw new NotImplementedException() })));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					switch(opc) {
						case 0x0: {
							// Runtime block!
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VSR[(int) (rt1)]);
							// Runtime block!
							((RuntimePointer<float>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x4)))).Value = (RuntimeValue<float>) (VSR[(int) (rt2)]);
							break;
						}
						case 0x1: {
							// Runtime block!
							((RuntimePointer<double>) (address)).Value = (RuntimeValue<double>) (VDR[(int) (rt1)]);
							// Runtime block!
							((RuntimePointer<double>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x8)))).Value = (RuntimeValue<double>) (VDR[(int) (rt2)]);
							break;
						}
						case 0x2: {
							// Runtime block!
							((RuntimePointer<Vector128<float>>) (address)).Value = (RuntimeValue<Vector128<float>>) (VR[(int) (rt1)]);
							// Runtime block!
							((RuntimePointer<Vector128<float>>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (0x10)))).Value = (RuntimeValue<Vector128<float>>) (VR[(int) (rt2)]);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* STR-immediate-postindex */
				if((inst & 0xBFE00C00U) == 0xB8000400U) {
					var size = (inst >> 30) & 0x1U;
					var imm = (inst >> 12) & 0x1FFU;
					var rd = (inst >> 5) & 0x1FU;
					var rs = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var simm = (long) (SignExt<long>(imm, 9));
					var address = ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						((RuntimePointer<uint>) (address)).Value = (RuntimeValue<uint>) ((rs) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rs]);
					} else {
						// Runtime else!
						((RuntimePointer<ulong>) (address)).Value = (RuntimeValue<ulong>) ((rs) == 31 ? 0UL : XR[(int) rs]);
					}
					// Runtime let!
					if(rd == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					else
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					return true;
				}
				/* STR-immediate-preindex */
				if((inst & 0xBFE00C00U) == 0xB8000C00U) {
					var size = (inst >> 30) & 0x1U;
					var imm = (inst >> 12) & 0x1FFU;
					var rd = (inst >> 5) & 0x1FU;
					var rs = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var simm = (long) (SignExt<long>(imm, 9));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						((RuntimePointer<uint>) (address)).Value = (RuntimeValue<uint>) ((rs) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rs]);
					} else {
						// Runtime else!
						((RuntimePointer<ulong>) (address)).Value = (RuntimeValue<ulong>) ((rs) == 31 ? 0UL : XR[(int) rs]);
					}
					// Runtime let!
					if(rd == 31)
						SPR = address;
					else
						XR[(int) rd] = address;
					return true;
				}
				/* STR-immediate-unsigned-offset */
				if((inst & 0xBFC00000U) == 0xB9000000U) {
					var size = (inst >> 30) & 0x1U;
					var imm = (inst >> 10) & 0xFFFU;
					var rd = (inst >> 5) & 0x1FU;
					var rs = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var pimm = (ulong) (((ulong) ((ulong) (imm))) << (int) ((long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x2) : (0x3))));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (pimm)))).Value = (RuntimeValue<uint>) ((rs) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rs]);
					} else {
						// Runtime else!
						((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (pimm)))).Value = (RuntimeValue<ulong>) ((rs) == 31 ? 0UL : XR[(int) rs]);
					}
					return true;
				}
				/* STR-register */
				if((inst & 0xBFE00C00U) == 0xB8200800U) {
					var size = (inst >> 30) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var scale = (inst >> 12) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r1 = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var r2 = (string) (((byte) ((((ulong) (option)) & ((ulong) (0x1)))) != 0) ? ("X") : ("W"));
					var amount = (long) (((byte) (((scale) == (0x0)) ? 1U : 0U) != 0) ? (0x0) : ((long) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? (0x2) : (0x3))));
					var extend = (string) ((option) switch { 0x2 => "UXTW", 0x6 => "SXTW", 0x7 => "SXTX", _ => "LSL" });
					var offset = ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) (((byte) (((option) == (0x6)) ? 1U : 0U)) != 0 ? ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))) : ((RuntimeValue<ulong>) (((byte) ((((ulong) (option)) & ((ulong) (0x1))))) != 0 ? ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])) : ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))))))).ShiftLeft(amount))).Store();
					// Runtime let!
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value = (RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt]);
					} else {
						// Runtime else!
						((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value = (RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt]);
					}
					return true;
				}
				/* STR-simd-postindex */
				if((inst & 0x3F600C00U) == 0x3C000400U) {
					var size = (inst >> 30) & 0x3U;
					var opc = (inst >> 23) & 0x1U;
					var imm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var rop = (byte) ((byte) (((byte) (byte) (((byte) (((byte) ((byte) ((byte) (0x0)))) << 0)) | ((byte) (((byte) (opc)) << 1)))) | ((byte) (((byte) (size)) << 2))));
					var r = (string) ((rop) switch { 0x0 => "B", 0x4 => "H", 0x8 => "S", 0xC => "D", 0x2 => "Q", _ => throw new NotImplementedException() });
					var simm = (long) (SignExt<long>(imm, 9));
					var address = ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])).Store();
					// Runtime let!
					switch(rop) {
						case 0x0: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VBR[(int) (rt)]);
							break;
						}
						case 0x4: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VHR[(int) (rt)]);
							break;
						}
						case 0x8: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VSR[(int) (rt)]);
							break;
						}
						case 0xC: {
							((RuntimePointer<double>) (address)).Value = (RuntimeValue<double>) (VDR[(int) (rt)]);
							break;
						}
						case 0x2: {
							((RuntimePointer<Vector128<float>>) (address)).Value = (RuntimeValue<Vector128<float>>) (VR[(int) (rt)]);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					// Runtime let!
					if(rn == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					else
						XR[(int) rn] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					return true;
				}
				/* STR-simd-preindex */
				if((inst & 0x3F600C00U) == 0x3C000C00U) {
					var size = (inst >> 30) & 0x3U;
					var opc = (inst >> 23) & 0x1U;
					var imm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var rop = (byte) ((byte) (((byte) (byte) (((byte) (((byte) ((byte) ((byte) (0x0)))) << 0)) | ((byte) (((byte) (opc)) << 1)))) | ((byte) (((byte) (size)) << 2))));
					var r = (string) ((rop) switch { 0x0 => "B", 0x4 => "H", 0x8 => "S", 0xC => "D", 0x2 => "Q", _ => throw new NotImplementedException() });
					var scale = (byte) ((byte) (((byte) (((byte) (size)) << 0)) | ((byte) (((byte) (opc)) << 2))));
					var simm = (long) (SignExt<long>(imm, 9));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					switch(rop) {
						case 0x0: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VBR[(int) (rt)]);
							break;
						}
						case 0x4: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VHR[(int) (rt)]);
							break;
						}
						case 0x8: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VSR[(int) (rt)]);
							break;
						}
						case 0xC: {
							((RuntimePointer<double>) (address)).Value = (RuntimeValue<double>) (VDR[(int) (rt)]);
							break;
						}
						case 0x2: {
							((RuntimePointer<Vector128<float>>) (address)).Value = (RuntimeValue<Vector128<float>>) (VR[(int) (rt)]);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					// Runtime let!
					if(rn == 31)
						SPR = address;
					else
						XR[(int) rn] = address;
					return true;
				}
				/* STR-simd-unsigned-offset */
				if((inst & 0x3F400000U) == 0x3D000000U) {
					var size = (inst >> 30) & 0x3U;
					var opc = (inst >> 23) & 0x1U;
					var imm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var rop = (byte) ((byte) (((byte) (byte) (((byte) (((byte) ((byte) ((byte) (0x0)))) << 0)) | ((byte) (((byte) (opc)) << 1)))) | ((byte) (((byte) (size)) << 2))));
					var r = (string) ((rop) switch { 0x0 => "B", 0x4 => "H", 0x8 => "S", 0xC => "D", 0x2 => "Q", _ => throw new NotImplementedException() });
					var scale = (byte) ((byte) (((byte) (((byte) (size)) << 0)) | ((byte) (((byte) (opc)) << 2))));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) ((ushort) ((imm) << (int) (scale))))).Store();
					// Runtime let!
					switch(rop) {
						case 0x0: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VBR[(int) (rt)]);
							break;
						}
						case 0x4: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VHR[(int) (rt)]);
							break;
						}
						case 0x8: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VSR[(int) (rt)]);
							break;
						}
						case 0xC: {
							((RuntimePointer<double>) (address)).Value = (RuntimeValue<double>) (VDR[(int) (rt)]);
							break;
						}
						case 0x2: {
							((RuntimePointer<Vector128<float>>) (address)).Value = (RuntimeValue<Vector128<float>>) (VR[(int) (rt)]);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* STRB-immediate-postindex */
				if((inst & 0xFFE00C00U) == 0x38000400U) {
					var imm = (inst >> 12) & 0x1FFU;
					var rd = (inst >> 5) & 0x1FU;
					var rs = (inst >> 0) & 0x1FU;
					var simm = (long) (SignExt<long>(imm, 9));
					var address = ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])).Store();
					// Runtime let!
					((RuntimePointer<byte>) (address)).Value = (RuntimeValue<byte>) ((RuntimeValue<byte>) ((RuntimeValue<uint>) ((rs) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rs])));
					// Runtime let!
					if(rd == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					else
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					return true;
				}
				/* STRB-immediate-preindex */
				if((inst & 0xFFE00C00U) == 0x38000C00U) {
					var imm = (inst >> 12) & 0x1FFU;
					var rd = (inst >> 5) & 0x1FU;
					var rs = (inst >> 0) & 0x1FU;
					var simm = (long) (SignExt<long>(imm, 9));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					((RuntimePointer<byte>) (address)).Value = (RuntimeValue<byte>) ((RuntimeValue<byte>) ((RuntimeValue<uint>) ((rs) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rs])));
					// Runtime let!
					if(rd == 31)
						SPR = address;
					else
						XR[(int) rd] = address;
					return true;
				}
				/* STRB-immediate-unsigned-offset */
				if((inst & 0xFFC00000U) == 0x39000000U) {
					var imm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value = (RuntimeValue<byte>) ((RuntimeValue<byte>) ((RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt])));
					return true;
				}
				/* STRB-register */
				if((inst & 0xFFE00C00U) == 0x38200800U) {
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var amount = (inst >> 12) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) ((((ulong) (option)) & ((ulong) (0x1)))) != 0) ? ("X") : ("W"));
					var str = (string) ((option) switch { 0x2 => "UXTW", 0x3 => "LSL", 0x6 => "SXTW", 0x7 => "SXTX", _ => throw new NotImplementedException() });
					var offset = ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) (((byte) (((option) == (0x6)) ? 1U : 0U)) != 0 ? ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))) : ((RuntimeValue<ulong>) (((byte) ((((ulong) (option)) & ((ulong) (0x1))))) != 0 ? ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])) : ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))))))).ShiftLeft(amount))).Store();
					// Runtime let!
					((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value = (RuntimeValue<byte>) ((RuntimeValue<byte>) ((RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt])));
					return true;
				}
				/* STRH-immediate-postindex */
				if((inst & 0xFFE00C00U) == 0x78000400U) {
					var imm = (inst >> 12) & 0x1FFU;
					var rd = (inst >> 5) & 0x1FU;
					var rs = (inst >> 0) & 0x1FU;
					var simm = (long) (SignExt<long>(imm, 9));
					var address = ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])).Store();
					// Runtime let!
					((RuntimePointer<ushort>) (address)).Value = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<uint>) ((rs) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rs])));
					// Runtime let!
					if(rd == 31)
						SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					else
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) (address) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm));
					return true;
				}
				/* STRH-immediate-preindex */
				if((inst & 0xFFE00C00U) == 0x78000C00U) {
					var imm = (inst >> 12) & 0x1FFU;
					var rd = (inst >> 5) & 0x1FU;
					var rs = (inst >> 0) & 0x1FU;
					var simm = (long) (SignExt<long>(imm, 9));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rd) == 31 ? SPR : XR[(int) rd])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					((RuntimePointer<ushort>) (address)).Value = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<uint>) ((rs) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rs])));
					// Runtime let!
					if(rd == 31)
						SPR = address;
					else
						XR[(int) rd] = address;
					return true;
				}
				/* STRH-immediate-unsigned-offset */
				if((inst & 0xFFC00000U) == 0x79000000U) {
					var rawimm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var imm = (ushort) ((rawimm) << (int) (0x1));
					((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ushort>) (imm)))).Value = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt])));
					return true;
				}
				/* STRH-register */
				if((inst & 0xFFE00C00U) == 0x78200800U) {
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var amount = (inst >> 12) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) ((((ulong) (option)) & ((ulong) (0x1)))) != 0) ? ("X") : ("W"));
					var str = (string) ((option) switch { 0x2 => "UXTW", 0x3 => "LSL", 0x6 => "SXTW", 0x7 => "SXTX", _ => throw new NotImplementedException() });
					var offset = ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) (((byte) (((option) == (0x6)) ? 1U : 0U)) != 0 ? ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), 32))))) : ((RuntimeValue<ulong>) (((byte) ((((ulong) (option)) & ((ulong) (0x1))))) != 0 ? ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])) : ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))))))).ShiftLeft(amount))).Store();
					// Runtime let!
					((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) (offset)))).Value = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt])));
					return true;
				}
				/* STUR */
				if((inst & 0xBFE00C00U) == 0xB8000000U) {
					var size = (inst >> 30) & 0x1U;
					var imm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var offset = (long) (SignExt<long>(imm, 9));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (offset)))).Value = (RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt]);
					} else {
						// Runtime else!
						((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (offset)))).Value = (RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt]);
					}
					return true;
				}
				/* STUR-simd */
				if((inst & 0x3F600C00U) == 0x3C000000U) {
					var size = (inst >> 30) & 0x3U;
					var opc = (inst >> 23) & 0x1U;
					var imm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var rop = (byte) ((byte) (((byte) (byte) (((byte) (((byte) ((byte) ((byte) (0x0)))) << 0)) | ((byte) (((byte) (opc)) << 1)))) | ((byte) (((byte) (size)) << 2))));
					var r = (string) ((rop) switch { 0x0 => "B", 0x4 => "H", 0x8 => "S", 0xC => "D", 0x2 => "Q", _ => throw new NotImplementedException() });
					var simm = (long) (SignExt<long>(imm, 9));
					var address = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (simm))).Store();
					// Runtime let!
					switch(rop) {
						case 0x0: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VBR[(int) (rt)]);
							break;
						}
						case 0x4: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VHR[(int) (rt)]);
							break;
						}
						case 0x8: {
							((RuntimePointer<float>) (address)).Value = (RuntimeValue<float>) (VSR[(int) (rt)]);
							break;
						}
						case 0xC: {
							((RuntimePointer<double>) (address)).Value = (RuntimeValue<double>) (VDR[(int) (rt)]);
							break;
						}
						case 0x2: {
							((RuntimePointer<Vector128<float>>) (address)).Value = (RuntimeValue<Vector128<float>>) (VR[(int) (rt)]);
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* STURB */
				if((inst & 0xFFE00C00U) == 0x38000000U) {
					var imm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var offset = (long) (SignExt<long>(imm, 9));
					((RuntimePointer<byte>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (offset)))).Value = (RuntimeValue<byte>) ((RuntimeValue<byte>) ((RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt])));
					return true;
				}
				/* STURH */
				if((inst & 0xFFE00C00U) == 0x78000000U) {
					var imm = (inst >> 12) & 0x1FFU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var offset = (long) (SignExt<long>(imm, 9));
					((RuntimePointer<ushort>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) + (RuntimeValue<ulong>) (RuntimeValue<long>) (offset)))).Value = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt])));
					return true;
				}
				/* STXR */
				if((inst & 0xBFE0FC00U) == 0x88007C00U) {
					var size = (inst >> 30) & 0x1U;
					var rs = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					XR[(int) rs] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<byte>) (((byte) (((size) == (0x0)) ? 1U : 0U)) != 0 ? ((RuntimeValue<byte>) (CallCompareAndSwap((RuntimePointer<uint>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])), (RuntimeValue<uint>) ((rt) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rt]), Exclusive32R))) : ((RuntimeValue<byte>) (CallCompareAndSwap((RuntimePointer<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])), (RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt]), Exclusive64R)))));
					return true;
				}
				/* SUB-immediate */
				if((inst & 0x7F800000U) == 0x51000000U) {
					var size = (inst >> 31) & 0x1U;
					var sh = (inst >> 22) & 0x1U;
					var imm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shift = (long) (((byte) (((sh) == (0x0)) ? 1U : 0U) != 0) ? (0x0) : (0xC));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn]))) - (RuntimeValue<uint>) (RuntimeValue<uint>) ((uint) (((uint) ((uint) (imm))) << (int) (shift)))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn]))) - (RuntimeValue<uint>) (RuntimeValue<uint>) ((uint) (((uint) ((uint) (imm))) << (int) (shift)))));
					} else {
						// Runtime else!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) - (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((ulong) (((ulong) ((ulong) (imm))) << (int) (shift))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) - (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((ulong) (((ulong) ((ulong) (imm))) << (int) (shift))));
					}
					return true;
				}
				/* SUB-extended-register */
				if((inst & 0x7FE00000U) == 0x4B200000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var imm = (inst >> 10) & 0x7U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r1 = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var r2 = (string) (((byte) ((((byte) ((((ulong) (option)) & ((ulong) (0x3))))) == (0x3)) ? 1U : 0U) != 0) ? ("X") : ("W"));
					var extend = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ((string) ((option) switch { 0x0 => "UXTB", 0x1 => "UXTH", 0x2 => "LSL", 0x3 => "UXTX", 0x4 => "SXTB", 0x5 => "SXTH", 0x6 => "SXTW", _ => "SXTX" })) : ((string) ((option) switch { 0x0 => "UXTB", 0x1 => "UXTH", 0x2 => "UXTW", 0x3 => "LSL", 0x4 => "SXTB", 0x5 => "SXTH", 0x6 => "SXTW", _ => "SXTX" })));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var m = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						if(rd == 31)
							SPR = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn]))) - (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((option) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFF))))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFF))))), 0x4 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<byte>) ((RuntimeValue<byte>) (m)), 8))))), 0x5 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (m)), 16))))), _ => (RuntimeValue<uint>) (m) })).ShiftLeft(imm)))));
						else
							XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn]))) - (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (((RuntimeValue<uint>) ((option) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFF))))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFF))))), 0x4 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<byte>) ((RuntimeValue<byte>) (m)), 8))))), 0x5 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (m)), 16))))), _ => (RuntimeValue<uint>) (m) })).ShiftLeft(imm)))));
					} else {
						// Runtime else!
						if(((byte) ((((byte) ((((ulong) (option)) & ((ulong) (0x3))))) == (0x3)) ? 1U : 0U)) != 0) {
							// Runtime if!
							if(rd == 31)
								SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) - (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).ShiftLeft(imm))));
							else
								XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) - (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).ShiftLeft(imm))));
						} else {
							// Runtime else!
							var m = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))).Store();
							// Runtime let!
							if(rd == 31)
								SPR = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) - (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((option) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFF))))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFF))))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFFFFFF))))), 0x4 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<byte>) ((RuntimeValue<byte>) (m)), 8))))), 0x5 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (m)), 16))))), 0x6 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>(m, 64))))), _ => (RuntimeValue<ulong>) (m) })).ShiftLeft(imm))));
							else
								XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn])) - (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((option) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFF))))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFF))))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFFFFFF))))), 0x4 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<byte>) ((RuntimeValue<byte>) (m)), 8))))), 0x5 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (m)), 16))))), 0x6 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>(m, 64))))), _ => (RuntimeValue<ulong>) (m) })).ShiftLeft(imm))));
						}
					}
					return true;
				}
				/* SUB-shifted-register */
				if((inst & 0x7F200000U) == 0x4B000000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var b = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])) - (RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((shift) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (((RuntimeValue<int>) ((RuntimeValue<int>) (b))).ShiftRight(imm))))), _ => throw new NotImplementedException() }))));
					} else {
						// Runtime else!
						var b = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])) - (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((shift) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftLeft(imm))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((b).ShiftRight(imm))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (((RuntimeValue<long>) ((RuntimeValue<long>) (b))).ShiftRight(imm))))), _ => throw new NotImplementedException() })));
					}
					return true;
				}
				/* SUBS-extended-register */
				if((inst & 0x7FE00000U) == 0x6B200000U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var option = (inst >> 13) & 0x7U;
					var imm = (inst >> 10) & 0x7U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r1 = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var r2 = (string) (((byte) ((((byte) ((((ulong) (option)) & ((ulong) (0x3))))) == (0x3)) ? 1U : 0U) != 0) ? ("X") : ("W"));
					var extend = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ((string) ((option) switch { 0x0 => "UXTB", 0x1 => "UXTH", 0x2 => "LSL", 0x3 => "UXTX", 0x4 => "SXTB", 0x5 => "SXTH", 0x6 => "SXTW", _ => "SXTX" })) : ((string) ((option) switch { 0x0 => "UXTB", 0x1 => "UXTH", 0x2 => "UXTW", 0x3 => "LSL", 0x4 => "SXTB", 0x5 => "SXTH", 0x6 => "SXTW", _ => "SXTX" })));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var m = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallAddWithCarrySetNzcv((RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? (SPR & 0xFFFFFFFFUL) : XR[(int) rn])), (RuntimeValue<uint>) (~((RuntimeValue<uint>) (((RuntimeValue<uint>) ((option) switch { 0x0 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFF))))), 0x1 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFF))))), 0x4 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<byte>) ((RuntimeValue<byte>) (m)), 8))))), 0x5 => (RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<uint>) ((RuntimeValue<int>) (SignExtRuntime<int>((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (m)), 16))))), _ => (RuntimeValue<uint>) (m) })).ShiftLeft(imm)))), 0x1)));
					} else {
						// Runtime else!
						if(((byte) ((((byte) ((((ulong) (option)) & ((ulong) (0x3))))) == (0x3)) ? 1U : 0U)) != 0) {
							// Runtime if!
							XR[(int) rd] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]), (RuntimeValue<ulong>) (~((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).ShiftLeft(imm)))), 0x1));
						} else {
							// Runtime else!
							var m = ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))).Store();
							// Runtime let!
							XR[(int) rd] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv((RuntimeValue<ulong>) ((rn) == 31 ? SPR : XR[(int) rn]), (RuntimeValue<ulong>) (~((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((option) switch { 0x0 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFF))))), 0x1 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFF))))), 0x2 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (m)) & ((RuntimeValue<ulong>) (0xFFFFFFFF))))), 0x4 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<byte>) ((RuntimeValue<byte>) (m)), 8))))), 0x5 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>((RuntimeValue<ushort>) ((RuntimeValue<ushort>) (m)), 16))))), 0x6 => (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<long>) (SignExtRuntime<long>(m, 64))))), _ => (RuntimeValue<ulong>) (m) })).ShiftLeft(imm)))), 0x1));
						}
					}
					return true;
				}
				/* SUBS-shifted-register */
				if((inst & 0x7F200000U) == 0x6B000000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x3U;
					var rm = (inst >> 16) & 0x1FU;
					var imm = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var mode32 = (byte) (((size) == (0x0)) ? 1U : 0U);
					var r = (string) ((mode32 != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL", 0x1 => "LSR", 0x2 => "ASR", _ => "ROR" });
					if((mode32) != 0) {
						// Runtime if!
						// Runtime block!
						var operand1 = ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).Store();
						// Runtime block!
						var operand2 = ((RuntimeValue<uint>) (~((RuntimeValue<uint>) (Shift((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm]), shift, imm))))).Store();
						// Runtime block!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallAddWithCarrySetNzcv(operand1, operand2, 0x1)));
					} else {
						// Runtime else!
						// Runtime block!
						var operand1 = ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Store();
						// Runtime block!
						var operand2 = ((RuntimeValue<ulong>) (~((RuntimeValue<ulong>) (Shift((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm]), shift, imm))))).Store();
						// Runtime block!
						XR[(int) rd] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv(operand1, operand2, 0x1));
					}
					return true;
				}
				/* SUBS-immediate */
				if((inst & 0x7F800000U) == 0x71000000U) {
					var size = (inst >> 31) & 0x1U;
					var shift = (inst >> 22) & 0x1U;
					var imm = (inst >> 10) & 0xFFFU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var mode32 = (byte) (((size) == (0x0)) ? 1U : 0U);
					var r = (string) ((mode32 != 0) ? ("W") : ("X"));
					var shiftstr = (string) ((shift) switch { 0x0 => "LSL #0", 0x1 => "LSL #12", _ => throw new NotImplementedException() });
					var rimm = (uint) ((shift != 0) ? ((uint) (((uint) ((uint) (imm))) << (int) (0xC))) : (imm));
					// Runtime let!
					if((mode32) != 0) {
						// Runtime if!
						// Runtime block!
						var operand1 = ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).Store();
						var operand2 = (uint) (~((uint) ((uint) (rimm))));
						// Runtime block!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (CallAddWithCarrySetNzcv(operand1, operand2, 0x1)));
					} else {
						// Runtime else!
						// Runtime block!
						var operand1 = ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Store();
						var operand2 = (ulong) (~((ulong) ((ulong) (rimm))));
						// Runtime block!
						XR[(int) rd] = (RuntimeValue<ulong>) (CallAddWithCarrySetNzcv(operand1, operand2, 0x1));
					}
					return true;
				}
				/* SVC */
				if((inst & 0xFFE0001FU) == 0xD4000001U) {
					var imm = (inst >> 5) & 0xFFFFU;
					CallVoid(nameof(Svc), imm);
					return true;
				}
				/* SYS */
				if((inst & 0xFFF80000U) == 0xD5080000U) {
					var op1 = (inst >> 16) & 0x7U;
					var cn = (inst >> 12) & 0xFU;
					var cm = (inst >> 8) & 0xFU;
					var op2 = (inst >> 5) & 0x7U;
					var rt = (inst >> 0) & 0x1FU;
					return true;
				}
				/* TBZ */
				if((inst & 0x7F000000U) == 0x36000000U) {
					var upper = (inst >> 31) & 0x1U;
					var bottom = (inst >> 19) & 0x1FU;
					var offset = (inst >> 5) & 0x3FFFU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((upper) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (byte) ((((byte) ((byte) ((upper) << (int) (0x5)))) | ((byte) (bottom))));
					var addr = (ulong) ((ulong) ((ulong) (pc)) + (ulong) ((long) (SignExt<long>((ushort) (((ushort) ((ushort) (offset))) << (int) (0x2)), 16))));
					Label temp_32 = Ilg.DefineLabel(), temp_33 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt])).ShiftRight(imm)))) & ((RuntimeValue<ulong>) (0x1))))) == (0x0))) == 0, temp_33);
					Branch(addr);
					Branch(temp_32);
					Label(temp_33);
					Branch(pc + 4);
					Label(temp_32);
					return true;
				}
				/* TBNZ */
				if((inst & 0x7F000000U) == 0x37000000U) {
					var upper = (inst >> 31) & 0x1U;
					var bottom = (inst >> 19) & 0x1FU;
					var offset = (inst >> 5) & 0x3FFFU;
					var rt = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((upper) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					var imm = (byte) ((((byte) ((byte) ((upper) << (int) (0x5)))) | ((byte) (bottom))));
					var addr = (ulong) ((ulong) ((ulong) (pc)) + (ulong) ((long) (SignExt<long>((ushort) (((ushort) ((ushort) (offset))) << (int) (0x2)), 16))));
					Label temp_34 = Ilg.DefineLabel(), temp_35 = Ilg.DefineLabel();
					BranchIf(((RuntimeValue<byte>) (((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((RuntimeValue<ulong>) ((rt) == 31 ? 0UL : XR[(int) rt])).ShiftRight(imm)))) & ((RuntimeValue<ulong>) (0x1))))) != (0x0))) == 0, temp_35);
					Branch(addr);
					Branch(temp_34);
					Label(temp_35);
					Branch(pc + 4);
					Label(temp_34);
					return true;
				}
				/* UADDLV */
				if((inst & 0xBF3FFC00U) == 0x2E303800U) {
					var Q = (inst >> 30) & 0x1U;
					var size = (inst >> 22) & 0x3U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) ((size) switch { 0x0 => "H", 0x1 => "S", 0x2 => "D", _ => throw new NotImplementedException() });
					var t = (string) (((byte) ((byte) (((byte) (((byte) (Q)) << 0)) | ((byte) (((byte) (size)) << 1))))) switch { 0x0 => "8B", 0x1 => "16B", 0x2 => "4H", 0x3 => "8H", 0x5 => "4S", _ => throw new NotImplementedException() });
					var esize = (long) ((0x8) << (int) (size));
					var count = (long) ((long) ((long) ((Q != 0) ? (0x80) : (0x40))) / (long) (esize));
					switch(size) {
						case 0x0: {
							VHR[(int) (rd)] = (RuntimeValue<ushort>) ((RuntimeValue<ushort>) ((RuntimeValue<uint>) (CallVectorSumUnsigned((RuntimeValue<Vector128<float>>) (VR[(int) (rn)]), esize, count))));
							break;
						}
						case 0x1: {
							VSR[(int) (rd)] = (RuntimeValue<float>) (((RuntimeValue<uint>) (CallVectorSumUnsigned((RuntimeValue<Vector128<float>>) (VR[(int) (rn)]), esize, count))).Bitcast<float>());
							break;
						}
						case 0x2: {
							VDR[(int) (rd)] = (RuntimeValue<double>) (((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) (CallVectorSumUnsigned((RuntimeValue<Vector128<float>>) (VR[(int) (rn)]), esize, count))))).Bitcast<double>());
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* UBFM */
				if((inst & 0x7F800000U) == 0x53000000U) {
					var size = (inst >> 31) & 0x1U;
					var N = (inst >> 22) & 0x1U;
					var immr = (inst >> 16) & 0x3FU;
					var imms = (inst >> 10) & 0x3FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						// Runtime block!
						var src = ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])).Store();
						var wmask = (uint) ((uint) ((ulong) (MakeWMask(N, imms, immr, 0x20, 0x0))));
						var tmask = (uint) ((uint) ((ulong) (MakeTMask(N, imms, immr, 0x20, 0x0))));
						// Runtime block!
						var bot = ((RuntimeValue<uint>) ((((RuntimeValue<uint>) ((RuntimeValue<uint>) (((src).ShiftLeft((RuntimeValue<uint>) (32 - (immr)))) | ((src).ShiftRight((RuntimeValue<uint>) (immr)))))) & ((RuntimeValue<uint>) (wmask))))).Store();
						// Runtime block!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((((RuntimeValue<uint>) (bot)) & ((RuntimeValue<uint>) (tmask)))));
					} else {
						// Runtime else!
						// Runtime block!
						var src = ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])).Store();
						var wmask = (ulong) (MakeWMask(N, imms, immr, 0x40, 0x0));
						var tmask = (ulong) (MakeTMask(N, imms, immr, 0x40, 0x0));
						// Runtime block!
						var bot = ((RuntimeValue<ulong>) ((((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (((src).ShiftLeft((RuntimeValue<uint>) (64 - (immr)))) | ((src).ShiftRight((RuntimeValue<uint>) (immr)))))) & ((RuntimeValue<ulong>) (wmask))))).Store();
						// Runtime block!
						XR[(int) rd] = (RuntimeValue<ulong>) ((((RuntimeValue<ulong>) (bot)) & ((RuntimeValue<ulong>) (tmask))));
					}
					return true;
				}
				/* UCVTF-scalar-integer */
				if((inst & 0x7F3FFC00U) == 0x1E230000U) {
					var size = (inst >> 31) & 0x1U;
					var type = (inst >> 22) & 0x3U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var st = (byte) ((byte) (((byte) (((byte) (type)) << 0)) | ((byte) (((byte) (size)) << 2))));
					var r1 = "";
					var r2 = "";
					switch(st) {
						case 0x3: {
							r1 = "H";
							r2 = "W";
							break;
						}
						case 0x0: {
							r1 = "S";
							r2 = "W";
							break;
						}
						case 0x1: {
							r1 = "D";
							r2 = "W";
							break;
						}
						case 0x7: {
							r1 = "H";
							r2 = "X";
							break;
						}
						case 0x4: {
							r1 = "S";
							r2 = "X";
							break;
						}
						case 0x5: {
							r1 = "D";
							r2 = "X";
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					switch(st) {
						case 0x0: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])));
							break;
						}
						case 0x1: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])));
							break;
						}
						case 0x4: {
							VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])));
							break;
						}
						case 0x5: {
							VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])));
							break;
						}
						default: {
							throw new NotImplementedException();
							break;
						}
					}
					return true;
				}
				/* UCVTF-vector-integer */
				if((inst & 0xFFBFFC00U) == 0x7E21D800U) {
					var size = (inst >> 22) & 0x1U;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("S") : ("D"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						VSR[(int) (rd)] = (RuntimeValue<float>) ((RuntimeValue<float>) ((RuntimeValue<uint>) (((RuntimeValue<float>) (VSR[(int) (rn)])).Bitcast<uint>())));
					} else {
						// Runtime else!
						VDR[(int) (rd)] = (RuntimeValue<double>) ((RuntimeValue<double>) ((RuntimeValue<ulong>) (((RuntimeValue<double>) (VDR[(int) (rn)])).Bitcast<ulong>())));
					}
					return true;
				}
				/* UDIV */
				if((inst & 0x7FE0FC00U) == 0x1AC00800U) {
					var size = (inst >> 31) & 0x1U;
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					var r = (string) (((byte) (((size) == (0x0)) ? 1U : 0U) != 0) ? ("W") : ("X"));
					if(((byte) (((size) == (0x0)) ? 1U : 0U)) != 0) {
						// Runtime if!
						var operand2 = ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (RuntimeValue<uint>) ((RuntimeValue<uint>) (Ternary<byte, uint>((RuntimeValue<byte>) ((RuntimeValue<byte>) ((operand2) == (0x0))), (uint) ((uint) (0x0)), (RuntimeValue<uint>) ((RuntimeValue<uint>) (RuntimeValue<uint>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])) / (RuntimeValue<uint>) (RuntimeValue<uint>) (operand2)))));
					} else {
						// Runtime else!
						var operand2 = ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])).Store();
						// Runtime let!
						XR[(int) rd] = (RuntimeValue<ulong>) (Ternary<byte, ulong>((RuntimeValue<byte>) ((RuntimeValue<byte>) ((operand2) == (0x0))), (ulong) ((ulong) (0x0)), (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])) / (RuntimeValue<ulong>) (RuntimeValue<ulong>) (operand2))));
					}
					return true;
				}
				/* UMADDL */
				if((inst & 0xFFE08000U) == 0x9BA00000U) {
					var rm = (inst >> 16) & 0x1FU;
					var ra = (inst >> 10) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((ra) == 31 ? 0UL : XR[(int) ra])) + (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rn) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rn])))) * (RuntimeValue<ulong>) (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<uint>) ((rm) == 31 ? 0U : (RuntimeValue<uint>) XR[(int) rm])))))));
					return true;
				}
				/* UMULH */
				if((inst & 0xFFE0FC00U) == 0x9BC07C00U) {
					var rm = (inst >> 16) & 0x1FU;
					var rn = (inst >> 5) & 0x1FU;
					var rd = (inst >> 0) & 0x1FU;
					XR[(int) rd] = (RuntimeValue<ulong>) ((RuntimeValue<ulong>) ((RuntimeValue<UInt128>) (((RuntimeValue<UInt128>) ((RuntimeValue<UInt128>) (RuntimeValue<UInt128>) ((RuntimeValue<UInt128>) ((RuntimeValue<UInt128>) ((RuntimeValue<ulong>) ((rn) == 31 ? 0UL : XR[(int) rn])))) * (RuntimeValue<UInt128>) (RuntimeValue<UInt128>) ((RuntimeValue<UInt128>) ((RuntimeValue<UInt128>) ((RuntimeValue<ulong>) ((rm) == 31 ? 0UL : XR[(int) rm])))))).ShiftRight(0x40))));
					return true;
				}

			}
			return false;
		}
	}
}