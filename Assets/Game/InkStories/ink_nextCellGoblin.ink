INCLUDE ink_globalVars.ink

CONST string_gah_short = "*Gah*"
CONST string_gah_medium = "*Gaaaahhhh*"
CONST string_gah_long = "*Gaaaaaaaaaaaaaaaaahhhhh*"

VAR bool_first_talked = false

-> startKnot

LIST HandEnum = Schere, Stein, Papier 

LIST GameResult = Verloren, Unentschieden, Gewonnen 

//-1: playerLost
// 0: draw:
// 1: playerWon
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
    
    * { not bool_first_talked } [Hallo? Kannst du reden?] -> knot_hello_goblin
    * { not bool_first_talked } [Ein Goblin?! Du kannst hier verrecken, Mistgeburt!] -> knot_insult_goblin
    + { bool_first_talked && not knot_got_name } [Verrätst du mir deinen Namen?] -> knot_ask_name
    + { knot_got_name } [-Schere, Stein, Papier spielen-] -> knot_play_game
    + [Gehen] -> END
        
        
    = knot_hello_goblin
    
        ~ bool_first_talked = true
        { string_gah_medium } ... Was? Jemand lebend? Du mir helfen? { string_gah_short }
        
        * [Helfen? Ich bin selbst eingesperrt.] -> knot_imprisoned_self
        * [Hast du einen Namen?] -> knot_ask_name
        + [Gehen] -> END
    
    
    = knot_imprisoned_self
        
        { string_gah_long } ... Verloren, alles verloren. { string_gah_short }
        
        * [Hast du einen Namen?] -> knot_ask_name
        + [Gehen] -> END
     
    = knot_ask_name
        
        { float_goblin_trust < 0: Du nicht nett! Verschwinde! }
        { float_goblin_trust == 0: Nö. Kenn' dich nicht. }
        { float_goblin_trust > 0: Name ... Gah! {string_gah_short} }
        
        + { float_goblin_trust > 0} [Sehr erfreut, Gah?] ->  knot_got_name
        + { float_goblin_trust <= 0 } [Na, dann nicht.] -> startKnot 
        + [Gehen] -> END
     
    = knot_insult_goblin
     
        ~ bool_first_talked = true
        ~ float_goblin_trust = float_goblin_trust - 10
        
        Verschwinde! Du, ... ETWAS!!
        + [...] -> startKnot
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
        
        {myChoiceString} vs. {hisChoiceString} a {resultString} !
         ~temp hisAnswerString = getAnswerString(int_result)
         {string_gah_short} {hisAnswerString}
        
        
        + [Nochmal spielen] -> knot_play_game
        + [Gehen] -> END
        
    
    