using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralOffensiveAbility : AbilityRisk
{
    public int baseDamage = 1;
    int damage = 0;
    int currentRiskFactor;
    
    public override void UseAbility(Character user, Character target)
    {
        CalculateMultiplier(target);
        int userDamage;
        if (isMagic)
            userDamage = user.intelligence;
        else
            userDamage = user.attack;
        user.mana -= manaCost;
        if(isMagic)
          damage = (int)((baseDamage + userDamage) * multiplier) * user.level - target.resistance * target.level;
        else
          damage = (int)((baseDamage + userDamage) * multiplier)*user.level - target.defense*target.level;
        if (damage < 0)
        {
            damage = 0;
        }
        currentRiskFactor = (int)(riskFactor / multiplier);
        int randomDice = Random.Range(1, 100);
        if (randomDice < currentRiskFactor)
        {
            hits = false;
            target.mana += manaCost;
        }
        else
        {
            hits = true;
            target.currentHP -= damage;
        }
        userName = user.name;
        targetName = target.name;
        Debug.Log(" " + user.name + " dealt " + damage.ToString() + " Damage to " + target.name);
        if (target.currentHP <= 0)
        {
            target.isDead = true;
        }
        ShowText();
    }
    public override void ShowText()
    {
        showText = "";
        if (hits)
        {
            showText = " " + userName +" used "+ this.name + " dealt " + damage.ToString() + " damage to " + targetName;
        }
        else 
        {
            showText =" "+ this.name + " missed and gave "+ manaCost + " mana to " + targetName;
        }
        Debug.Log("el nuevo texto es: " + showText);
    }
}
