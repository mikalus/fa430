\ MSP
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

( finis)
