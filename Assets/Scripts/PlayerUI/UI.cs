using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public CombatController combatController;
    public Button attackButton;
    public Button defenseButton;
    public Button gambleButton;
    public Character target;
    List<List<GameObject>> buttonListPerCharacter = new List<List<GameObject>>(); 
    bool targetConfirmed = false;
    public GameObject buttonPrefab;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Character character in combatController.characters) {

            character.m_MyEvent.AddListener(delegate { returnTarget(character); });
        }
        int index = 0;
        foreach (Character character in combatController.characters)
        {
            character.playerIndex = index;
            List<GameObject> buttonList = new List<GameObject>();
            if (character.isPlayer && character.behaviour.abilities.Count > 2) {
                int initialX = 266; //values taken from scene, may change later
                int initialY = 300;
                int anotherInitialValueX = 440;
                int additionalY = 0;
                bool isLeft = true;

                GameObject createdButton;
                for (int i = 2; i < character.behaviour.abilities.Count; i++) {
                    if (isLeft) {
                    createdButton = ButtonConstructor(new Vector3(initialX, initialY + additionalY, 0), 
                        character.behaviour.abilities[i].abilityName, character.behaviour.abilities[i]);
                    } else {
                    createdButton = ButtonConstructor(new Vector3(anotherInitialValueX, initialY + additionalY, 0), 
                        character.behaviour.abilities[i].abilityName, character.behaviour.abilities[i]);
                        additionalY -= 28;
                    }
                    createdButton.gameObject.SetActive(false);
                    buttonList.Add(createdButton);
                    isLeft = !isLeft;
                }
                combatController.players.Add(character);
            }
            
            buttonListPerCharacter.Add(buttonList);
            index++;

        }


        attackButton.onClick.AddListener(AttackButtonPress);
        defenseButton.onClick.AddListener(DefenseButtonPress);
        gambleButton.onClick.AddListener(GambleButtonPress);
    }

    GameObject ButtonConstructor(Vector3 position, string text, Ability ability)
    {

        GameObject createdButton = Instantiate(buttonPrefab); 
        createdButton.GetComponent<RectTransform>().SetParent(canvas.transform);
        createdButton.GetComponent<RectTransform>().position = new Vector3(position.x + canvas.GetComponent<RectTransform>().position.x, position.y + canvas.GetComponent<RectTransform>().position.y, 0);
        createdButton.GetComponent<RectTransform>().SetParent(canvas.transform);
        createdButton.GetComponentInChildren<Text>().text = text;
     //   createdButton.GetComponent<Button>().onClick.AddListener(() => ability.UseAbility(this.GetComponent<Character>(), target));
        return createdButton;
    }

    // Update is called once per frame
    void Update()
    {
        switch (combatController.state)
        {
            case CombatController.State.PLAYER_SELECTING_COMMAND:
                attackButton.gameObject.SetActive(true);
                defenseButton.gameObject.SetActive(true);
                gambleButton.gameObject.SetActive(true);
                break;
            case CombatController.State.SELECTING_TURN:                
            case CombatController.State.END_OF_ROUND:              
            case CombatController.State.END_OF_COMBAT:             
            case CombatController.State.ENEMY_TURN:            
            case CombatController.State.ENEMY_ANIMATION:              
            case CombatController.State.PLAYER_SELECTING_TARGET:            
            case CombatController.State.PLAYER_SELECTING_ABILITY:             
            case CombatController.State.PLAYER_GAMBLEING:
             int playerIndex =  combatController.actTurn.character.playerIndex;
             buttonListPerCharacter[playerIndex].ForEach(x => x.SetActive(true));
                break;
            case CombatController.State.END_OF_TURN:
                attackButton.gameObject.SetActive(false);
                defenseButton.gameObject.SetActive(false);
                gambleButton.gameObject.SetActive(false);
                break;
        }

    }
    //return Target in this case is exclusive to solo targets, then multiple targets may be added later
    void returnTarget(Character characterTarget) {
        Character previousTarget = target;
        if (previousTarget != characterTarget)
        {
            if (combatController.state == CombatController.State.PLAYER_SELECTING_TARGET)
            {
                Debug.Log("new target: " + characterTarget.name + " has been selected");
                foreach (Character character in combatController.characters)
                {

                    character.gameObject.transform.position = character.originalPosition;
                    character.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
                    character.gameObject.GetComponent<ColorLerp>().enabled = false;
                    character.gameObject.GetComponent<LerpPosition>().enabled = false;
                }

                characterTarget.gameObject.GetComponent<ColorLerp>().enabled = true;
                characterTarget.gameObject.GetComponent<LerpPosition>().enabled = true;
                target = characterTarget;
            }
            combatController.targetIsConfirmed = false;
        }
        else {
            foreach (Character character in combatController.characters)
            {
                character.gameObject.transform.position = character.originalPosition;
                character.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
                character.gameObject.GetComponent<ColorLerp>().enabled = false;
                character.gameObject.GetComponent<LerpPosition>().enabled = false;

            }
            CombatEvent combatEvent = new CombatEvent();
            combatEvent.eventType = CombatEvent.EventType.START_ATTACK;
            combatEvent.targets.Clear();
            combatEvent.targets.Add(target);
            target = null;
            combatController.targetIsConfirmed = true;
            combatController.eventList.Add(combatEvent);
            //ejecutar acción y nos movemos al final del turno
        }
    }

    void AttackButtonPress() {
        Debug.Log("he atacao");
        combatController.state = CombatController.State.PLAYER_SELECTING_TARGET;
        CombatEvent combatEvent = new CombatEvent();
        combatEvent.eventType = CombatEvent.EventType.START_ATTACK;
        combatEvent.playerCommand = CombatEvent.PlayerCommand.ATTACK;
        combatController.eventList.Add(combatEvent);
        
    }
    void DefenseButtonPress()
    {
        Debug.Log("he defendio");
        CombatEvent combatEvent = new CombatEvent();
        combatController.state = CombatController.State.PLAYER_SELECTING_TARGET;
        combatEvent.eventType = CombatEvent.EventType.START_DEFENSE;
        combatEvent.playerCommand = CombatEvent.PlayerCommand.DEFEND;
        combatController.eventList.Add(combatEvent);
    }
    void GambleButtonPress() {
        Debug.Log("me la he jugado");
        combatController.state = CombatController.State.PLAYER_GAMBLEING;
    
    
    }

}
