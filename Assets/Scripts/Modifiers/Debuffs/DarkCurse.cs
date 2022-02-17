using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCurse : Modifier
{
    // Start is called before the first frame update
    [SerializeField]
    int curseDamage = 100;
    override public void OnTurnBegin() {
        if (turnsExpiration == 0 || isExpired) {
            character.currentHP -= curseDamage;
            if (character.currentHP <= 0) {
                character.isDead = true;
            
            }
        }
    
    
    }
}
