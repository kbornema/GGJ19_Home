INCLUDE ink_externalFunctions.ink
INCLUDE ink_globalVars.ink

=== startKnot ===
    
    Eine Kiste.
    
    * {not knot_foundKey}[Durchsuchen] -> knot_foundKey
    + [Gehen] -> END
    
    = knot_foundKey
    
        { giveItem(STRING_ITEM_SMALL_KEY) }
        Die findest einen kleinen Schlüssel.
        + [Gehen] -> knot_end
        
    = knot_end
    
        { destroyDialogueOwner() }
        -> END
        