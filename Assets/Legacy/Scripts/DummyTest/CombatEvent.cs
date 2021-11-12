using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvent
{

    public List<Character> targets =  new List<Character>();

    public enum EventType {
        START_COMBAT,
        START_ATTACK,
        FINISH_ANIMATION,
        START_PLAYER_TURN,
        SELECT_TARGET,        
        PLAYER_COMMAND,
        SELECTGAMBLE,
        DUMMY
    }
    public enum PlayerCommand {
        ATTACK,
        DEFEND,
        GAMBLE,


    }
    public enum GambleCommand { 
    
        GAMBLEBIG,
        GAMBLESMALL,
        CANCEL
    
    }
    public EventType eventType;
    public PlayerCommand playerCommand;
    public GambleCommand gambleCommand;
    
}
