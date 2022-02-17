using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{

    //this is a regular monoTargetSkill
    public string abilityName;
    public string showText = "something is wrong";
    public string description = "";
    public string userName;
    public string targetName;
    public int manaCost = 0;
    public float gaugeFillValue;
    public Vector3 targetPosition;
    public Vector3 offsetdistance;
    public GameObject referenceText;
    public GameObject particleEffects;
    public bool usable = true;
    public virtual void UseAbility(Character user ,Character target)
    {
        

    }
    public virtual void UseAbility(Character user)
    {

    }
    public virtual void UseAbility(Character user ,List<Character> targets) { }

    public virtual string FillDescription() {

        return "";
    }
    public virtual void ShowText() { }

    public virtual void SummonParticleEffects() {


        if (particleEffects != null)
        {
            GameObject particles = particleEffects;
            Instantiate(particles, targetPosition + offsetdistance,particleEffects.transform.localRotation);
        }
        
    }

    public virtual void CheckIfUsable(Character user) {



    }

}
