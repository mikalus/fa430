\ Length of MSP430-Assembler: 24964 Bytes 



herem=$0 
$0    (       24    000000              label0:    ; )  0 >label  .lst<0> 
$0    (       25    000000                         ; )  .lst<0> 
$0 $FF23    (       26    000000 FF23         JNE label0 ; )  0 label> JNE,  .lst<0> 
$2 $FE27    (       27    000002 FE27         JEQ label0 ; )  0 label> JEQ, .lst<0> 
$4 $FD2B    (       28    000004 FD2B         JNC label0 ; )  0 label> JNC, .lst<0> 
$6 $FC2F    (       29    000006 FC2F         JC  label0 ; )  0 label> JC,  .lst<0> 
$8 $FB33    (       30    000008 FB33         JN  label0 ; )  0 label> JN, .lst<0> 
$A $FA37    (       31    00000A FA37         JGE label0 ; )  0 label> JGE,  .lst<0> 
$C $F93B    (       32    00000C F93B         JL  label0 ; )  0 label> JL, .lst<0> 
$E $F83F    (       33    00000E F83F         JMP label0 ; )  0 label> JMP, .lst<0> 
$E $F83F    (       34    000010                         ; )  .lst<0> 
$10 $F723    (       35    000010 F723         JNZ label0 ; )  0 label> JNZ, .lst<0> 
$12 $F627    (       36    000012 F627         JZ  label0 ; )  0 label> JZ, .lst<0> 
$14 $F52B    (       37    000014 F52B         JLO label0 ; )  0 label> JLO, .lst<0> 
$16 $F42F    (       38    000016 F42F         JHS label0 ; )  0 label> JHS,  .lst<0> 
$16 $F42F    (       39    000018                         ; )  .lst<0> 

