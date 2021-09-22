using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState
{
    // Start is called before the first frame update
    public int hp;
    public int maxhp;
    public int attack;
    public int defense;
    public int intelligence;
    public int resistance;
    public int agility;
    public int mana;
    public List<Ability> abilities = new List<Ability>();
    public Behaviour behaviour;
    public Team team;
    public int remainingRoundTurns;
    // List<Ability> avaliableAbilities;

}
