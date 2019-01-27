INCLUDE ink_externalFunctions.ink
INCLUDE ink_goblinFlavor.ink
INCLUDE ink_globalVars.ink

-> startKnot


=== startKnot ===
    
    { knot_can_help  : "Kleiner Schlüssel finden. Tür auf! Brauchst mich." { randGoblinGah() } }
    { not knot_can_help:  { randGoblinGah() } "Will raus. Bitte." }
    
    * ["Wie kann ich helfen?"] -> knot_playerWantsToHelp
    * { not knot_can_help } ["Haha, dummer Goblin"] -> knot_insulted_goblin
    * { not knot_play_game } ["Möchtest du etwas spielen?"] -> knot_play_game
    + { hasItem(STRING_ITEM_SMALL_KEY) } [Tür mit Schlüssel öffnen] -> knot_open_door
    + [Gehen] ->END
    
    = knot_play_game
    
        "Keine Zeit. Kinderkacke. Tür öffnen!" { randGoblinGah() }
        + [Weiter] -> knot_playerWantsToHelp
        + [Gehen] -> END
        
    = knot_insulted_goblin
        ~float_goblin_trust -= 10
        { randGoblinGah() } "Wirst schon sehen... Abschaum... Karma."
        + [Weiter] -> knot_playerWantsToHelp
        + [Gehen] -> END
        
    = knot_can_help
        
        { float_goblin_trust <= 0: "Brauchst mich, { goblinPlayerLabel(float_goblin_trust) }. Habe großen Schlüssel! {randGoblinGah()}. Los, los!"}
        
        { float_goblin_trust > 0: {randGoblinGah()} "Kann helfen, { goblinPlayerLabel(float_goblin_trust) }. Habe großen Schlüssel! Los, los!" }
        
        + [Gehen] -> END
        
        
    = knot_playerWantsToHelp
        
        "{ goblinPlayerLabel(float_goblin_trust) }. Finde kleinen Schlüssel. Tür öffnen." { randGoblinGah() }.
        
        + [Weiter] -> knot_can_help
        + [Gehen] -> END
        
    = knot_open_door
        
        { triggerInteractable(0) } 
        { triggerInteractable(1) }
        { triggerInteractable(2) } 
        { removeItem(STRING_ITEM_SMALL_KEY) }
        { enableNpc(STRING_NPC_GOBLIN, STRING_WHERE_IN_CELL, false) }
        { enableNpc(STRING_NPC_GOBLIN, STRING_WHERE_BEFORE_IRRWISH, true) }
        
        Der Goblin lief schnell aus der Zelle den Gang hinab.  
        
        + [Weiter] -> knot_goblin_runs
        
    = knot_goblin_runs

        { destroyDialogueOwner() }
        
        + [Gehen] -> END
    