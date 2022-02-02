using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : Ability
{

    int manaGain = 10;

    public override void UseAbility(Character user)
    {
        userName = user.charactername;

        user.isDefending = true;
        user.mana += manaGain;
        //happends until the end of the turn
    }
    public override void ShowText()
    {
        showText = userName + " defends, gaining: " + manaGain.ToString()  + " mana!";
    }
}
