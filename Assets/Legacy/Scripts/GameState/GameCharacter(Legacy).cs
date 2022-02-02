using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter : MonoBehaviour
{

    public int hp;
    public int maxhp;
    public int attack;
    public int defense;
    public int intelligence;
    public int resistance;
    public int agility;
    public int mana;
    public List<string> abilities = new List<string>();
    public string behaviour = "Attack";
    public Team team;
    public int remainingRoundTurns;
    // Start is called before the first frame update

    List<AbilityEnum> toAbilityEnums(List<Ability> abilityList) {

        List<AbilityEnum> abilitiesEnum = new List<AbilityEnum>();

        foreach (Ability ability in abilityList) {

            if (ability is Attack) {
                abilitiesEnum.Add(AbilityEnum.attack);
            }
            if (ability is Defend) {
                abilitiesEnum.Add(AbilityEnum.defend);
            }
            if (ability is Heal) {
                abilitiesEnum.Add(AbilityEnum.heal);
            }

        }
        return abilitiesEnum;
    }

/*    List<Ability> ToAbility(List<AbilityEnum> abilityList)
    {

        List<Ability> abilities = new List<Ability>();

        foreach (AbilityEnum ability in abilityList)
        {

            if (ability == AbilityEnum.attack)
            {
                Attack attack = new Attack();
                abilities.Add(attack);
            }
            if (ability == AbilityEnum.defend)
            {
                Defend defend = new Defend();
                abilities.Add(defend);
            }
            if (ability == AbilityEnum.heal)
            {
                Heal heal = new Heal();

                abilities.Add(heal);
            }

        }
        return abilities;
    } */


    public CharacterState getCharacterState() {
        CharacterState characterState = new CharacterState();

        characterState.hp = hp;
        characterState.maxhp = maxhp;
        characterState.attack = attack;
        characterState.defense = defense;
        characterState.intelligence = intelligence;
        characterState.resistance = resistance;
        characterState.agility = agility;
        characterState.mana = mana;
        foreach (string ability in abilities)
        {
            Type abilityType = GetType(ability);
            characterState.abilities.Add(Activator.CreateInstance(abilityType) as Ability);
            
        }
        Type behaviourType = GetType(behaviour);
        characterState.behaviour = Activator.CreateInstance(behaviourType) as Behaviour;
        characterState.team = team;
        characterState.remainingRoundTurns = remainingRoundTurns;
        return characterState;


    }

   public void UpdateCharacter(CharacterState characterState) {

        hp = characterState.hp;
        maxhp = characterState.maxhp;
        attack = characterState.attack;
        defense = characterState.defense;
        intelligence = characterState.intelligence;
        resistance = characterState.resistance;
        agility = characterState.agility;
        mana = characterState.mana;

        foreach (Ability ability in characterState.abilities)
        {
            abilities.Add(ability.GetType().ToString());
        }
        //  abilities = characterState.abilities;
        Type behaviourType = characterState.behaviour.GetType();


        behaviour = behaviourType.ToString();
        team = characterState.team;
        remainingRoundTurns = characterState.remainingRoundTurns;
    }


    public static Type GetType(string TypeName)
    {

        // Try Type.GetType() first. This will work with types defined
        // by the Mono runtime, etc.
        var type = Type.GetType(TypeName);

        // If it worked, then we're done here
        if (type != null)
            return type;

        // Get the name of the assembly (Assumption is that we are using
        // fully-qualified type names)
        var assemblyName = TypeName.Substring(0, TypeName.IndexOf('.'));

        // Attempt to load the indicated Assembly
        var assembly = System.Reflection.Assembly.LoadWithPartialName(assemblyName);
        if (assembly == null)
            return null;

        // Ask that assembly to return the proper Type
        return assembly.GetType(TypeName);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
