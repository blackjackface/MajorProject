using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRisk : Ability
{
    public enum Element {
        PHYSICAL, //daño f�sico
        FIRE, //daño de fuego
        WATER, //daño de agua y hielo
        EARTH, // daño de tierra y naturaleza
        WIND, //daño de viento y tempestades
        THUNDER, //daño eléctrico
        LIGHT, // daño de luz   
        DARKNESS, //daño de oscuridad
        ESSENCE, //daño de esencia, equivalente a da�o todopoderoso
        SUPREME, //daño supremo, es prácticamente el mejor tipo de daño además de que tiene DRIP
        NONE //Sin elemento, en su defecto hay que cambiar de elemento     
    }

    public Element element = Element.NONE;
    public int riskFactor = 0;
    bool isAttackMove = true;
    bool isPhysical = false;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
