using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : Ability
{


    int damage = 0;
    int manaGain = 0;
    

    
    public override  void UseAbility(Character user, Character target)
    {


        damage = user.currentAttack - target.currentDefense;
        if (damage < 0) {
            damage = 0;
        }

        target.currentHP -= damage;
        userName = user.charactername;
        targetName = target.charactername;
        Debug.Log(" " +  user.charactername + " dealt " + damage.ToString() + " Damage to " + target.charactername);
        if (target.currentHP <= 0) {
            target.isDead = true;
        }
        ShowText();
    //here happens animation in battle.
    }



    public override void ShowText() {
        showText = "";
        showText = " " + userName + " dealt " + damage.ToString() + " Damage to " + targetName;
        Debug.Log("el nuevo texto es: "+ showText);
    }

    public override string FillDescription()
    {
        return "deals regular damage to enemies, no risk involved";
    }

}
