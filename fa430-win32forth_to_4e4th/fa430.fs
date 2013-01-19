\ FORTH Assembler for TI MSP430x2xx Family

\ Simple Assembler for Texas Instruments MSP430 Microcomputers
\ made with gforth 0-7-0 on MacOSX 10.4.11 PowerPC G4.
\ Autor:          Michael Kalus (mk)

\ Adopted to win32forth on Windows XP for 4e4th-IDE needs.  jan 19 2013 mk

\ See:
\ MSP430x2xx Family User's Guide
\ SLAU144HâDecember 2004âRevised April 2011, S. 64, CPU.
\ © 2004â2011, Texas Instruments Incorporated



\ Basic Syntax

\ Put source item on data stack, followed by source adressmode modifier word.
\ Put destination item on data stack, followed by destination adressmode modifier word.
\ The mnemonic opcode word finaly compiles the Instruktion, using mode, source and destination information.

\ Assembler         Forth Assembler
\ MOV r1,r2         r1 sRn r2 dRn MOV,
\ AND #0AA55h,TOM   $0AA55 s#N TOM dADDR AND,



\ Syntax Layer
\ This part provides a more comfortable notation.


\ Emulation Layer
\ Provides common combinations of instructions and macros.

\ ********************************************************


\ : --- bye ;
\ : %% 2 base ! ;

\ HERE .( here) .s



vocabulary msp430assembler   msp430assembler definitions

true constant msp430

: HEX.  ( n -- ) base @ r>   hex u.   r> base ! ;


\ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \

msp430 [if]

\ ***  target cross memory  ( adr == tstart + tadr )

create tstart $3F00 allot \ 16Kbyte
here constant tend


variable tdp  0 tdp !
: there  tdp @ ;

there value loca  \ last opcode address. ( .lst .chk   gesetzt mit [op] )
 defer [op]
: setloca  ( -- )    there to loca ; ( startcode, op, )

: tclear       tstart tend over - 0 fill   tstart tdp !   setloca ;
tclear


: X_c@ ( adr -- c ) c@ ;
: X_c! ( n adr -- ) c! ;
: X_c, ( n -- ) there X_c!   1 tdp +! ;

\ Big endian 16Bit
\ :  X_@  { adr -- w }    adr X_c@ 8 lshift     adr 1+ X_c@ + ;
\ :  X_!  { w adr -- w }  w 8 rshift adr X_c!   w adr 1+ X_c! ;
\ :  X_,  { w -- }        w 8 rshift X_c,       w X_c, ;
\ :  X_.  ( w -- )        hex. ;

\ Little endian 16Bit
:  X_@  { adr -- w }    adr 1+ X_c@ 8 lshift     adr X_c@ + ;
:  X_!  { w adr -- w }  w 8 rshift adr 1+ X_c!   w adr X_c! ;
:  X_,  { w -- }        w X_c,                   w 8 rshift X_c, ;
:  X_.  { w -- }        w $00FF and 8 lshift  w $FF00 and 8 rshift or hex. ;

: MSP430CODE  ( <name> -- )
  tclear   bl word count  cr cr ." MSP430CODE "  2dup upper type cr ;

: ?CR ( i -- ) $0F and 0= if cr then ;
: END-CODE    ( --- )
  base @ >r  hex
  there  tstart  ?do   i x_@ u.   ." i, "  i ?CR  2 +LOOP
  cr ." END-CODE "
  r> base !  cr cr ;



[then]

\ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \



\ *** collect data for cross compiling

 0 value mode \ addressmode mask
 0 value src  \ source
 0 value dst  \ destination

false value sflag
false value dflag

: reset-mode   0 to mode ;
: reset-src    false to sflag ;
: reset-dst    false to dflag ;

: set-src      true to sflag ;
: set-dst      true to dflag ;

: src,  ( -- ) sflag if src X_, then reset-src ;
: dst,  ( -- ) dflag if dst X_, then reset-dst ;

: op@   ( -- op ) loca X_@ ; ( wird gar nicht benutzt)
: op!   ( op -- ) loca X_! ; ( wird gar nicht benutzt)
: op,   ( op -- ) [op]  X_, reset-mode ; ( formI,II,III, reti)

msp430 [if]  ' setloca is [op]  [else]  ' nop is [op]  [then]


