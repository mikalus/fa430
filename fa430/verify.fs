
: xxx ; 
cr (  jxx, instructions) 
cr clrlabels 
cr .startcode 
 (       24    000000              label0:    ; )  0 >label  .lst
 (       25    000000                         ; )  .lst
 (       26    000000 FF23         JNE label0 ; )  0 label> JNE,  .lst
 (       27    000002 FE27         JEQ label0 ; )  0 label> JEQ, .lst
 (       28    000004 FD2B         JNC label0 ; )  0 label> JNC, .lst
 (       29    000006 FC2F         JC  label0 ; )  0 label> JC,  .lst
 (       30    000008 FB33         JN  label0 ; )  0 label> JN, .lst
 (       31    00000A FA37         JGE label0 ; )  0 label> JGE,  .lst
 (       32    00000C F93B         JL  label0 ; )  0 label> JL, .lst
 (       33    00000E F83F         JMP label0 ; )  0 label> JMP, .lst
 (       34    000010                         ; )  .lst
 (       35    000010 F723         JNZ label0 ; )  0 label> JNZ, .lst
 (       36    000012 F627         JZ  label0 ; )  0 label> JZ, .lst
 (       37    000014 F52B         JLO label0 ; )  0 label> JLO, .lst
 (       38    000016 F42F         JHS label0 ; )  0 label> JHS,  .lst
 (       39    000018                         ; )  .lst
