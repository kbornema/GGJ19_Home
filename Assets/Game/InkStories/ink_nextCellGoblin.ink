INCLUDE ink_globalVars.ink
INCLUDE ink_goblinFlavor.ink

VAR bool_first_talked = false

-> startKnot

LIST HandEnum = Schere, Stein, Papier 

LIST GameResult = Verloren, Unentschieden, Gewonnen 

//-1: playerLost, 0: draw, 1: playerWon
=== function scissorsGame(playerChoice, otherChoice) ===
    ~ temp diff = playerChoice - otherChoice
    
    { 
    - diff == 2:
        ~return -1
    - diff == -2:
        ~return 1
    }
    
    ~ return diff
    
=== function getAnswerString(int_gameResult) ===
    
    {
        - int_gameResult == 0:
        	~ return "Hm, nochmal?"
        - int_gameResult == -1:
        	~ return "Hihi. Spaß! Nochmal!"
        - int_gameResult == 1:
        	~ return "Nein, neeein, nein!!"
    }
    

=== startKnot ===
    
    { ~{ string_gah_short } | ... | {string_gah_medium} | {string_gah_long} }
    
    * { not bool_first_talked } ["Hallo? Kannst du reden?"] -> knot_hello_goblin
    * { not bool_first_talked } ["Ein Goblin?! Du kannst hier verrecken, Mistgeburt!"] -> knot_insult_goblin
    + { bool_first_talked && not knot_got_name } ["Verrätst du mir deinen Namen?"] -> knot_ask_name
    + { knot_got_name } ["Gah, willst du etwas spielen?"] -> knot_play_game
    + [Gehen] -> END
        
        
    = knot_hello_goblin
    
        ~ bool_first_talked = true
        { string_gah_medium } "... Was? Jemand lebend? Du mir helfen?" { string_gah_short }
        
        + ["Helfen? Ich bin selbst eingesperrt."] -> knot_imprisoned_self
        + ["Dir helfen Goblin?! Lieber erhänge ich mich!"] -> knot_insult_goblin
    
    
    = knot_imprisoned_self
        
        { string_gah_long } "... Verloren, alles verloren." { string_gah_short }
        
        + ["Keine Angst, ich hole uns hier schon raus"] -> knot_friendly_goblin
        + ["Ja, du bist hier verloren, Grünhaut!"] -> knot_insult_goblin
     
    = knot_ask_name
        
        { float_goblin_trust < 0: "Du nicht nett! Verschwinde!" }
        { float_goblin_trust == 0: "Nö. Kenn' dich nicht." }
        { float_goblin_trust > 0: "Name ... Gah!" {string_gah_short} }
        
        + { float_goblin_trust > 0} ["Sehr erfreut, Gah?"] ->  knot_got_name
        + { float_goblin_trust <= 0 } ["Na, dann nicht." Gehen] -> END 
        
    = knot_friendly_goblin
    
        ~ float_goblin_trust = float_goblin_trust + 10
        "Danke, { goblinPlayerLabel(float_goblin_trust) }. Du nett!"
        
        + [Weiter] -> startKnot
        + [Gehen]-> END
     
    = knot_insult_goblin
     
        ~ bool_first_talked = true
        ~ float_goblin_trust = float_goblin_trust - 10
        
        "Verschwinde! Du, ... { goblinPlayerLabel (float_goblin_trust) }"
        + [Gehen] -> END
        
    //just here so that we can ask if the knot was visited
    = knot_got_name
        + -> startKnot
        
    = knot_play_game
        
        Wähle...
        
        + [Schere] -> knot_finalGame(1)
        + [Stein]  -> knot_finalGame(2)
        + [Papier] -> knot_finalGame(3)
        
     = knot_finalGame(int_myChoice)
        
        ~temp int_hisChoice = RANDOM(1, 3)
        ~temp int_result = scissorsGame(int_myChoice, int_hisChoice)
        
        ~temp resultString = GameResult(int_result + 2)
        
        ~temp hisChoiceString = HandEnum(int_hisChoice)
        ~temp myChoiceString = HandEnum(int_myChoice)
        
        
         ~temp hisAnswerString = getAnswerString(int_result)
         
        Ergebnis: {myChoiceString} vs. {hisChoiceString} - {resultString}. "{string_gah_short} {hisAnswerString}"
        
        
        + [Nochmal spielen] -> knot_play_game
        + [Gehen] -> END
        
    
    