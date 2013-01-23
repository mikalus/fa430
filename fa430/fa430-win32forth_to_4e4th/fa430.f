\ FORTH Assembler for TI MSP430x2xx Family

\ Simple Assembler for Texas Instruments MSP430 Microcomputers
\ made with gforth 0-7-0 on MacOSX 10.4.11 PowerPC G4.
\ Autor:          Michael Kalus (mk)

\ Adopted to win32forth on Windows XP for 4e4th-IDE needs.  jan 19 2013 mk

\ See:
\ MSP430x2xx Family User's Guide, Table 3-17, Instruction Set
\ SLAU144H‚December 2004‚Revised April 2011, S. 64, CPU.
\ © 2004‚2011, Texas Instruments Incorporated



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
HERE \ use this to calculate byts used for assembler definitions.

vocabulary msp430assembler   msp430assembler definitions
include msp430-crosscompiler.f             \ cr .( a .s) .s
include msp430-addressing-modes.f          \ cr .( b .s) .s
include msp430-formatI-instructions.f      \ cr .( c .s) .s
include msp430-formatII-instructions.f     \ cr .( d .s) .s
include msp430-formatIII-instructions.f    \ cr .( e .s) .s
include syntaxlayer.f                      \ cr .( f .s) .s
include emuset.f                           \ cr .( g .s) .s
include msp430-cross-assembler-labels.f    \ cr .( h .s) .s

HERE  SWAP -  cr . .( bytes used for this assembler ) CR
hex \  .( -- words so far: ) words cr  .( -- end of wordlist) cr cr

: ..   bye ;
( finis)

