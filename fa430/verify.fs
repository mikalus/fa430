\ verify all MSP430 instructions.


verification

cr cr cr .(  Jxx == format-III instructions)   
cr
  s"  24    000000              label0:    ; "         0 >label       .lst
  s"  25    000000                         ; "                        .lst
  s"  26    000000 FF23         JNE label0 ; "         0 label> JNE,  .chk
  s"  27    000002 FE27         JEQ label0 ; "         0 label> JEQ,  .chk
  s"  28    000004 FD2B         JNC label0 ; "         0 label> JNC,  .chk
  s"  29    000006 FC2F         JC  label0 ; "         0 label> JC,   .chk
  s"  30    000008 FB33         JN  label0 ; "         0 label> JN,   .chk
  s"  31    00000A FA37         JGE label0 ; "         0 label> JGE,  .chk
  s"  32    00000C F93B         JL  label0 ; "         0 label> JL,   .chk
  s"  33    00000E F83F         JMP label0 ; "         0 label> JMP,  .chk
  s"  34    000010                         ; "                        .lst
  s"  35    000010 F723         JNZ label0 ; "         0 label> JNZ,  .chk
  s"  36    000012 F627         JZ  label0 ; "         0 label> JZ,   .chk
  s"  37    000014 F52B         JLO label0 ; "         0 label> JLO,  .chk
  s"  38    000016 F42F         JHS label0 ; "         0 label> JHS,  .chk
  s"  39    000018                         ; "                        .lst


cr cr cr .( format-II instructions) 
cr herem constant exec 
  s"  40    000018               EXEC:     ; "                         .lst
  s"  41    000018 B012....     CALL #EXEC ; "    exec #k CALL,        .chk
  s"  42    00001C 9012FAFF     CALL EXEC ; "    exec addr CALL,       .chk
  s"  43    000020 9212....     CALL &EXEC ; "    exec &addr CALL,     .chk
  s"  44    000024 8512         CALL R5 ; "    5 rn CALL,              .chk
  s"  45    000026 A512         CALL @R5 ; "    5 @rn CALL,            .chk
  s"  46    000028 B512         CALL @R5+ ; "    5 @rn+ CALL,          .chk
  s"  47    00002A 95120001     CALL 0x100(R5) ; " 100 5 x(rn) CALL,   .chk
cr                                               
  s"  48    00002E              ; "                                    .lst
  s"  49    00002E 3012....     PUSH #EXEC ; "    exec #k PUSH,        .chk
  s"  50    000032 1012E4FF     PUSH EXEC ; "    exec addr PUSH,       .chk
  s"  51    000036 1212....     PUSH &EXEC ; "    exec &addr PUSH,     .chk
  s"  52    00003A 0512         PUSH R5 ; "    5 rn PUSH,              .chk
  s"  53    00003C 2512         PUSH @R5 ; "    5 @rn PUSH,            .chk
  s"  54    00003E 3512         PUSH @R5+ ; "    5 @rn+ PUSH,          .chk
  s"  55    000040 15120001     PUSH 0x100(R5) ; " 100 5 x(rn) PUSH,   .chk
cr                                               
  s"  57    000044 70120B00     PUSH.B #11 ; "   &11 #k PUSH.B,              .chk
  s"  58    000048 5012CEFF     PUSH.B EXEC ; "    exec addr PUSH.B,       .chk
  s"  59    00004C 5212....     PUSH.B &EXEC ; "    exec &addr PUSH.B,     .chk
  s"  60    000050 4512         PUSH.B R5 ; "    5 rn PUSH.B,              .chk
  s"  61    000052 6512         PUSH.B @R5 ; "    5 @rn PUSH.B,            .chk
  s"  62    000054 7512         PUSH.B @R5+ ; "    5 @rn+ PUSH.B,          .chk
  s"  63    000056 55120001     PUSH.B 0x100(R5) ; " 100 5 x(rn) PUSH.B,   .chk
cr
  s"  65    00005A 0013         RETI ; "    RETI,                    .chk
