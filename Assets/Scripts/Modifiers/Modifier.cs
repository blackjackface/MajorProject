using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier : MonoBehaviour
{
    // if any expiration reach 0 the modifier will fade away
    public int maxTurnsExpiration = -1;
    public int maxAttackExpiration = -1;
    public int turnsExpiration = -1; 
    public int roundsExpiration = -1;
    public int attackExpiration = -1;
    public bool isExpired = false;
    [SerializeField]
    public int additionalAttack = 0;
    [SerializeField]
    public int additionalDefense = 0;
    [SerializeField]
    public int additionalIntelligence = 0;
    [SerializeField]
    public int additionalResistance = 0;
    [SerializeField]
    public int additionalAgility = 0;
    [SerializeField]
    public float attackMultiplier = 1;
    [SerializeField]
    public float defenseMultiplier = 1;
    [SerializeField]
    public float intelligenceMultiplier = 1;
    [SerializeField]
    public float resistanceMultiplier = 1;
    [SerializeField]
    public float agilityMultiplier = 1;
    [SerializeField]
    public float initiativeMultiplier = 1;
    // the current stats are constantly recalculated, meaning that after each state there should be a statuscheck


    public void Start()
    {
        turnsExpiration = maxTurnsExpiration;
        attackExpiration = maxAttackExpiration;
    }
    
    public string modifierName = "";
    public string description = "";

    //triggers when character performs an action
    public void OnAction() { }
    //triggers when character get hit
    public void OnGettingAttacked() { }
    //triggers at turn begin
    public void OnTurnBegin() { }
    //triggers at turn end
    public void OnTurnEnd() { }
    // triggers on round begins
    public void OnRoundBegin() { }
    // triggers on round ends
    public void OnRoundEnds() { }
    public void AddStatCalculation(Character character) {
         character.currentAttack += additionalAttack;
         character.currentDefense += additionalDefense;
         character.currentIntelligence += additionalIntelligence;
         character.currentResistance += additionalResistance;
         character.currentAgility += additionalAgility;
        
    }

    public void MultiplyStatCalculation(Character character) {
        character.currentAttack = (int)(character.currentAttack *attackMultiplier);
        character.currentDefense = (int)(character.currentDefense * defenseMultiplier);
        character.currentIntelligence = (int)(character.currentIntelligence * intelligenceMultiplier);
        character.currentResistance = (int)(character.currentResistance * resistanceMultiplier);
        character.currentAgility = (int)(character.currentAgility * agilityMultiplier);
    }

}
