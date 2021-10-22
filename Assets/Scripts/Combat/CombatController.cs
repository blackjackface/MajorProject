using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatController : MonoBehaviour
{
  public List<Character> characters = new List<Character>();
    //A round is a list of turns, so a list of a list of turns should make sense
    List<List<Turn>> roundList = new List<List<Turn>>();
    RoundController roundController;
    int roundIndex = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        generateRounds();
    }
    
    void CombatAct() {


        foreach (Turn actTurn in roundList[roundIndex]) {

                actTurn.character.behaviour.Act(characters);
            if (actTurn.character.isPlayer == true) {
                new WaitUntil(() => actTurn.character.behaviour.GetComponent<PlayerBehaviour>().hasTurnCompleted == true);
            }
        }
        
        if (!characters.All(c => c.GetComponent<Character>().isDead))
        {
            
            roundIndex++;
            generateRounds();
        }
    }


    IEnumerator waitUntilTurnCompleted(PlayerBehaviour playerBehaviour) {

        yield return new WaitUntil(() => playerBehaviour.hasTurnCompleted == true);


        playerBehaviour.hasTurnCompleted = false;
    }   


    void generateRounds() {
        roundController = new RoundController(characters);
        roundController.CalculateTurn(characters);
        roundList.Add(roundController.turns);
        CombatAct();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
