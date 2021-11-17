using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWave : Ability
{
    [SerializeField]
    int damage = 0;
    bool success = true;
    public override void UseAbility(Character user, Character target)
    {
        riskFactor = 60;
        damage = user.currentIntelligence - target.currentResistance;
        damage = (int) (damage * 1.2f);
        if (damage < 0)
        {
            damage = 0;
        }


        int randomDice = Random.RandomRange(1, 100);
        if (randomDice >= 60)
        {
            target.currentHP -= damage;

            userName = user.name;
            targetName = target.name;
            Debug.Log(" " + user.name + " dealt " + damage.ToString() + " Damage to " + target.name);
            if (target.currentHP <= 0)
            {
                target.isDead = true;
            }
           success = true;
        }
        else {
            success = false;
        }
        ShowText();
    }

    public override void ShowText()
    {
        
        showText = "";
        if (success)
            showText = " " + userName + " dealt " + damage.ToString() + " damage to " + targetName + "of dark damage";
        else
            showText = userName + " shoots a dark wave but failed, miserably";
    }
}