\ *** Addressing modes

\ Adressing modes patch opcode befor opcode is compiled.
\ They patch the As or Ad bits and source and/or register bits.

: >sreg ( rn -- ) %1111 and 8 lshift mode or   to mode ;
: >dreg ( rn -- ) %1111 and          mode or   to mode ;

: s>16 ( n -- n16 ) \ convert single precission to 16bit 2-complement.
    dup %1111111111111111 and swap 0< if %1000000000000000 or then ;

: src-adr-offset ( adr -- 16bit-pc-offset )  there - 2 - s>16 ;
: dst-adr-offset ( adr -- 16bit-pc-offset )  there - 4 - s>16 ;

\ patch mode bits
\ set   bit4,5       %0000000000xx0000 or
\ clear bit4,5       %1111111111xx1111 and
\ set   bit7         %0000000010000000 or
\ clear bit7         %1111111101111111 and

: 00As ( -- )   mode %1111111111001111 and   to mode ;
: 01As ( -- )   00As mode %0000000000010000 or    to mode ;
: 10As ( -- )   00As mode %0000000000100000 or    to mode ;
: 11As ( -- )   mode %0000000000110000 or    to mode ;
:  0Ad ( -- )   mode %1111111101111111 and   to mode ;
:  1Ad ( -- )   mode %0000000010000000 or    to mode ;

\ -- Seven addressing modes for the source operand.
\ %0 = register mode Rn - As and Ad bits.
: sRn   ( rn -- )  >sreg  reset-src    00As ;
:  Rn   ( rn -- )  >dreg  reset-src   00As ;

\ %1 = indexed mode X(Rn) - As and Ad bits.
\ (Rn + X) points to the operand.
\ X is stored in the next word.
: sX(Rn)  ( x rn -- )  >sreg  to src set-src  01As ;
:  X(Rn)  ( x rn -- )  >dreg  to src set-src  01As ;

\ %1 Symbolic mode ADDR - As and Ad bits.
\ (PC + X) points to the operand.
\ X is stored in the next word. ( x == offset 16bit two-complement)
\ Indexed mode X(PC) is used.
: sADDR   ( adr -- ) ( PC) 0 >sreg  src-adr-offset to src set-src   01As ;
:  ADDR   ( adr -- ) ( PC) 0 >dreg  src-adr-offset to src set-src   01As ;

\ %1 Absolute mode &ADDR - As and Ad bits.
\ The word following the instruction contains the absolute address.
\ Indexed mode X(SR) is used.
: s&ADDR   ( adr -- ) ( SR) 2 >sreg  to src set-src  01As  ;
:  &ADDR   ( adr -- ) ( SR) 2 >dreg  to src set-src  01As  ;

\ %10 Indirect register mode @Rn - As bits only.
\ Rn is used as a pointer to the operand.
: s@Rn   ( rn -- ) >sreg  reset-src   10As  ;
:  @Rn   ( rn -- ) >dreg  reset-src   10As  ;

\ %11 Indirect register autoincrement @Rn+ - As bits only.
\ Rn is used as a pointer to the operand.
\ Rn is incremented afterwards by 1 for .B instructions
\ and by 2 for .W instructions.
: s@Rn+  ( rn -- ) >sreg reset-src   11As  ;
:  @Rn+  ( rn -- ) >dreg reset-src   11As  ;

