using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CharacterStateNotFoundException : Exception
{
    public CharacterStateNotFoundException()
    { }

    public CharacterStateNotFoundException(string message)
        : base(message)
    { }

    public CharacterStateNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    { }

}
