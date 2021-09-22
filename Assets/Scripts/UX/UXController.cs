using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UXController 
{
    public void processTurn(Turn turn, RoundController roundController, RoundState finalState);

    
}
