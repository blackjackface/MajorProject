using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : Ability
{


    int damage = 0;
    string userName;
    string targetName;
    

    
    public override  void UseAbility(Character user, Character target)
    {


        damage = user.currentAttack - target.currentDefense;
        if (damage < 0) {
            damage = 0;
        }

        target.currentHP -= damage;
        userName = user.name;
        targetName = target.name;
        Debug.Log(" " +  user.name + " dealt " + damage.ToString() + " Damage to " + target.name);
        if (target.currentHP <= 0) {
            target.isDead = true;
        }
        ShowText();
    //here happens animation in battle.
    }


    void PerformAnimation() {
    
    //pasan cosas y numeritos en pantalla
    
    }
    public override void ShowText() {
        showText = " " + userName + " dealt " + damage.ToString() + " Damage to " + targetName;
    }


}