cr ( formII ibstructions) 
herem constant exec 
   (  40    000018               EXEC:     ;  ) .lst
   (  41    000018 B012....     CALL #EXEC ; ) exec #k CALL, .lst
   (  42    00001C 9012FAFF     CALL EXEC ; ) exec addr CALL, .lst
   (  43    000020 9212....     CALL &EXEC ; ) exec &addr CALL, .lst
   (  44    000024 8512         CALL R5 ; ) 5 rn CALL, .lst
   (  45    000026 A512         CALL @R5 ; ) 5 @rn CALL, .lst
   (  46    000028 B512         CALL @R5+ ; ) 5 @rn+ CALL, .lst
   (  47    00002A 95120001     CALL 0x100(R5 ; ) 100 5 x(rn) CALL, .lst
                                               
   (  48    00002E              ; ) .lst
   (  49    00002E 3012....     PUSH #EXEC ; ) exec #k PUSH, .lst
   (  50    000032 1012E4FF     PUSH EXEC ; ) exec addr PUSH, .lst
   (  51    000036 1212....     PUSH &EXEC ; ) exec &addr PUSH, .lst
   (  52    00003A 0512         PUSH R5 ; ) 5 rn PUSH, .lst
   (  53    00003C 2512         PUSH @R5 ; ) 5 @rn PUSH, .lst
   (  54    00003E 3512         PUSH @R5+ ; ) 5 @rn+ PUSH, .lst
   (  55    000040 15120001     PUSH 0x100(R5 ; ) 100 5 x(rn) PUSH, .lst
                                               
   (  57    000044 70120B00     PUSH.B #11 ; ) 11 #k PUSH, .lst
   (  58    000048 5012CEFF     PUSH.B EXEC ; ) exec addr PUSH.B, .lst
   (  59    00004C 5212....     PUSH.B &EXEC ; ) exec &addr PUSH.B, .lst
   (  60    000050 4512         PUSH.B R5 ; ) 5 rn PUSH.B, .lst
   (  61    000052 6512         PUSH.B @R5 ; ) 5 @rn PUSH.B, .lst
   (  62    000054 7512         PUSH.B @R5+ ; ) 5 @rn+ PUSH.B, .lst
   (  63    000056 55120001     PUSH.B 0x100(R5 ; ) 100 5 x(rn) PUSH.B, .lst

   (  65    00005A 3041         RET ; ) ( no addressmodes) RET, .lst
                                    
   (  67    00005C              ; RRA #EXEC ; ) \ exec #k RRA, .lst
   (  68    00005C 1011BAFF     RRA EXEC ; ) exec addr RRA, .lst
   (  69    000060 1211....     RRA &EXEC ; ) exec &addr RRA, .lst
   (  70    000064 0511         RRA R5 ; ) 5 rn RRA, .lst
   (  71    000066 2511         RRA @R5 ; ) 5 @rn RRA, .lst
   (  72    000068 3511         RRA @R5+ ; ) 5 @rn+ RRA, .lst
   (  73    00006A 15110001     RRA 0x100(R5 ; ) 100 5 x(rn) RRA, .lst
                                              
   (  75    00006E              ; RRA.B #EXEC ; ) \ exec #k RRA.B, .lst                                
   (  76    00006E 5011A8FF     RRA.B EXEC ; ) exec addr RRA.B, .lst
   (  77    000072 5211....     RRA.B &EXEC ; ) exec &addr RRA.B, .lst
   (  78    000076 4511         RRA.B R5 ; ) 5 rn RRA.B, .lst
   (  79    000078 6511         RRA.B @R5 ; ) 5 @rn RRA.B, .lst
   (  80    00007A 7511         RRA.B @R5+ ; ) 5 @rn+ RRA.B, .lst
   (  81    00007C 55110001     RRA.B 0x100(R5 ; ) 100 5 x(rn) RRA.B, .lst
                                                
   (  83    000080              ; RRC #EXEC ; ) \ exec #k RRC, .lst
   (  84    000080 101096FF     RRC EXEC ; ) exec addr RRC, .lst
   (  85    000084 1210....     RRC &EXEC ; ) exec &addr RRC, .lst
   (  86    000088 0510         RRC R5 ; ) 5 rn RRC, .lst
   (  87    00008A 2510         RRC @R5 ; ) 5 @rn RRC, .lst
   (  88    00008C 3510         RRC @R5+ ; ) 5 @rn+ RRC, .lst
   (  89    00008E 15100001     RRC 0x100(R5 ; ) 100 5 x(rn) RRC, .lst
                                              
   (  91    000092              ; RRC.B #EXEC ; ) \ exec #k RRC.B, .lst
   (  92    000092 501084FF     RRC.B EXEC ; ) exec addr RRC.B, .lst
   (  93    000096 5210....     RRC.B &EXEC ; ) exec &addr RRC.B, .lst
   (  94    00009A 4510         RRC.B R5 ; ) 5 rn RRC.B, .lst
   (  95    00009C 6510         RRC.B @R5 ; ) 5 @rn RRC.B, .lst
   (  96    00009E 7510         RRC.B @R5+ ; ) 5 @rn+ RRC.B, .lst
   (  97    0000A0 55100001     RRC.B 0x100(R5 ; ) 100 5 x(rn) RRC.B, .lst
                                                
   (  99    0000A4              ;SWPB #EXEC ; ) \ exec #k SWPB, .lst
   ( 100    0000A4 901072FF     SWPB EXEC ; ) exec addr SWPB, .lst
   ( 101    0000A8 9210....     SWPB &EXEC ; ) exec &addr SWPB, .lst
   ( 102    0000AC 8510         SWPB R5 ; ) 5 rn SWPB, .lst
   ( 103    0000AE A510         SWPB @R5 ; ) 5 @rn SWPB, .lst 
   ( 104    0000B0 B510         SWPB @R5+ ; ) 5 @rn+ SWPB, .lst
   ( 105    0000B2 95100001     SWPB 0x100(R5 ; ) 100 5 x(rn) SWPB, .lst
   (                                            
   ( 107    0000B6              ; SXT #EXEC ; ) \ exec #k SXT, .lst
   ( 108    0000B6 901160FF     SXT EXEC ; ) exec addr SXT, .lst
   ( 109    0000BA 9211....     SXT &EXEC ; ) exec &addr SXT, .lst
   ( 110    0000BE 8511         SXT R5 ; ) 5 rn SXT, .lst
   ( 111    0000C0 A511         SXT @R5 ; ) 5 @rn SXT, .lst
   ( 112    0000C2 B511         SXT @R5+ ; ) 5 @rn+ SXT, .lst
   ( 113    0000C4 95110001     SXT 0x100(R5 ; ) 100 5 x(rn) SXT, .lst

cr .endcode

