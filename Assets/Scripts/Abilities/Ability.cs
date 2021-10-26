using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

    //this is a regular monoTargetSkill
    public string abilityName;
    public Character user;

    public virtual void UseAbility(Character target)
    {
        

    }

    public virtual void UseAbility(List<Character> targets) { }
}
