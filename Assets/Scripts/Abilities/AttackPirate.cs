using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPirate : Ability
{


    int damage = 0;
    public int extraDamage = 0;
    public int manaDrain = 0;
    int manaDrained = 0;

    
    public override  void UseAbility(Character user, Character target)
    {

        userName = user.charactername;
        targetName = target.charactername;
        damage = (user.currentAttack + extraDamage) - target.currentDefense;
        if (damage < 0) {
            damage = 0;
        }

        target.currentHP -= damage;
        if (target.mana < manaDrain)
        {
            user.mana += target.mana;
            manaDrained = target.mana;
            target.mana = 0;
        }
        else {
            target.mana -= manaDrain;
            manaDrained = manaDrain;
            user.mana += manaDrain;
        }


        Debug.Log(" " +  user.charactername + " attacks, dealt " + damage.ToString() + " Damage to " + target.charactername);
        if (target.currentHP <= 0) {
            target.isDead = true;
        }
        userName = user.charactername;
        targetName = target.charactername;
        ShowText();
    }



    public override void ShowText() {
        showText = "";
        showText = " " + userName + " dealt " + damage.ToString() + " damage to " + targetName + " and stole " + manaDrained.ToString() + " mana" ;
        Debug.Log("el nuevo texto es: "+ showText);
    }

    public override string FillDescription()
    {
        return "deals regular damage to enemies, no risk involved";
    }

}
