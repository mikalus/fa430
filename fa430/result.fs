\ Length of MSP430-Assembler: 25548 Bytes 

herem=$0 



 Jxx == format-III instructions

  s"  24    000000              label0:    ; "         0 >label       .lst   $0 <0> 
  s"  25    000000                         ; "                        .lst   $0 <0> 
  s"  26    000000 FF23         JNE label0 ; "         0 label> JNE,  .chk   $0 $FF23 <0> Vop 
  s"  27    000002 FE27         JEQ label0 ; "         0 label> JEQ,  .chk   $2 $FE27 <0> Vop 
  s"  28    000004 FD2B         JNC label0 ; "         0 label> JNC,  .chk   $4 $FD2B <0> Vop 
  s"  29    000006 FC2F         JC  label0 ; "         0 label> JC,   .chk   $6 $FC2F <0> Vop 
  s"  30    000008 FB33         JN  label0 ; "         0 label> JN,   .chk   $8 $FB33 <0> Vop 
  s"  31    00000A FA37         JGE label0 ; "         0 label> JGE,  .chk   $A $FA37 <0> Vop 
  s"  32    00000C F93B         JL  label0 ; "         0 label> JL,   .chk   $C $F93B <0> Vop 
  s"  33    00000E F83F         JMP label0 ; "         0 label> JMP,  .chk   $E $F83F <0> Vop 
  s"  34    000010                         ; "                        .lst   $E $F83F <0> 
  s"  35    000010 F723         JNZ label0 ; "         0 label> JNZ,  .chk   $10 $F723 <0> Vop 
  s"  36    000012 F627         JZ  label0 ; "         0 label> JZ,   .chk   $12 $F627 <0> Vop 
  s"  37    000014 F52B         JLO label0 ; "         0 label> JLO,  .chk   $14 $F52B <0> Vop 
  s"  38    000016 F42F         JHS label0 ; "         0 label> JHS,  .chk   $16 $F42F <0> Vop 
  s"  39    000018                         ; "                        .lst   $16 $F42F <0> 


