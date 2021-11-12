using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : Behaviour
{
    // Start is called before the first frame update
    public List<Character> characterList = new List<Character>();
    public List<Character> enemyList = new List<Character>();
    public List<Character> allyList = new List<Character>();
    public List<Button> buttons = new List<Button>();
    public Character target;
    public Canvas canvas;
    public GameObject buttonPrefab;
    public bool hasTurnCompleted = false;
    override public void Start()
    {
        base.Start();
        CreateButtons();
        enemyList = characterList.FindAll(character => character.isPlayer == false);
        allyList = characterList.FindAll(character => character.isPlayer);
    }
    void CreateButtons() {
 //       GameObject buttonAttack = ButtonConstructor(new Vector3(320,-90,0),"Attack",abilities.Find(ability => ability.name.Contains("Attack")));
 //       GameObject buttonDefend = ButtonConstructor(new Vector3(320, -120, 0), "Defend", abilities.Find(ability => ability.name.Contains("Defend")));           
    }

    IEnumerator targetSelect() {

        int selectionIndex = 0;
        int maxIndex = enemyList.Count;
        bool selectionIsConfirmed = false;
        

        yield return  new GameObject();
    
        
    }

    void targetSelectionOffensive() {
        //mover de derecha o izquierda para que el índice se mueva y cambie de enemigo                
    }


    void AbilityListener(Ability ability) {

        ability.UseAbility(this.GetComponent<Character>(), target);
    }
    void Gamble()
    {
    }
    IEnumerator ActPlayer() {

        while (this.hasTurnCompleted != true) {
            yield return null;
        }
    }


    public override void Act(Character user, List<Character> targets)
    {
        Debug.Log("PlayerBehaviour.act");
  //      StartCoroutine(ActPlayer());

    }

}



