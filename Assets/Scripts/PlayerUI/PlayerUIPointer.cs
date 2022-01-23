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
        playerName.text = SelfCharacter.name;
        if (SelfCharacter.opportunityMode)
        {
            playerName.color = new Color(1.0F, 0.8F, 0.2F);



        }
        else {
            playerName.color = new Color(1, 1, 1);
        
        }
       

    }
}