format-II instructions

  s"  40    000018               EXEC:     ; "                         .lst   $16 $F42F <0> 
  s"  41    000018 B012....     CALL #EXEC ; "    exec #k CALL,        .chk   $18 $B012 $1800 <0> Vop 
  s"  42    00001C 9012FAFF     CALL EXEC ; "    exec addr CALL,       .chk   $1C $9012 $FAFF <0> Vop 
  s"  43    000020 9212....     CALL &EXEC ; "    exec &addr CALL,     .chk   $20 $9212 $1800 <0> Vop 
  s"  44    000024 8512         CALL R5 ; "    5 rn CALL,              .chk   $24 $8512 <0> Vop 
  s"  45    000026 A512         CALL @R5 ; "    5 @rn CALL,            .chk   $26 $A512 <0> Vop 
  s"  46    000028 B512         CALL @R5+ ; "    5 @rn+ CALL,          .chk   $28 $B512 <0> Vop 
  s"  47    00002A 95120001     CALL 0x100(R5) ; " 100 5 x(rn) CALL,   .chk   $2A $9512 $1 <0> Vop 

  s"  48    00002E              ; "                                    .lst   $2A $9512 $1 <0> 
  s"  49    00002E 3012....     PUSH #EXEC ; "    exec #k PUSH,        .chk   $2E $3012 $1800 <0> Vop 
  s"  50    000032 1012E4FF     PUSH EXEC ; "    exec addr PUSH,       .chk   $32 $1012 $E4FF <0> Vop 
  s"  51    000036 1212....     PUSH &EXEC ; "    exec &addr PUSH,     .chk   $36 $1212 $1800 <0> Vop 
  s"  52    00003A 0512         PUSH R5 ; "    5 rn PUSH,              .chk   $3A $512 <0> Vop 
  s"  53    00003C 2512         PUSH @R5 ; "    5 @rn PUSH,            .chk   $3C $2512 <0> Vop 
  s"  54    00003E 3512         PUSH @R5+ ; "    5 @rn+ PUSH,          .chk   $3E $3512 <0> Vop 
  s"  55    000040 15120001     PUSH 0x100(R5) ; " 100 5 x(rn) PUSH,   .chk   $40 $1512 $1 <0> Vop 

  s"  57    000044 70120B00     PUSH.B #11 ; "   &11 #k PUSH.B,              .chk   $44 $7012 $B00 <0> Vop 
  s"  58    000048 5012CEFF     PUSH.B EXEC ; "    exec addr PUSH.B,       .chk   $48 $5012 $CEFF <0> Vop 
  s"  59    00004C 5212....     PUSH.B &EXEC ; "    exec &addr PUSH.B,     .chk   $4C $5212 $1800 <0> Vop 
  s"  60    000050 4512         PUSH.B R5 ; "    5 rn PUSH.B,              .chk   $50 $4512 <0> Vop 
  s"  61    000052 6512         PUSH.B @R5 ; "    5 @rn PUSH.B,            .chk   $52 $6512 <0> Vop 
  s"  62    000054 7512         PUSH.B @R5+ ; "    5 @rn+ PUSH.B,          .chk   $54 $7512 <0> Vop 
  s"  63    000056 55120001     PUSH.B 0x100(R5) ; " 100 5 x(rn) PUSH.B,   .chk   $56 $5512 $1 <0> Vop 

  s"  65    00005A 0013         RETI ; "    RETI,                    .chk   $5A $13 <0> Vop 

  s"  67    00005C              ; RRA #EXEC ; "    ( exec #k RRA, )  .LST   $5A $13 <0> 
  s"  68    00005C 1011BAFF     RRA EXEC ; "    exec addr RRA,       .chk   $5C $1011 $BAFF <0> Vop 
  s"  69    000060 1211....     RRA &EXEC ; "    exec &addr RRA,     .chk   $60 $1211 $1800 <0> Vop 
  s"  70    000064 0511         RRA R5 ; "    5 rn RRA,              .chk   $64 $511 <0> Vop 
  s"  71    000066 2511         RRA @R5 ; "    5 @rn RRA,            .chk   $66 $2511 <0> Vop 
  s"  72    000068 3511         RRA @R5+ ; "    5 @rn+ RRA,          .chk   $68 $3511 <0> Vop 
  s"  73    00006A 15110001     RRA 0x100(R5) ; " 100 5 x(rn) RRA,   .chk   $6A $1511 $1 <0> Vop 

  s"  75    00006E              ; RRA.B #EXEC ; "    ( exec #k RRA.B, )  .LST   $6A $1511 $1 <0> 
  s"  76    00006E 5011A8FF     RRA.B EXEC ; "    exec addr RRA.B,       .chk   $6E $5011 $A8FF <0> Vop 
  s"  77    000072 5211....     RRA.B &EXEC ; "    exec &addr RRA.B,     .chk   $72 $5211 $1800 <0> Vop 
  s"  78    000076 4511         RRA.B R5 ; "    5 rn RRA.B,              .chk   $76 $4511 <0> Vop 
  s"  79    000078 6511         RRA.B @R5 ; "    5 @rn RRA.B,            .chk   $78 $6511 <0> Vop 
  s"  80    00007A 7511         RRA.B @R5+ ; "    5 @rn+ RRA.B,          .chk   $7A $7511 <0> Vop 
  s"  81    00007C 55110001     RRA.B 0x100(R5) ; " 100 5 x(rn) RRA.B,  .chk   $7C $5511 $1 <0> Vop 

  s"  83    000080              ; RRC #EXEC ; "    ( exec #k RRC, )  .LST   $7C $5511 $1 <0> 
  s"  84    000080 101096FF     RRC EXEC ; "    exec addr RRC,       .chk   $80 $1010 $96FF <0> Vop 
  s"  85    000084 1210....     RRC &EXEC ; "    exec &addr RRC,     .chk   $84 $1210 $1800 <0> Vop 
  s"  86    000088 0510         RRC R5 ; "    5 rn RRC,              .chk   $88 $510 <0> Vop 
  s"  87    00008A 2510         RRC @R5 ; "    5 @rn RRC,            .chk   $8A $2510 <0> Vop 
  s"  88    00008C 3510         RRC @R5+ ; "    5 @rn+ RRC,          .chk   $8C $3510 <0> Vop 
  s"  89    00008E 15100001     RRC 0x100(R5) ; "   100 5 x(rn) RRC, .chk   $8E $1510 $1 <0> Vop 

  s"  91    000092              ; RRC.B #EXEC ; "    ( exec #k RRC.B, )   .LST   $8E $1510 $1 <0> 
  s"  92    000092 501084FF     RRC.B EXEC ; "    exec addr RRC.B,        .chk   $92 $5010 $84FF <0> Vop 
  s"  93    000096 5210....     RRC.B &EXEC ; "    exec &addr RRC.B,      .chk   $96 $5210 $1800 <0> Vop 
  s"  94    00009A 4510         RRC.B R5 ; "    5 rn RRC.B,               .chk   $9A $4510 <0> Vop 
  s"  95    00009C 6510         RRC.B @R5 ; "    5 @rn RRC.B,             .chk   $9C $6510 <0> Vop 
  s"  96    00009E 7510         RRC.B @R5+ ; "    5 @rn+ RRC.B,           .chk   $9E $7510 <0> Vop 
  s"  97    0000A0 55100001     RRC.B 0x100(R5) ; "    100 5 x(rn) RRC.B, .chk   $A0 $5510 $1 <0> Vop 

  s"  99    0000A4              ;SWPB #EXEC ; "    ( exec #k SWPB, )     .LST   $A0 $5510 $1 <0> 
  s" 100    0000A4 901072FF     SWPB EXEC ; "    exec addr SWPB,         .chk   $A4 $9010 $72FF <0> Vop 
  s" 101    0000A8 9210....     SWPB &EXEC ; "    exec &addr SWPB,       .chk   $A8 $9210 $1800 <0> Vop 
  s" 102    0000AC 8510         SWPB R5 ; "    5 rn SWPB,                .chk   $AC $8510 <0> Vop 
  s" 103    0000AE A510         SWPB @R5 ; "    5 @rn SWPB,              .chk    $AE $A510 <0> Vop 
  s" 104    0000B0 B510         SWPB @R5+ ; "    5 @rn+ SWPB,            .chk   $B0 $B510 <0> Vop 
  s" 105    0000B2 95100001     SWPB 0x100(R5) ; " 100 5 x(rn) SWPB,     .chk   $B2 $9510 $1 <0> Vop 

  s" 107    0000B6              ; SXT #EXEC ; "    ( exec #k SXT, )      .LST   $B2 $9510 $1 <0> 
  s" 108    0000B6 901160FF     SXT EXEC ; "    exec addr SXT,           .chk   $B6 $9011 $60FF <0> Vop 
  s" 109    0000BA 9211....     SXT &EXEC ; "    exec &addr SXT,         .chk   $BA $9211 $1800 <0> Vop 
  s" 110    0000BE 8511         SXT R5 ; "    5 rn SXT,                  .chk   $BE $8511 <0> Vop 
  s" 111    0000C0 A511         SXT @R5 ; "    5 @rn SXT,                .chk   $C0 $A511 <0> Vop 
  s" 112    0000C2 B511         SXT @R5+ ; "    5 @rn+ SXT,              .chk   $C2 $B511 <0> Vop 
  s" 113    0000C4 95110001     SXT 0x100(R5) ; " 100 5 x(rn) SXT,       .chk   $C4 $9511 $1 <0> Vop 


format-I instructions, tested using mov

  s" 116    0000C8              EXECm: ; "    herem constant execm .lst   $C4 $9511 $1 <0> 
  s" 117    0000C8              LABELm: ; "    herem constant labelm .lst   $C4 $9511 $1 <0> 

  s" 119    0000C8              ; 1 drn " .lst   $C4 $9511 $1 <0> 
  s" 120    0000C8 3440....     MOV #EXECm,r4 ; "    execm s#k 4 drn MOV,       .chk   $C8 $3440 $C800 <0> Vop 
  s" 121    0000CC 1440FAFF     MOV EXECm,r4 ; "    execm saddr 4 drn MOV,      .chk   $CC $1440 $FAFF <0> Vop 
  s" 122    0000D0 1442....     MOV &EXECm,r4 ; "    execm s&addr 4 drn MOV,    .chk   $D0 $1442 $C800 <0> Vop 
  s" 123    0000D4 0445         MOV R5,r4 ; "    5 srn 4 drn MOV,               .chk   $D4 $445 <0> Vop 
  s" 124    0000D6 2445         MOV @R5,r4 ; "    5 s@rn 4 drn MOV,             .chk   $D6 $2445 <0> Vop 
  s" 125    0000D8 3445         MOV @R5+,r4 ; "    5 s@rn+ 4 drn MOV,           .chk   $D8 $3445 <0> Vop 
  s" 126    0000DA 14450001     MOV 0x100(R5),r4 ; "    100 5 sx(rn) 4 drn MOV, .chk   $DA $1445 $1 <0> Vop 
  s" 128    0000DE              ; 2 dX(Rn) " .lst   $DA $1445 $1 <0> 
  s" 129    0000DE B440....0A00 MOV #EXECm,10(r4)  ; "    execm s#k 10 4 dX(rn) MOV,       .chk   $DE $B440 $C800 $1000 <0> Vop 
  s" 130    0000E4 9440E2FF0A00 MOV EXECm,10(r4)  ; "    execm saddr 10 4 dX(rn) MOV,      .chk   $E4 $9440 $E2FF $1000 <0> Vop 
  s" 131    0000EA 9442....0A00 MOV &EXECm,10(r4)  ; "    execm s&addr 10 4 dX(rn) MOV,    .chk   $EA $9442 $C800 $1000 <0> Vop 
  s" 132    0000F0 84450A00     MOV R5,10(r4)  ; "    5 srn 10 4 dX(rn) MOV,               .chk   $F0 $8445 $1000 <0> Vop 
  s" 133    0000F4 A4450A00     MOV @R5,10(r4)  ; "    5 s@rn 10 4 dX(rn) MOV,             .chk   $F4 $A445 $1000 <0> Vop 
  s" 134    0000F8 B4450A00     MOV @R5+,10(r4)  ; "    5 s@rn+ 10 4 dX(rn) MOV,           .chk   $F8 $B445 $1000 <0> Vop 
  s" 135    0000FC 944500010A00 MOV 0x100(R5),10(r4)  ; "    100 5 sx(rn) 10 4 dX(rn) MOV, .chk   $FC $9445 $1 $1000 <0> Vop 

  s" 137    000102              ; 3 dADDR " .lst   $FC $9445 $1 $1000 <0> 
  s" 138    000102 B040....C2FF MOV #EXECm,LABELm ; "    execm s#k labelm daddr MOV,        .chk   $102 $B040 $C800 $C2FF <0> Vop 
  s" 139    000108 9040BEFFBCFF MOV EXECm,LABELm ; "    execm saddr labelm daddr MOV,       .chk   $108 $9040 $BEFF $BCFF <0> Vop 
  s" 140    00010E 9042....B6FF MOV &EXECm,LABELm ; "    execm s&addr labelm daddr MOV,     .chk   $10E $9042 $C800 $B6FF <0> Vop 
  s" 141    000114 8045B2FF     MOV R5,LABELm ; "    5 srn labelm daddr MOV,                .chk   $114 $8045 $B0FF <0> Vop 
  s" 142    000118 A045AEFF     MOV @R5,LABELm ; "    5 s@rn labelm daddr MOV,              .chk   $118 $A045 $ACFF <0> Vop 
  s" 143    00011C B045AAFF     MOV @R5+,LABELm ; "    5 s@rn+ labelm daddr MOV,            .chk   $11C $B045 $A8FF <0> Vop 
  s" 144    000120 90450001A4FF MOV 0x100(R5) ,LABELm ; "    100 5 sx(rn) labelm daddr MOV, .chk   $120 $9045 $1 $A4FF <0> Vop 

  s" 146    000126              ; 4 d&ADDR " .lst   $120 $9045 $1 $A4FF <0> 
  s" 147    000126 B240........ MOV #EXECm,&LABELm ; "    execm s#k labelm d&addr MOV,        .chk   $126 $B240 $C800 $C800 <0> Vop 
  s" 148    00012C 92409AFF.... MOV EXECm,&LABELm ; "    execm saddr labelm d&addr MOV,       .chk   $12C $9240 $9AFF $C800 <0> Vop 
  s" 149    000132 9242........ MOV &EXECm,&LABELm ; "    execm s&addr labelm d&addr MOV,     .chk   $132 $9242 $C800 $C800 <0> Vop 
  s" 150    000138 8245....     MOV R5,&LABELm ; "    5 srn labelm d&addr MOV,                .chk   $138 $8245 $C800 <0> Vop 
  s" 151    00013C A245....     MOV @R5,&LABELm ; "    5 s@rn labelm d&addr MOV,              .chk   $13C $A245 $C800 <0> Vop 
  s" 152    000140 B245....     MOV @R5+,&LABELm ; "    5 s@rn+ labelm d&addr MOV,            .chk   $140 $B245 $C800 <0> Vop 
  s" 153    000144 92450001.... MOV 0x100(R5) ,&LABELm ; "    100 5 sx(rn) labelm d&addr MOV, .chk   $144 $9245 $1 $C800 <0> Vop 


CG1 and CG2 tested using mov instruction.

  s" 157    00014A 0543         MOV #0,r5 ; "    0#  5 dRn mov, .chk    $14A $543 <0> Vop 
  s" 158    00014C 1543         MOV #1,r5 ; "    1#  5 dRn mov, .chk    $14C $1543 <0> Vop 
  s" 159    00014E 2543         MOV #2,r5 ; "    2#  5 dRn mov, .chk    $14E $2543 <0> Vop 
  s" 160    000150 2542         MOV #4,r5 ; "    4#  5 dRn mov, .chk    $150 $2542 <0> Vop 
  s" 161    000152 3542         MOV #8,r5 ; "    8#  5 dRn mov, .chk    $152 $3542 <0> Vop 
  s" 162    000154 3543         MOV #0xFFFF,r5 ; "    ffff#  5 dRn mov, .chk   $154 $3543 <0> Vop 


0 non matching opcodes
