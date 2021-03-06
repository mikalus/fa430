\ MSP430
\ Jump (Format-III) Instructions
\ Conditional-jump instruction format:
\ Bits: | 15 14 13 | 12 11 10 | 9 8 7 6 5 4 3 2 1 0 |
\       |  Op-code |    C     |   10-Bit PC Offset  |

\   statusbits: [ V N Z C ]
\   * The status bit is affected
\   � The status bit is not affected
\   0 The status bit is cleared
\   1 The status bit is set



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

: s>10 ( n -- n10 ) \ convert single precision to 10bit 2-complement.
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

( finis)
