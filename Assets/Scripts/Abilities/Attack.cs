using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability
{

   public override  void UseAbility( Character target)
    {

        int damage = user.currentAttack - target.currentDefense;
        if (damage < 0) {
            damage = 0;
        }

        target.currentHP -= damage;
        
        Debug.Log(" " +  user.name + "dealt " + damage.ToString() + " Damage to " + target.name);
        if (target.currentHP <= 0) {
            target.isDead = true;
        }

       
        
        //here happens animation in battle.
    }


    void PerformAnimation(int damage, Animator animation) {
    
    //pasan cosas y numeritos en pantalla
    
    }

}
