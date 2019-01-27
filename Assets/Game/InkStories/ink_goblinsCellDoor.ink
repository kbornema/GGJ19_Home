INCLUDE ink_externalFunctions.ink
INCLUDE ink_goblinFlavor.ink
INCLUDE ink_globalVars.ink

-> startKnot

=== startKnot ===
    
    { randGoblinGah() } Will raus. Bitte.
    
    + [Wie kann ich helfen?] -> knot_can_help
    * [Haha, dummer Goblin] -> knot_insulted_goblin
    * { not knot_play_game } [-Schere, Stein, Papier spielen-] -> knot_play_game
    + [Gehen] ->END
    
    = knot_play_game
    
        Keine Zeit. Kinderkacke. Hilf mir! { randGoblinGah() }
        + [Weiter] -> knot_can_help
        + [Gehen] -> END
        
    = knot_insulted_goblin
        ~float_goblin_trust -= 10
        { randGoblinGah() } Wirst schon sehen.
        + [Weiter] -> startKnot
        + [Gehen] -> END
        
    = knot_can_help
        Kann helfen. Habe Schlüssel
        + [Weiter] -> startKnot
        + [Gehen] -> END