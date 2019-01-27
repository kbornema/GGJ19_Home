INCLUDE ink_globalVars.ink
INCLUDE ink_externalFunctions.ink

=== startKnot ===

    Das Gitter ist beschädigt. Vielleicht kann man es zerstören?
   
    +  { hasItem(STRING_ITEM_STONE) } [Stein benutzen] -> knot_open_with_stone
    + [Gewalt anwenden] -> knot_try_brute_force
    + [Gehen] -> END

    = knot_try_brute_force
        Mit bloßen Händen bekommst du das Tor nicht auf.
        + [Gewalt anwenden] -> startKnot
        + [Gehen] -> END
        
        
    = knot_open_with_stone
        Du zerstörst das Gitter mit dem Stein. Leider ist er dabei ebenfalls zerbrochen. 
        { removeItem(STRING_ITEM_STONE) } 
        { destroyDialogueOwner() }
        + [Gehen] -> END