using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonTransformation : Ability
{

    string nameTransformation = "";
    [SerializeField]
    Sprite demonSprite;
    public override void UseAbility(Character user)
    {
        userName = user.name;
        user.maxHP = 211;
        user.currentHP = user.maxHP;
        user.currentAttack = user.attack;
        user.attack = 18;
        user.currentAttack = user.attack;
        user.defense = 18;
        user.currentDefense = user.defense;
        user.intelligence = 18;
        user.currentIntelligence = user.attack;
        user.resistance = 18;
        user.currentResistance = user.resistance;
        user.agility = 18;
        user.currentAgility = user.agility;
        user.initiative = 1.3f;
        user.gameObject.GetComponent<SpriteRenderer>().sprite = demonSprite;
        user.gameObject.GetComponent<BoxCollider>().size =new Vector3 (3.6f, 3.6f,0.2f);
        user.gameObject.GetComponent<Transform>().position = new Vector3(user.gameObject.GetComponent<Transform>().position.x, user.gameObject.GetComponent<Transform>().position.y + 1, user.gameObject.GetComponent<Transform>().position.z);
        user.gameObject.GetComponent<LerpPosition>().InitialPosition += new Vector3(0, 1, 0);
        user.gameObject.GetComponent<LerpPosition>().targetPosition += new Vector3(0, 1.5f, 0);

    }
}
