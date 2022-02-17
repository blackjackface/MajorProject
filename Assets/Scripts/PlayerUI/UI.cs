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
    public Button cancelButton;
    [SerializeField]
    List<Text> turnListText = new List<Text>();
    public Character target;
    [SerializeField]
    List<List<GameObject>> buttonListPerCharacter = new List<List<GameObject>>(); 
    bool targetConfirmed = false;
    public GameObject buttonPrefab;
    public Canvas canvas;
    [SerializeField]
    AudioSource buttonPressClickSound;
    [SerializeField]
    AudioClip playSound;
    [SerializeField]
    Text referenceText;
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
                int initialX =500; //values taken from scene, may change later
                int initialY = 400;
                int anotherInitialValueX = 800;
                int additionalY = 0;
                bool isLeft = true;

                GameObject createdButton;
                for (int i = 2; i < character.behaviour.abilities.Count; i++) {
                    if (isLeft) {
                    createdButton = ButtonConstructor(new Vector3(initialX, initialY + additionalY, 0), 
                        character.behaviour.abilities[i].abilityName, this, i, character.behaviour.abilities[i].FillDescription());
                    } else {
                    createdButton = ButtonConstructor(new Vector3(anotherInitialValueX, initialY + additionalY, 0), 
                        character.behaviour.abilities[i].abilityName, this , i, character.behaviour.abilities[i].FillDescription());
                        additionalY -= 70;
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
        cancelButton.onClick.AddListener(CancelButtonPress);
    }

    GameObject ButtonConstructor(Vector3 position, string text, UI ui, int index, string abilityText)
    {

        GameObject createdButton = Instantiate(buttonPrefab); 
        createdButton.GetComponent<RectTransform>().SetParent(canvas.transform);
        createdButton.GetComponent<RectTransform>().position = new Vector3(position.x + canvas.GetComponent<RectTransform>().position.x, position.y + canvas.GetComponent<RectTransform>().position.y, 0);
        createdButton.GetComponent<RectTransform>().SetParent(canvas.transform);
        createdButton.GetComponentInChildren<Text>().text = text;
        createdButton.GetComponent<Button>().onClick.AddListener(() => ui.UseAbilitywithIndex(index));
        createdButton.GetComponent<Button>().onClick.AddListener(() => ui.PlaySound());
        createdButton.GetComponent<CombatTextTip>().descriptionString = abilityText;
        createdButton.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        return createdButton;
    }

    void PlaySound() {
        float randomNumber = Random.Range(1f, 1.1f);
        buttonPressClickSound.pitch = randomNumber;
        buttonPressClickSound.PlayOneShot(playSound);
    }

    void UseAbilitywithIndex(int index) {

        combatController.abilityIndex = index;
        combatController.state = CombatController.State.PLAYER_SELECTING_TARGET;
        CombatEvent combatEvent = new CombatEvent();
        combatEvent.eventType = CombatEvent.EventType.START_ATTACK;
        combatController.eventList.Add(combatEvent);
    }

    // Updates calls the combat controller State and changes the UI depending which phase it is.    
    void Update()
    {
        int playerIndex = combatController.actTurn.character.playerIndex;
        switch (combatController.state)
        {
            case CombatController.State.PLAYER_SELECTING_COMMAND:
                attackButton.gameObject.SetActive(true);
                defenseButton.gameObject.SetActive(true);
                gambleButton.gameObject.SetActive(true);
                playerIndex = combatController.actTurn.character.playerIndex;
                buttonListPerCharacter[playerIndex].ForEach(x => x.SetActive(false));
                cancelButton.gameObject.SetActive(false);

                break;
            case CombatController.State.SELECTING_TURN:   
                cancelButton.gameObject.SetActive(false);
                UpdateTurnListText();


                break;            
            case CombatController.State.END_OF_ROUND:
                UpdateTurnListText();
                cancelButton.gameObject.SetActive(false);
                break;
            case CombatController.State.END_OF_COMBAT:             
            case CombatController.State.ENEMY_TURN:
                cancelButton.gameObject.SetActive(false);
                break;            
            case CombatController.State.ENEMY_ANIMATION:
                cancelButton.gameObject.SetActive(false);
                UpdateTurnListText();
                break;                         
            case CombatController.State.PLAYER_SELECTING_TARGET:
                attackButton.gameObject.SetActive(false);
                defenseButton.gameObject.SetActive(false);
                gambleButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(true);
                buttonListPerCharacter[playerIndex].ForEach(x => x.SetActive(false));
                break;
            case CombatController.State.PLAYER_PERFORMING_ACTION:
                cancelButton.gameObject.SetActive(false);
                UpdateTurnListText();
                break;        
            case CombatController.State.PLAYER_SELECTING_ABILITY:

            case CombatController.State.PLAYER_GAMBLEING:
                playerIndex =  combatController.actTurn.character.playerIndex;
                buttonListPerCharacter[playerIndex].ForEach(x => x.SetActive(true));
                int i = 0;
                foreach (Ability ability in combatController.actTurn.character.behaviour.abilities) {
                    ability.CheckIfUsable(combatController.actTurn.character);
                    if (i>1)
                    {
                        if ((combatController.actTurn.character.mana < ability.manaCost) || ability.usable == false)
                        {
                            buttonListPerCharacter[playerIndex][i - 2].GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 155);
                            buttonListPerCharacter[playerIndex][i - 2].GetComponent<Button>().interactable = false;
                        }
                        else {
                            buttonListPerCharacter[playerIndex][i - 2].GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
                            buttonListPerCharacter[playerIndex][i - 2].GetComponent<Button>().interactable = true;
                        }
                    }
                    i++;
                }
                cancelButton.gameObject.SetActive(true);
                break;
            case CombatController.State.COMBAT_CHECK_ALIVE:
                attackButton.gameObject.SetActive(false);
                defenseButton.gameObject.SetActive(false);
                gambleButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(false);
                UpdateTurnListText();
                break;
            case CombatController.State.END_OF_TURN:
                attackButton.gameObject.SetActive(false);
                defenseButton.gameObject.SetActive(false);
                gambleButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(false);
                UpdateTurnListText();
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
                Debug.Log("new target: " + characterTarget.charactername + " has been selected");
                foreach (Character character in combatController.characters)
                {

                    character.gameObject.transform.position = character.originalPosition;
        //            character.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
                    if (character.gameObject.GetComponent<ColorLerp>() != null)
                        character.gameObject.GetComponent<ColorLerp>().enabled = false;
                    if (character.gameObject.GetComponent<ColorLerp>() != null)
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
        //        character.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
                if (character.gameObject.GetComponent<ColorLerp>() != null)
                    character.gameObject.GetComponent<ColorLerp>().enabled = false;
                if (character.gameObject.GetComponent<ColorLerp>() != null)
                    character.gameObject.GetComponent<LerpPosition>().enabled = false;

            }
            CombatEvent combatEvent = new CombatEvent();
            combatEvent.eventType = CombatEvent.EventType.START_ATTACK;
            combatEvent.targets.Clear();
            combatEvent.targets.Add(target);
            target = null;
            combatController.targetIsConfirmed = true;
            combatController.eventList.Add(combatEvent);
            //ejecutar acci�n y nos movemos al final del turno
        }
    }
    public void UpdateTurnListText()
    {
        foreach (Text text in turnListText) {

            Destroy(text.gameObject);
        
        
        }
        turnListText.Clear();
        int heigh = 960;
        foreach (Turn turn in combatController.roundList[0])
        {
            if (!turn.character.isDead)
            {
                GameObject gameObjectText = new GameObject();
                string characterName = "";
                if (turn.character.isPlayer)
                {
                   characterName = ">" + turn.character.charactername;
                }
                else {

                   characterName = turn.character.charactername;
                }
                Text characterNameText = gameObjectText.AddComponent<Text>();
                if (!turn.character.isPlayer) {

                    characterNameText.color = Color.red;
                }
                characterNameText.GetComponent<Text>().font = referenceText.font;
                characterNameText.GetComponent<Text>().fontSize = referenceText.fontSize;
                characterNameText.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                characterNameText.text = characterName;
                if (turn.character.currentOpportunityTurn > 0)
                {
                    characterNameText.text = characterName + "*";
                }
                characterNameText.GetComponent<RectTransform>().position = new Vector3(960, heigh, 0);
                characterNameText.GetComponent<RectTransform>().SetParent(canvas.transform);
                turnListText.Add(characterNameText);
                heigh -= 25;
            }
        }
        if(turnListText.Count != 0)
        turnListText[0].color = Color.yellow;
    }


    void AttackButtonPress() {
        Debug.Log("he atacao");
        combatController.abilityIndex = 0;
        PlaySound();
        combatController.state = CombatController.State.PLAYER_SELECTING_TARGET;
        CombatEvent combatEvent = new CombatEvent();
        combatEvent.eventType = CombatEvent.EventType.START_ATTACK;
        combatEvent.playerCommand = CombatEvent.PlayerCommand.ATTACK;
        combatController.eventList.Add(combatEvent);
        
    }
    void DefenseButtonPress()
    {
        Debug.Log("he defendio");
        PlaySound();
        combatController.abilityIndex = 1;
        CombatEvent combatEvent = new CombatEvent();
        combatController.state = CombatController.State.PLAYER_SELECTING_TARGET;
        combatEvent.eventType = CombatEvent.EventType.START_DEFENSE;
        combatEvent.playerCommand = CombatEvent.PlayerCommand.DEFEND;
        combatController.eventList.Add(combatEvent);
    }

    void CancelButtonPress()
    {
        Debug.Log("he cancelado");
        CombatEvent combatEvent = new CombatEvent();
        combatController.state = CombatController.State.PLAYER_SELECTING_COMMAND;
        foreach (Character character in combatController.characters)
        {

            character.gameObject.transform.position = character.originalPosition;
            character.gameObject.GetComponent<SpriteRenderer>().color = character.gameObject.GetComponent<ColorLerp>().baseColor;
            if (character.gameObject.GetComponent<ColorLerp>()!= null)
              character.gameObject.GetComponent<ColorLerp>().enabled = false;
            if (character.gameObject.GetComponent<ColorLerp>() != null)
              character.gameObject.GetComponent<LerpPosition>().enabled = false;
        }
        PlaySound();
        target = null;
        combatEvent.eventType = CombatEvent.EventType.PLAYER_COMMAND;
        combatEvent.playerCommand = CombatEvent.PlayerCommand.DEFEND;
        combatController.eventList.Add(combatEvent);
    }
    void GambleButtonPress() {
        Debug.Log("me la he jugado");
        combatController.state = CombatController.State.PLAYER_GAMBLEING;
        PlaySound();
    }
}
