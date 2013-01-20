\ MSP430 cross assembler code verification process
\ use it with file verify.fs

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

( finis)

