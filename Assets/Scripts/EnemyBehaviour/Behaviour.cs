using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour : MonoBehaviour
{
    //this behaviour is considered to be dum-dum and almost just attacks and attacks until one of both dies

    public List<Ability> abilities;
    public string textToShow;
 //   public Character user;

   public virtual void Act(Character user, List<Character> targets) {

    }
    public virtual void ShowText()
    {
        
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
  /*      user = this.gameObject.GetComponent<Character>();

        foreach (Ability ability in abilities) {

            ability.user = user;
        }
  */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
