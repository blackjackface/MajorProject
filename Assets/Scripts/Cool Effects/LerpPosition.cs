using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPosition : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 InitialPosition;
   public Vector3 targetPosition;


    void Start()
    {
        InitialPosition = GetComponent<Transform>().position;
        targetPosition = InitialPosition;
        targetPosition.y += 0.50f;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = Vector3.LerpUnclamped(InitialPosition, targetPosition, Mathf.PingPong(Time.time, 1f));

    }
}
