using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLegacy : AbilityLegacy
{
   override public bool appliable(CharacterState character, List<CharacterState> targets)
    {

     
        return true; //can target everyone
    }


  /* override public RoundState Use(CharacterState character, List<CharacterState> targets, int currentTarget , RoundState currentState)
    {
        int damage = 0;

        damage = character.attack - targets[currentTarget].defense;

        if (damage < 0) {
            damage = 0;
        }

        targets[currentTarget].hp -= damage;

        currentState.characterStates[currentTarget].hp = targets[currentTarget].hp;
        //TODO: averiguar como obtener un elemento que está en 2 listas.
        return currentState;
    }*/

}
