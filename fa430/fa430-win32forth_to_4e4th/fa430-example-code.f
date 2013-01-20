\ MSP code examples

\ These redefined words have an x in front.
\ Compare with the original code from IAR in the notes below.

MSP430CODE XDUP     ( x -- x x )   \  duplicate top of stack
        #2 PSP        SUB          \ push old TOS..
        TOS 0 (PSP)   MOV          \ ..onto stack
        NEXT
END-CODE

MSP430CODE XDROP    ( x -- )        \  drop top of stack
        @PSP+ TOS     MOV
        NEXT
END-CODE

MSP430CODE XSWAP    ( x1 x2 -- x2 x1 )   \ swap top two items
        @PSP W        MOV
        TOS 0 (PSP)   MOV
        W TOS         MOV
        NEXT
END-CODE


MSP430CODE XOVER   ( x1 x2 -- x1 x2 x1 )  \ per stack diagram
        @PSP W       MOV
        #2 PSP       SUB
        TOS 0 (PSP)  MOV
        W TOS        MOV
        NEXT
END-CODE


MSP430CODE XROT   ( x1 x2 x3 -- x2 x3 x1 )  \ per stack diagram
        @PSP W       MOV
        TOS 0 (PSP)  MOV
        2 (PSP) TOS  MOV
        W 2 (PSP)    MOV
        NEXT
END-CODE


MSP430CODE XNIP   ( x1 x2 -- x2 )          \ per stack diagram
        ADD     #2,PSP       ADD
        NEXT
END-CODE


false [if]
\ Copy of the original code from IAR; core430G2553.s34

...
; STACK OPERATIONS

;C DUP      x -- x x      duplicate top of stack
        HEADER  DUP,3,'DUP',DOCODE
PUSHTOS: SUB    #2,PSP          ; 1  push old TOS..
        MOV     TOS,0(PSP)      ; 4  ..onto stack
        NEXT                    ; 4
...

;C DROP     x --          drop top of stack
        HEADER  DROP,4,'DROP',DOCODE
        MOV     @PSP+,TOS       ; 2
        NEXT                    ; 4

;C SWAP     x1 x2 -- x2 x1    swap top two items
        HEADER  SWAP,4,'SWAP',DOCODE
        MOV     @PSP,W          ; 2
        MOV     TOS,0(PSP)      ; 4
        MOV     W,TOS           ; 1
        NEXT                    ; 4

;C OVER    x1 x2 -- x1 x2 x1   per stack diagram
        HEADER  OVER,4,'OVER',DOCODE
        MOV     @PSP,W          ; 2
        SUB     #2,PSP          ; 2
        MOV     TOS,0(PSP)      ; 4
        MOV     W,TOS           ; 1
        NEXT                    ; 4

;C ROT    x1 x2 x3 -- x2 x3 x1  per stack diagram
        HEADER  ROT,3,'ROT',DOCODE
        MOV     @PSP,W          ; 2 fetch x2
        MOV     TOS,0(PSP)      ; 4 store x3
        MOV     2(PSP),TOS      ; 3 fetch x1
        MOV     W,2(PSP)        ; 4 store x2
        NEXT                    ; 4
...

( finis)
