using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
  public List<Character> characters = new List<Character>();
    //A round is a list of turns, so a list of a list of turns should make sense
    List<List<Turn>> roundList = new List<List<Turn>>();
    RoundController roundController;
    int roundIndex = 0;
    public List<CombatEvent> eventList = new List<CombatEvent>();
    int characterIndex = 0;
    Turn actTurn = new Turn();
    public Text text;
    public enum State {
        SELECTING_TURN,
        END_OF_ROUND,
        END_OF_COMBAT,
        ENEMY_TURN,
        ENEMY_ANIMATION,
        PLAYER_SELECTING_COMMAND,
        PLAYER_SELECTING_TARGET,
        PLAYER_SELECTING_ABILITY,
        PLAYER_GAMBLEING,
        END_OF_TURN
    }
    public State state;
    void ProcessEvent(CombatEvent processEvent) {

   /*     if (processEvent.eventType == CombatEvent.EventType.START_COMBAT) {
            state = State.SELECTING_TURN;
        
        }*/
        switch (state)
        {

            case State.SELECTING_TURN:
                if (roundList.Count == 0) {
                //    state = State.END_OF_COMBAT;
                    break;
                } //end of combat
                if (roundList[0].Count == 0) {
                    state = State.END_OF_ROUND;
                    
                    break;
                } //end of round
                actTurn = roundList[0][0];
                roundList[0].RemoveAt(0);
                

                if (actTurn.character.isPlayer) {
                    CombatEvent combatEvent = new CombatEvent();
                    combatEvent.eventType = CombatEvent.EventType.PLAYER_COMMAND;
                    eventList.Add(combatEvent);
                    state = State.PLAYER_SELECTING_COMMAND;
                }
                else {  
                    CombatEvent combatEvent = new CombatEvent();
                    combatEvent.eventType = CombatEvent.EventType.START_ATTACK;
                    eventList.Add(combatEvent);
                    state = State.ENEMY_TURN; 
                }
                break;

            case State.ENEMY_TURN:

                //      if (processEvent.eventType == CombatEvent.EventType.START_ATTACK) {
    
                actTurn.character.behaviour.Act(actTurn.character, characters);
                CombatEvent testEvento = new CombatEvent();
                testEvento.eventType = CombatEvent.EventType.FINISH_ANIMATION;
                eventList.Add(testEvento);
                state = State.ENEMY_ANIMATION;
          //      }

                break;
            case State.ENEMY_ANIMATION:

                actTurn.character.behaviour.ShowText();
                text.text = actTurn.character.behaviour.textToShow;
                StartCoroutine(Vanish());



                break;            
            case State.PLAYER_SELECTING_COMMAND:
                if (processEvent.eventType == CombatEvent.EventType.PLAYER_COMMAND)
                {

                    
                    switch (processEvent.playerCommand) {
                        case CombatEvent.PlayerCommand.GAMBLE:
                            //todo una función
                            state = State.PLAYER_GAMBLEING;
                            break;                       
                        case CombatEvent.PlayerCommand.ATTACK:
                        //todo añadir comando de seleccionar objetivo
                        // character.processTurn.attack
                        state = State.END_OF_TURN;
                            break;                       
                        case CombatEvent.PlayerCommand.DEFEND:
                        //todo añadir comando de defenderse
                            break;
                    }
                    //todo añadir EnemyAnimationComplete()
                    state = State.PLAYER_SELECTING_COMMAND;
                }
                break;
            case State.PLAYER_SELECTING_TARGET:
                break;
            case State.PLAYER_GAMBLEING:
                if (processEvent.eventType == CombatEvent.EventType.SELECTGAMBLE)
                    {
                    switch (processEvent.gambleCommand)
                    {
                        case CombatEvent.GambleCommand.GAMBLEBIG:
                            break;
                        case CombatEvent.GambleCommand.GAMBLESMALL:
                            break;
                        case CombatEvent.GambleCommand.CANCEL:
                            // todo remove gamble UX
                            state = State.PLAYER_SELECTING_COMMAND;
                            break;
                    }
                }
                break;            
            case State.PLAYER_SELECTING_ABILITY:
                break;
            case State.END_OF_TURN:
                state = State.SELECTING_TURN;
                break;
            case State.END_OF_ROUND:
                generateRounds();
                state = State.SELECTING_TURN;
                break;
        }
    }


    public void ChangeState() {
        int index = 0;
        index = (int) state;
        index++;
        if (index == (int)State.END_OF_TURN) {
            index = 0;
        
        }
        state = (State) index;
    
    }

    IEnumerator Vanish() {

        yield return new WaitForSecondsRealtime(2.5f);
        text.text = "";
        StartTurn();
    }

    void Start()
    {
        
        generateRounds();
        StartTurn();
    }

    void StartTurn() {

        CombatEvent testEvent = new CombatEvent();
        testEvent.eventType = CombatEvent.EventType.START_COMBAT;
        eventList.Add(testEvent);
        state = State.SELECTING_TURN;

    }

    void Update()
    {
        if (eventList.Count != 0)
        {
            CombatEvent combatEvent = eventList.Last();
            eventList.RemoveAt(eventList.Count - 1);
            ProcessEvent(combatEvent);
        }

    }

//create a new function which returns an integer
    IEnumerator waitUntilTurnCompleted(PlayerBehaviour playerBehaviour) {

        yield return new WaitUntil(() => playerBehaviour.hasTurnCompleted == true);


        playerBehaviour.hasTurnCompleted = false;
    }   


    void generateRounds() {
        roundController = new RoundController(characters);
        roundController.CalculateTurn(characters);
        roundList.Add(roundController.turns);
       // CombatAct();
    }

    // Update is called once per frame

}
