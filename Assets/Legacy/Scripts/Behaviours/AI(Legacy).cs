using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI 
{    
   virtual public Turn GetTurn(RoundState round) {       
        return new Turn();
    }
}
