\ MSP code example

MSP430CODE XDUP     ( x -- x x )   \ duplicate top of stack
        2# PSP        SUB          \ push old TOS..
        TOS 0 (PSP)   MOV          \ ..onto stack
        NEXT
END-CODE



\ Output will look like this:
\
\ HEX
\ MSP430CODE XDUP
\ 8324 i, 4784 i, 0 i,
\ 4536 i, 4630 i,
\ END-CODE
\
\ Copy&paste this source code to 4e4th then.
\ This redefined word has an x in front so you can use the original DUP as well.
\
\
\
\ Verification
\
\
\ To verify our MSP430CODE you can compare it to a known code.
\ Take a IAR Listing or dump 4e4th code.
\ Or review opcode with the CHECKI CHECKII or CHECKIII command.
\ The use of a CHECK is explained in the formatI-III instruction files.
\
\
\
\ Verify by IAR
\
\ This is a the original source code from IAR, core430G2553.s34
\
\ ;C DUP      x -- x x      duplicate top of stack
\          HEADER  DUP,3,'DUP',DOCODE
\ PUSHTOS: SUB     #2,PSP           ; 1  push old TOS..
\          MOV     TOS,0(PSP)      ; 4  ..onto stack
\          NEXT                    ; 4
\
\ IAR compiled:
\
\ ...
\     174    0000E6              ; STACK OPERATIONS
\     175    0000E6
\     176    0000E6              ;C DUP      x -- x x      duplicate top of
\                                 stack
\     177    0000E6                      HEADER  DUP,3,'DUP',DOCODE
\     177.1  000000                      PUBLIC  DUP
\     177.2  0000E6 ....                 DW      link
\     177.3  0000E8 FF                   DB      0FFh       ; not immediate
\     177.4  0000E9              link    SET     $
\     177.5  0000E9 03                   DB      3
\     177.6  0000EA 445550               DB      'DUP'
\     177.7  0000ED 00                   EVEN
\     177.8  0000EE                      IF      'DOCODE'='DOCODE'
\     177.9  0000EE ....         DUP: DW     $+2
\     177.10 0000F0                      ELSE
\     177.11 0000F0              DUP: DW      DOCODE
\     177.12 0000F0                      ENDIF
\     177.13 0000F0                      ENDM
\     178    0000F0 2483         PUSHTOS: SUB    #2,PSP          ; 1  push old TOS..
\     179    0000F2 84470000             MOV     TOS,0(PSP)      ; 4  ..onto stack
\     180    0000F6                      NEXT                    ; 4
\     180.1  0000F6 3645                 MOV @IP+,W     // ; fetch word address into W
\     180.2  0000F8 3046                 MOV @W+,PC     // ; fetch code address into PC, W=PFA
\     180.3  0000FA                      ENDM
\
\
\
\ We look for the code, not the header. It is:
\
\ 2483 8447 0000 3645 3046    (Dump is in low byte high byte order.)
\
\ Our MSP430CODE for XDUP is:
\ 8324 i, 4784 i, 0 i, 4536 i, 4630 i,
\
\ Download to 4e4th it compiles:
\ HEX
\ ' xdup >body 20 dump
\ C00A   24 83 84 47  0  0 36 45 30 46 FF FF FF FF FF FF  $  G  6E0F~~~~~~
\ C01A   FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF  ~~~~~~~~~~~~~~~~$10ok
\
\ Compare to DUP:
\ ' dup >body 20 dump
\ D8F0   24 83 84 47  0  0 36 45 30 46 E9 D8 FF  4 3F 44  $  G  6E0FiX~ ?D
\ D900   55 50  4 D9  7 93 F4 23 36 45 30 46 FD D8 FF  4  UP Y  t#6E0F}X~ $10ok
\ Its the same code.
\ So it is ok.
( finis)
