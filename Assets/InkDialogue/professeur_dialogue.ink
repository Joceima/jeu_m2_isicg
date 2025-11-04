Alors... #speaker:Mr_Champignon #portrait:mr_champignon_neutral #layout:left
-> prof_question
=== prof_question ===
Alors, tu as compris la leçon ? #timer:5
+ Oui, je pense avoir compris ! 
    -> continue_cours

+ Pas vraiment... 
    -> Questions
    
+ [Rester silencieux] 
    Toujours aussi silencieux... #speaker:Mr_Champignon #portrait:mr_champignon_sad #layout:left
    -> continue_cours
-> DONE 
    
=== continue_cours ====
Continuons notre cours sur les lavabos ! #speaker:Mr_Champignon #portrait:mr_champignon_neutral #layout:left
-> DONE

=== Questions === 
Je peux répéter si tu veux !
-> DONE 
-> continue_cours

