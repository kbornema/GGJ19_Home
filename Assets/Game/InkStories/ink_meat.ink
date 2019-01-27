INCLUDE ink_externalFunctions.ink
INCLUDE ink_globalVars.ink

=== startKnot ===
    
    Auf dem Boden liegt ein Stück Fleisch.
    
    * [Mitnehmen] -> knot_pickUp
    * [Essen] -> knot_eat
    + [Gehen] -> END
    
    = knot_pickUp
    
        { giveItem(STRING_ITEM_MEAT) }
        + [Gehen] -> knot_end
        
    = knot_end
    
        { destroyDialogueOwner() }
        -> END
        
    = knot_eat
    
        Du isst das Stück Fleisch. Es schmeckt ein wenig merkwürdig...
        
        + [Gehen] -> knot_end
        