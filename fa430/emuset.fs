\ Emulated MSP430 instruction set 
\ Some instructions are available in basic syntax only.



\ ADC Emulation : ADDC #0,dst
: ADC,   (1)  \ Add C to destination:   dst + C --> dst [ * * * * ] 
    0 s#N  ADDC,  ; 
: ADC.B, (1)  \ Add C to destination:   dst + C --> dst [ * * * * ] 
    0 s#N  ADDC.B,  ; 
 ' ADC, alias ADC.W, 



\ BR Emulation : MOV src,PC 
: BR,    (1)  \ Branch to destination:   src --> PC [ - - - - ] 
    PC dRn MOV, ; 
 ' BR, alias BRANCH, 



\ CLR Emulation: MOV #0,dst: CLR,   (1)  \ Clear destination:  0 --> dst [ - - - - ] 
    0 s#N  MOV,  ; 
: CLR.B, (1)  \ Clear destination:  0 --> dst [ - - - - ] 
    0 s#N  MOV.B,  ; 
 ' CLR, alias CLR.W, 



\ CLRC Emulation: BIC #1,SR 
: CLRC, (1)   \ Clear C   0 --> C [ - - - 0 ] 
    1 s#N  SR dRn  BIC, ; 

' CLRC, alias CLRC 



\ CLRN Emulation: BIC #4,SR 
: CLRN, (1)   \ Clear N   0 --> N[ - 0 - - ] 
    $4 s#N  SR dRn  BIC, ; 

' CLRN, alias CLRN 



\ CLRZ Emulation: BIC #2,SR 
: CLRZ, (1)   \ Clear Z   0 --> Z[ - - 0 - ] 
    $2 s#N  SR dRn  BIC, ; 

' CLRZ, alias CLRZ 



\ DADC Emulation: DADD #0,dst
: DADC,   (1) \ Add C decimally to destination:   dst + C --> dst   (decimally) [ * * * * ] 
    0  s#N  DADD,  ; 
: DADC.B, (1) \ Add C decimally to destination:   dst + C --> dst   (decimally) [ * * * * ] 
    0 s#N  DADD.B,  ; 
 ' DADC, alias DADC.W, 



\ DEC Emulation: SUB #1,dst 
: DEC,   (1)  \ Decrement destination:  dst   - 1 --> dst [ * * * * ] 
    1 s#N SUB,  ; 
: DEC.B, (1)  \ Decrement destination:  dst   - 1 --> dst [ * * * * ] 
    1 s#N  SUB.B,  ; 
 ' DEC, alias DEC.W, 



\ DECD Emulation: SUB #2,dst 
: DECD,   (1) \ Double-decrement destination:  dst   - 2 --> dst [ * * * * ] 
    &2 s#N  SUB,  ; 
: DECD.B, (1) \ Double-decrement destination:  dst   - 2 --> dst [ * * * * ] 
    &2 s#N  SUB.B,  ; 
 ' DECD, alias DECD.W, 



\ DINT Emulation: BIC #8,SR 
: DINT,   (1)  \  Disable interrupts:  0 --> GIE [ - - - - ] 
    &8 s#N  SR dRn BIC, ; 

' DINT alias DINT



\ EINT Emulation: BIS #8,SR 
: EINT,   (1)  \  Enable interrupts:  1 --> GIE[ - - - - ] 
    &8 s#N  SR dRn BIS,  ; 

' EINT, alias EINT 



\ INC Emulation: ADD #1,dst 
: INC,    (1)  \ Increment destination:  dst +1 --> dst [ * * * * ] 
    1 s#N  ADD,  ; 
: INC.B,  (1)  \ Increment destination:  dst +1 --> dst [ * * * * ] 
    1 s#N  ADD.B,  ; 
 ' INC, alias INC.W, 



\ INCD Emulation: ADD #2,dst 
: INCD,   (1) \ Double-increment destination:  dst+2 --> dst [ * * * * ] 
    2 s#N  ADD,  ; 
: INCD.B, (1) \ Double-increment destination:  dst+2 --> dst [ * * * * ] 
    2 s#N  ADD.B, ; 
 ' INCD, alias INCD.W, 



\ INV Emulation: XOR #0FFFFh,dst 
: INV,   (1)  \ Invert destination:  .not.dst --> dst [ * * * * ] 
    $0FFFF s#N XOR,  ; 
: INV.B, (1)  \ Invert destination:  .not.dst --> dst [ * * * * ] 
    $0FFFF s#N XOR.B,  ; 
 ' INV, alias INV.W, 



\ NOP Emulation: MOV #0, R3 
: NOP,    (2)  \  No operation[ - - - - ] 
     0 s#N R3 dRn  MOV,  ; 

' NOP, alias NOP 



\ POP Emulation: MOV @SP+,dst 
: POP,   (2)  \ Pop item from stack to destination:   @SP --> dst, SP+2 --> SP [ - - - - ] 
     SP s@Rn+  MOV,  ; 
: POP.B, (2)  \ Pop item from stack to destination:   @SP --> dst, SP+2 --> SP [ - - - - ] 
     SP s@Rn+  MOV,  ; 
 ' POP, alias POP.W, 



\ RLA Emulation: ADD dst,dst 
: RLA,   (2)  \ Rotate left arithmetically [ * * * * ] 
    ADD,  ; 
: RLA.B, (2)  \ Rotate left arithmetically [ * * * * ] 
    ADD.B,  ; 
 ' RLA, alias RLA.W, 



\ RLC Emulation: ADDC dst,dst 
: RLC,   (2)  \ Rotate left through C [ * * * * ] 
    ADDC,  ; 
: RLC.B, (2)  \ Rotate left through C [ * * * * ] 
    ADDC.B,  ; 
 ' RLC, alias RLC.W, 



\ SBC Emulation: SUBC #0,dst 
: SBC,   (2)  \ Subtract not(C) from destination:  dst + 0FFFFh + C --> dst [ * * * * ] 
    0 s#N  SUBC,  ; 
: SBC.B, (2)  \ Subtract not(C) from destination:  dst + 0FFFFh + C --> dst [ * * * * ] 
    0 s#N  SUBC,  ; 
 ' SBC, alias SBC.W, 



\ SETC Emulation: BIS #1,SR 
: SETC, (2)  ( -- )   \ Set C 1 --> C [ - - - 1 ] 
    1 s#N  SR dRn  BIS,  ; 

' SETC, alias SETC 



\ SETN Emulation: BIS #4,SR 
: SETN, (2)  ( -- )   \ Set N 1 --> N [ - 1 - - ] 
    &4 s#N  SR dRn  BIS,  ; 

' SETN, alias SETN 



\ SETZ Emulation: BIS #2,SR 
: SETZ, (2)  ( -- )   \ Set Z 1 --> C [ - - 1 - ] 
    &2 s#N  SR dRn  BIS,  ; 

' SETZ, alias SETZ 



\ TST Emulation: CMP #0,dst 
: TST,   (2)  \ Test destination:  dst + 0FFFFh + 1 [ 0 * * 1 ] 
    0 s#N  CMP,  ; 
: TST.B, (2)  \ Test destination:  dst + 0FFFFh + 1 [ 0 * * 1 ] 
    0  s#N  CMP.B,  ; 
 ' TST, alias TST.W, 



\ finis
