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
        float AverageInitiative = 0.0f;
        foreach (Character character in characters) {
            cumulativeInitiative += character.initiative;        
        }
        AverageInitiative = cumulativeInitiative / characters.Count;
        for (int i = 0; i < 100; i++) {

            foreach (Character character in characters) {
                character.turnGauge += 1 + character.initiative / AverageInitiative;
                if (character.turnGauge >= 100) {
                    character.turnGauge -= 100;
                    Turn turn = new Turn();
                    turn.character = character;
                    turns.Add(turn);
                //TODO AÑADIR TURNOS A LA LISTA DE TURNOS, OTORGÁNDOSELO AL JUGADOR O ENEMIGO
                }   
            }
        }
    }
}
