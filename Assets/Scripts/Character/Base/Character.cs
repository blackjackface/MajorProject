using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{

    
    public string name = "name";
    public int maxHP = 10;
    public int currentHP = 10;
    public int attack = 5;
    public int currentAttack = 5;
    public int defense = 5;
    public int currentDefense = 5;
    public int intelligence = 5;
    public int currentIntelligence = 5;
    public int resistance = 5;
    public int currentResistance = 5;
    public int agility = 5;
    public int currentAgility = 5;
    public int mana = 0;
    public float initiative = 0.0f;
    public bool isDead = false;
    public float turnGauge = 0.0f;
    public bool isPlayer = false;
    public int playerIndex = -1;
    public Behaviour behaviour;
    public UnityEvent m_MyEvent;
    public bool isDefending = false;
    public Vector3 originalPosition;
    //La barrita invisible de los turnos funciona de tal manera que se van rellenando
    //a lo largo del tiempo usando principalment el stat de agilidad
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        currentAttack = attack;
        currentDefense = defense;
        currentIntelligence = intelligence;
        currentResistance = resistance;
        currentAgility = agility;
        originalPosition = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("has pinchao a: " + name);
        m_MyEvent.Invoke();
    }
}
