using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour, UXController
{

    public List<GameCharacter> gameCharacters;
    AI ai;
    RoundController roundController;

    public void processTurn(Turn turn, RoundController roundController, RoundState finalState)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        ai = new BehaviourAI();
        roundController = new RoundController(initialGamestate(),ai,this);
    }

    GameState initialGamestate() {
        List<CharacterState> characterStates = new List<CharacterState>();
        foreach (var character in gameCharacters) {
            CharacterState characterstate = character.getCharacterState();
            characterStates.Add(characterstate);
        }
        return new GameState(characterStates);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
