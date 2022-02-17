using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripartBehaviour : Behaviour
{
    [SerializeField]
    GameObject minionA;
    [SerializeField]
    GameObject minionB;
    [SerializeField]
    Modifier firstModifier;
    [SerializeField]
    Modifier secondModifier;
    [SerializeField]
    Modifier debuffModifier;
    [SerializeField]
    List<GameObject> possibleMinions;
    public List<Character> players;
    Ability recentlyUsedAbility;
    public enum BehaviourState
    {
        TWO_MINIONS_ALIVE,
        MINION_A_ALIVE,
        MINION_B_ALIVE,
        ALONE,
        ENRAGED
    }
    [SerializeField]
    BehaviourState currentState;
    // Start is called before the first frame update
    new void Awake()
    {

        firstModifier = minionA.GetComponent<Modifier>();
        abilities[2] = minionA.GetComponent<Behaviour>().abilities[3];
        secondModifier = minionB.GetComponent<Modifier>();
        abilities[3] = minionB.GetComponent<Behaviour>().abilities[3];

    }
    public override void Act(Character user, List<Character> targets)
    {
        players = targets.FindAll(c => c.isPlayer && !c.isDead);


        Ability attack = abilities[0];//attack Ability
        Ability defend = abilities[1];//defend Ability
        Ability abilityFromMA = abilities[2];
        Ability abilityFromMB = abilities[3];
        Ability aloneAbility = abilities[4];
        var random = new System.Random();
        players = targets.FindAll(c => c.isPlayer && !c.isDead);
        Character target = players[random.Next(players.Count)];
        currentState = CalculateState(user);

        switch (currentState) {

            case BehaviourState.TWO_MINIONS_ALIVE:
                //stall
                abilities[5].UseAbility(user, target);
                recentlyUsedAbility = abilities[5];
                break;
            case BehaviourState.MINION_A_ALIVE:
                //uses minion B main ability
                abilityFromMB.UseAbility(user, target);
                recentlyUsedAbility = abilityFromMB;
                break;
            case BehaviourState.MINION_B_ALIVE:
                //uses minion A main ability
                abilityFromMA.UseAbility(user, target);
                recentlyUsedAbility = abilityFromMA;

                break;
            case BehaviourState.ALONE:
                int randomAction = 0;
                randomAction = Random.RandomRange(0, 2);
                if (randomAction == 0)
                {
                    attack.UseAbility(user, target);
                    recentlyUsedAbility = attack;
                }
                else
                {
                    aloneAbility.UseAbility(user, target);
                }
                break;
        }


    }

    public BehaviourState CalculateState(Character user)
    {
        if (minionA.GetComponent<Character>().isDead && minionB.GetComponent<Character>().isDead)
        {
            ApplyModifier(debuffModifier, user);
            return BehaviourState.ALONE;
            
        }

        if ((minionB.GetComponent<Character>().isDead && !minionA.GetComponent<Character>().isDead)) {

            ApplyModifier(firstModifier,user);
            return BehaviourState.MINION_A_ALIVE;

        
        }
        if ((minionA.GetComponent<Character>().isDead && !minionB.GetComponent<Character>().isDead)) {
            ApplyModifier(secondModifier, user);
            return BehaviourState.MINION_B_ALIVE;        
        }

        if (!minionA.GetComponent<Character>().isDead && !minionB.GetComponent<Character>().isDead)
        {
            ApplyModifier(firstModifier, user);
            ApplyModifier(secondModifier, user);

            return BehaviourState.TWO_MINIONS_ALIVE;


        }

        return BehaviourState.ENRAGED;
    }

    void ApplyModifier(Modifier modifier,Character user) {
        Modifier addModifier = new Modifier();
        addModifier = modifier;
        if (user.modifiers.Contains(modifier))
        {
            user.modifiers.Remove(modifier);

        }
        addModifier.SetExpiration();
        addModifier.character = user;
        user.modifiers.Add(addModifier);
    }

    public override void ShowText()
    {
        recentlyUsedAbility.ShowText();
        textToShow = recentlyUsedAbility.showText;
    }

}
