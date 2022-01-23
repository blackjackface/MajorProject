using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier : MonoBehaviour
{
    // if any expiration reach 0 the modifier will fade away

    public int turnsExpiration = -1; 
    public int roundsExpiration = -1;
    public int attackExpiration = -1;
    public bool isExpired = false;
    // the current stats are constantly recalculated, meaning that after each state there should be a statuscheck

    float attackMultiplier = 1;
    float defenseMultiplier = 1;
    float intelligenceMultiplier = 1;
    float resistanceMultiplier = 1;
    float agilityMultiplier = 1;
    float initiativeMultiplier = 1;

    string description = "";

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
}
