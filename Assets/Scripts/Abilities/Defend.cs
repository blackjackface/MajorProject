using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : Ability
{

    public override void UseAbility(Character user, Character target)
    {
        user.currentDefense =user.currentDefense + user.currentDefense/2;
        user.mana += 10;
        //happends until the end of the turn
    }

}
