using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    
    public string name = "name";
    public int maxHP = 10;
    public int currentHP = 10;
    public int attack = 5;
    public int currentAttack = 5;
    public int defense = 5;
    public int currentDefense = 5;
    public int inteligence = 5;
    public int currentInteligence = 5;
    public int resistance = 5;
    public int currentResistance = 5;
    public int agility = 5;
    public int currentAgility = 5;
    public int mana = 0;
    public float initiative = 0.0f;
    public bool isDead = false;
    public float turnGauge = 0.0f;
    public bool isPlayer = false;
    public Behaviour behaviour;
    //La barrita invisible de los turnos funciona de tal manera que se van rellenando
    //a lo largo del tiempo usando principalment el stat de agilidad
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        currentAttack = attack;
        currentDefense = defense;
        currentInteligence = inteligence;
        currentResistance = resistance;
        currentAgility = agility;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
