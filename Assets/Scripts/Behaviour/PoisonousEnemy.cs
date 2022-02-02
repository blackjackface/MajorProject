using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousEnemyBehaviour : Behaviour
{
    public List<Character> players;
    Ability recentlyUsedAbility;
    // Start is called before the first frame update
    public override void Act(Character user, List<Character> targets)
    {
        //Performs an offensive Action
        players = targets.FindAll(c => c.isPlayer && !c.isDead);


        Ability attack = abilities[0];//attack Ability
        Ability defend = abilities[1];//defend Ability
        AbilityRisk abilityRisk;
        var random = new System.Random();
        abilityRisk = (AbilityRisk)abilities[2];//risk Ability
        players = targets.FindAll(c => c.isPlayer && !c.isDead);
        Character target = players[random.Next(players.Count)];
        int randomAbility = UnityEngine.Random.Range(0, 8);

        if (randomAbility < 7)
        {
            if (abilityRisk.manaCost < user.mana)
            {
                abilityRisk.UseAbility(user, target);
                recentlyUsedAbility = abilityRisk;
            }
            else
            {
                int randomAction = UnityEngine.Random.Range(0, 2);
                if (randomAction == 0)
                {
                    attack.UseAbility(user, target);
                    recentlyUsedAbility = attack;
                }
                else
                {
                    defend.UseAbility(user);
                    recentlyUsedAbility = defend;
                }
            }
        }
        else
        {
            int randomAction = UnityEngine.Random.Range(0, 1);
            if (randomAction == 0)
            {
                attack.UseAbility(user, target);
                recentlyUsedAbility = attack;
            }
            else
            {
                defend.UseAbility(user);
                recentlyUsedAbility = defend;
            }
        }
    }


    public override void ShowText()
    {
        recentlyUsedAbility.ShowText();
        textToShow = recentlyUsedAbility.showText;
    }
}
