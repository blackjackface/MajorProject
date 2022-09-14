using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEnhanced : Attack
{


    public int damage = 0;
    public int extraDamage = 0;
    int manaGain = 0;
    

    
    public override  void UseAbility(Character user, Character target)
    {


        damage = (user.currentAttack + extraDamage) - target.currentDefense;
        if (damage < 0)
        {
            damage = 0;
        }

        target.currentHP -= damage;


        Debug.Log(" " + user.charactername + " dealt " + damage.ToString() + " Damage to " + target.charactername);
        if (target.currentHP <= 0)
        {
            target.isDead = true;
        }
        userName = user.charactername;
        targetName = target.charactername;
        ShowText();

        //here happens animation in battle.
    }



    public override void ShowText() {
        showText = "";
        showText = " " + userName + " attacked and dealt " + damage.ToString() + " Damage to " + targetName;
        Debug.Log("el nuevo texto es: "+ showText);
    }

    public override string FillDescription()
    {
        return "deals regular damage to enemies, no risk involved";
    }

}
