Bonjour, est-ce que c'est votre deuxième cours de la journée ?
#speaker:Mr.Requing #portrait:mr_requing_neutral #layout:left
-> main

=== main ===
Oui...  #speaker:Vous #portrait:main_character_sad #layout:right 

Ah d'accord, qu'est-ce que vous avez eu ce matin ? #speaker:Mr.Requing #portrait:mr_requing_neutral #layout:left
+ Un cours sur le cerveau #speaker:Vous #portrait:main_character_neutral #layout:right 
->reponseNeutreBis1
+ Un cours sur...la psychologie ? Je ne sais pas. #speaker:Vous #portrait:main_character_neutral #layout:right 
->mauvaiseReponseBis1
+ Un cours sur les réseaux de neuronnes, je suppose #speaker:Vous #portrait:main_character_neutral #layout:right 
->suiteCoursBis1


=== reponseNeutreBis1 ===
Et qu'est-ce que vous avez fait ?  #speaker:Mr.Requing #portrait:mr_requing_neutral #layout:left
+ Je ne sais pas... #speaker:Vous #portrait:main_character_neutral #layout:right 
->suiteCoursBis1
+ [Laisser un autre étudiant répondre]
->intervention1
+ Euh...On doit faire un réseau de neuronnes avec deux hémisphères. #speaker:Vous #portrait:main_character_neutral #layout:right 
->suiteCoursBis1

=== suiteCoursBis1 ===
Bon super ! Aujourd'hui, nous allons parler des matériaux blancs.  #speaker:Mr.Requing #portrait:mr_requing_neutral #layout:left
Pourquoi les tableaux sont-il blancs ? #QTEConcentration:5,7
Pourquoi lorsqu'on doit projeter un film, le support doit-il être blanc ? #QTEConcentration:5,7
Pourquoi est-ce que la couleur du plastique des prises est-elle blanche ? #QTEConcentration:5,7
-> DONE

=== mauvaiseReponseBis1 ===
Comment ça ? Bon, c'est peut-être intéressant, je ne sais pas.  #speaker:Mr.Requing #portrait:mr_requing_neutral #layout:left
->suiteCoursBis1

=== intervention1 === 
Quelques choses en lien avec notre cerveau et les réseaux de neuronnal.#speaker:Rose #portrait:rose_neutral #layout:right
-> suiteCoursBis1 


-> END
