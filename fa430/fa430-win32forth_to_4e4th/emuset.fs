\ Emulated MSP430 instruction set 



\ ADC Emulation : ADDC #0,dst
: ADC         \ Add C to destination:   dst + C --> dst [ * * * * ] 
    0# swap  ADDC   ; 
: ADC.B       \ Add C to destination:   dst + C --> dst [ * * * * ] 
    0# swap  ADDC.B ; 



\ BR Emulation : MOV src,PC 
: BR          \ Branch to destination:   src --> PC [ - - - - ] 
    PC MOV,  ;
' BR alias BRANCH


\ CLR Emulation: MOV #0,dst: CLR         \ Clear destination:  0 --> dst [ - - - - ] 
    0# swap  MOV  ; 
: CLR.B       \ Clear destination:  0 --> dst [ - - - - ] 
    0# swap  MOV.B   ; 



\ CLRC Emulation: BIC #1,SR 
: CLRC        \ Clear C   0 --> C [ - - - 0 ] 
    1# SR  BIC  ; 



\ CLRN Emulation: BIC #4,SR 
: CLRN        \ Clear N   0 --> N[ - 0 - - ] 
    4# SR  BIC  ; 



\ CLRZ Emulation: BIC #2,SR 
: CLRZ        \ Clear Z   0 --> Z[ - - 0 - ] 
    2# SR  BIC  ; 



\ DADC Emulation: DADD #0,dst
: DADC        \ Add C decimally to destination:   dst + C --> dst   (decimally) [ * * * * ] 
    0# swap  DADD   ; 
: DADC.B      \ Add C decimally to destination:   dst + C --> dst   (decimally) [ * * * * ] 
    0# swap  DADD.B   ; 



\ DEC Emulation: SUB #1,dst 
: DEC         \ Decrement destination:  dst   - 1 --> dst [ * * * * ] 
    1# swap SUB   ; 
: DEC.B       \ Decrement destination:  dst   - 1 --> dst [ * * * * ] 
    1# swap SUB.B   ; 



\ DECD Emulation: SUB #2,dst 
: DECD        \ Double-decrement destination:  dst   - 2 --> dst [ * * * * ] 
    2# swap  SUB   ; 
: DECD.B      \ Double-decrement destination:  dst   - 2 --> dst [ * * * * ] 
    2# swap  SUB.B   ; 



\ DINT Emulation: BIC #8,SR 
: DINT         \  Disable interrupts:  0 --> GIE [ - - - - ] 
    8# SR  BIC, ; 



\ EINT Emulation: BIS #8,SR 
: EINT         \  Enable interrupts:  1 --> GIE[ - - - - ] 
    8# SR  BIS   ; 



\ INC Emulation: ADD #1,dst 
: INC         \ Increment destination:  dst +1 --> dst [ * * * * ] 
    1# swap  ADD   ; 
: INC.B        \ Increment destination:  dst +1 --> dst [ * * * * ] 
    1# swap  ADD.B   ; 



\ INCD Emulation: ADD #2,dst 
: INCD        \ Double-increment destination:  dst+2 --> dst [ * * * * ] 
    2# swap  ADD   ; 
: INCD.B,     \ Double-increment destination:  dst+2 --> dst [ * * * * ] 
    2# swap  ADD.B  ; 



\ INV Emulation: XOR #0FFFFh,dst 
: INV         \ Invert destination:  .not.dst --> dst [ * * * * ] 
    FFFF# swap  XOR.W   ; 
: INV.B       \ Invert destination:  .not.dst --> dst [ * * * * ] 
    FFFF# swap  XOR.B   ; 



\ NOP Emulation: MOV #0, R3 
: NOP          \  No operation[ - - - - ] 
     0# R3  MOV   ; 



\ POP Emulation: MOV @SP+,dst 
: POP         \ Pop item from stack to destination:   @SP --> dst, SP+2 --> SP [ - - - - ] 
     @SP+ swap  MOV   ; 
: POP.B       \ Pop item from stack to destination:   @SP --> dst, SP+2 --> SP [ - - - - ] 
     @SP+ swap  MOV.B ; 


\ RET Emulation: MOV @SP+,PC
: RET,        \ Return from subroutine: @SP --> PC, SP+2 --> SP [ - - - - ] 
     @SP+ PC  MOV   ;  



\ RLA Emulation: ADD dst,dst 
: RLA ( exp --- )        \ Rotate left arithmetically [ * * * * ] 
    dup ADD   ; 
: RLA.B ( exp --- )      \ Rotate left arithmetically [ * * * * ] 
    dup ADD.B   ; 



\ RLC Emulation: ADDC dst,dst 
: RLC ( exp --- )        \ Rotate left through C [ * * * * ] 
    dup ADDC   ; 
: RLC.B ( exp --- )      \ Rotate left through C [ * * * * ] 
    dup ADDC.B   ; 



\ SBC Emulation: SUBC #0,dst 
: SBC         \ Subtract not(C) from destination:  dst + 0FFFFh + C --> dst [ * * * * ] 
    0# swap  SUBC   ; 
: SBC.B       \ Subtract not(C) from destination:  dst + 0FFFFh + C --> dst [ * * * * ] 
    0# swap  SUBC,  ; 



\ SETC Emulation: BIS #1,SR 
: SETC       ( -- )   \ Set C 1 --> C [ - - - 1 ] 
    1# SR  BIS   ; 



\ SETN Emulation: BIS #4,SR 
: SETN       ( -- )   \ Set N 1 --> N [ - 1 - - ] 
    4# SR  BIS  ; 



\ SETZ Emulation: BIS #2,SR 
: SETZ      ( -- )   \ Set Z 1 --> C [ - - 1 - ] 
    2# SR  BIS  ; 



\ TST Emulation: CMP #0,dst 
: TST        \ Test destination:  dst + 0FFFFh + 1 [ 0 * * 1 ] 
    0# swap  CMP   ; 
: TST.B       \ Test destination:  dst + 0FFFFh + 1 [ 0 * * 1 ] 
    0# swap  CMP.B   ; 



\ finis
