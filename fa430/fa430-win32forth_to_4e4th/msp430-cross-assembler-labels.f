\ MSP430 cross assembler labels

\ forward jump:   L0: 000 Jxx  ...  L0 >>>  ...
\ backward jump:  L0:  ...  L0 Jxx  ...


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

: .labels   ( -- )   ." labels" cr maxlabels 0 do i . i label> . cr loop ;
: clrlabels ( -- )   maxlabels 0 do i >label loop ;

( finis)
