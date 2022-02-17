using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StallAbility : Ability
{
    // Start is called before the first frame update
    [SerializeField]
    string textDescription = "";

    public override void UseAbility(Character user, Character target)
    {


        
    }

    public override void ShowText()
    {
        showText = textDescription;
    }
}
