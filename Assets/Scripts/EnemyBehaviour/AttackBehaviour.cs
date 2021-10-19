using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : Behaviour
{
    //Lista de personajes el cual son personajes enemigos


   public override void Act(List<Character> characters)
    {
        //Performs an offensive Action

        Ability ability = abilities[0];
        Character target = characters[Random.Range(0, characters.Count)];

        while (target == this.gameObject.GetComponent<Character>()) {
            
            target = characters[Random.Range(0, characters.Count)];
        }

        ability.UseAbility(self,characters[Random.Range(0, characters.Count)]);


    }


    // Start is called before the first frame update


    // Update is called once per frame

}
