INCLUDE ink_externalFunctions.ink
INCLUDE ink_globalVars.ink



=== startKnot ===

    Ein Haufen Müll.

    * {not knot_get_stone} [Durchsuchen] -> knot_get_stone
    + {knot_get_stone} [Durchsuchen] -> knot_failed_search
    + [Gehen] -> END
    
    = knot_get_stone
        Du findest einen großen Stein. { giveItem(STRING_ITEM_STONE) }
        + [Durchsuchen] -> knot_failed_search
        + [Gehen] -> END
        
    = knot_failed_search
        Du findest nichts Nützliches.
        + [Durchsuchen] -> knot_failed_search
        + [Gehen] -> END