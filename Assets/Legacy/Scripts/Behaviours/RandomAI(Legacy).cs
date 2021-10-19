using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : AI
{
    [System.Obsolete]
    override public Turn GetTurn(RoundState round)
    {
       
        Turn turn = new Turn();
       /* List<string> keys = new List<string>(round.NPCTeamState.character.Keys);
        string key = keys[Random.RandomRange(0,keys.Count)];
        CharacterState characterState = round.NPCTeamState.character[key];

        List<string> targetKeys = new List<string>(round.NPCTeamState.character.Keys);
        string targetKey = targetKeys[Random.RandomRange(0, targetKeys.Count)];
        CharacterState targetState = round.NPCTeamState.character[targetKey];

        Ability ability = characterState.abilities[Random.RandomRange(0, characterState.abilities.Count)];
        turn.ability = ability;
        turn.character = characterState;*/
        return turn;
    }



}
