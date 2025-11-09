Bonjour chers étudiants ! #speaker:Mr.Champignon #portrait:mr_champignon_neutral #layout:left
-> main

=== main ===
Le cerveau humain est fascinant. Il peut se concentrer sur une tâche pendant 3 minutes avant de penser à du chocolat.
Certains d'entre vous viennent de penser à un brownie ou à une mousse au chocolat. Voilà, une preuve vivante. #portait:mr_champignon_happy #QTEConcentration:5,3
Aujourd'hui nous allons parler d'un instrument magique qui est le cerveau. Cette instrument qui décide de nous nous faire les projets au dernier moment.
Nos cerveaux sont composés de deux hémisphères...
Peux tu me dire à quoi correspond l'hémisphère de gauche ? #timer:5
+ Heuu...Je ne sais pas, je suis droitier. #speaker:main_character #portait:main_character_neutral #layout:right
    ->mauvaiseReponse1
+ La logique ? Mais je suis droitier. #speaker:main_character #portait:main_character_neutral #layout:right
    ->bonneReponse1
+ [Ne pas répondre et paniquer mentalement] #speaker:main_character #portait:main_character_neutral #layout:right
    ->mauvaiseReponse2

=== suiteCours ===
Le cerveau n'aime pas l'incertitude, alors il invente des histoires...
Parfois, les pensées sont tellement forte, que les évènements deviennent réels. #QTEConcentration:5,5
Les évènements deviennent réels car le cerveau croit tellement à ces histoires fictives, qu'inconsciamment, l'humain reproduit les actions négatives qu'ils imaginent.
L'humain est en quelques soit maudit par sa propre cerveau... #portait:mr_champignon_sad
Mais il y a des solutions ! Qu'est-ce tu souhaites proposer ? #timer:5
+ Réagir de manière positive en acceptant le présent quelques soient la gravité de l'évènement. #speaker:main_character #portait:main_character_neutral #layout:right
    ->bonneReponse2
+ Labotomiser notre cerveau. #speaker:main_character #portait:main_character_neutral #layout:right
    ->mauvaiseReponse3
+ Je ne sais pas, aller voir un psychologue ? #speaker:main_character #portait:main_character_neutral #layout:right)
    ->reponseNeutre1

=== finCours ===
Bon le cours est terminé pour aujourd'hui. Je vais vous donner le sujet du projet ! Vous allez devoir programmer un réseau neuronnal avec deux hémisphères ! 

-> DONE

=== mauvaiseReponse1 ===
Ahhh c'est la logique, je l'avais dit au cours précédent ! Bon passons. 
-> suiteCours

=== mauvaiseReponse2 ===
Tu n'es composé que de cerveau droit !
-> suiteCours

=== bonneReponse1 ===
Oui et le droit est là pour nous faire paniquer sans raison apparente.
-> suiteCours


=== bonneReponse2 ===
Oui c'est peut-être l'une des meilleures choses à faire...C'est difficile certe !
-> finCours

=== mauvaiseReponse3 ===
Un peu trop radicale...
-> finCours

=== reponseNeutre1 ===
Oui c'est une solution mais parfois ce n'est pas la meilleure
-> finCours

-> END
