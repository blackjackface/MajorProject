using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBehaviour : Behaviour
{
    // Start is called before the first frame update


    public List<Character> players;
    Ability recentlyUsedAbility;
    [SerializeField]
    int numberToUnleash = 2;
    [SerializeField]
    int counterToUnleash = 0;
    bool isCharging = false;
    public enum BehaviourState {
        BEGINNING_OF_THE_BATTLE,
        CHARGING_ATTACK,
        UNLEASHING_ATTACK,
        ENRAGED
    
    }
    [SerializeField]
    BehaviourState currentState = BehaviourState.BEGINNING_OF_THE_BATTLE;
    
    public override void Act(Character user, List<Character> targets)
    {
        //Performs an offensive Action
        players = targets.FindAll(c => c.isPlayer && !c.isDead);


        Ability attack = abilities[0];//attack Ability
        Ability defend = abilities[1];//defend Ability
        Ability devastatingAttack = abilities[2];
        Ability enragedAbility = abilities[3];
        AbilityRisk abilityRisk;
        var random = new System.Random();
        abilityRisk = (AbilityRisk)abilities[2];//risk Ability
        players = targets.FindAll(c => c.isPlayer && !c.isDead);
        Character target = players[random.Next(players.Count)];
        currentState = CalculateState(user);

        switch (currentState) {
            case BehaviourState.BEGINNING_OF_THE_BATTLE: {
                    attack.UseAbility(user, target);
                    recentlyUsedAbility = attack;
            break;
            }
            case BehaviourState.CHARGING_ATTACK: {
                    if (counterToUnleash == 1)
                        abilities[4].UseAbility(user,target);
                        recentlyUsedAbility = abilities[4];
                    if (counterToUnleash == 2) {
                        abilities[5].UseAbility(user, target);
                        recentlyUsedAbility = abilities[5];
                    }
                    break;
                }
            case BehaviourState.UNLEASHING_ATTACK: {
                    devastatingAttack.UseAbility(user, players);
                    recentlyUsedAbility = devastatingAttack;
                    break;
                }
            case BehaviourState.ENRAGED: {
                    int randomAction = 0;
                    randomAction = Random.RandomRange(0, 2);
                    if (randomAction == 0) {
                        attack.UseAbility(user, target);
                        recentlyUsedAbility = attack;

                    } else {
                        enragedAbility.UseAbility(user, target);
                        recentlyUsedAbility = enragedAbility;
                    }
                    break;
                }
        }
        return;

    }


    public BehaviourState CalculateState(Character user) {
        float userCurrentHealthPercentage = ((float)user.currentHP / (float)user.maxHP);

        if (userCurrentHealthPercentage > 0.8f) {
            return BehaviourState.BEGINNING_OF_THE_BATTLE;        
        }
        if (userCurrentHealthPercentage > 0.4f && isCharging == false) {
            isCharging = true;
            return BehaviourState.CHARGING_ATTACK;
        }
        if (isCharging && counterToUnleash < numberToUnleash) {
            counterToUnleash++;
            return BehaviourState.CHARGING_ATTACK;
        }
        if (isCharging && counterToUnleash == numberToUnleash) {
            counterToUnleash = 0;
            isCharging = false;
            return BehaviourState.UNLEASHING_ATTACK;
        } if (userCurrentHealthPercentage > 0.0f) {
            return BehaviourState.ENRAGED;
        }
        return BehaviourState.BEGINNING_OF_THE_BATTLE;
    }

    public override void ShowText()
    {
        recentlyUsedAbility.ShowText();
        textToShow = recentlyUsedAbility.showText;
    }
}
