using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : Behaviour
{


    public override void Act(Character user, List<Character> targets)
    {
        Debug.Log("PlayerBehaviour.act");
  //      StartCoroutine(ActPlayer());
    }


}



