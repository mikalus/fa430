\ MSP430
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
: sRn   ( rn -- )  >sreg  reset-src   00As ;
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
  R2  00 Register mode
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

( finis)

