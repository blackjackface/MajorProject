using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    // Start is called before the first frame update
    Color baseColor;
    Color white = new Color(1, 1, 1);
    float speed = 0.05f;
    void Start()
    {
        baseColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<SpriteRenderer>().material.color = Color.LerpUnclamped(baseColor, white, Mathf.PingPong(Time.time, 1.25f));

    }
}
