

cr .(  jxx, instructions) 
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


cr .endcode

