INCLUDE ink_externalFunctions.ink

VAR string_objectText = "Ein unbekanntes Objekt"

-> startKnot

=== startKnot ===
    
    { string_objectText }
    
    + [Durchsuchen] -> knot_nothing
    + [Gehen] -> END
        
    = knot_nothing
        Du findest nichts Interessantes.
        + [Gehen] -> knot_end
        
    = knot_end
        { destroyDialogueOwner() }
        -> END
        