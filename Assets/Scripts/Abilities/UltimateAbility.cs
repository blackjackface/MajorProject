using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class  UltimateAbility : GeneralOffensiveAbility
{
   
    int damage = 0;
    int currentRiskFactor;
    
    
    public override void UseAbility(Character user, Character target)
    {

        CalculateMultiplier(target);
        int userDamage;
        userName = user.charactername;
        targetName = target.charactername;
        if (isMagic)
            userDamage = user.intelligence;
        else
            userDamage = user.attack;
        user.mana -= manaCost;
        if(isMagic)
          damage = (int)((baseDamage + userDamage) * multiplier) * user.level - target.resistance * target.level;
        else
          damage = (int)((baseDamage + userDamage) * multiplier)*user.level - target.defense*target.level;
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
            target.currentHP -= damage;
            user.currentOpportunityTurn = 0;
            user.opportunityMode = false;
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
                    target.modifiers.Add(addModifier);
                    addModifier.character = target;

                    addModifier.character = target;

                }
                hits = true;
                target.currentHP -= damage;
                user.opportunityGauge += gaugeFillingValue * gaugeFillingFactorOnHit;
                target.opportunityGauge += gaugeFillingValue * gaugeFillingFactorOnHitEnemy;
            }
            userName = user.charactername;
            targetName = target.charactername;
            Debug.Log(" " + user.charactername + " dealt " + damage.ToString() + " Damage to " + target.charactername);
        }

        if (target.currentHP <= 0)
        {
            target.isDead = true;
        }

        PopUpNumber(damage, target);
            ShowText();
        
    }
    public override void ShowText()
    {
        showText = "";
        if (hits)
        {
            showText = " " + userName +" used "+ abilityName + " dealt " + damage.ToString() + " damage to " + targetName;
        }
        else 
        {
            showText =" "+ abilityName + " missed and gave "+ manaCost + " mana to " + targetName;
        }
        Debug.Log("el nuevo texto es: " + showText);
       
    }



    public override void CheckIfUsable(Character user)
    {
        if (user.opportunityMode || user.currentOpportunityTurn > 0)
        {
            usable = true;
        }
        else
        {
            usable = false;
        }
    }

    public void PopUpNumber(int damage, Character characterPosition) {

        GameObject DamageTextInstance = Instantiate(popUpText);
        targetPosition = characterPosition.originalPosition;
        colorSelection();
        if (hits)
        {
            DamageTextInstance.GetComponent<TextMeshPro>().text = damage.ToString();
            DamageTextInstance.GetComponent<TextMeshPro>().color = elementColor;
            SummonParticleEffects();
        }
        else {
            DamageTextInstance.GetComponent<TextMeshPro>().text = "+ " + manaCost + "mana";
            DamageTextInstance.GetComponent<TextMeshPro>().color = Color.white;

        }
        DamageTextInstance.GetComponent<RectTransform>().position = new Vector3(characterPosition.originalPosition.x+0.2f, characterPosition.originalPosition.y - 1.5f, characterPosition.originalPosition.z - 0.5f);
        
    }

    override public string FillDescription()
    {
        string textColorPerElement = "#" + ColorUtility.ToHtmlStringRGBA(elementColor);

        if (currentModifier != null)
            return "Ultimate skill of " + $" <color={textColorPerElement}>{element.ToString()}</color>" + "\n" + "Damage: "
                + baseDamage.ToString() + "\n" + "Mana cost: " + manaCost + "\n" + "Risk: " + riskFactor + "%" + "\n" + "and applies: " + currentModifier.modifierName + currentModifier.description;
        else {
            return "Ultimate skill of " + $" <color={textColorPerElement}>{element.ToString()}</color>" + "\n" + "Damage: "
        + baseDamage.ToString() + "\n" + "Mana cost: " + manaCost + "\n" + "Risk: " + riskFactor + "%";

        }
    }


}
