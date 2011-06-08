\ FORTH Assembler for TI MSP430x2xx Family                    
\ v05.001  mk 20110608 syntaxlayer hinzufŸgen.
\ v04.001  mk 20110527
\ v03.002+ mk 20110504
\ v00.001  mk 20110424 

\ Made with gforth 0-7-0 on MacOSX 10.4.11 PowerPC G4.


\ 
\ 
\ Information: 
\ This simple Assembler is for Texas Instruments MSP430 Microcomputers.
\ See: 
\ MSP430x2xx Family User's Guide 
\ SLAU144HâDecember 2004âRevised April 2011, S. 64, CPU. 
\ © 2004â2011, Texas Instruments Incorporated 
\ 
\ *** Syntax 
\ Adressmode modifying words follow an item.
\ Mnemonic Opcode Words compile the Instruktion.
\ Assembler         Forth Assembler
\ MOV r1,r2         r1 sRn r2 dRn MOV,  
\ AND #0AA55h,TOM   $0AA55 #N TOM ADDR AND,  
\ 
\ 
\ Autor:          Michael Kalus (mk) 


vocabulary msp430assembler   msp430assembler definitions 


HERE
: .. bye ; 
: %% 2 base ! ; 



\ ***  cross compiler memory  

create startMSP $3F00 allot \ 16Kbyte
here constant endMSP 
: clearmsp   startmsp endmsp over - 0 fill ; clearmsp 
variable dpm  0 dpm ! 
: herem  dpm @ ; 

\ cross mem access words.
\ adr == startMSP + adrm 
: c@m ( adrm -- c ) startMSP + c@ ; 
: c!m ( n adrm -- ) startMSP + c! ; 
: c,m ( n -- ) herem c!m   1 dpm +! ; 


true  constant big-endian
false constant little-endian

little-endian
[if]   \ Big endian
:  w@m  { adr -- w }    adr c@m 8 lshift     adr 1+ c@m + ; 
:  w!m  { w adr -- w }  w 8 rshift adr c!m   w adr 1+ c!m ; 
:  w,m  { w -- }        w 8 rshift c,m       w c,m ;        
:  w.m  ( w -- )        hex. ; 
[else] \ Little endian
:  w@m  { adr -- w }    adr 1+ c@m 8 lshift     adr c@m + ;    
:  w!m  { w adr -- w }  w 8 rshift adr 1+ c!m   w adr c!m ;    
:  w,m  { w -- }        w c,m                   w 8 rshift c,m ; 
:  w.m  { w -- }        w $00FF and 8 lshift  w $FF00 and 8 rshift or hex. ; 
[then]




0 value lop \ last opcode address. 
:  [op]  ( -- ) herem to lop ; 
:  op@ ( -- op ) lop w@m ; 
:  op! ( op -- ) lop w!m ; 

0 [if]
\ *** Assembler facilities 

\ The CPU incorporates sixteen 16-bit registers R0 ... R15 
\ R0, R1, R2, and R3 have dedicated functions. 
    &00 constant R0     ' R0 alias PC  \ program counter 
    &01 constant R1     ' R1 alias SP  \ stack ponter 
    &02 constant R2     ' R2 alias SR  \ status register 
                        ' R2 alias CG1 \ Constant Generator Registers CG1 
    &03 constant R3     ' R3 alias CG2 \ Constant Generator Registers CG2 

\ R4 to R15 are working registers for general use.
    &04 constant R4  
    &05 constant R5  
    &06 constant R6  
    &07 constant R7  
    &08 constant R8  
    &09 constant R9  
    &10 constant R10 
    &11 constant R11 
    &12 constant R12 
    &13 constant R13 
    &14 constant R14 
    &15 constant R15 

\ for camelforth test 
' R1 alias RSP 
' R4 alias PSP 
' R5 alias IP 
' R6 alias W 
' R7 alias TOS 
[then]


\ *** Addressmodes

\ Adressmodes patch opcode befor opcode is compiled. 
\ They patch the As or As bits.

 0 value mode \ addressmode mask 
 0 value src  \ source 
 0 value dst  \ destination 
false value srcflag 
false value dstflag 
: reset-mode  0 to mode ; 
: reset-src  false to srcflag ; 
: reset-dst  false to dstflag ; 
: set-src  true to srcflag ; 
: set-dst  true to dstflag ; 

: >sreg ( rn -- ) %1111 and 8 lshift mode or   to mode ; 
: >dreg ( rn -- ) %1111 and          mode or   to mode ; 

