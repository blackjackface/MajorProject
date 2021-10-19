using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityLegacy
{
    string name;
    public virtual bool appliable(CharacterState character, List<CharacterState> targets) {
        return false;
    }
        
    public AbilityLegacy(){
    
    
    }

  /*  public virtual RoundState Use(CharacterState character, List<CharacterState> targets,int currentTarget, RoundState currentState) {


        return currentState;
    }
  */

}