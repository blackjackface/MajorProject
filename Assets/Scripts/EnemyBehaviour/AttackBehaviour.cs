using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AttackBehaviour : Behaviour
{
    //Lista de personajes el cual son personajes enemigos


   public override void Act(Character user, List<Character> targets)
    {
        //Performs an offensive Action

        Ability ability = abilities[0];
        Character target = targets[Random.Range(0, targets.Count)];

        while (target == user || !target.isPlayer) {
                target = targets[Random.Range(0, targets.Count)];
        }

        ability.UseAbility(user, target);


    }

    

    public override void ShowText()
    {

        Ability ability = abilities[0];
        ability.ShowText();
        textToShow = ability.showText;


    }
    // Start is called before the first frame update


    // Update is called once per frame

}
