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
        SUPREME //daño supremo, es prácticamente el mejor tipo de daño además de que tiene DRIP 
    }
    public Element element = Element.NONE;
    public float multiplier = 1;
    public int riskFactor = 0;
    public int manaCost = 0;
    public bool isAttackMove = true;
    public bool isMagic = false;
    public bool hits = false;



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
}
