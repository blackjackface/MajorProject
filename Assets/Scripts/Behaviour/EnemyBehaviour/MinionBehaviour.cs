using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBehaviour : Behaviour
{

    public List<Character> players;
    Ability recentlyUsedAbility;
    // Start is called before the first frame update

    public enum BehaviourState
    {
        HEALTHY,
        ENRAGED

    }
    BehaviourState currentState;

    public override void Act(Character user, List<Character> targets)
    {
        players = targets.FindAll(c => c.isPlayer && !c.isDead);
        Ability attack = abilities[0];//attack Ability
        Ability defend = abilities[1];//defend Ability
        Ability basicElementalAbility = abilities[2];
        Ability enragedElementalAbility = abilities[3];
        var random = new System.Random();
        players = targets.FindAll(c => c.isPlayer && !c.isDead);
        Character target = players[random.Next(players.Count)];
        CalculateState(user);

        switch (currentState) {
            case BehaviourState.HEALTHY: {
                    int randomAction = 0;
                    randomAction = Random.RandomRange(0, 2);
                    if (randomAction == 0)
                    {
                        attack.UseAbility(user, target);
                        recentlyUsedAbility = attack;
                    }
                    else
                    {
                        if (user.mana >= basicElementalAbility.manaCost)
                        {

                            basicElementalAbility.UseAbility(user, target);
                            recentlyUsedAbility = basicElementalAbility;
                        }
                        else
                        {
                            attack.UseAbility(user, target);
                            recentlyUsedAbility = attack;
                        }
                    }

                    break;
            }
            case BehaviourState.ENRAGED:
                {
                    {
                        if (user.mana >= enragedElementalAbility.manaCost)
                        {
                            enragedElementalAbility.UseAbility(user, target);
                            recentlyUsedAbility = enragedElementalAbility;
                        } else {
                            attack.UseAbility(user, target);
                            recentlyUsedAbility = attack;
                        }
                    }
                    break;
                }
        }

    }

    public void enemyInitialize(GameObject character)
    {
     // gameObject.GetComponent<Character>() = character.GetComponent<Character>();


    }

    public BehaviourState CalculateState(Character user)
    {
        float userCurrentHealthPercentage = ((float)user.currentHP / (float)user.maxHP);
        if (userCurrentHealthPercentage > 0.5f)
        {

            return BehaviourState.HEALTHY;
        }
        else {

            return BehaviourState.ENRAGED;
        }
    }

    public override void ShowText()
    {
        recentlyUsedAbility.ShowText();
        textToShow = recentlyUsedAbility.showText;
    }
}
