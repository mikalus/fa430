\ Syntax Layer



\ ****** Make a smarter Notation for register adressing modes 

\   MSP430x2xx Family User' s Guide, Page 50, 3.3 Addressing Modes 
\   The syntax description is given in Table 3-3, "Source/Destination Operand Addressing Modes" 

\   The modes are: 
\     Register mode           Rn      Register contents are operand 
\     Indexed mode            X(Rn)   (Rn + X) points to the operand. X is stored in the next word. 
\     Symbolic mode           ADDR    (PC + X) points to the operand. X is stored in the next word. 
                                      Indexed mode X(PC) is used. 
\     Absolute mode           &ADDR   The word following the instruction contains the absolute address. 
                                      Indexed mode X(SR) is used. 
\     Indirect register mode  @Rn     Rn is used as a pointer to the operand. 
\     Indirect autoincrement  @Rn+    Rn is used as a pointer to the operand. 
                                      Rn is incremented afterwards by 1 for .B instructions and by 2 for .W instructions. 
\     Immediate mode          #N      The word following the instruction contains the immediate constant N. 
                                      Indirect autoincrement mode @PC+ is used. 


\   Now lets turn this into a notation.

    ASSEMBLER  FORTH
1.  Rn         rn         ( rn -- )   \ register# is on stack, mode# is set to 1 
2.  x(Rn)      x (rn)     ( x rn -- ) \ x and register# are on stack, mode# is set to 2 
3.  ADDR       a          ( a -- )    \ adr is on stack; default mode# = 3 
4.  &ADDR      a &a       ( a -- )    \ adr is on stack, mode# is set to 4 
5.  @Rn        @rn        ( rn -- )   \ register# is on stack, mode# is set to 5 
6.  @Rn+       @rn+       ( rn -- )   \ register# is on stack, mode# is set to 6 
7.  #n         n #n       ( n -- )    \ constant is on stack, mode# is set to 7 
\ All seven are aplicable to source, 1..4 to destination only.



\ *** Here we go:  Definig 7 modes. 

$33 value mode#
: reset-mode#  $33 to mode# ;

\ shift in modes: upper nible=src, lower nibble= dest
: accumulate-mode  ( n -- ) mode# 4 lshift +  to mode# ;

\ Seven addressing modes.
: m1 ( -- )       1 accumulate-mode ; \ Rn
: m2 ( -- )       2 accumulate-mode ; \ x(Rn)
: m3 ( -- )       3 accumulate-mode ; \ ADDR
: m4 ( -- )       4 accumulate-mode ; \ &ADDR
: m5 ( -- )       5 accumulate-mode ; \ @Rn
: m6 ( -- )       6 accumulate-mode ; \ @Rn+
: m7 ( -- )       7 accumulate-mode ; \ #N
\ All seven are aplicable to source, 1..4 to destination only.



\ *** Now we do register names, knowing their mode. 

\ The CPU incorporates sixteen 16-bit registers R0 ... R15
\ R0, R1, R2, and R3 have dedicated functions.
\ R4 to R15 are general pupose registers.
\ Rn ( -- n ) ( set mode value too)



\ 1.   Register mode           Rn       Make a name for each register. 
\ Function: Put register# on stack, set mode# to 1. 

: R0 0 m1 ;     ' R0 alias PC  \ program counter
: R1 1 m1 ;     ' R1 alias SP  \ stack pointer
: R2 2 m1 ;     ' R2 alias SR  \ status register
                ' R2 alias CG1 \ Constant Generator Registers CG1
: R3 3 m1 ;     ' R3 alias CG2 \ Constant Generator Registers CG2

: R4 4 m1 ;
: R5 5 m1 ;
: R6 6 m1 ;
: R7 7 m1 ;
: R8 8 m1 ;
: R9 9 m1 ;
: R10 10 m1 ;
: R11 11 m1 ;
: R12 12 m1 ;
: R13 13 m1 ;
: R14 14 m1 ;
: R15 15 m1 ;

  \ alias for MSP430
' R1 alias RSP \ retur stack pointer
' R4 alias PSP \ parameter stack pointer
' R5 alias IP  \ instruction pointer
' R6 alias W   \ working register
' R7 alias TOS \ top of (parameter) stack



\ 2.   Indexed mode        x (Rn)     Make a name for each register. 
\ Function: x is on stack, put register# on stack, set mode# to 2. 

: (R0) 0 m2 ;     ' (R0) alias (PC)  \ program counter
: (R1) 1 m2 ;     ' (R1) alias (SP)  \ stack ponter
: (R2) 2 m2 ;     ' (R2) alias (SR)  \ status register
: (R3) 3 m2 ;
: (R4) 4 m2 ;
: (R5) 5 m2 ;
: (R6) 6 m2 ;
: (R7) 7 m2 ;
: (R8) 8 m2 ;
: (R9) 9 m2 ;
: (R10) 10 m2 ;
: (R11) 11 m2 ;
: (R12) 12 m2 ;
: (R13) 13 m2 ;
: (R14) 14 m2 ;
: (R15) 15 m2 ;

  \ alias for MSP430