\ %11 = Immediate mode #K - As bits only. See constant generator too
\ (It is called #K instead of #N because #N is used in syntax layer later on.)
\ The word following the instruction contains the immediate constant N.
\ Indirect autoincrement mode @PC+ is used.
: s#K   ( k -- ) ( PC) 0 >sreg  to src set-src   11As ;
:  #K   ( k -- ) ( PC) 0 >dreg  to src set-src   11As ;

0 [if] \ Constant Generator Comment
  One word less is compiled if CG1 or CG2 can be used as constant.
  Table:
  CG1 As
  R2  00 Ð Ð Ð Ð Ð Register mode
  R2  01 (0) Absolute address mode
  R2  10 00004h +4, bit processing
  R2  11 00008h +8, bit processing
  CG2 As
  R3  00 00000h 0, word processing
  R3  01 00001h +1
  R3  10 00002h +2, bit processing
  R3  11 0FFFFh 1, word processing

  To take advantage of constant gererators CG1 or CG2 use constant words.
  e.g.:  2# 5 dRn mov,   ( instead of 2 s#N 5 dRn mov,  )
[then]

: 0#    ( -- ) 3 >sreg  reset-src  00As ;
: 1#    ( -- ) 3 >sreg  reset-src  01As ;
: 2#    ( -- ) 3 >sreg  reset-src  10As ;
: 4#    ( -- ) 2 >sreg  reset-src  10As ;
: 8#    ( -- ) 2 >sreg  reset-src  11As ;
: ffff# ( -- ) 3 >sreg  reset-src  11As ;


\ -- Four addressing modes for the destination operand
\ %0 = register mode Rn - As and Ad bits.
: dRn   ( rn -- ) >dreg  reset-dst   0Ad  ;

\ %1 = indexed mode X(Rn) - As and Ad bits.
\ (Rn + X) points to the operand.
\ X is stored in the next word.
: dX(Rn)  ( x rn -- ) >dreg to dst set-dst  1Ad  ;

\ %1 Symbolic mode ADDR - As and Ad bits.
\ (PC + X) points to the operand.
\ X is stored in the next word.
\ Indexed mode X(PC) is used.
: dADDR  ( adr -- ) ( PC) 0 >dreg dst-adr-offset to dst set-dst   1Ad  ;

\ %1 Absolute mode &ADDR - As and Ad bits.
\ The word following the instruction contains the absolute address.
\ Indexed mode X(SR) is used.
: d&ADDR  ( adr -- ) ( SR) 2 >dreg to dst set-dst   1Ad  ;



\ *** Table 3-17. MSP430 Instruction Set
\ Core instructions come first.
\ Emulatet instructions follow below.
\ Alphabetic order.

: (1) ; immediate  \ comment: is emulated instruction.
: (2) ; immediate  \ comment: is emulated instruction.

\   statusbits: [ V N Z C ]
\   * The status bit is affected
\   Ð The status bit is not affected
\   0 The status bit is cleared
\   1 The status bit is set

: .W ( op -- op.W ) %1111111110111111 and ;
: .B ( op -- op.B ) %0000000001000000 or  ;

\ Core Instruction Set

\ Double-Operand (Format I) Instructions
\ bits: | 15 14 13 12 | 11 10 9 8 |  7 |  6  | 5 4 | 3 2 1 0 |
\       |   Op-code   |   S-Reg   | Ad | B/W | As  | D-Reg   |

: %16u.r  $FFFF and &16 base @ >r  2 base ! u.r r> base !  ;

: checkI ( opcode -- )
  cr ." formatI is: "
  cr ." CCCC=Op-Code, SSSS=S-reg, d=Ad w=B/W, ss=As, RRRR=D/S-Reg."
  cr ." FEDCBA9876543210"
  cr ." CCCCSSSSdwssRRRR"
  cr dup %16u.r space hex.
  cr ;

\ Double-Operand syntax
\ Assembler example: ADD #10,R5
\ Forth: 10 s#N r5 dRn RRC,
\ src 10 is immediate mode, dst R5 is Register mode

: formatI ( op -- )  mode or op, src, dst, ;
: mneI: ( opcode "name" -- ) create ,   does>  ( adr -- op ) @ formatI  ;

$4000 .W mneI: MOV.W,
$5000 .W mneI: ADD.W,
$6000 .W mneI: ADDC.W,
$7000 .W mneI: SUBC.W,
$8000 .W mneI: SUB.W,
$9000 .W mneI: CMP.W,
$A000 .W mneI: DADD.W,
$B000 .W mneI: BIT.W,
$C000 .W mneI: BIC.W,
$D000 .W mneI: BIS.W,
$E000 .W mneI: XOR.W,
$F000 .W mneI: AND.W,

$4000 .B mneI: MOV.B,
$5000 .B mneI: ADD.B,
$6000 .B mneI: ADDC.B,
$7000 .B mneI: SUBC.B,
$8000 .B mneI: SUB.B,
$9000 .B mneI: CMP.B,
$A000 .B mneI: DADD.B,
$B000 .B mneI: BIT.B,
$C000 .B mneI: BIC.B,
$D000 .B mneI: BIS.B,
$E000 .B mneI: XOR.B,
$F000 .B mneI: AND.B,

 ' MOV.W,  alias MOV,
 ' ADD.W,  alias ADD,
 ' ADDC.W, alias ADDC,
 ' sUBC.W, alias SUBC,
 ' SUB.W,  alias SUB,
 ' CMP.W,  alias CMP,
 ' DADD.W, alias DADD,
 ' BIT.W,  alias BIT,
 ' BIC.W,  alias BIC,
 ' BIS.W,  alias BIS,
 ' XOR.W,  alias XOR,
 ' AND.W,  alias AND,


\ Single-Operand (Format II) Instructions
\ BIts: | 15 14 13 12 11 10 9 8 7 | 6 | 5 4 | 3 2 1 0
\       |         Op-code          B/W   As   D/S-Reg

: checkII ( opcode -- )
  cr ." formatII is:"
  cr ." CCCCCCCCC=Op-Code, w=B/W, ss=As, RRRR=D/S-Reg."
  cr ." FEDCBA9876543210"
  cr ." CCCCCCCCCwssRRRR"
  cr dup %16u.r space hex.
  cr ;

\ Single-Operand syntax
\ Assembler example: RRC R5  ; Register mode
\ Forth: r5 rn RRC,  \ r5 is a constant ( Rn may NOT be absent).

: formatII ( op -- )  mode or op, src,  ;
: mneII: ( opcode "name" -- ) create ,   does>  ( adr -- op ) @ formatII  ;

$1000 .W mneII: RRC.W,   ( src -- )   \ Rotate right through C [ * * * * ]
$1080 .W mneII: SWPB,    ( src -- )   \ Swap bytes [ - - - - ]
$1100 .W mneII: RRA.W,   ( src -- )   \ Rotate right arithmetically [ 0 * * * ]
$1180 .W mneII: SXT,     ( src -- )   \ Extend sign [ 0 * * * ]
$1200 .W mneII: PUSH.W,  ( src -- )   \ Push source onto stack:  SP - 2 --> SP, src --> @SP [ - - - - ]
$1280 .W mneII: CALL,    ( src -- )   \ Call destination:  PC+2 --> stack, dst --> PC [ - - - - ]

$1040 .B mneII: RRC.B,   ( src -- )   \ Rotate right through C [ * * * * ]
$1140 .B mneII: RRA.B,   ( src -- )   \ Rotate right arithmetically [ 0 * * * ]
$1240 .B mneII: PUSH.B,  ( src -- )   \ Push source onto stack:  SP - 2 --> SP, src --> @SP [ - - - - ]

 ' RRC.W,  alias RRC,
 ' RRA.W,  alias RRA,
 ' PUSH.W, alias PUSH,

\ RETI is listed as a formatII instruction in User Guide,
\ but does not take any item from stack. So dont use mneII to create it.
: RETI,    ( -- )  $1300 op, ;          \ Return from subroutine:  @SP --> PC, SP + 2 --> SP [ - - - - ]



\ Jump (Format-III) Instructions
\ Conditional-jump instruction format:
\ Bits: | 15 14 13 | 12 11 10 | 9 8 7 6 5 4 3 2 1 0 |
\       |  Op-code |    C     |   10-Bit PC Offset  |

: checkIII ( opcode -- )
  cr ." formatIII is:"
  cr ." CCCCCC=Op-Code, ooooooooo=offset."
  cr ." FEDCBA9876543210"
  cr ." CCCCCCoooooooooo"
  cr dup %16u.r space hex.
  cr ;

\ offset = number of words, i.e. bytes * 2 (10bit 2-complement).
\ Jump syntax
\ Assembler example: JMP label
\ Forth: label JMP,

: s>10 ( n -- n10 ) \ convert single precission to 10bit 2-complement.
    dup %1111111111 and swap 0< if %1000000000 or then ;

: offsetIIIforward ( adr -- ) there swap - 2/ 1- s>10 ;
: offsetIIIback ( adr -- 10bit-pc-offset )  there - 2/ 1- s>10 ;
: formatIII ( adr op -- )  mode or  swap offsetIIIback or  op, ;
: mneIII: ( opcode "name" -- ) create ,   does>  ( adr -- op ) @ formatIII  ;

$2000 mneIII: JNE, ( adr -- )   \ Jump if not equal/Jump if Z not set [ - - - - ]
$2400 mneIII: JEQ, ( adr -- )   \ Jump if equal/Jump if Z set [ - - - - ]
$2800 mneIII: JNC, ( adr -- )   \ Jump if C not set/Jump if lower [ - - - - ]
$2C00 mneIII: JC,  ( adr -- )   \ Jump if C set/Jump if higher or same [ - - - - ]
$3000 mneIII: JN,  ( adr -- )   \ Jump if N set [ - - - - ]
$3400 mneIII: JGE, ( adr -- )   \ Jump if greater or equal[ - - - - ]
$3800 mneIII: JL,  ( adr -- )   \ Jump if less[ - - - - ]
$3C00 mneIII: JMP, ( adr -- )   \ Jump PC + 2 * offset --> PC [ - - - - ]

  ' JNE, alias JNZ,
  ' JEQ, alias JZ,
  ' JNC, alias JLO,
  ' JC,  alias JHS,


\ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \ \

\ -- label tabel
&10 constant maxlabels
variable (lbl) maxlabels cells allot
: >label    ( n -- )  there swap cell * (lbl) + ! ;
: label>    ( n -- addr )  cell * (lbl) + @ ;

: L0: 0 >label ;
: L1: 1 >label ;
: L2: 2 >label ;
: L3: 3 >label ;
: L4: 4 >label ;
: L5: 5 >label ;
: L6: 6 >label ;
: L7: 7 >label ;
: L8: 8 >label ;
: L9: 9 >label ;

: L0  0 label> ;
: L1  1 label> ;
: L2  2 label> ;
: L3  3 label> ;
: L4  4 label> ;
: L5  5 label> ;
: L6  6 label> ;
: L7  7 label> ;
: L8  8 label> ;
: L9  9 label> ;

: solve-forward  ( adr -- ) >r  r@ offsetIIIforward  r@ X_@  or  r> X_! ;
' solve-forward alias >>>
\ forward jump:   L0: 000 Jxx  ...  L0 >>>  ...
\ backward jump:  L0:  ...  L0 Jxx  ...

: .labels   ( -- )   ." labels" cr maxlabels 0 do i . i label> . cr loop ;
: clrlabels ( -- )   maxlabels 0 do i >label loop ;


false [if] ( verification process )

variable nops   variable errnops
: (.lst) ( adr n  -- adr n ) \ list last compiled instruction.
    cr source type 3 spaces loca hex.
    there loca - 0  ?do loca i + X_@ X_. 2 +loop  ;
: .lst   ( adr n  -- )  2drop (.lst) .s ;
: $op  ( adr n -- )  drop &14 + 4 evaluate  1 nops +! ;
: X_w@be  { adr -- w }  adr X_c@ 8 lshift  adr 1+ X_c@ + ; \ big endian.
: ops? ( adr n -- ) $op loca X_w@be = ;
: .chk ( adr n -- )  \ check last compiled instruktion.
    (.lst) ops? if .s ." Vop " else .s ." <--- Nop " 1 errnops +! then ;

0 value startadr   0 value endadr
: .startcode ( -- )
    there to startadr ." there=" there hex. cr
    [op]   reset-src   reset-dst   0 to mode ;
: .endcode   ( -- )
    there to endadr ." there=" there hex. cr ;
: .dumpcode  ( -- )
    cr ." dump code area" startadr tstart +  endadr startadr -  dump ;
: .dumpcode-all ( -- )
    cr ." dump code area" tstart  endadr   dump ;

: verification ( -- ) cr .startcode  clrlabels  0 nops ! ;
: .result ( -- )
   cr    nops @ . ." opcodes & addressing modes tested."
   cr errnops @ . ." non matching opcodes." ;

[then]



include syntaxlayer.fs ( secondary syntax layer to ease coding)
\ cr .( .s) .s
include emuset.fs ( often used phrases)
\ cr .( .s) .s



\ HERE  SWAP -  .( \ Length of MSP430-Assembler: ) . .( Bytes ) CR

\ hex

\ .( -- words so far: ) words cr  .( -- end of wordlist) cr cr
\ finis
