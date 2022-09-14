using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSpell : AbilityRisk
{   
    [SerializeField]
    int healingAmount = 0;
    int healingValue = 0;
    int maxhealingoverlifeValue = 0;
    public override void UseAbility(Character user, Character target)
    {
        healingValue = healingAmount + user.resistance;
        userName = user.charactername;
        targetName = target.charactername;
        maxhealingoverlifeValue = (target.currentHP + healingValue);
        user.mana -= manaCost;

        if (maxhealingoverlifeValue > target.maxHP)
        {
            target.currentHP = target.maxHP;
        }
        else
        {
            target.currentHP += healingValue;
        }


        if (currentModifier != null)
        {
            Modifier addModifier = new Modifier();
            addModifier = currentModifier;
            if (target.modifiers.Contains(currentModifier))
            {
                target.modifiers.Remove(currentModifier);

            }
            addModifier.SetExpiration();
            target.modifiers.Add(addModifier);
        }
    }

    public override void ShowText()
    {
        showText = "";
        showText = " " + userName + " healed " + $"<color=Green>{healingValue.ToString()}</color>" + " HP to " + targetName;
        Debug.Log("el nuevo texto es: " + showText);
    }

    override public string FillDescription()
    {
        string textColorPerElement = "#" + ColorUtility.ToHtmlStringRGBA(elementColor);
        if (currentModifier == null)
            return "A healing spell of element " + $" <color={textColorPerElement}>{element.ToString()}</color>" + "\n" + "Heals at least: "
                + healingAmount.ToString() + "\n" + "Mana cost: " + manaCost + "\n" + "Risk: " + riskFactor + "%";
        else
            return "A healing spell of element " + $" <color={textColorPerElement}>{element.ToString()}</color>" + "\n" + "Heals at least: "
                + healingAmount.ToString() + "\n" + "Mana cost: " + manaCost + "\n" + "Risk: " + riskFactor + "%" + "\n" + "and applies: " + currentModifier.modifierName + currentModifier.description;
                

    }

}