: s>16 ( n -- n16 ) \ convert single precission to 16bit 2-complement. 
    dup %1111111111111111 and swap 0< if %1000000000000000 or then ; 

: src-adr-offset ( adr -- 16bit-pc-offset )  herem - 2 - s>16 ; 
: dst-adr-offset ( adr -- 16bit-pc-offset )  herem - 4 - s>16 ; 

\ patch mode bits 
\ set   bit4,5       %0000000000xx0000 or 
\ clear bit4,5       %1111111111xx1111 and 
\ set   bit7         %0000000010000000 or 
\ clear bit7         %1111111101111111 and 

: 00As ( -- )   mode %1111111111001111 and   to mode ; 
: 01As ( -- )   mode %0000000000010000 or    to mode ; 
: 10As ( -- )   mode %0000000000100000 or    to mode ; 
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

\ %10 Indirect register mode @Rn - As bit only. 
\ Rn is used as a pointer to the operand. 
: s@Rn   ( rn -- ) >sreg  reset-src   10As  ; 
:  @Rn   ( rn -- ) >dreg  reset-src   10As  ; 

\ %11 Indirect autoincrement @Rn+ - As bit only. 
\ Rn is used as a pointer to the operand. 
\ Rn is incremented afterwards by 1 for .B instructions 
\ and by 2 for .W instructions. 
: s@Rn+  ( rn -- ) >sreg reset-src   11As  ; 
:  @Rn+  ( rn -- ) >dreg reset-src   11As  ; 

