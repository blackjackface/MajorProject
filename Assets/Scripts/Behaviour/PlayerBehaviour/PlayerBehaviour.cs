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



