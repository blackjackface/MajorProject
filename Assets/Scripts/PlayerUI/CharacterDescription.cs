using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static CombatController;

public class CharacterDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string descriptionString = "";
    public GameObject combatText;
    Character selfCharacter;
    CombatController combatController;
    // Start is called before the first frame update
    void Start()
    {
        selfCharacter = gameObject.GetComponent<Character>();
        combatController = GameObject.Find("CombatManager").GetComponent<CombatController>();
        combatText = GameObject.Find("CombatText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (combatController.state == State.PLAYER_SELECTING_COMMAND || combatController.state == State.PLAYER_SELECTING_TARGET)
        {
            string modifiers = "";
            if (selfCharacter.modifiers.Count != 0)
                foreach (Modifier modifier in selfCharacter.modifiers)
                {
                    modifiers += modifier.modifierName + " ";
                }
            else
                modifiers = "NONE";

            descriptionString = "maxHP: " + selfCharacter.maxHP.ToString() + " Attack: " + selfCharacter.currentAttack.ToString() + " Defense: " +
                selfCharacter.currentDefense + "\n" + "Intelligence: " + selfCharacter.currentIntelligence.ToString() + " resistance: " + selfCharacter.currentResistance.ToString() + "\n" +
                "Initiative: " + selfCharacter.initiative.ToString() + " Mana charge per turn " + selfCharacter.manaRechargePerTurn + "\n"  + " Current modifiers: " + modifiers;
            combatText.GetComponent<Text>().text = descriptionString;
        }
    }

    public void OnMouseOver()
    {
        if (combatController.state == State.PLAYER_SELECTING_COMMAND || combatController.state == State.PLAYER_SELECTING_TARGET)
        {
            string modifiers = "";
            if (selfCharacter.modifiers.Count != 0)
                foreach (Modifier modifier in selfCharacter.modifiers)
                {
                    modifiers += modifier.modifierName + " ";
                }
            else
                modifiers = "NONE";

            descriptionString = "maxHP: " + selfCharacter.maxHP.ToString() + " Attack: " + selfCharacter.currentAttack.ToString() + " Defense: " +
                selfCharacter.currentDefense + "\n" + "Intelligence: " + selfCharacter.currentIntelligence.ToString() + " resistance: " + selfCharacter.currentResistance.ToString() + "\n" +
                "Initiative: " + selfCharacter.initiative.ToString() + " Mana charge per turn " + selfCharacter.manaRechargePerTurn + "\n" + "Current modifiers: " + modifiers;
            combatText.GetComponent<Text>().text = descriptionString;
        }
    }

    public void OnMouseExit()
    {
        if (combatController.state == State.PLAYER_SELECTING_COMMAND)
            combatText.GetComponent<Text>().text = "Select a command";
        if (combatController.state == State.PLAYER_SELECTING_TARGET)
            combatText.GetComponent<Text>().text = "Select a target";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (combatController.state == State.PLAYER_SELECTING_COMMAND)
            combatText.GetComponent<Text>().text = "Select a command";
        if(combatController.state == State.PLAYER_SELECTING_TARGET)
            combatText.GetComponent<Text>().text = "Select a target";

    }
}
