using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : Behaviour
{
    // Start is called before the first frame update
    public List<Character> characterList = new List<Character>();
    public Character target;
    public Canvas canvas;
    public GameObject buttonPrefab;
    private void Start()
    {
        CreateButtons();
    }
    void CreateButtons() {
        GameObject buttonAttack = Instantiate(buttonPrefab, new Vector3(250f, -250f, 0), Quaternion.Euler(0, 0, 0));
        buttonAttack.transform.position = new Vector3(320, -90, 0);
        buttonAttack.GetComponent<Text>().text = "Attack";
        buttonAttack.GetComponent<Button>().onClick.AddListener(Attack);
        GameObject buttonDefend = Instantiate(buttonPrefab, new Vector3(250f, -250f, 0), Quaternion.Euler(0, 0, 0));
        buttonDefend.transform.position = new Vector3(320, -120, 0);
        buttonDefend.GetComponent<Text>().text = "Attack";
        buttonDefend.GetComponent<Button>().onClick.AddListener(Defend);    
    }
    void Attack() {
        foreach (Ability ability in abilities) {
            if (ability is Attack) {
                ability.UseAbility(self,target);
            }
        }
    }
    void Defend() {
        foreach (Ability ability in abilities)
        {
            if (ability is Defend)
            {
                ability.UseAbility(self,target);
            }
        }
    }
    void Gamble() {
    
     
    
    
    }
    public override void Act(List<Character> characters)
    {
        base.Act(characters);
    }

}



