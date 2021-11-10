using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public CombatController combatController;
    public Button attackButton;
    public Button defenseButton;
    public Button gambleButton;


    
    // Start is called before the first frame update
    void Start()
    {
        attackButton.onClick.AddListener(AttackButtonPress);
        defenseButton.onClick.AddListener(DefenseButtonPress);
        gambleButton.onClick.AddListener(GambleButtonPress);

    }

    // Update is called once per frame
    void Update()
    {
        switch (combatController.state)
        {
            case CombatController.State.PLAYER_SELECTING_COMMAND:
                attackButton.interactable = true;
                defenseButton.interactable = true;
                gambleButton.interactable = true;
                break;
            case CombatController.State.SELECTING_TURN:                
            case CombatController.State.END_OF_ROUND:              
            case CombatController.State.END_OF_COMBAT:             
            case CombatController.State.ENEMY_TURN:            
            case CombatController.State.ENEMY_ANIMATION:              
            case CombatController.State.PLAYER_SELECTING_TARGET:            
            case CombatController.State.PLAYER_SELECTING_ABILITY:             
            case CombatController.State.PLAYER_GAMBLEING:             
            case CombatController.State.END_OF_TURN:
                attackButton.interactable = false;
                defenseButton.interactable = false;
                gambleButton.interactable = false;
                break;
        }

    }    

    void AttackButtonPress() {
        Debug.Log("he atacao");
        CombatEvent combatEvent = new CombatEvent();
        combatEvent.eventType = CombatEvent.EventType.PLAYER_COMMAND;
        combatEvent.playerCommand = CombatEvent.PlayerCommand.ATTACK;
        combatController.eventList.Add(combatEvent);

    }
    void DefenseButtonPress()
    {
        Debug.Log("he defendio");
        CombatEvent combatEvent = new CombatEvent();
        combatEvent.eventType = CombatEvent.EventType.PLAYER_COMMAND;
        combatEvent.playerCommand = CombatEvent.PlayerCommand.DEFEND;
        combatController.eventList.Add(combatEvent);
    }
    void GambleButtonPress() { }

}
