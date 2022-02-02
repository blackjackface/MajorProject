using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIPointer : MonoBehaviour
{
    // Start is called before the first frame update

    public Text playerHP;
    public Text playerMana;
    public Text playerName;
    public Character SelfCharacter;
    void Start()
    {
        SelfCharacter = this.gameObject.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHP.text ="HP " + SelfCharacter.currentHP.ToString() + "/" +  SelfCharacter.maxHP.ToString();
        playerMana.text = "mana " + SelfCharacter.mana.ToString();
        if (SelfCharacter.currentOpportunityTurn > 0)
        {
            playerName.color = new Color(1.0F, 1.0F, 0.4F);
            playerName.text = SelfCharacter.charactername + " *";

        }
        else {
            playerName.color = new Color(1, 1, 1);
            playerName.text = SelfCharacter.charactername;

        }


    }
}