cr
  s"  67    00005C              ; RRA #EXEC ; "    ( exec #k RRA, )  .LST
  s"  68    00005C 1011BAFF     RRA EXEC ; "    exec addr RRA,       .chk
  s"  69    000060 1211....     RRA &EXEC ; "    exec &addr RRA,     .chk
  s"  70    000064 0511         RRA R5 ; "    5 rn RRA,              .chk
  s"  71    000066 2511         RRA @R5 ; "    5 @rn RRA,            .chk
  s"  72    000068 3511         RRA @R5+ ; "    5 @rn+ RRA,          .chk
  s"  73    00006A 15110001     RRA 0x100(R5) ; " 100 5 x(rn) RRA,   .chk
cr
  s"  75    00006E              ; RRA.B #EXEC ; "    ( exec #k RRA.B, )  .LST
  s"  76    00006E 5011A8FF     RRA.B EXEC ; "    exec addr RRA.B,       .chk
  s"  77    000072 5211....     RRA.B &EXEC ; "    exec &addr RRA.B,     .chk
  s"  78    000076 4511         RRA.B R5 ; "    5 rn RRA.B,              .chk
  s"  79    000078 6511         RRA.B @R5 ; "    5 @rn RRA.B,            .chk
  s"  80    00007A 7511         RRA.B @R5+ ; "    5 @rn+ RRA.B,          .chk
  s"  81    00007C 55110001     RRA.B 0x100(R5) ; " 100 5 x(rn) RRA.B,  .chk
cr
  s"  83    000080              ; RRC #EXEC ; "    ( exec #k RRC, )  .LST
  s"  84    000080 101096FF     RRC EXEC ; "    exec addr RRC,       .chk
  s"  85    000084 1210....     RRC &EXEC ; "    exec &addr RRC,     .chk
  s"  86    000088 0510         RRC R5 ; "    5 rn RRC,              .chk
  s"  87    00008A 2510         RRC @R5 ; "    5 @rn RRC,            .chk
  s"  88    00008C 3510         RRC @R5+ ; "    5 @rn+ RRC,          .chk
  s"  89    00008E 15100001     RRC 0x100(R5) ; "   100 5 x(rn) RRC, .chk
cr                                              
  s"  91    000092              ; RRC.B #EXEC ; "    ( exec #k RRC.B, )   .LST
  s"  92    000092 501084FF     RRC.B EXEC ; "    exec addr RRC.B,        .chk
  s"  93    000096 5210....     RRC.B &EXEC ; "    exec &addr RRC.B,      .chk
  s"  94    00009A 4510         RRC.B R5 ; "    5 rn RRC.B,               .chk
  s"  95    00009C 6510         RRC.B @R5 ; "    5 @rn RRC.B,             .chk
  s"  96    00009E 7510         RRC.B @R5+ ; "    5 @rn+ RRC.B,           .chk
  s"  97    0000A0 55100001     RRC.B 0x100(R5) ; "    100 5 x(rn) RRC.B, .chk
cr                                                
  s"  99    0000A4              ;SWPB #EXEC ; "    ( exec #k SWPB, )     .LST
  s" 100    0000A4 901072FF     SWPB EXEC ; "    exec addr SWPB,         .chk
  s" 101    0000A8 9210....     SWPB &EXEC ; "    exec &addr SWPB,       .chk
  s" 102    0000AC 8510         SWPB R5 ; "    5 rn SWPB,                .chk
  s" 103    0000AE A510         SWPB @R5 ; "    5 @rn SWPB,              .chk 
  s" 104    0000B0 B510         SWPB @R5+ ; "    5 @rn+ SWPB,            .chk
  s" 105    0000B2 95100001     SWPB 0x100(R5) ; " 100 5 x(rn) SWPB,     .chk
cr
  s" 107    0000B6              ; SXT #EXEC ; "    ( exec #k SXT, )      .LST
  s" 108    0000B6 901160FF     SXT EXEC ; "    exec addr SXT,           .chk
  s" 109    0000BA 9211....     SXT &EXEC ; "    exec &addr SXT,         .chk
  s" 110    0000BE 8511         SXT R5 ; "    5 rn SXT,                  .chk
  s" 111    0000C0 A511         SXT @R5 ; "    5 @rn SXT,                .chk
  s" 112    0000C2 B511         SXT @R5+ ; "    5 @rn+ SXT,              .chk
  s" 113    0000C4 95110001     SXT 0x100(R5) ; " 100 5 x(rn) SXT,       .chk



cr cr cr .( format-I instructions, tested using mov)
cr 
  s" 116    0000C8              EXECm: ; "    herem constant execm .lst
  s" 117    0000C8              LABELm: ; "    herem constant labelm .lst
cr
  s" 119    0000C8              ; 1 drn " .lst
  s" 120    0000C8 3440....     MOV #EXECm,r4 ; "    execm s#k 4 drn MOV,       .chk
  s" 121    0000CC 1440FAFF     MOV EXECm,r4 ; "    execm saddr 4 drn MOV,      .chk
  s" 122    0000D0 1442....     MOV &EXECm,r4 ; "    execm s&addr 4 drn MOV,    .chk
  s" 123    0000D4 0445         MOV R5,r4 ; "    5 srn 4 drn MOV,               .chk
  s" 124    0000D6 2445         MOV @R5,r4 ; "    5 s@rn 4 drn MOV,             .chk
  s" 125    0000D8 3445         MOV @R5+,r4 ; "    5 s@rn+ 4 drn MOV,           .chk
  s" 126    0000DA 14450001     MOV 0x100(R5),r4 ; "    100 5 sx(rn) 4 drn MOV, .chk

  s" 128    0000DE              ; 2 dX(Rn) " .lst
  s" 129    0000DE B440....0A00 MOV #EXECm,10(r4)  ; "    execm s#k 10 4 dX(rn) MOV,       .chk
  s" 130    0000E4 9440E2FF0A00 MOV EXECm,10(r4)  ; "    execm saddr 10 4 dX(rn) MOV,      .chk
  s" 131    0000EA 9442....0A00 MOV &EXECm,10(r4)  ; "    execm s&addr 10 4 dX(rn) MOV,    .chk
  s" 132    0000F0 84450A00     MOV R5,10(r4)  ; "    5 srn 10 4 dX(rn) MOV,               .chk
  s" 133    0000F4 A4450A00     MOV @R5,10(r4)  ; "    5 s@rn 10 4 dX(rn) MOV,             .chk
  s" 134    0000F8 B4450A00     MOV @R5+,10(r4)  ; "    5 s@rn+ 10 4 dX(rn) MOV,           .chk
  s" 135    0000FC 944500010A00 MOV 0x100(R5),10(r4)  ; "    100 5 sx(rn) 10 4 dX(rn) MOV, .chk
cr
  s" 137    000102              ; 3 dADDR " .lst
  s" 138    000102 B040....C2FF MOV #EXECm,LABELm ; "    execm s#k labelm daddr MOV,        .chk
  s" 139    000108 9040BEFFBCFF MOV EXECm,LABELm ; "    execm saddr labelm daddr MOV,       .chk
  s" 140    00010E 9042....B6FF MOV &EXECm,LABELm ; "    execm s&addr labelm daddr MOV,     .chk
  s" 141    000114 8045B2FF     MOV R5,LABELm ; "    5 srn labelm daddr MOV,                .chk
  s" 142    000118 A045AEFF     MOV @R5,LABELm ; "    5 s@rn labelm daddr MOV,              .chk
  s" 143    00011C B045AAFF     MOV @R5+,LABELm ; "    5 s@rn+ labelm daddr MOV,            .chk
  s" 144    000120 90450001A4FF MOV 0x100(R5) ,LABELm ; "    100 5 sx(rn) labelm daddr MOV, .chk
cr
  s" 146    000126              ; 4 d&ADDR " .lst
  s" 147    000126 B240........ MOV #EXECm,&LABELm ; "    execm s#k labelm d&addr MOV,        .chk
  s" 148    00012C 92409AFF.... MOV EXECm,&LABELm ; "    execm saddr labelm d&addr MOV,       .chk
  s" 149    000132 9242........ MOV &EXECm,&LABELm ; "    execm s&addr labelm d&addr MOV,     .chk
  s" 150    000138 8245....     MOV R5,&LABELm ; "    5 srn labelm d&addr MOV,                .chk
  s" 151    00013C A245....     MOV @R5,&LABELm ; "    5 s@rn labelm d&addr MOV,              .chk
  s" 152    000140 B245....     MOV @R5+,&LABELm ; "    5 s@rn+ labelm d&addr MOV,            .chk
  s" 153    000144 92450001.... MOV 0x100(R5) ,&LABELm ; "    100 5 sx(rn) labelm d&addr MOV, .chk



cr cr cr .( CG1 and CG2 tested using mov instruction.)
cr
  s" 157    00014A 0543         MOV #0,r5 ; "    0#  5 dRn mov, .chk 
  s" 158    00014C 1543         MOV #1,r5 ; "    1#  5 dRn mov, .chk 
  s" 159    00014E 2543         MOV #2,r5 ; "    2#  5 dRn mov, .chk 
  s" 160    000150 2542         MOV #4,r5 ; "    4#  5 dRn mov, .chk 
  s" 161    000152 3542         MOV #8,r5 ; "    8#  5 dRn mov, .chk 
  s" 162    000154 3543         MOV #0xFFFF,r5 ; "    ffff#  5 dRn mov, .chk
cr
0 [if] (I was just curious ;-) 
  s" 164    000156 0543         MOV #0,r5 ; "    0 s#k  5 dRn mov, .chk 
  s" 165    000158 1543         MOV #1,r5 ; "    1 s#k  5 dRn mov, .chk 
  s" 166    00015A 2543         MOV #2,r5 ; "    2 s#k  5 dRn mov, .chk 
  s" 167    00015C 35400300     MOV #3,r5 ; "    3 s#k  5 dRn mov, .chk 
  s" 168    000160 2542         MOV #4,r5 ; "    4 s#k  5 dRn mov, .chk 
  s" 169    000162 35400500     MOV #5,r5 ; "    5 s#k  5 dRn mov, .chk 
  s" 170    000166 35400600     MOV #6,r5 ; "    6 s#k  5 dRn mov, .chk 
  s" 171    00016A 35400700     MOV #7,r5 ; "    7 s#k  5 dRn mov, .chk 
  s" 172    00016E 3542         MOV #8,r5 ; "    8 s#k  5 dRn mov, .chk 
  s" 173    000170 35400900     MOV #9,r5 ; "    9 s#k  5 dRn mov, .chk 
  s" 174    000174 3543         MOV #0xFFFF,r5 ; "    $FFFF s#k  5 dRn mov, .chk
[then]
cr .result
cr
