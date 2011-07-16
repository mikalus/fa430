\ MSP430 primitives

\ cfCode:  cfCode   Umsetzung von camel forth (mk)
\ mkCode  meine eigener Versuche.
\ ???Code ist ungeklŠrt.
\ ?! ist noch nicht Ÿbersetzt in MSP430






\ Copyright (C) 2006,2007 Free Software Foundation, Inc.

\ This file is part of Gforth.

\ Gforth is free software; you can redistribute it and/or
\ modify it under the terms of the GNU General Public License
\ as published by the Free Software Foundation, either version 3
\ of the License, or (at your option) any later version.

\ This program is distributed in the hope that it will be useful,
\ but WITHOUT ANY WARRANTY; without even the implied warranty of
\ MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
\ GNU General Public License for more details.

\ You should have received a copy of the GNU General Public License
\ along with this program. If not, see http://www.gnu.org/licenses/.



\ * Registers used: 
\  MSP430   Forth    used for
\   R1      RSP   returnstack pointer
\   R4      PSP   parameter stack pointer
\   R5      IP    instruction pointer
\   R6      W     working register
\   R7      TOS   top of parameter stack

\ MSP430 is 1s complement signed nummers? 

' Code alias cfCode 
' Code alias mkCode 


start-macros

    \ inline next. 
    \ A jump to a central next takes 2 words too, but is slower. 
    : next,      
    @IP+ W  MOV
    @W+  PC MOV ; 

    : savetos
      2# PSP sub
      TOS 0 (PSP) mov  ;

    : loadtos
      @PSP+ TOS MOV ;

end-macros



\ * Memory 
\ **************************************************************

  unlock
    $00000 $1FFFF region address-space
\    $0FFFF $0FFE0  ( inertupt vector tabel )
    $0FFDF $???? region rom-dictionary
    $???? $0200 region ram-dictionary
  .regions
  setup-target
  lock

\ ==============================================================
\ rom starts with jump to GFORTH-kernel (must be at $0000 !!!)
\ ==============================================================
  Label into-forth
???    $ffff #n ip mov            \ ip will be patched
???    $0780 #n dsp mov                \ sp at $0600...$0700
???    $0800 #n rsp mov            \ rp at $0780...$0800
???    $C084 #n intbl xxx
???      $0F #n $E3  xxx
???      $0F #n $E1  xxx


folgendes aus COLD noch klŠren: 
  Label mem-init
    $01 , $0A bset:g
    $00 , $05 bset:g                \ open data RAM
    $01 , $0A bclr:g
  Label clock-init                  \ default is 125kHz/8
    $00 , $0A  bset:g
    # $2808 , $06  mov.w:g
    AHEAD  THEN
    2 , $0C bclr:g
    # $00 , $08  mov.b:g            \ set to 20MHz
    $00 , $0A  bclr:g
  Label uart-init
    # $27 , $B0  mov.b:g      \ hfs
\    # $8105 , $A8  mov.w:g    \ ser1: 9600 baud, 8N1  \ hfs
\    # $2005 , $A8  mov.w:g    \ ser1: 38k4 baud, 8N1  \ hfs
    # $0500 , $AC  mov.w:g      \ hfs
    I fset
  next,
  End-Label



\ ==============================================================
\ GFORTH minimal primitive set
\ ==============================================================

reset-mode# 

 \ inner interpreter

  align

  cfCode :docol
    ip push
    w ip mov
    next,
  End-Code

  align

  cfCode :docon
    2# PSP sub
    tos 0 (psp) mov
    @w tos mov    
    next,
  End-Code

  align

  ???Code: :dovalue
\    '2 dout,                    \ only for debugging
    tos push.w:g
    4 [w] , w mov.w:g  [w] , tos mov.w:g
    next,
  End-Code

  align

  ???Code: :dofield
      4 [w] , tos add.w:g
      next,
  end-code
  
  align

  ???Code: :dodefer
