using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float timePassed = 0;
    public float timeDuration = 15f;


    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > timeDuration)
        {
            Destroy(this.gameObject);
        }


    }
}
