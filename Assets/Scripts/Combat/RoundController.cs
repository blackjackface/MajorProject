using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController
{
    List<Character> characters;
    public List<Turn> turns = new List<Turn>();

    public RoundController(List<Character> gamecharacters) {

        characters = gamecharacters; 
    
    }
   public void CalculateTurn(List<Character> characters) {     
        float cumulativeInitiative = 0.0f;
        float averageInitiative = 0.0f;
        foreach (Character character in characters) {
            cumulativeInitiative += character.initiative;        
        }
        averageInitiative = cumulativeInitiative / characters.Count;
        for (int i = 0; i < 100; i++) {

            foreach (Character character in characters) {
                if (!character.isDead)
                {
                    character.turnGauge += 1 + character.initiative / averageInitiative;
                }
                if (character.turnGauge >= 100) {
                    character.turnGauge -= 100;
                    Turn turn = new Turn();
                    turn.character = character;

                    Debug.Log(character.charactername + " Has obtained a turn");
                    turns.Add(turn);
                }   
            }
        }
    }
}
