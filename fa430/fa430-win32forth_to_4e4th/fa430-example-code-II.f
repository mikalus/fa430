\ MOre MSP code examples
\
\ These redefined words have an x in front.

\ Be carefull not to use # as a prefix for constant generator mode.
\ Win32forth interprets # prefix of a number as decimal base for the number.
\
\ decimal
\ #10 $10 .s [2] 10 16  ok..
\ 2drop hex  ok
\ #10 $10 .s [2] Ah 10h  ok..
\
\ So I ue n# to switch to constant generator mode, with n={0,1,2,4,8,FFFF}


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
        2# PSP       SUB
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
        2# PSP       ADD
        NEXT
END-CODE


\ This is a copy of the original code from IAR; core430G2553.s34
\
\ ; STACK OPERATIONS
\ ...
\ ;C DROP     x --          drop top of stack
\         HEADER  DROP,4,'DROP',DOCODE
\         MOV     @PSP+,TOS       ; 2
\         NEXT                    ; 4
\ \
\ ;C SWAP     x1 x2 -- x2 x1    swap top two items
\         HEADER  SWAP,4,'SWAP',DOCODE
\         MOV     @PSP,W          ; 2
\         MOV     TOS,0(PSP)      ; 4
\         MOV     W,TOS           ; 1
\         NEXT                    ; 4
\ \
\ ;C OVER    x1 x2 -- x1 x2 x1   per stack diagram
\         HEADER  OVER,4,'OVER',DOCODE
\         MOV     @PSP,W          ; 2
\         SUB     #2,PSP          ; 2
\         MOV     TOS,0(PSP)      ; 4
\         MOV     W,TOS           ; 1
\         NEXT                    ; 4
\ \
\ ;C ROT    x1 x2 x3 -- x2 x3 x1  per stack diagram
\         HEADER  ROT,3,'ROT',DOCODE
\         MOV     @PSP,W          ; 2 fetch x2
\         MOV     TOS,0(PSP)      ; 4 store x3
\         MOV     2(PSP),TOS      ; 3 fetch x1
\         MOV     W,2(PSP)        ; 4 store x2
\         NEXT                    ; 4
\ \
\ ;X NIP    x1 x2 -- x2           per stack diagram
\         HEADER  NIP,3,'NIP',DOCODE
\         ADD     #2,PSP          ; 1
\         NEXT                    ; 4
\
\
\ And IAR compiled this:
\
\ ...
\
\     187    00010C
\     188    00010C              ;C DROP     x --          drop top of stack
\     189    00010C                      HEADER  DROP,4,'DROP',DOCODE
\     189.1  000000                      PUBLIC  DROP
\     189.2  00010C ....                 DW      link
\     189.3  00010E FF                   DB      0FFh       ; not immediate
\     189.4  00010F              link    SET     $
\     189.5  00010F 04                   DB      4
\     189.6  000110 44524F50             DB      'DROP'
\     189.7  000114                      EVEN
\     189.8  000114                      IF      'DOCODE'='DOCODE'
\     189.9  000114 ....         DROP: DW     $+2
\     189.10 000116                      ELSE
\     189.11 000116              DROP: DW      DOCODE
\     189.12 000116                      ENDIF
\     189.13 000116                      ENDM
\     190    000116 3744                 MOV     @PSP+,TOS       ; 2
\     191    000118                      NEXT                    ; 4
\     191.1  000118 3645                 MOV @IP+,W     // ; fetch word address into W
\     191.2  00011A 3046                 MOV @W+,PC     // ; fetch code address into PC, W=PFA
\     191.3  00011C                      ENDM
\
\
\     193    00011C              ;C SWAP     x1 x2 -- x2 x1    swap top two items
\     194    00011C                      HEADER  SWAP,4,'SWAP',DOCODE
\     194.1  000000                      PUBLIC  SWAP
\     194.2  00011C ....                 DW      link
\     194.3  00011E FF                   DB      0FFh       ; not immediate
\     194.4  00011F              link    SET     $
\     194.5  00011F 04                   DB      4
\     194.6  000120 53574150             DB      'SWAP'
\     194.7  000124                      EVEN
\     194.8  000124                      IF      'DOCODE'='DOCODE'
\     194.9  000124 ....         SWAP: DW     $+2
\     194.10 000126                      ELSE
\     194.11 000126              SWAP: DW      DOCODE
\     194.12 000126                      ENDIF
\     194.13 000126                      ENDM
\     195    000126 2644                 MOV     @PSP,W          ; 2
\     196    000128 84470000             MOV     TOS,0(PSP)      ; 4
\     197    00012C 0746                 MOV     W,TOS           ; 1
\     198    00012E                      NEXT                    ; 4
\     198.1  00012E 3645                 MOV @IP+,W     // ; fetch word address into W
\     198.2  000130 3046                 MOV @W+,PC     // ; fetch code address into PC, W=PFA
\     198.3  000132                      ENDM
\
\
\     200    000132              ;C OVER    x1 x2 -- x1 x2 x1   per stack diagram
\     201    000132                      HEADER  OVER,4,'OVER',DOCODE
\     201.1  000000                      PUBLIC  OVER
\     201.2  000132 ....                 DW      link
\     201.3  000134 FF                   DB      0FFh       ; not immediate
\     201.4  000135              link    SET     $
\     201.5  000135 04                   DB      4
\     201.6  000136 4F564552             DB      'OVER'
\     201.7  00013A                      EVEN
\     201.8  00013A                      IF      'DOCODE'='DOCODE'
\     201.9  00013A ....         OVER: DW     $+2
\     201.10 00013C                      ELSE
\     201.11 00013C              OVER: DW      DOCODE
\     201.12 00013C                      ENDIF
\     201.13 00013C                      ENDM
\     202    00013C 2644                 MOV     @PSP,W          ; 2
\     203    00013E 2483                 SUB     #2,PSP          ; 2
\     204    000140 84470000             MOV     TOS,0(PSP)      ; 4
\     205    000144 0746                 MOV     W,TOS           ; 1
\     206    000146                      NEXT                    ; 4
\     206.1  000146 3645                 MOV @IP+,W     // ; fetch word address into W
\     206.2  000148 3046                 MOV @W+,PC     // ; fetch code address into PC, W=PFA
\     206.3  00014A                      ENDM
\
\
\     208    00014A              ;C ROT    x1 x2 x3 -- x2 x3 x1  per stack diagram
\     209    00014A                      HEADER  ROT,3,'ROT',DOCODE
\     209.1  000000                      PUBLIC  ROT
\     209.2  00014A ....                 DW      link
\     209.3  00014C FF                   DB      0FFh       ; not immediate
\     209.4  00014D              link    SET     $
\     209.5  00014D 03                   DB      3
\     209.6  00014E 524F54               DB      'ROT'
\     209.7  000151 00                   EVEN
\     209.8  000152                      IF      'DOCODE'='DOCODE'
\     209.9  000152 ....         ROT: DW     $+2
\     209.10 000154                      ELSE
\     209.11 000154              ROT: DW      DOCODE
\     209.12 000154                      ENDIF
\     209.13 000154                      ENDM
\     210    000154 2644                 MOV     @PSP,W          ; 2 fetch x2
\     211    000156 84470000             MOV     TOS,0(PSP)      ; 4 store x3
\     212    00015A 17440200             MOV     2(PSP),TOS      ; 3 fetch x1
\     213    00015E 84460200             MOV     W,2(PSP)        ; 4 store x2
\     214    000162                      NEXT                    ; 4
\     214.1  000162 3645                 MOV @IP+,W     // ; fetch word address into W
\     214.2  000164 3046                 MOV @W+,PC     // ; fetch code address into PC, W=PFA
\     214.3  000166                      ENDM
\
\
\     216    000166              ;X NIP    x1 x2 -- x2           per stack
\                                 diagram
\     217    000166                      HEADER  NIP,3,'NIP',DOCODE
\     217.1  000000                      PUBLIC  NIP
\     217.2  000166 ....                 DW      link
\     217.3  000168 FF                   DB      0FFh       ; not immediate
\     217.4  000169              link    SET     $
\     217.5  000169 03                   DB      3
\     217.6  00016A 4E4950               DB      'NIP'
\     217.7  00016D 00                   EVEN
\     217.8  00016E                      IF      'DOCODE'='DOCODE'
\     217.9  00016E ....         NIP: DW     $+2
\     217.10 000170                      ELSE
\     217.11 000170              NIP: DW      DOCODE
\     217.12 000170                      ENDIF
\     217.13 000170                      ENDM
\     218    000170 2453                 ADD     #2,PSP          ; 1
\     219    000172                      NEXT                    ; 4
\     219.1  000172 3645                 MOV @IP+,W     // ; fetch word address into W
\     219.2  000174 3046                 MOV @W+,PC     // ; fetch code address into PC, W=PFA
\     219.3  000176                      ENDM
\
\  Go ahead and verify the code as shown in example code I.

( finis)
