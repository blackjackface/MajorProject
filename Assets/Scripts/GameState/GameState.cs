using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState 
{

    public List<CharacterState> characterStates;

    public GameState(List<CharacterState> characterStates) {
        this.characterStates = characterStates;
    }
}
