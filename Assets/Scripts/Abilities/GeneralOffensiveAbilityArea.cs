using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralOffensiveAbilityArea : GeneralOffensiveAbility
{
    // Start is called before the first frame update
    int damageMean = 0;
    int numberOfTargets = 0;
    public override void UseAbility(Character user, List<Character> targets) {


        //   CalculateMultiplier(target);
        int userDamage;
        numberOfTargets = targets.Count;
        damageMean = 0;

        if (isMagic)
            userDamage = user.intelligence;
        else
            userDamage = user.attack;
        user.mana -= manaCost;
        foreach (Character target in targets)
        {
            CalculateMultiplier(target);

            if (isMagic)
                damage = (int)((baseDamage + userDamage) * multiplier) * user.level - target.resistance * target.level;
            else
                damage = (int)((baseDamage + userDamage) * multiplier) * user.level - target.defense * target.level;
            if (damage < 0)
            {
                damage = 0;
            }

            if (user.opportunityMode || user.currentOpportunityTurn > 0)
            {
                if (currentModifier != null)
                {
                    Modifier addModifier = new Modifier();
                    addModifier = currentModifier;
                    if (target.modifiers.Contains(currentModifier))
                    {
                        target.modifiers.Remove(currentModifier);

                    }
                    addModifier.SetExpiration();
                    addModifier.character = target;

                    target.modifiers.Add(addModifier);
                }
                hits = true;
                damage = (int)(damage * opportunityModeMultiplier);
                damageMean += damage;
                target.currentHP -= damage;
                
            }

            else
            {

                currentRiskFactor = (int)(riskFactor / multiplier);
                int randomDice = Random.Range(1, 100);
                if (randomDice < currentRiskFactor)
                {
                    user.opportunityGauge += gaugeFillingValue * gaugeFillingFactorOnMiss;
                    target.opportunityGauge += gaugeFillingValue * gaugeFillingFactorOnMissEnemy;
                    hits = false;
                    target.mana += manaCost;
                }
                else
                {
                    if (currentModifier != null)
                    {
                        Modifier addModifier = new Modifier();
                        addModifier = currentModifier;
                        if (target.modifiers.Contains(currentModifier))
                        {
                            target.modifiers.Remove(currentModifier);

                        }
                        addModifier.SetExpiration();
                        addModifier.character = target;
                        target.modifiers.Add(addModifier);
                    }
                    hits = true; 
                    damageMean += damage;
                    target.currentHP -= damage;
                    user.opportunityGauge += gaugeFillingValue * gaugeFillingFactorOnHit;
                    target.opportunityGauge += gaugeFillingValue * gaugeFillingFactorOnHitEnemy;
                }
                userName = user.charactername;
                targetName = target.charactername;
                Debug.Log(" " + user.charactername + " dealt " + damage.ToString() + " Damage to " + target.charactername);
                PopUpNumber(damage, target);
            }
            //            target.currentHP -= damage;

            if (target.currentHP <= 0)
            {
                target.isDead = true;
            }

        }
        ShowText();
    }
    public override void ShowText()
    {
        int damageMeanToText = (damageMean / numberOfTargets);
        showText = "";
        showText = " " + userName + " used " + abilityName + " and dealt a mean of " + damageMeanToText.ToString() + " to the whole group";
        Debug.Log("el nuevo texto es: " + showText);
        
    }

}
