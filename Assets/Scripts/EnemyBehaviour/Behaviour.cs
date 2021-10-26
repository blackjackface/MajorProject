using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour : MonoBehaviour
{
    //this behaviour is considered to be dum-dum and almost just attacks and attacks until one of both dies

    public List<Ability> abilities;
    public Character self;

   public virtual void Act(List<Character> characters) {

    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        self = GetComponent<Character>();

        foreach (GameObject gameObject in self.abilities) {

          abilities.Add(gameObject.GetComponent<Ability>());
          
        }
        foreach (Ability ability in abilities) {

            ability.user = self;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
