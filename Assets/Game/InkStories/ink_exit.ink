INCLUDE ink_globalVars.ink
INCLUDE ink_externalFunctions.ink
INCLUDE ink_goblinFlavor.ink


->startKnot

=== startKnot ===

    ~float_goblin_trust = -10

    Die Tür führt nach draußen. Endlich kannst du Nachhause!
    
    + [Weiter] -> knot_next
    
    
    = knot_next
    
        { float_goblin_trust < 0 :  -> knot_bad_ending }
        { float_goblin_trust >= 0 :  -> knot_make_friends }
            
    
    = knot_end
        { endGame() } 
        ->END
        
    = knot_make_friends
    
        {randGoblinGah()} Du hörst ein Geräusch, drehst dich um und der Goblin steht vor dir.
        * [Weiter] -> knot_next_next
        
     = knot_next_next
        "{goblinPlayerLabel(float_goblin_trust)}. Geschafft. Yay!"
        * [Spiel Beenden] -> knot_end
        
    =knot_bad_ending
    
        {randGoblinGah()} Du hörst ein Geräusch und spürst wie dir jemand ein Messer in den Rücken sticht. {goblinPlayerLabel(float_goblin_trust)} 
        +[Spiel Beenden] -> knot_end
        