\      # $05 , $E1 mov.b:g
     4 [w] , w mov.w:g  [w] , w mov.w:g
     next1,
  End-Code

  align
  
  ???Code: :dodoes  ( -- pfa ) \ get pfa and execute DOES> part
\    '6 dout,                    \ only for debugging
\      # $06 , $E1 mov.b:g
     tos push.w:g
     w , tos mov.w:g   # 4 , tos add.w:q
     # -2 , rp add.w:q  2 [w] , r1 mov.w:g
     rp , w mov.w:g  ip , [w] mov.w:g
     r1 , ip mov.w:g
     next,                                       \ execute does> part
  End-Code

  $FF $C0FE here - tcallot  ???
  
  ???Code: :dovar  ist das nicht gleich docon ? mk
\    '2 dout,                    \ only for debugging
    tos push.w:g
    # 4 , w add.w:q
    w , tos mov.w:g
    next,
  End-Code

\ program flow
  mkCode ;s       ( -- ) \ exit colon definition
    @RSP+ IP mov    \ pop old IP from return stack
    next,
  End-Code

  cfCode execute   ( xt -- ) \ execute colon definition
    tos w mov       \ copy tos to w
    @psp+ tos mov   \ get new tos
    @w+ pc          \ mov fetch code address into PC, W=PFA
  End-Code

