using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    string name;
    bool appliable(Character character, List<Character> targets) {
        return false;
    }

    public Ability(){
    
    
    }

   public RoundState Use(Character character, List<Character> targets, RoundState currentState) {


        return currentState;
    }


}