' (R1) alias (RSP) \ retur stack pointer
' (R4) alias (PSP) \ parameter stack pointer
' (R5) alias (IP)  \ instruction pointer
' (R6) alias (W)   \ working register
' (R7) alias (TOS) \ top of (parameter) stack



\ 3.   Symbolic mode            a         This mode is default, no modifier. 
\ Function: Address a is on stack, mode# is 3. 



\ 4.   Absolute mode            a &a      Make a mode modifier. 
\ Function: Address a is on stack and stays unchanged. &a sets mode# to 4. 
  ' m4 alias &A ( addr -- addr )



\ 5.   Indirect register mode   @rn       Make a name for each register. 
\ Function: Put register# on stack, set mode# to 5. 

: @R0 0 m5 ;     ' @R0 alias @PC  \ program counter
: @R1 1 m5 ;     ' @R1 alias @SP  \ stack ponter
: @R2 2 m5 ;     ' @R2 alias @SR  \ status register
: @R3 3 m5 ;
: @R4 4 m5 ;
: @R5 5 m5 ;
: @R6 6 m5 ;
: @R7 7 m5 ;
: @R8 8 m5 ;
: @R9 9 m5 ;
: @R10 10 m5 ;
: @R11 11 m5 ;
: @R12 12 m5 ;
: @R13 13 m5 ;
: @R14 14 m5 ;
: @R15 15 m5 ;

  \ alias for MSP430
' @R1 alias @RSP \ retur stack pointer
' @R4 alias @PSP \ parameter stack pointer
' @R5 alias @IP  \ instruction pointer
' @R6 alias @W   \ working register
' @R7 alias @TOS \ top of (parameter) stack



\ 6.   Indirect autoincrement  @rn+       Make a name for each register. 
\ Function: Put register# on stack, set mode# to 1. 

: @R0+ 0 m6 ;     ' @R0+ alias @PC+  \ program counter
: @R1+ 1 m6 ;     ' @R1+ alias @SP+  \ stack ponter
: @R2+ 2 m6 ;     ' @R2+ alias @SR+  \ status register
: @R3+ 3 m6 ;
: @R4+ 4 m6 ;
: @R5+ 5 m6 ;
: @R6+ 6 m6 ;
: @R7+ 7 m6 ;
: @R8+ 8 m6 ;
: @R9+ 9 m6 ;
: @R10+ 10 m6 ;
: @R11+ 11 m6 ;
: @R12+ 12 m6 ;
: @R13+ 13 m6 ;
: @R14+ 14 m6 ;
: @R15+ 15 m6 ;

  \ alias for MSP430
' @R1+ alias @RSP+ \ retur stack pointer
' @R4+ alias @PSP+ \ parameter stack pointer
' @R5+ alias @IP+  \ instruction pointer
' @R6+ alias @W+   \ working register
' @R7+ alias @TOS+ \ top of (parameter) stack



\ 7.   Immediate mode          n n#    Make mode modifier.
\ Function: Constant n is on stack, mode# is set to 7. 

  ' m7 alias #N ( n -- n )



\ ****** Here are the mnemonics, that use the smarter adressing mode notation. 

\ *** Double-Operand Instructions (Format I)

: smode# ( -- n )  mode# 4 rshift $F and ;
: smodeI ( src | x src -- )
    smode#
    dup 1 = if drop sRn     exit then
    dup 2 = if drop sx(Rn)  exit then
    dup 3 = if drop sADDR   exit then
    dup 4 = if drop s&ADDR  exit then
    dup 5 = if drop s@Rn    exit then
    dup 6 = if drop s@Rn+   exit then
    dup 7 = if drop s#K     exit then
    1 throw ;
: dmode# ( -- n )  mode# $F and ;
: dmodeI ( dst | x dst -- )
    dmode#
    dup 1 = if drop dRn     exit then
    dup 2 = if drop dx(Rn)  exit then
    dup 3 = if drop dADDR   exit then
    dup 4 = if drop d&ADDR  exit then
    1 throw ;