\ wird high level bereit gestellt:  : perform ( adr - ) @ execute ;
\  Code perform   ( adr -- ) \ execute colon definition at adr.
\    tos , w mov.w:g                          \ copy tos to w
\    tos pop.w:g                              \ get new tos
\    [w] , w mov.w:g
\    next1,
\  End-Code

  ???Code ?branch   ( f -- ) \ jump on f=0
      # 2 , ip add.w:q
      tos , tos tst.w   0= IF  -2 [ip] , ip mov.w:g   THEN
      tos pop.w:g
      next,
  End-Code

  ???Code (for) ( n -- r:0 r:n )
      # -4 , rp add.w:q  rp , w mov.w:g
      r3 , 2 [w] mov.w:g
      tos , [w] mov.w:g
      tos pop.w:g
      next,
  End-Code
  
  ???Code (?do) ( n -- r:0 r:n )
      # 2 , ip add.w:q
      # -4 , rp add.w:q  rp , w mov.w:g
      tos , [w] mov.w:g
      r1 pop.w:g
      r1 , 2 [w] mov.w:g
      tos pop.w:g
      [w] , r1 sub.w:g
      0= IF  -2 [ip] , ip mov.w:g   THEN
      next,
  End-Code
  
  ???Code (do) ( n -- r:0 r:n )
      # -4 , rp add.w:q  rp , w mov.w:g
      tos , [w] mov.w:g
      tos pop.w:g
      tos , 2 [w] mov.w:g
      tos pop.w:g
      next,
  End-Code
  
  ?))Code (next) ( -- )
      # 2 , ip add.w:q
      rp , w mov.w:g  [w] , r1 mov.w:g
      # -1 , r1 add.w:q  r1 , [w] mov.w:g
      u>= IF  -2 [ip] , ip mov.w:g  THEN
      next,
  End-Code

  ???Code (loop) ( -- )
      # 2 , ip add.w:q
      rp , w mov.w:g  [w] , r1 mov.w:g
      # 1 , r1 add.w:q  r1 , [w] mov.w:g
      2 [w] , r1 sub.w:g
      0<> IF  -2 [ip] , ip mov.w:g  THEN
      next,
  End-Code

  ???Code (+loop) ( n -- )
      # 2 , ip add.w:q
      rp , w mov.w:g  [w] , r1 mov.w:g
      2 [w] , r1 sub.w:g  # $8000 , r1 xor.w
      tos , r1 add.w:g
      no IF  -2 [ip] , ip mov.w:g  THEN
      tos , [w] add.w:g
      tos pop.w:g
      next,
  End-Code



 \ memory access
  cfCode @        ( addr -- n ) \ read cell
    @tos tos mov
    next,
   End-Code

  cfCode !        ( n addr -- ) \ write cell
    @PSP+ 0 (TOS) mov 
    @PSP+ TOS mov 
    next,
   End-Code

  cfCode +!        ( n addr -- ) \ write cell
    @PSP+ 0 (TOS) add
    @PSP+ TOS mov
    next,
   End-Code

  cfCode c@        ( addr -- uc ) \ read cell
    @TOS TOS mov
    next,
   End-Code

  mkCode count     ( addr -- addr+1 uc ) \ read cell
    2# psp sub
    tos 0 (psp) mov 
    1# tos add
    @tos tos mov
    next,
   End-Code

  cfCode c!        ( n addr -- ) \ write cell
    @PSP+ W mov 
    W 0 (TOS) mov.b 
    @PSP+ TOS mov 
    next,
   End-Code

 \ arithmetic and logic
  cfCode +        ( n1 n2 -- n3 ) \ addition
    @PSP+ TOS add
    next,
  End-Code
  
  cfCode 2*        ( n1 -- n2 ) 
    TOS TOS add
    next,
  End-Code
  
  cfCode -        ( n1 n2 -- n3 ) \ subtract n2 - n1
    @PSP+ W mov
    TOS W sub
    W TOS mov
    next,
  End-Code

  cfCode negate ( n1 -- n2 )
    -1 #N TOS xor.w
    1# TOS add
    next,
  End-Code
  
  cfCode invert ( n1 -- n2 )
    -1 #N TOS xor.w
    next,
  End-Code
  
  cfCode 1+        ( n1 -- n2 ) \ addition
    1# TOS add
    next,
  End-Code
  
  cfCode 1-        ( n1 -- n2 ) \ addition
    1# TOS sub
    next,
  End-Code
  
  cfCode cell+        ( n1 -- n2 ) \ addition
    2# tos add
    next,
  End-Code
  
  cfCode and        ( n1 n2 -- n3 ) \ logic
    @PSP+ TOS and.w
    next,
  End-Code
  
  cfCode or       ( n1 n2 -- n3 ) \ logic
    @PSP+ TOS bis
    next,
  End-Code
  
  cfCode xor      ( n1 n2 -- n3 ) \ logic
    @PSP+ TOS xor.w
    next,
   End-Code

 \ moving datas between stacks
  cfCode r>       ( -- n ; R: n -- )
    2# PSP sub 
    TOS 0 (PSP) mov 
    @RSP+ TOS mov
    next,
  End-Code
  
  \ bei drei Schleifen liegt auf dem returnstack: ( R: k' k j' j i' i -- )
  \ Dabei ist i' das limit der innersten Schleife, i der ZŠhler, usw.

  cfCode i       ( -- i ; R: i -- i ) 
    2# PSP sub
    TOS 0 (PSP) mov
    @RSP TOS mov
    next,
   End-Code

  mkCode i'       ( -- limit ; R: i' i  -- i' i )
    savetos
    2 (RSP) tos mov
      next,
   End-Code

  mkCode j       ( -- j ; ( R: j' j i' i -- j' j i' i )
    savetos
    4 (RSP) tos mov
      next,
   End-Code

  mkCode k       ( -- k ; ( R: k' k j' j i' i -- k' k j' j i' i )
    savetos
    6 (RSP) tos mov
      next,
   End-Code

   cfCode >r       ( n -- ; R: -- n )
    TOS push
    @PSP+ TOS mov
    next,
   End-Code

   mkCode rdrop       ( R: n -- )
    2# RSP add
    next,
   End-Code

   mkCode unloop       ( R: n -- )
    4# rsp add 
    next,
   End-Code

 \ datastack and returnstack address
  cfCode sp@      ( -- sp ) \ get stack address
    2# PSP sub
    TOS 0 (PSP) mov
    PSP TOS mov
    next,
  End-Code

  cfCode sp!      ( sp -- ) \ set stack address
    TOS PSP mov
    @PSP+ TOS mov
    next,
  End-Code

  cfCode rp@      ( -- rp ) \ get returnstack address
    2# PSP sub
    TOS 0 (PSP) mov
    RSP TOS mov
    next,
  End-Code

  cfCode rp!      ( rp -- ) \ set returnstack address
    TOS RSP mov
    @PSP+ TOS mov
    next,
  End-Code

 \ branch and literal
  mkCode branch   ( -- ) \ unconditional branch
    @ip ip mov 
    next,
   End-Code

  cfCode lit     ( -- n ) \ inline literal
    2# PSP sub 
    TOS 0 (PSP) mov 
    @IP+ TOS mov 
    next,
   End-Code

\ Code: :doesjump  ( in ITC nicht nštig.) 
\ end-code

\ ==============================================================
\ usefull lowlevel words
\ ==============================================================

 \ data stack words
  cfCode dup      ( n -- n n )
    2# PSP sub  \ push old TOS..
    TOS  0 (PSP) mov \ ..onto stack
    next,
   End-Code

  mkCode 2dup     ( d -- d d ) \  over over
    4# PSP SUB 
    4 (PSP) 0 (PSP) mov
    tos 2 (PSP) mov
    next,
   End-Code

  cfCode drop     ( n -- )
    @PSP+ TOS mov
    next,
   End-Code

  cfCode 2drop    ( d -- )
    @PSP+ TOS mov
    @PSP+ TOS mov
    next,
   End-Code

  cfCode swap     ( n1 n2 -- n2 n1 )
    @PSP W mov
    TOS 0 (PSP) mov
    W TOS mov
    next,
   End-Code

  cfCode over     ( n1 n2 -- n1 n2 n1 )
    @PSP W mov
    2# PSP sub
    TOS 0 (PSP) mov
    W TOS mov
    next,
   End-Code

  cfCode rot      ( n1 n2 n3 -- n2 n3 n1 )
    @PSP W mov 
    TOS 0 (PSP) mov 
    2 (PSP) TOS mov 
    W 2 (PSP) mov 
    next,
   End-Code

  mkCode -rot     ( n1 n2 n3 -- n3 n1 n2 )
    tos w mov
    0 (psp) tos mov
    2 (psp) 0 (psp) mov
    w 2 (psp) mov
    next,
   End-Code


 \ return stack
   ' i alias r@ 



 \ arithmetic

\ Adopted from section 5.1.1 of MSP430 Family Application Reports. 
' TOS alias IROP1 \ First operand 
' R5 alias IROP2L \ Second operand low word 
' R6 alias IROP2M \ Second operand high word 
' R7 alias IRACL  \ Result low word 
' R8 alias IRACM  \ Result high word 
' R9 alias IRBT   \ Bit test register MPY 

  mkCode um*      ( u1 u2 -- ud ) \ unsigned multiply
    \ u2 ist schon im TOS 
    @PSP IROP2L MOV     \ get u1, leave room on stack
    \ T.I. SIGNED MULTIPLY SUBROUTINE: TOS x R5 -> R8|R7
    IRACL  CLR 
    IRACM  CLR 
    IROP2M CLR 
    1# IRBT MOV           \ BIT TEST REGISTER
L2:   IRBT IROP1 BIT   \ TEST ACTUAL BIT
L1: 000 JZ           \ IF 0: DO NOTHING
          IROP2L IRACL ADD    \ IF 1: ADD MULTIPLIER TO RESULT
          IROP2M IRACM ADDC 
L1 >>>  IROP2L RLA       \ MULTIPLIER x 2
        IROP2M RLC 
        IRBT RLA       \ NEXT BIT TO TEST
L2    JNC          \ IF BIT IN CARRY: FINISHED
    IRACL 0 (PSP) MOV   \ low result on stack
    IRACM TOS MOV       \ high result in TOS
    next,
   End-Code

  ?!Code m*      ( u1 u2 -- ud ) \ unsigned multiply
      rp , r3 mov.w:g
      r2 pop.w:g
      r2 , r2r0 mul.w:g
      r0 push.w:g
      r2 , tos mov.w:g
      r3 , rp mov.w:g
      r3 , r3 xor.w
      next,
   End-Code

clrlabels
  mkCode um/mod   ( ud u -- r q ) \ unsiged divide 
    \ u2 ist schon im TOS 
        @PSP+ IROP2M MOV   ; get ud hi
        @PSP  IROP2L MOV   ; get ud lo, leave room on stack
DIVIDE: IRACL CLR  
        &17 #N IRBT  MOV ; INITIALIZE LOOP COUNTER
L1:     IROP1 IROP2M  CMP
L2:     000 JLO  
        IROP1 IROP2M SUB 
L2 >>>  IRACL RLC
L4:     000 JC      ; Error: result > 16 bits
        IRBT DEC    ; Decrement loop counter
L3:     000 JZ      ; Is 0: terminate w/o error
        IROP2L RLA 
        IROP2M RLC 
L1      JNC 
        IROP1 IROP2M SUB 
        SETC
L2      JMP
L3 >>>  CLRC        ; No error, C = 0
L4 >>>  ; Error indication in C
        IROP2M 0 (PSP) MOV  ; remainder on stack
        IRACL TOS MOV       ; quotient in TOS
      next,
   End-Code



 \ shift

  cfCode 2*       ( n1 -- n2 ) \ arithmetic shift left
    TOS TOS ADD
    next,
   End-Code

  cfCode 2/       ( n1 -- n2 ) \ arithmetic shift right
    TOS RRA
    next,
   End-Code

clrlabels
  cfCode lshift   ( n1 n2 -- n3 ) \ shift n1 left n2 bits
    @PSP+ W MOV
    $1F N# TOS AND       ; no need to shift more than 16
L0: 000 JZ
L1: W W ADD  
    #1 TOS SUB
L1  JNZ
L0 >>> W TOS MOV
    next,
   End-Code

clrlabels
  cfCode rshift   ( n1 n2 -- n3 ) \ shift n1 right n2 bits
     @PSP+ W MOV
     $1F N# TOS AND       ; no need to shift more than 16
L0:  000 JZ
L1:  CLRC   
     W RRC   
     #1 TOS SUB  
L1  JNZ
L0 >>>  W,TOS MOV
     next,
   End-Code



 \ compare
  cfCode 0=       ( n -- f ) \ Test auf 0
    1# TOS sub     ; borrow (clear cy) if TOS was 0
    TOS TOS subc    ; TOS=-1 if borrow was set
    next,
   End-Code

   cfCode 0<       ( n -- f ) \ Test auf 0
    TOS TOS add    ; set cy if TOS negative
    TOS TOS subc    ; TOS=-1 if carry was clear
    -1 #N TOS xor.w    ; TOS=-1 if carry was set
    next,
   End-Code

  ?!Code =        ( n1 n2 -- f ) \ Test auf Gleichheit
    @PSP+ W MOV
    TOS W SUB  ; x1-x2 in W, flags set
??    0= IF  ffff# TOS MOV   ELSE   0# TOS MOV   THEN 
   End-Code

   ' = alias u=
   
  ?!Code u<        ( n1 n2 -- f ) \ Test auf Gleichheit
    r1 pop.w:g
    r1 , tos sub.w:g
    u> IF  # -1 , tos mov.w:q   next,
    THEN   #  0 , tos mov.w:q   next,
   End-Code

  ?!Code u>        ( n1 n2 -- f ) \ Test auf Gleichheit
    r1 pop.w:g
    r1 , tos sub.w:g
    u< IF  # -1 , tos mov.w:q   next,
    THEN   #  0 , tos mov.w:q   next,
   End-Code

  ?!Code <        ( n1 n2 -- f ) \ Test auf Gleichheit
    r1 pop.w:g
    r1 , tos sub.w:g
    > IF  # -1 , tos mov.w:q   next,
    THEN   #  0 , tos mov.w:q   next,
   End-Code

  ?!Code >        ( n1 n2 -- f ) \ Test auf Gleichheit
    r1 pop.w:g
    r1 , tos sub.w:g
    < IF  # -1 , tos mov.w:q   next,
    THEN   #  0 , tos mov.w:q   next,
   End-Code



\ I/O

  ?!Code (key)    ( -- char ) \ get character
      tos push.w:g
      BEGIN  3 , $AD  btst:g  0<> UNTIL
      $AE  , tos mov.w:g  r0h , r0h xor.b
    next,
   End-Code

  ?!Code (emit)     ( char -- ) \ output character
      BEGIN  1 , $AD  btst:g  0<> UNTIL
      tos.b , $AA  mov.b:g
      tos pop.w:g
      next,
  End-Code

 \ additon io routines
  ?!Code (key?)     ( -- f ) \ check for read sio character
      tos push.w:g
      3 , $AD  btst:g
      0<> IF  # -1 , tos mov.w:q   next,
      THEN    #  0 , tos mov.w:q   next,
   End-Code

  ?!Code emit?    ( -- f ) \ check for write character to sio
      tos push.w:g
      1 , $AD  btst:g
      0<> IF  # -1 , tos mov.w:q   next,
      THEN    #  0 , tos mov.w:q   next,
   End-Code

   \ String operations

   ?!Code fill ( addr u char -- )
       R3 pop.w:g  ip , r1 mov.w:g  A1 pop.w:g
       sstr.b  tos pop.w:g
       R3 , R3 xor.w  r1 , ip mov.w:g  next,
   End-Code

   ?!Code cmove ( from to count -- )
       tos , R3 mov.w:g  ip , r1 mov.w:g
       a1 pop.w:g  a0 pop.w:g  r1 push.w:g  r1 , r1 xor.w
       smovf.b
       R3 , R3 xor.w  ip pop.w:g  tos pop.w:g next,
   End-Code
   
   ?!Code cmove> ( from to count -- )
       tos , R3 mov.w:g  ip , r1 mov.w:g
       a1 pop.w:g  a0 pop.w:g  r1 push.w:g  r1 , r1 xor.w
       r3 , a0 add.w:g  # -1 , a0 add.w:q
       r3 , a1 add.w:g  # -1 , a1 add.w:q
       smovb.b
       R3 , R3 xor.w  ip pop.w:g  tos pop.w:g next,
   End-Code
   
   ?!Code (find-samelen) ( u f83name1 -- u f83name2/0 )
       tos , w mov.w:g  r0 pop.w:g
       BEGIN  2 [w] , r0h mov.b:g  # $1F , r0h and.b:g
	   r0l , r0h cmp.b:g  0<> WHILE  [w] , w mov.w:g
	   0= UNTIL  THEN
       r0h , r0h xor.b  r0 push.w:g  w , tos mov.w:g
       next,
   End-Code

: capscomp ( c_addr1 u c_addr2 -- n )
 swap bounds
 ?DO  dup c@ I c@ <>
     IF  dup c@ toupper I c@ toupper =
     ELSE  true  THEN  WHILE  1+  LOOP  drop 0
 ELSE  c@ toupper I c@ toupper - unloop  THEN  sgn ;
: sgn ( n -- -1/0/1 )
 dup 0= IF EXIT THEN  0< 2* 1+ ;
       
   ?!Code btst ( b# addr -- f ) \ check for bit set in addr
      tos , w mov.w:g  # 3 , w shl.w
      r1 pop.w:g       r1 , w add.w:g      [w] btst:g
      0<> IF    # -1 , tos mov.w:q   next,
          THEN  #  0 , tos mov.w:q   next,
   End-Code

   ?!Code bset ( b# addr -- ) \ set bit in addr
      tos , w mov.w:g  # 3 , w shl.w
      r1 pop.w:g       r1 , w add.w:g      [w] bset:g
      tos pop.w:g      next,
   End-Code

   ?!Code bclr ( b# addr -- ) \ clr bit in addr
      tos , w mov.w:g  # 3 , w shl.w
      r1 pop.w:g       r1 , w add.w:g      [w] bclr:g
      tos pop.w:g      next,
   End-Code

   ?!Code us ( n -- ) \ n microseconds delay
       BEGIN  AHEAD  THEN  AHEAD  THEN
           r1 , r1 mov.w:g
           # -1 , tos  add.w:q  0= UNTIL
       tos pop.w:g
       next,
   end-code



   Variable timer
   
   ?!Code ms-irq ( -- )
       # 1 , timer add.w:g
       reit
   end-code

   ' ms-irq >body $C084 $40 + ! 0 $C084 $42 + c!

   : timer-init ( -- )
       &19999 $9E !
       $0401 $9A !
       1 $50 c! ;

   : noop ;
   defer pause ' noop is pause
   

   : ms ( n -- )  timer @ +
       BEGIN  pause dup timer @ - 0<  UNTIL  drop ;
   
   $400 constant ram-start
   $2FFC Constant ram-shadow
   0 Constant ram-mirror
   0 Constant ram-size
   $E0 Constant port0
   $E1 Constant port1
   
   : led!  port1 c! ;
   : >lcd ( 4bit -- )
       1+ dup port0 c! dup 8 + port0 c!  1 us  port0 c!
       &40 us ;
   : lcdctrl!  ( n -- )
       dup $F0 and >lcd
       4 lshift >lcd
       &100 us ;
   : lcdemit ( n -- )  &100 us
       dup $F0 and 4 + >lcd
       4 lshift 4 + >lcd
       &250 us ;
   : lcdtype  bounds ?DO  I c@ lcdemit  LOOP ;
   : lcdpage  $01 lcdctrl! &15 ms ;
   : lcdcr    $C0 lcdctrl! ;
   : lcdinit ( -- )
       $02 $0A bset $FD $E2 c!
       &20 ms $30 >lcd  5 ms  $33 lcdctrl! 5 ms $20 >lcd
       &5 ms  $28 lcdctrl!
       &1 ms  $0C lcdctrl!
       &1 ms  lcdpage ;
   \ default channel is channel 6
   : adc@ ( chan -- value )  $80 + $D6 c!  $28 $D7 c!
       6 $D6 bset  BEGIN  6 $D6 btst 0=  UNTIL  $C0 @ ;
   : ?flash  BEGIN  $1B7 c@ 1 and 1 =  UNTIL ;
   : flashc! ( c addr -- )  $40 over c! c! ?flash ;
   : flash! ( x addr -- )  2dup flashc! >r 8 rshift r> 1+ flashc! ;
   : flash-off ( addr -- )  $20 over c! $D0 swap c! ?flash ;
   : flash-enable ( -- )   1 $1b7 c! 3 $1b7 c! 0 $1b5 c! 2 $1b5 c! ;
   : 9k6   $8105 $A8 ! ; \ baud setting
   : 38k4  $2005 $A8 ! ; \ fast terminal
   : r8cboot ( -- ) ['] noop IS pause
       timer-init flash-enable lcdinit 38k4
       s" Gforth EC R8C" lcdtype boot ;
   ' r8cboot >body $C002 !
   : savesystem ( -- )
       dpp @ >r rom here normal-dp @ ram-start tuck - tuck
       here over allot r> dpp ! -rot
       bounds ?DO  I c@ over flashc! 1+  LOOP  drop
       ram-shadow tuck flash! cell+ flash! ;
   : refill-loop ( -- )
       BEGIN  3 emit refill  WHILE  interpret  REPEAT ;   
   : included ( addr u -- )
       2 emit dup $20 + emit type
       echo @ IF
	   echo off ['] refill-loop catch
	   dup IF  4 emit  THEN  echo on  throw
       THEN ;
   : include ( "file" -- )  parse-name included ;
   : empty ( -- )  $2800 flash-off $2000 flash-off
       forth-wordlist ram-mirror + ram-start - @ forth-wordlist !
       normal-dp ram-mirror + ram-start - @ normal-dp ! $2000 flash-dp ! ;

\ finis

