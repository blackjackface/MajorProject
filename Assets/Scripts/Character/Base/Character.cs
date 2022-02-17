using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string charactername = "name";
    public int level = 1;
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
    public int maxTurnsOpportunity = 3;
    public int currentOpportunityTurn = -1;
    public int mana = 0;
    public int manaRechargePerTurn = 0;
    public float initiative = 0.0f;
    public bool isDead = false;
    public float turnGauge = 0.0f;
    public bool isPlayer = false;
    public float opportunityGauge = 0.0f;
    public float maxOpportunityGaugeBar = 100.0f;
    public int playerIndex = -1;
    public Behaviour behaviour;
    public List<Modifier> modifiers = new List<Modifier>();
    public UnityEvent m_MyEvent;
    public bool isDefending = false;
    public bool mouse_over = false;
    public bool opportunityMode = false;
    public Vector3 originalPosition;
    // es un array que contiene las compatibilidades de elementos donde 'C' es la efectividad base de 100%
    public List<char> ElementCompatibility = new List<char> { 'C', 'C', 'C', 'C', 'C', 'C', 'C', 'C', 'C', 'C', 'C' };
    //TODO: A�adir lista de buffs y debuffs
    //List<StatModifier> statModifiers = new List<StatModifier>

    //La barrita invisible de los turnos funciona de tal manera que se van rellenando
    //a lo largo del tiempo usando principalment el stat de agilidad
    // Start is called before the first frame update
    void Awake()
    {
        currentHP = maxHP;
        currentAttack = attack;
        currentDefense = defense;
        currentIntelligence = intelligence;
        currentResistance = resistance;
        currentAgility = agility;
    }

    void Start() {
        originalPosition = GetComponent<Transform>().position;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ManaCharge() {

        mana += manaRechargePerTurn;
    
    }

    void OnMouseDown()
    {
        Debug.Log("has pinchao a: " + charactername);
        m_MyEvent.Invoke();
    }

    public void onClickCharacter() {

        Debug.Log("has pinchao a: " + charactername);
        m_MyEvent.Invoke();

    }

    void OnMouseUp() { }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        Debug.Log("Mouse exit");
    }
}