\ expression ex may be single or doubble item. res
 : MOV.W  ( ex1 ex2  -- ) dmodeI smodeI     mov.w,  reset-mode# ;
 : ADD.W  ( ex1 ex2  -- ) dmodeI smodeI     add.w,  reset-mode# ;
 : ADDC.W ( ex1 ex2  -- ) dmodeI smodeI     addc.w, reset-mode# ;
 : SUBC.W ( ex1 ex2  -- ) dmodeI smodeI     subc.w, reset-mode# ;
 : SUB.W  ( ex1 ex2  -- ) dmodeI smodeI     sub.w,  reset-mode# ;
 : CMP.W  ( ex1 ex2  -- ) dmodeI smodeI     cmp.w,  reset-mode# ;
 : DADD.W ( ex1 ex2  -- ) dmodeI smodeI     dadd.w, reset-mode# ;
 : BIT.W  ( ex1 ex2  -- ) dmodeI smodeI     bit.w,  reset-mode# ;
 : BIC.W  ( ex1 ex2  -- ) dmodeI smodeI     bic.w,  reset-mode# ;
 : BIS.W  ( ex1 ex2  -- ) dmodeI smodeI     bis.w,  reset-mode# ;
 : XOR.W  ( ex1 ex2  -- ) dmodeI smodeI     xor.w,  reset-mode# ;
 : AND.W  ( ex1 ex2  -- ) dmodeI smodeI     and.w,  reset-mode# ;

 : MOV.B  ( ex1 ex2  -- ) dmodeI smodeI     mov.B,  reset-mode# ;
 : ADD.B  ( ex1 ex2  -- ) dmodeI smodeI     add.B,  reset-mode# ;
 : ADDC.B ( ex1 ex2  -- ) dmodeI smodeI     addc.B, reset-mode# ;
 : SUBC.B ( ex1 ex2  -- ) dmodeI smodeI     subc.B, reset-mode# ;
 : SUB.B  ( ex1 ex2  -- ) dmodeI smodeI     sub.B,  reset-mode# ;
 : CMP.B  ( ex1 ex2  -- ) dmodeI smodeI     cmp.B,  reset-mode# ;
 : DADD.B ( ex1 ex2  -- ) dmodeI smodeI     dadd.B, reset-mode# ;
 : BIT.B  ( ex1 ex2  -- ) dmodeI smodeI     bit.B,  reset-mode# ;
 : BIC.B  ( ex1 ex2  -- ) dmodeI smodeI     bic.B,  reset-mode# ;
 : BIS.B  ( ex1 ex2  -- ) dmodeI smodeI     bis.B,  reset-mode# ;
 : XOR.B  ( ex1 ex2  -- ) dmodeI smodeI     xor.B,  reset-mode# ;
 : AND.B  ( ex1 ex2  -- ) dmodeI smodeI     and.B,  reset-mode# ;

 ' MOV.W     alias MOV
 ' ADD.W     alias ADD
 ' ADDC.W    alias ADDC
 ' SUBC.W    alias SUBC
 ' SUB.W     alias SUB
 ' CMP.W     alias CMP
 ' DADD.W    alias DADD
 ' BIT.W     alias BIT
 ' BIC.W     alias BIC
 ' BIS.W     alias BIS
\ ' XOR.W     alias XOR
\ ' AND.W     alias AND



\ *** Single-Operand Instructions ( Format II )

: modeII ( src | x src -- )
    mode# $F and
    dup 1 = if drop Rn     exit then
    dup 2 = if drop x(Rn)  exit then
    dup 3 = if drop ADDR   exit then
    dup 4 = if drop &ADDR  exit then
    dup 5 = if drop @Rn    exit then
    dup 6 = if drop @Rn+   exit then
    dup 7 = if drop #K     exit then
    1 throw ;

: RRC.W   ( ex -- ) modeII RRC.W,  reset-mode# ;
: SWPB    ( ex -- ) modeII SWPB,   reset-mode# ;
: RRA.W   ( ex -- ) modeII RRA.W,  reset-mode# ;
: SXT     ( ex -- ) modeII SXT,    reset-mode# ;
: PUSH.W  ( ex -- ) modeII PUSH.W, reset-mode# ;
: CALL    ( ex -- ) modeII CALL,   reset-mode# ;
' RETI, alias RETI

: RRC.B   ( ex -- ) modeII RRC.B,  reset-mode# ;
: RRA.B   ( ex -- ) modeII RRA.B,  reset-mode# ;
: PUSH.B  ( ex -- ) modeII PUSH.B, reset-mode# ;

 ' RRC.W  alias RRC
 ' RRA.W  alias RRA
 ' PUSH.W alias PUSH



\ *** Jump Instructions ( FormatIII )
' JNE, ( adr -- )  alias JNE
' JEQ, ( adr -- )  alias JEQ
' JNC, ( adr -- )  alias JNC
' JC,  ( adr -- )  alias JC
' JN,  ( adr -- )  alias JN
' JGE, ( adr -- )  alias JGE
' JL,  ( adr -- )  alias JL
' JMP, ( adr -- )  alias JMP

  ' JNE, alias JNZ
  ' JEQ, alias JZ
  ' JNC, alias JLO
  ' JC,  alias JHS

( finis )