\ %11 = Immediate mode #K - As bit only.
\ (It is called #K instead of #N because #N is used in syntax layer later on.)
\ The word following the instruction contains the immediate constant N. 
\ Indirect autoincrement mode @PC+ is used. 
: s#K   ( k -- ) ( PC) 0 >sreg  to src set-src   11As ; 
:  #K   ( k -- ) ( PC) 0 >dreg  to src set-src   1Ad ; 

\ Constant Generator 
0 [if]
R2 00 Ð Ð Ð Ð Ð Register modeR2 01 (0) Absolute address modeR2 10 00004h +4, bit processingR2 11 00008h +8, bit processingR3 00 00000h 0, word processingR3 01 00001h +1R3 10 00002h +2, bit processingR3 11 0FFFFh 1, word processing
It is one word less if a constant of CG1 or CG2 can be used.
[then]

\ To take advantage of constant gererators CG1 or CG2 use constant words.
\ e.g.:  #2 5 dRn mov,   ( instead of 2 s#N 5 dRn mov,  )  

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

: op,    ( op -- ) [op] w,m reset-mode ; 
: ?src,  ( -- ) srcflag if src w,m then reset-src ; 
: ?dst,  ( -- ) dstflag if dst w,m then reset-dst ; 

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

: formatI ( op -- )  mode or op, ?src, ?dst, ; 
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

: formatII ( op -- )  mode or op, ?src,  ; 
: mneII: ( opcode "name" -- ) create ,   does>  ( adr -- op ) @ formatII  ;  

$1000 .W mneII: RRC.W,   ( src -- )   \ Rotate right through C [ * * * * ] 
$1080 .W mneII: SWPB,    ( src -- )   \ Swap bytes [ - - - - ] 
$1100 .W mneII: RRA.W,   ( src -- )   \ Rotate right arithmetically [ 0 * * * ] 
$1180 .W mneII: SXT,     ( src -- )   \ Extend sign [ 0 * * * ] 
$1200 .W mneII: PUSH.W,  ( src -- )   \ Push source onto stack:  SP - 2 --> SP, src --> @SP [ - - - - ] 
$1280 .W mneII: CALL,    ( src -- )   \ Call destination:  PC+2 --> stack, dst --> PC [ - - - - ] 
$1300 .W mneII: RETI,    ( -- )       \ Return from subroutine:  @SP --> PC, SP + 2 --> SP [ - - - - ] 

$1040 .B mneII: RRC.B,   ( src -- )   \ Rotate right through C [ * * * * ] 
$1140 .B mneII: RRA.B,   ( src -- )   \ Rotate right arithmetically [ 0 * * * ] 
$1240 .B mneII: PUSH.B,  ( src -- )   \ Push source onto stack:  SP - 2 --> SP, src --> @SP [ - - - - ] 

 ' RRC.W,  alias RRC, 
 ' RRA.W,  alias RRA, 
 ' PUSH.W, alias PUSH,
 ' RETI,   alias RET, 



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

: offsetIII ( label -- 10bit-pc-offset )  here - 2/ 1- s>10 ; 
: formatIII ( label op -- )  mode or  swap offsetIII or  op, ; 
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


false [if]
\ Emulated instruction set

\ ADC Emulation : ADDC #0,dst
: ADC,   (1)  \ Add C to destination:   dst + C --> dst [ * * * * ] 
    0 s#N  ADDC,  ; 
: ADC.B, (1)  \ Add C to destination:   dst + C --> dst [ * * * * ] 
    0 s#N  ADDC.B,  ; 
 ' ADC, alias ADC.W, 

\ BR Emulation : MOV src,PC 
: BR,    (1)  \ Branch to destination:   src --> PC [ - - - - ] 
    PC dRn MOV, ; 
 ' BR, alias BRANCH, 

\ CLR Emulation: MOV #0,dst: CLR,   (1)  \ Clear destination:  0 --> dst [ - - - - ] 
    0 s#N  MOV,  ; 
: CLR.B, (1)  \ Clear destination:  0 --> dst [ - - - - ] 
    0 s#N  MOV.B,  ; 
 ' CLR, alias CLR.W, 

\ CLRC Emulation: BIC #1,SR 
: CLRC, (1)   \ Clear C   0 --> C [ - - - 0 ] 
    1 s#N  SR dRn  BIC, ; 

\ CLRN Emulation: BIC #4,SR 
: CLRN, (1)   \ Clear N   0 --> N[ - 0 - - ] 
    $4 s#N  SR dRn  BIC, ; 

\ CLRZ Emulation: BIC #2,SR 
: CLRZ, (1)   \ Clear Z   0 --> Z[ - - 0 - ] 
    $2 s#N  SR dRn  BIC, ; 

\ DADC Emulation: DADD #0,dst
: DADC,   (1) \ Add C decimally to destination:   dst + C --> dst   (decimally) [ * * * * ] 
    0  s#N  DADD,  ; 
: DADC.B, (1) \ Add C decimally to destination:   dst + C --> dst   (decimally) [ * * * * ] 
    0 s#N  DADD.B,  ; 
 ' DADC, alias DADC.W, 

\ DEC Emulation: SUB #1,dst 
: DEC,   (1)  \ Decrement destination:  dst   - 1 --> dst [ * * * * ] 
    1 s#N SUB,  ; 
: DEC.B, (1)  \ Decrement destination:  dst   - 1 --> dst [ * * * * ] 
    1 s#N  SUB.B,  ; 
 ' DEC, alias DEC.W, 

\ DECD Emulation: SUB #2,dst 
: DECD,   (1) \ Double-decrement destination:  dst   - 2 --> dst [ * * * * ] 
    &2 s#N  SUB,  ; 
: DECD.B, (1) \ Double-decrement destination:  dst   - 2 --> dst [ * * * * ] 
    &2 s#N  SUB.B,  ; 
 ' DECD, alias DECD.W, 

\ DINT Emulation: BIC #8,SR 
: DINT,   (1)  \  Disable interrupts:  0 --> GIE [ - - - - ] 
    &8 s#N  SR dRn BIC, ; 

\ EINT Emulation: BIS #8,SR 
: EINT,   (1)  \  Enable interrupts:  1 --> GIE[ - - - - ] 
    &8 s#N  SR dRn BIS,  ; 

\ INC Emulation: ADD #1,dst 
: INC,    (1)  \ Increment destination:  dst +1 --> dst [ * * * * ] 
    1 s#N  ADD,  ; 
: INC.B,  (1)  \ Increment destination:  dst +1 --> dst [ * * * * ] 
    1 s#N  ADD.B,  ; 
 ' INC, alias INC.W, 

\ INCD Emulation: ADD #2,dst 
: INCD,   (1) \ Double-increment destination:  dst+2 --> dst [ * * * * ] 
    2 s#N  ADD,  ; 
: INCD.B, (1) \ Double-increment destination:  dst+2 --> dst [ * * * * ] 
    2 s#N  ADD.B, ; 
 ' INCD, alias INCD.W, 

\ INV Emulation: XOR #0FFFFh,dst 
: INV,   (1)  \ Invert destination:  .not.dst --> dst [ * * * * ] 
    $0FFFF s#N XOR,  ; 
: INV.B, (1)  \ Invert destination:  .not.dst --> dst [ * * * * ] 
    $0FFFF s#N XOR.B,  ; 
 ' INV, alias INV.W, 

\ NOP Emulation: MOV #0, R3 
: NOP,    (2)  \  No operation[ - - - - ] 
     0 s#N R3 dRn  MOV,  ; 

\ POP Emulation: MOV @SP+,dst 
: POP,   (2)  \ Pop item from stack to destination:   @SP --> dst, SP+2 --> SP [ - - - - ] 
     SP s@Rn+  MOV,  ; 
: POP.B, (2)  \ Pop item from stack to destination:   @SP --> dst, SP+2 --> SP [ - - - - ] 
     SP s@Rn+  MOV,  ; 
 ' POP, alias POP.W, 

\ RLA Emulation: ADD dst,dst 
: RLA,   (2)  \ Rotate left arithmetically [ * * * * ] 
    ADD,  ; 
: RLA.B, (2)  \ Rotate left arithmetically [ * * * * ] 
    ADD.B,  ; 
 ' RLA, alias RLA.W, 

\ RLC Emulation: ADDC dst,dst 
: RLC,   (2)  \ Rotate left through C [ * * * * ] 
    ADDC,  ; 
: RLC.B, (2)  \ Rotate left through C [ * * * * ] 
    ADDC.B,  ; 
 ' RLC, alias RLC.W, 

\ SBC Emulation: SUBC #0,dst 
: SBC,   (2)  \ Subtract not(C) from destination:  dst + 0FFFFh + C --> dst [ * * * * ] 
    0 s#N  SUBC,  ; 
: SBC.B, (2)  \ Subtract not(C) from destination:  dst + 0FFFFh + C --> dst [ * * * * ] 
    0 s#N  SUBC,  ; 
 ' SBC, alias SBC.W, 

\ SETC Emulation: BIS #1,SR 
: SETC, (2)  ( -- )   \ Set C 1 --> C [ - - - 1 ] 
    1 s#N  SR dRn  BIS,  ; 

\ SETN Emulation: BIS #4,SR 
: SETN, (2)  ( -- )   \ Set N 1 --> N [ - 1 - - ] 
    &4 s#N  SR dRn  BIS,  ; 

\ SETZ Emulation: BIS #2,SR 
: SETZ, (2)  ( -- )   \ Set Z 1 --> C [ - - 1 - ] 
    &2 s#N  SR dRn  BIS,  ; 

\ TST Emulation: CMP #0,dst 
: TST,   (2)  \ Test destination:  dst + 0FFFFh + 1 [ 0 * * 1 ] 
    0 s#N  CMP,  ; 
: TST.B, (2)  \ Test destination:  dst + 0FFFFh + 1 [ 0 * * 1 ] 
    0  s#N  CMP.B,  ; 
 ' TST, alias TST.W, 

[then]

\ *** more assembler facilities 

\ list a compiled instruction. 
: .lst ( -- ) 
    lop hex. 
    herem lop - 0  ?do lop i + w@m w.m 2 +loop 
    ."   " source type .s cr ; 

0 value startadr 
0 value endadr 
: .startcode ( -- ) 
    herem to startadr ." herem=" herem hex. cr 
    [op]   reset-src   reset-dst   0 to mode ; 
: .endcode   ( -- ) 
    herem to endadr ." herem=" herem hex. cr ; 
: .dumpcode  ( -- ) 
    cr ." dump code area" startadr startMSP +  endadr startadr -  dump ; 
: .dumpcode-all ( -- ) 
    cr ." dump code area" startMSP  endadr   dump ; 


\ Handling of labels adopted from Lubos Pekny code. 
\   AvrAsm - assembler Atmega chips, Lubos Pekny, www.forth.cz
\   Library for amforth 3.0 mFC 1.0
\   V.1.1v, 29.01.2009.

  \ Address for jump back. Example: 
: <adr     ( adr -- k )
    herem 1+ - ;

&10 constant maxlabels 
variable (lbl) maxlabels cells allot  \ RAM for 10 labels

' herem alias lbl> 

  \ store addr to vector of labels
: >lbl ( addr c -- )  \ index c=0..9
    cell * (lbl) + ! ; 

  \ read addr from vector of labels
: <lbl ( c -- addr )  \ index c=0..9
    cell * (lbl) + @ ;

: lbl? ( -- ) \ list labels
  ." list labels" cr maxlabels 0 do i . i <lbl . cr loop ; 

: clrlbl ( -- ) \ clear label tabel
  maxlabels 0 do 0 i >lbl loop ; 





 HERE  SWAP -  .( \ Length of MSP430-Assembler: ) . .( Bytes ) CR

hex

\ .( - words so far: ) words cr 
\ .( - end of wordlist) cr cr 
\ finis 
