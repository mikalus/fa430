
\ : upper 2drop ;  \ gforth compatibility


\ MSP cross compiler
\ Compile the code for the target provisionally in host memory
\ END_CODE reads this memory to generate source for the target MCU

: HEX.  ( n -- ) base @ >r   hex u.   r> base ! ;



create tstart   $3F00 allot   here constant tend    \ simulate 16Kbyte target memory
variable tdp      \ target dictionary pointer
: there  tdp @ ;

defer [op]



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


\ assemble

\ used for code verification in opcode verification file
there value loca  \ save last opcode address.
: setloca  ( -- )    there to loca ;

$33 value mode#  \ mode numers of syntax layer
  0 value mode   \ addressmode mask
  0 value src    \ source
  0 value dst    \ destination

false value sflag
false value dflag

: reset-mode#  $33 to mode#  ;
: reset-mode   0 to mode ;
: reset-src    false to sflag ;
: reset-dst    false to dflag ;

: set-src      true to sflag ;
: set-dst      true to dflag ;

: tclear       tstart tend over - $FF fill   tstart tdp !   setloca ;   tclear


: src,  ( -- ) sflag if src X_, then reset-src ;
: dst,  ( -- ) dflag if dst X_, then reset-dst ;

: op@   ( -- op ) loca X_@ ; ( wird gar nicht benutzt)
: op!   ( op -- ) loca X_! ; ( wird gar nicht benutzt)
: op,   ( op -- ) [op]  X_, reset-mode ; ( formI,II,III, reti)

true [if]  ' setloca is [op]  [else]  ' nop is [op]  [then]  \ switch compiling location

: NEWCODE ( -- )
  reset-mode# reset-mode reset-src reset-dst tclear ;

: CODE  ( <name> -- )
  \ at runtime <name> executes code.
  newcode   cr ." HEX "   bl word count cr ." CODE "  2dup upper type cr ;

: CODEADR  ( <name> -- )
\ at runtime: ( -- adr ) address where code starts.
  newcode   cr ." HEX "   bl word count cr ." CODEADR "  2dup upper type cr ;

: ?CR ( i -- ) $0F and 0= if cr then ;
: END-CODE    ( --- )
  base @ >r  hex
  there  tstart  ?do   i x_@ u.   ." i, "  i ?CR  2 +LOOP
  cr ." END-CODE "
  r> base ! cr ;

( finis )
