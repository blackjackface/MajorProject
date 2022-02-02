using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour : MonoBehaviour
{
    //this behaviour is considered to be dum-dum and almost just attacks and attacks until one of both dies
    public List<Ability> initialAbilities;
    public List<Ability> abilities;
    public string textToShow;
    int abilityUsed = 0;
 //   public Character user;

   public virtual void Act(Character user, List<Character> targets) {

   }
    public virtual void ShowText()
    {
        
    }
    public virtual void ShowText(int abilityIndex)
    {
        abilities[abilityIndex].ShowText();
        textToShow = abilities[abilityIndex].showText;
    }
    // Start is called before the first frame update
    public virtual void Awake()
    {
        abilities = initialAbilities;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    


}
