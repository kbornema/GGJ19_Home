INCLUDE ink_globalVars.ink
INCLUDE ink_goblinFlavor.ink
INCLUDE ink_externalFunctions.ink

->startKnot

=== startKnot ===
    
    { hasItem(STRING_ITEM_MEAT): -> knot_has_meat }
    { not hasItem(STRING_ITEM_MEAT): -> knot_has_not_meat }
    
    = knot_has_meat
        
        { randGoblinGah() } "Hunger!" *schnüffelt* Fleisch? Gib! Gib! { randGoblinGah() }
    
        + ["Dir Essen geben?! Sieh mal zu." *Alles selbst essen*] -> knot_eat_meat
        + ["Hmm, hier du kannst etwas haben." *Fleisch teilen*] -> knot_share_meat
        + ["Hier, nimm ruhig alles" *Fleisch geben*] -> knot_give_meat
        + [Gehen] -> END
        
     = knot_has_not_meat
     
        { float_goblin_trust >= 10 : { randGoblinGah() } { goblinPlayerLabel(float_goblin_trust) }, Achtung! Irrwisch. Gefährlich. Verstecken du musst. }
        
        { float_goblin_trust < 0 : { randGoblinGah() } { goblinPlayerLabel(float_goblin_trust) }. Irrwisch. Ist ein Freund, musst angucken! *Hihi* { goblinPlayerLabel(float_goblin_trust) }! }
        
        { float_goblin_trust < 10 && float_goblin_trust >= 0 : { randGoblinGah() } { goblinPlayerLabel(float_goblin_trust) }. Irrwisch! Gefährlich! Aufpassen. }
        
        + ["Danke..."] ->END
        + [Gehen] ->END
    
     = knot_eat_meat
     
        ~ float_goblin_trust = float_goblin_trust - 10
        { removeItem(STRING_ITEM_MEAT) }
        { randGoblinGah() } { goblinPlayerLabel(float_goblin_trust) }. { goblinPlayerLabel(float_goblin_trust) }. { goblinPlayerLabel(float_goblin_trust) }. { randGoblinGah()}
        + [*Haha*] ->startKnot
        
    = knot_share_meat   
    
        ~ float_goblin_trust = float_goblin_trust + 5
        { removeItem(STRING_ITEM_MEAT) }
        { randGoblinGah() } Ihr teilt das Fleisch gemeinschaftlich. Wie schön. { randGoblinGah()}
        
        + [Weiter] ->startKnot
        
    = knot_give_meat
    
        ~ float_goblin_trust = float_goblin_trust + 10
        { removeItem(STRING_ITEM_MEAT) } 
        
        { randGoblinGah() } Der Goblin verschlingt das Fleisch am Stück. "Danke!" { randGoblinGah()}
        
        + [Weiter] ->knot_tip
        
        
    = knot_tip
        
        ~temp bool_givesPlayerSecret = float_goblin_trust >= 10
        
        { bool_givesPlayerSecret : { randGoblinGah() } Geheimer Raum! Finde! Geh' zurück. { randGoblinGah()} }
        { not bool_givesPlayerSecret : *Rülpst* { randGoblinGah() } War gut! }
        
        * { bool_givesPlayerSecret } ["Danke!" (Gehen)] -> END
        * { not bool_givesPlayerSecret } [Weiter] -> startKnot
            
  
 
        