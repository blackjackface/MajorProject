using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState 
{

    public List<CharacterState> characterStates;

    public GameState(List<CharacterState> characterStates) {
        this.characterStates = characterStates;
    }

    public CharacterState CharacterAtIndex(int index, Team team) {

        int characterIndex = 0;

            foreach (CharacterState charaState in characterStates) {
                if (characterIndex == index && charaState.team == team) {
                    return charaState;
                }
                if (charaState.team == team) {
                        characterIndex++;
                }
            }
            throw new CharacterStateNotFoundException();
    }
   public int CharacterInTeam(Team team) {
        
        int characterIndex = 0;
        foreach (CharacterState charaState in characterStates) {

            if (charaState.team == team) {
                characterIndex++;            
            }   
        }
        return characterIndex;    
    }
}