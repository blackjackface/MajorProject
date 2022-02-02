using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossesedSoldierBehaviour : Behaviour
{
    bool hasTriggeredTransformation = false;
    public List<Character> players;
    Ability recentlyUsedAbility;
    // Start is called before the first frame update
    public override void Act(Character user, List<Character> targets)
    {
        float currentLifePercentaje = ((float)user.currentHP / user.maxHP);
        players = targets.FindAll(c => c.isPlayer && !c.isDead);
        if (hasTriggeredTransformation == false)
        {


            if (currentLifePercentaje > 0.8f)
            {
                //if ability is abilities[0] means that is using the attack command
                abilities[0].UseAbility(user, players[Random.Range(0, targets.Count-1)]);
                recentlyUsedAbility = abilities[0];
                Debug.Log("he is going to attack");
            }
            else if (currentLifePercentaje > 0.41f)
            {

                //ability 2 will be a simple dark spell
                abilities[1].UseAbility(user, players[Random.Range(0, targets.Count-1)]);
                recentlyUsedAbility = abilities[1];

            }
            else if (currentLifePercentaje <= 0.4f)
            {
                //ability 3 will be to transform the character into a demon which will change it's stats and will transform his other abilities into new ones
                abilities[2].UseAbility(user);
                recentlyUsedAbility = abilities[2];

                hasTriggeredTransformation = true;
            } else {
                if (currentLifePercentaje > 0.8f)
                {
                    int randomAction = Random.Range(0, 1);
                    if (randomAction == 0)
                    {
                        abilities[0].UseAbility(user, players[Random.Range(0, targets.Count - 1)]);
                        recentlyUsedAbility = abilities[0];

                    }
                    else
                    {
                        abilities[1].UseAbility(user, players[Random.Range(0, targets.Count - 1)]);
                        recentlyUsedAbility = abilities[1];

                    }
                }
                else if(currentLifePercentaje > 0.6f) 
                {
                     //will cast a high risk - medium damage spell wich is designed to make the player into opportunity mode.
                     abilities[3].UseAbility(user, players[Random.Range(0, targets.Count - 1)]);                    
                } else if(currentLifePercentaje <= 0.6f)
                {
                    int randomAction = Random.Range(0, 1);

                    if (randomAction == 0)
                    {
                        abilities[0].UseAbility(user, players[Random.Range(0, targets.Count - 1)]);
                        recentlyUsedAbility = abilities[0];

                    }
                    else
                    {
                        textToShow = "The " + user.charactername + " seems stunned... ";
                        

                    }

                }
            }
        } 
    }
    public override void ShowText()
    {

        recentlyUsedAbility.ShowText();
        textToShow = recentlyUsedAbility.showText;
    }


}
