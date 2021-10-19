using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TurnNotFoundException : Exception
{
    public TurnNotFoundException()
    { }

    public TurnNotFoundException(string message)
        : base(message)
    { }

    public TurnNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    { }

}
