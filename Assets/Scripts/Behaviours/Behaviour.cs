using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour 
{
    virtual public Turn GetTurn(RoundState round, CharacterState characterState) {


        throw new NotImplementedException();
    }

    public Behaviour() { }


}
