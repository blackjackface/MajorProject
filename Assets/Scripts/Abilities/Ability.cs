using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{

    //this is a regular monoTargetSkill
    public string abilityName;
    public string showText = "something is wrong";
    public string userName;
    public string targetName;
    public virtual void UseAbility(Character user ,Character target)
    {
        

    }
    public virtual void UseAbility(Character user)
    {


    }
    public virtual void UseAbility(Character user ,List<Character> targets) { }

    public virtual void ShowText() { }
}