$16 $F42F      (  40    000018               EXEC:     ;  ) .lst<0> 
$18 $8012 $1800      (  41    000018 B012....     CALL #EXEC ; ) exec #k CALL, .lst<0> 
$1C $9012 $FAFF      (  42    00001C 9012FAFF     CALL EXEC ; ) exec addr CALL, .lst<0> 
$20 $9212 $1800      (  43    000020 9212....     CALL &EXEC ; ) exec &addr CALL, .lst<0> 
$24 $8512      (  44    000024 8512         CALL R5 ; ) 5 rn CALL, .lst<0> 
$26 $A512      (  45    000026 A512         CALL @R5 ; ) 5 @rn CALL, .lst<0> 
$28 $B512      (  46    000028 B512         CALL @R5+ ; ) 5 @rn+ CALL, .lst<0> 
$2A $9512 $1      (  47    00002A 95120001     CALL 0x100(R5 ; ) 100 5 x(rn) CALL, .lst<0> 

$2A $9512 $1      (  48    00002E              ; ) .lst<0> 
$2E $8012 $1800      (  49    00002E 3012....     PUSH #EXEC ; ) exec #k PUSH, .lst<0> 
$32 $1012 $E4FF      (  50    000032 1012E4FF     PUSH EXEC ; ) exec addr PUSH, .lst<0> 
$36 $1212 $1800      (  51    000036 1212....     PUSH &EXEC ; ) exec &addr PUSH, .lst<0> 
$3A $512      (  52    00003A 0512         PUSH R5 ; ) 5 rn PUSH, .lst<0> 
$3C $2512      (  53    00003C 2512         PUSH @R5 ; ) 5 @rn PUSH, .lst<0> 
$3E $3512      (  54    00003E 3512         PUSH @R5+ ; ) 5 @rn+ PUSH, .lst<0> 
$40 $1512 $1      (  55    000040 15120001     PUSH 0x100(R5 ; ) 100 5 x(rn) PUSH, .lst<0> 

$44 $8012 $1100      (  57    000044 70120B00     PUSH.B #11 ; ) 11 #k PUSH, .lst<0> 
$48 $5012 $CEFF      (  58    000048 5012CEFF     PUSH.B EXEC ; ) exec addr PUSH.B, .lst<0> 
$4C $5212 $1800      (  59    00004C 5212....     PUSH.B &EXEC ; ) exec &addr PUSH.B, .lst<0> 
$50 $4512      (  60    000050 4512         PUSH.B R5 ; ) 5 rn PUSH.B, .lst<0> 
$52 $6512      (  61    000052 6512         PUSH.B @R5 ; ) 5 @rn PUSH.B, .lst<0> 
$54 $7512      (  62    000054 7512         PUSH.B @R5+ ; ) 5 @rn+ PUSH.B, .lst<0> 
$56 $5512 $1      (  63    000056 55120001     PUSH.B 0x100(R5 ; ) 100 5 x(rn) PUSH.B, .lst<0> 

$5A $13      (  65    00005A 3041         RET ; ) ( no addressmodes) RET, .lst<0> 

$5C $1011 $BAFF      (  68    00005C 1011BAFF     RRA EXEC ; ) exec addr RRA, .lst<0> 
$60 $1211 $1800      (  69    000060 1211....     RRA &EXEC ; ) exec &addr RRA, .lst<0> 
$64 $511      (  70    000064 0511         RRA R5 ; ) 5 rn RRA, .lst<0> 
$66 $2511      (  71    000066 2511         RRA @R5 ; ) 5 @rn RRA, .lst<0> 
$68 $3511      (  72    000068 3511         RRA @R5+ ; ) 5 @rn+ RRA, .lst<0> 
$6A $1511 $1      (  73    00006A 15110001     RRA 0x100(R5 ; ) 100 5 x(rn) RRA, .lst<0> 

$6E $5011 $A8FF      (  76    00006E 5011A8FF     RRA.B EXEC ; ) exec addr RRA.B, .lst<0> 
$72 $5211 $1800      (  77    000072 5211....     RRA.B &EXEC ; ) exec &addr RRA.B, .lst<0> 
$76 $4511      (  78    000076 4511         RRA.B R5 ; ) 5 rn RRA.B, .lst<0> 
$78 $6511      (  79    000078 6511         RRA.B @R5 ; ) 5 @rn RRA.B, .lst<0> 
$7A $7511      (  80    00007A 7511         RRA.B @R5+ ; ) 5 @rn+ RRA.B, .lst<0> 
$7C $5511 $1      (  81    00007C 55110001     RRA.B 0x100(R5 ; ) 100 5 x(rn) RRA.B, .lst<0> 

$80 $1010 $96FF      (  84    000080 101096FF     RRC EXEC ; ) exec addr RRC, .lst<0> 
$84 $1210 $1800      (  85    000084 1210....     RRC &EXEC ; ) exec &addr RRC, .lst<0> 
$88 $510      (  86    000088 0510         RRC R5 ; ) 5 rn RRC, .lst<0> 
$8A $2510      (  87    00008A 2510         RRC @R5 ; ) 5 @rn RRC, .lst<0> 
$8C $3510      (  88    00008C 3510         RRC @R5+ ; ) 5 @rn+ RRC, .lst<0> 
$8E $1510 $1      (  89    00008E 15100001     RRC 0x100(R5 ; ) 100 5 x(rn) RRC, .lst<0> 

$92 $5010 $84FF      (  92    000092 501084FF     RRC.B EXEC ; ) exec addr RRC.B, .lst<0> 
$96 $5210 $1800      (  93    000096 5210....     RRC.B &EXEC ; ) exec &addr RRC.B, .lst<0> 
$9A $4510      (  94    00009A 4510         RRC.B R5 ; ) 5 rn RRC.B, .lst<0> 
$9C $6510      (  95    00009C 6510         RRC.B @R5 ; ) 5 @rn RRC.B, .lst<0> 
$9E $7510      (  96    00009E 7510         RRC.B @R5+ ; ) 5 @rn+ RRC.B, .lst<0> 
$A0 $5510 $1      (  97    0000A0 55100001     RRC.B 0x100(R5 ; ) 100 5 x(rn) RRC.B, .lst<0> 

$A4 $9010 $72FF      ( 100    0000A4 901072FF     SWPB EXEC ; ) exec addr SWPB, .lst<0> 
$A8 $9210 $1800      ( 101    0000A8 9210....     SWPB &EXEC ; ) exec &addr SWPB, .lst<0> 
$AC $8510      ( 102    0000AC 8510         SWPB R5 ; ) 5 rn SWPB, .lst<0> 
$AE $A510      ( 103    0000AE A510         SWPB @R5 ; ) 5 @rn SWPB, .lst <0> 
$B0 $B510      ( 104    0000B0 B510         SWPB @R5+ ; ) 5 @rn+ SWPB, .lst<0> 
$B2 $9510 $1      ( 105    0000B2 95100001     SWPB 0x100(R5 ; ) 100 5 x(rn) SWPB, .lst<0> 

$B6 $9011 $60FF      ( 108    0000B6 901160FF     SXT EXEC ; ) exec addr SXT, .lst<0> 
$BA $9211 $1800      ( 109    0000BA 9211....     SXT &EXEC ; ) exec &addr SXT, .lst<0> 
$BE $8511      ( 110    0000BE 8511         SXT R5 ; ) 5 rn SXT, .lst<0> 
$C0 $A511      ( 111    0000C0 A511         SXT @R5 ; ) 5 @rn SXT, .lst<0> 
$C2 $B511      ( 112    0000C2 B511         SXT @R5+ ; ) 5 @rn+ SXT, .lst<0> 
$C4 $9511 $1      ( 113    0000C4 95110001     SXT 0x100(R5 ; ) 100 5 x(rn) SXT, .lst<0> 



$C4 $9511 $1     (  119    0000C8              ; 1 drn ) .lst<0> 
$C8 $3440 $C800     (  120    0000C8 3440....     MOV #EXECm,r4 ; ) execm s#k 4 drn MOV, .lst<0> 
$CC $1440 $FAFF     (  121    0000CC 1440FAFF     MOV EXECm,r4 ; ) execm saddr 4 drn MOV, .lst<0> 
$D0 $1442 $C800     (  122    0000D0 1442....     MOV &EXECm,r4 ; ) execm s&addr 4 drn MOV, .lst<0> 
$D4 $445     (  123    0000D4 0445         MOV R5,r4 ; ) 5 srn 4 drn MOV, .lst<0> 
$D6 $2445     (  124    0000D6 2445         MOV @R5,r4 ; ) 5 s@rn 4 drn MOV, .lst<0> 
$D8 $3445     (  125    0000D8 3445         MOV @R5+,r4 ; ) 5 s@rn+ 4 drn MOV, .lst<0> 
$DA $1445 $1     (  126    0000DA 14450001     MOV 0x100(R5 ,r4 ; ) 100 5 sx(rn) 4 drn MOV, .lst<0> 
$DA $1445 $1     (  128    0000DE              ; 2 dX(Rn) .lst<0> 
$E4 $9440 $E2FF $1000     (  130    0000E4 9440E2FF0A00 MOV EXECm,10(r4  ; ) execm saddr 10 4 dX(rn) MOV, .lst<0> 
$EA $9442 $C800 $1000     (  131    0000EA 9442....0A00 MOV &EXECm,10(r4  ; ) execm s&addr 10 4 dX(rn) MOV, .lst<0> 
$F0 $8445 $1000     (  132    0000F0 84450A00     MOV R5,10(r4  ; ) 5 srn 10 4 dX(rn) MOV, .lst<0> 
$F4 $A445 $1000     (  133    0000F4 A4450A00     MOV @R5,10(r4  ; ) 5 s@rn 10 4 dX(rn) MOV, .lst<0> 
$F8 $B445 $1000     (  134    0000F8 B4450A00     MOV @R5+,10(r4  ; ) 5 s@rn+ 10 4 dX(rn) MOV, .lst<0> 
$FC $9445 $1 $1000     (  135    0000FC 944500010A00 MOV 0x100(R5 ,10(r4  ; ) 100 5 sx(rn) 10 4 dX(rn) MOV, .lst<0> 

$FC $9445 $1 $1000     (  137    000102              ; 3 dADDR ) .lst<0> 
$102 $B040 $C800 $C2FF     (  138    000102 B040....C2FF MOV #EXECm,LABELm ; ) execm s#k labelm daddr MOV, .lst<0> 
$108 $9040 $BEFF $BCFF     (  139    000108 9040BEFFBCFF MOV EXECm,LABELm ; ) execm saddr labelm daddr MOV, .lst<0> 
$10E $9042 $C800 $B6FF     (  140    00010E 9042....B6FF MOV &EXECm,LABELm ; ) execm s&addr labelm daddr MOV, .lst<0> 
$114 $8045 $B0FF     (  141    000114 8045B2FF     MOV R5,LABELm ; ) 5 srn labelm daddr MOV, .lst<0> 
$118 $A045 $ACFF     (  142    000118 A045AEFF     MOV @R5,LABELm ; ) 5 s@rn labelm daddr MOV, .lst<0> 
$11C $B045 $A8FF     (  143    00011C B045AAFF     MOV @R5+,LABELm ; ) 5 s@rn+ labelm daddr MOV, .lst<0> 
$120 $9045 $1 $A4FF     (  144    000120 90450001A4FF MOV 0x100(R5 ,LABELm ; ) 100 5 sx(rn) labelm daddr MOV, .lst<0> 

$120 $9045 $1 $A4FF     ( 146    000126              ; 4 d&ADDR ) .lst<0> 
$126 $B240 $C800 $C800     (  147    000126 B240........ MOV #EXECm,&LABELm ; ) execm s#k labelm d&addr MOV, .lst<0> 
$12C $9240 $9AFF $C800     (  148    00012C 92409AFF.... MOV EXECm,&LABELm ; ) execm saddr labelm d&addr MOV, .lst<0> 
$132 $9242 $C800 $C800     (  149    000132 9242........ MOV &EXECm,&LABELm ; ) execm s&addr labelm d&addr MOV, .lst<0> 
$138 $8245 $C800     (  150    000138 8245....     MOV R5,&LABELm ; ) 5 srn labelm d&addr MOV, .lst<0> 
$13C $A245 $C800     (  151    00013C A245....     MOV @R5,&LABELm ; ) 5 s@rn labelm d&addr MOV, .lst<0> 
$140 $B245 $C800     (  152    000140 B245....     MOV @R5+,&LABELm ; ) 5 s@rn+ labelm d&addr MOV, .lst<0> 
$144 $9245 $1 $C800     (  153    000144 92450001.... MOV 0x100(R5 ,&LABELm ; ) 100 5 sx(rn) labelm d&addr MOV, .lst<0> 


herem=$14A 
