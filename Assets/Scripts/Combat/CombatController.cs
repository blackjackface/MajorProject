using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class CombatController : MonoBehaviour
{
  public List<Character> characters = new List<Character>();
    //A round is a list of turns, so a list of a list of turns should make sense
    List<List<Turn>> roundList = new List<List<Turn>>();
    RoundController roundController;
    int roundIndex = 0;
    List<TestEvent> testEventList = new List<TestEvent>();
    public enum State {
        ENEMY_TURN,
        ENEMY_ANIMATION,
        PLAYER_SELECTING_COMMAND,
        PLAYER_SELECTING_TARGET,
        PLAYER_SELECTING_ABILITY,
        PLAYER_GAMBLEING,
        LAST
    }
    public State state;
    void ProcessEvent(TestEvent testEvent) {
        switch (state)
        {

            case State.ENEMY_TURN:
                if (testEvent.eventType == TestEvent.EventType.START_ATTACK) {
                    //todo añadir enemyact()
                    state = State.ENEMY_ANIMATION;

                }

                break;
            case State.ENEMY_ANIMATION:
                if (testEvent.eventType == TestEvent.EventType.FINISH_ANIMATION)
                {
                    //todo añadir EnemyAnimationComplete()
                    state = State.PLAYER_SELECTING_COMMAND;
                }
                break;            
            case State.PLAYER_SELECTING_COMMAND:
                if (testEvent.eventType == TestEvent.EventType.PLAYER_COMMAND)
                {
                    switch (testEvent.playerCommand) {
                        case TestEvent.PlayerCommand.GAMBLE:
                            //todo una función
                            state = State.PLAYER_GAMBLEING;
                            break;                       
                        case TestEvent.PlayerCommand.ATTACK:

                            break;                       
                        case TestEvent.PlayerCommand.DEFEND:

                            break;
                    
                    
                    
                    }
                    //todo añadir EnemyAnimationComplete()
                    state = State.PLAYER_SELECTING_COMMAND;
                }
                break;
            case State.PLAYER_SELECTING_TARGET:
                break;
            case State.PLAYER_GAMBLEING:
                if (testEvent.eventType == TestEvent.EventType.SELECTGAMBLE)
                    {
                    switch (testEvent.gambleCommand)
                    {
                        case TestEvent.GambleCommand.GAMBLEBIG:
                            break;
                        case TestEvent.GambleCommand.GAMBLESMALL:
                            break;
                        case TestEvent.GambleCommand.CANCEL:
                            // todo remove gamble UX
                            state = State.PLAYER_SELECTING_COMMAND;
                            break;
                    }
                }
                break;            
            case State.PLAYER_SELECTING_ABILITY:
                break;
        }


    }


    public void ChangeState() {
        int index = 0;
        index = (int) state;
        index++;
        if (index == (int)State.LAST) {
            index = 0;
        
        }
        state = (State) index;
    
    }

  /* 
   private async void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        TestEvent testEvent = new TestEvent();
        testEvent.eventType = TestEvent.EventType.FINISH_ANIMATION;
        testEventList.Add(testEvent);
    }
    void enemyAct() {
      // todo perform action
        // simulate animation delay        
      //  Timer t = new Timer(1000);
      //  t.AutoReset = true;
      //  t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
      //  t.Start();
        


    }
  */
    // Start is called before the first frame update
    void Start()
    {
        generateRounds();
        TestEvent testEvent = new TestEvent();
        testEvent.eventType = TestEvent.EventType.START_COMBAT;
        testEventList.Add(testEvent);
    }
    
    void CombatAct() {


        foreach (Turn actTurn in roundList[roundIndex]) {

                actTurn.character.behaviour.Act(characters);

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
     void Update()
    {
        if (testEventList.Count != 0) {
            ProcessEvent(testEventList.Last());
            testEventList.RemoveAt(testEventList.Count - 1 );
               
        }
        
    }
}
