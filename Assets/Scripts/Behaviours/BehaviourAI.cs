using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourAI : AI
{
    private CharacterState NextCharacter(RoundState roundState)
    {

        CharacterState state = new CharacterState();
        return state;

    }

    public override Turn GetTurn(RoundState round)
    {
        CharacterState nextCharacter = NextCharacter(round);
        try
        {
            Turn turn = nextCharacter.behaviour.GetTurn(round, nextCharacter);
            return turn;
        }
        catch (TurnNotFoundException exception)
        {


            throw (exception);
        }

       
    }


}
