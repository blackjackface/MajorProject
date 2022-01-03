using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDebugSpell : Ability
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void UseAbility(Character user, Character target)
    {   
        Debug.Log("you used the Debug Spell");
  //      base.UseAbility(user, target);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
