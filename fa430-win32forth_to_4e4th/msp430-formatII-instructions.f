\ MSP430
\ Single-Operand (Format II) Instructions
\ BIts: | 15 14 13 12 11 10 9 8 7 | 6 | 5 4 | 3 2 1 0
\       |         Op-code          B/W   As   D/S-Reg

\   statusbits: [ V N Z C ]
\   * The status bit is affected
\   Ð The status bit is not affected
\   0 The status bit is cleared
\   1 The status bit is set



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

( finis)
