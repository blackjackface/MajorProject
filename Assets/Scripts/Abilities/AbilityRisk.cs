using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRisk : Ability
{
    public enum Element {
        
        NONE, //Sin elemento, en su defecto hay que cambiar de elemento    
        PHYSICAL, //daño f�sico
        FIRE, //daño de fuego
        WATER, //daño de agua y hielo
        EARTH, // daño de tierra y naturaleza
        WIND, //daño de viento y tempestades
        THUNDER, //daño eléctrico
        LIGHT, // daño de luz   
        DARKNESS, //daño de oscuridad
        ESSENCE, //daño de esencia, equivalente a da�o todopoderoso
        SUPREME //daño supremo, es prácticamente el mejor tipo de daño
    }
    public Element element = Element.NONE;
    public int baseDamage = 1;
    public float multiplier = 1;
    public int riskFactor = 0;
    public bool isAttackMove = true;
    public bool isMagic = false;
    public bool hits = false;
    public Modifier initialModifier;
    public Modifier currentModifier;
    public GameObject popUpText;
    [SerializeField]
    public Color elementColor;

    private void Awake()
    {
        currentModifier = initialModifier;
    }
    private void Start()
    {

       description = "An elemental Attack of " + element.ToString() + "/n" + "Damage: " + baseDamage.ToString();
    }

    public void colorSelection() {
        switch (element) {
            case Element.PHYSICAL:
                elementColor = Color.grey;
                break;
            case Element.FIRE:
                elementColor = Color.red;
                break;
            case Element.WATER:
                elementColor = Color.blue;
                break;
            case Element.EARTH:
                elementColor = Color.green;
                break;
            case Element.WIND:
                elementColor = Color.gray;
                break;
            case Element.THUNDER:
                elementColor = new Color32(250, 255, 55,255);
                break;
            case Element.LIGHT:
                elementColor = Color.yellow;
                break;
            case Element.DARKNESS:
                elementColor = Color.magenta;
                break;
            case Element.ESSENCE:
                elementColor = Color.white;
                break;
            case Element.SUPREME:
                elementColor = new Color32(255, 157, 6, 255);
                break;
        }
    }

    public void CalculateMultiplier(Character target){
        int elementtoIntPosition = (int)element;
        char getCompatibilitValue = target.ElementCompatibility[elementtoIntPosition];
        switch(getCompatibilitValue){
            case ('S'):
            multiplier = 2.5f;
            break;
            case('A'):
            multiplier = 1.75f;
            break;
            case('B'):
            multiplier = 1.30f;
            break;
            case('C'):
            multiplier = 1.0f;
            break;
            case('D'):
            multiplier = 0.8f;
            break;
            case('E'):
            multiplier = 0.5f;
            break;
            case('F'):
            multiplier = 0.0f;
            break;
            case('G'):
            multiplier = -0.5f;
            break;
        }
    }

    override public string FillDescription()
    {
        string textColorPerElement ="#" + ColorUtility.ToHtmlStringRGBA(elementColor);
        if (currentModifier == null)
            return "An elemental Attack of " + $" <color={textColorPerElement}>{element.ToString()}</color>" + "\n" + "Damage: "
            + baseDamage.ToString() + "\n" + "Mana cost: " + manaCost + "\n" + "Risk: " + riskFactor + "%";
        else
            return "An elemental Attack of " + $" <color={textColorPerElement}>{element.ToString()}</color>" + "\n" + "Damage: "
            + baseDamage.ToString() + "\n" + "Mana cost: " + manaCost + "\n" + "Risk: " + riskFactor + "%" + "\n" + "and applies: " + currentModifier.modifierName + currentModifier.description;
    }
}
