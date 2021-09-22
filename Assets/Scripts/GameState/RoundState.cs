using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundState
{
    // Start is called before the first frame update
    public int roundCount = 0;
    public List<CharacterState> characterStates;

    public RoundState(GameState gameState) {

        characterStates = gameState.characterStates;

    }
}
