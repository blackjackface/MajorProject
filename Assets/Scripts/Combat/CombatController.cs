using System;
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
    [SerializeField]
    public List<List<Turn>> roundList = new List<List<Turn>>();
    RoundController roundController;
    public List<Character> players = new List<Character>();
    [SerializeField]
    List<Character> playerCharacters = new List<Character>();
    [SerializeField]
    List<Character> EnemyCharacters = new List<Character>();
    public int playerIndex = 0;
    public int abilityIndex = 0;
    public bool targetIsConfirmed = false;
    int roundIndex = 0;
    [SerializeField]
    List<Turn> currentRound = new List<Turn>();
    public List<CombatEvent> eventList = new List<CombatEvent>();
    [SerializeField]
    public Turn actTurn = new Turn();
    [SerializeField]
    Character characterDebugTurn;
    [SerializeField]    
    public Text text;
    bool victory = false;
    List<TimeEvent> timeEvents = new List<TimeEvent>();
    public enum State {
        SELECTING_TURN,
        END_OF_ROUND,
        END_OF_COMBAT,
        ENEMY_TURN,
        ENEMY_ANIMATION,
        PLAYER_SELECTING_COMMAND,
        PLAYER_SELECTING_TARGET,
        PLAYER_SELECTING_ABILITY,
        PLAYER_PERFORMING_ACTION,
        PLAYER_GAMBLEING,
        SHOWING_TURBO_MODE,
        COMBAT_CHECK_ALIVE,
        END_OF_TURN
    }

    public class TimeEvent {
        public CombatEvent combatEvent;
        public DateTime time;
        public bool done = false;
        public TimeEvent(CombatEvent nextCombatEvent, Double timeToWait) {
            combatEvent = nextCombatEvent;
            time = DateTime.Now.AddSeconds(timeToWait);

        }

    }

    public State state;
    void ProcessEvent(CombatEvent processEvent) {

        switch (state)
        {

            case State.SELECTING_TURN:
                if (roundList.Count == 0) {
                    GoToState(State.END_OF_COMBAT);
                    break;
                } //end of combat
                if (roundList[0].Count == 0) {
                    GoToState(State.END_OF_ROUND);                 
                    break;
                } //end of round

                actTurn = roundList[0][0];
                characterDebugTurn = actTurn.character;

                roundList[0].RemoveAt(0);
                if(actTurn.character.modifiers.Count != 0)
                foreach (Modifier modifier in actTurn.character.modifiers)
                {
                    
                    modifier.turnsExpiration -= 1;
                    modifier.OnTurnBegin();
                        if (modifier.turnsExpiration == 0) {

                        modifier.isExpired = true;                    
                    }                    
                }
                actTurn.character.ManaCharge();

                actTurn.character.modifiers.RemoveAll(s => s.isExpired);
                if (actTurn.character.opportunityGauge >= actTurn.character.maxOpportunityGaugeBar)
                {
                    actTurn.character.opportunityGauge -= actTurn.character.maxOpportunityGaugeBar;
                    actTurn.character.opportunityMode = true;
                    actTurn.character.currentOpportunityTurn = actTurn.character.maxTurnsOpportunity;
                    text.text = actTurn.character.charactername + " has entered into opportunity Mode";
                }
                if (actTurn.character.isPlayer) {

                    CombatEvent combatEvent = new CombatEvent();
                    combatEvent.eventType = CombatEvent.EventType.PLAYER_COMMAND;
                    eventList.Add(combatEvent);
                    state = State.PLAYER_SELECTING_COMMAND;
                    actTurn.character.isDefending = false;
                    playerIndex = actTurn.character.playerIndex;
                    actTurn.character.currentDefense = actTurn.character.defense;
                }
                else {

                    CombatEvent combatEvent = new CombatEvent();
                    combatEvent.eventType = CombatEvent.EventType.START_ATTACK;
                    eventList.Add(combatEvent);

                    state = State.ENEMY_TURN; 
                }
                break;

            case State.ENEMY_TURN:                
                actTurn.character.behaviour.Act(actTurn.character, characters);
                CombatEvent testEvento = new CombatEvent();
                testEvento.eventType = CombatEvent.EventType.FINISH_ANIMATION;
                eventList.Add(testEvento);
                state = State.ENEMY_ANIMATION;
                break;
            case State.ENEMY_ANIMATION:
                actTurn.character.behaviour.ShowText();
                text.text = actTurn.character.behaviour.textToShow;
                DeplenishOpportunityMode();
                StartCoroutine(Vanish());

                break;            
            case State.PLAYER_SELECTING_COMMAND:
                if (processEvent.eventType == CombatEvent.EventType.PLAYER_COMMAND)
                {
                    text.text = "Select a command";
                    
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
                if (processEvent.eventType == CombatEvent.EventType.START_ATTACK) {

                    text.text = "Select a target";
                    if (targetIsConfirmed)
                    {
                        actTurn.character.behaviour.abilities[abilityIndex].UseAbility(actTurn.character, processEvent.targets[0]);                        
                        targetIsConfirmed = false;
                        GoToState(State.PLAYER_PERFORMING_ACTION);
                        //adaptarlo a esta estructura
                    }                    
                }


                if (processEvent.eventType == CombatEvent.EventType.START_DEFENSE) {

                    actTurn.character.behaviour.abilities[abilityIndex].UseAbility(actTurn.character);
                    targetIsConfirmed = false;
                    actTurn.character.currentDefense = (int)(actTurn.character.currentDefense * 1.5f);
                    GoToState(State.PLAYER_PERFORMING_ACTION);
                }
                break;
            case State.PLAYER_PERFORMING_ACTION:
                actTurn.character.behaviour.ShowText(abilityIndex);
                text.text = actTurn.character.behaviour.textToShow;
                DeplenishOpportunityMode();
                StartCoroutine(Vanish());

                break;
            case State.PLAYER_GAMBLEING:
                if (processEvent.eventType == CombatEvent.EventType.SELECTGAMBLE)
                    {
                }
                break;            
            case State.PLAYER_SELECTING_ABILITY:
                break;
            case State.COMBAT_CHECK_ALIVE:
                foreach (Character character in characters)
                {
                    if (character.isDead)
                    {
                        roundList[0].RemoveAll(s => s.character.isDead);
                        character.gameObject.SetActive(false);
                    }
                }

                if (playerCharacters.All(character => character.isDead))
                {
                    GoToState(State.END_OF_COMBAT);
                }
                else if (EnemyCharacters.All(character => character.isDead))
                {
                    victory = true;
                    GoToState(State.END_OF_COMBAT);
                }
                else {

                    foreach (Modifier modifier in actTurn.character.modifiers)
                    {
                        modifier.OnTurnEnd();
                    }
                    cleanse();
                    recalculateStats();
                    
                    StartTurn();
                
                }


                break; 

            case State.END_OF_TURN:


                foreach (Modifier modifier in actTurn.character.modifiers)
                {
                    modifier.OnTurnEnd();
                }
                cleanse();
                recalculateStats();

                GoToState(State.SELECTING_TURN);
                break;


            case State.END_OF_ROUND:
                roundList.RemoveAt(0);
                generateRounds();
                StartTurn();
                break;

            case State.END_OF_COMBAT:
                if (victory)
                {
                    text.text = "you win!";
                }
                else
                {
                    text.text = "you lose...";
                }
                break;
        }
    }




    void DeplenishOpportunityMode() {
        if (actTurn.character.opportunityMode)
        {
            actTurn.character.currentOpportunityTurn--;
            if (actTurn.character.currentOpportunityTurn <= 0) {
                actTurn.character.opportunityMode = false;                        
            }
        }    
    }
    void cleanse() {
        foreach (Character character in characters) {            
            character.currentAttack = character.attack;
            character.currentDefense = character.defense;
            character.currentIntelligence = character.intelligence;
            character.currentResistance = character.resistance;
            character.currentAgility = character.agility;
        }        
    }

    void recalculateStats() {
        foreach (Character character in characters) {
            if(character.modifiers.Count != 0)
            foreach (Modifier modifier in character.modifiers) {
                modifier.AddStatCalculation(character);
                modifier.MultiplyStatCalculation(character);
            }
        
        
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
        yield return new WaitForSecondsRealtime(3f);
        text.text = "";
        GoToState(State.COMBAT_CHECK_ALIVE);
    }

    IEnumerator goToTargetStateWithText(State state) {
        yield return new WaitForSecondsRealtime(1.7f);
        text.text = "";
        GoToState(state);
    }


    void Start()
    {

        foreach (Character character in characters) {
            if (character.isPlayer)
            {
                playerCharacters.Add(character);
            }
            else {
                EnemyCharacters.Add(character);            
            }
                
        }
        generateRounds();
        StartTurn();
    }

    void StartTurn() {

        CombatEvent testEvent = new CombatEvent();
        testEvent.eventType = CombatEvent.EventType.START_COMBAT;
        eventList.Add(testEvent);
        state = State.SELECTING_TURN;

    }

    void GoToState(State newState)
    {

        CombatEvent testEvent = new CombatEvent();
        testEvent.eventType = CombatEvent.EventType.DUMMY;
        eventList.Add(testEvent);
        state = newState;
        
    }

    void FixedUpdate()
    {


        foreach (TimeEvent timeEvent in timeEvents) {
            if (DateTime.Now.CompareTo(timeEvent.time) < 0) {
                if (!timeEvent.done) {
                    eventList.Add(timeEvent.combatEvent);
                    timeEvent.done = true;
                }
            
            }
               
        }
        
        

        if (eventList.Count != 0)
        {
            CombatEvent combatEvent = eventList.Last();
            eventList.RemoveAt(eventList.Count - 1);
            ProcessEvent(combatEvent);
        }
        timeEvents.RemoveAll(s => s.done);

    }

    void generateRounds() {

        roundController = new RoundController(characters);
        roundController.CalculateTurn(characters);
        roundList.Add(roundController.turns);
    }
    // Update is called once per frame
}
