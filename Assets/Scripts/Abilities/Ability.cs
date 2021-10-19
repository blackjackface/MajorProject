using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

    //this is a regular monoTargetSkill
   public string abilityName;
    public virtual void UseAbility(Character user,Character target)
    {
        

    }
}
