using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    // Start is called before the first frame update
    Color baseColor;
    Color white = new Color(1, 1, 1);
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float baseColorGray = 0.6f;


    void Start()
    {
        baseColor = new Color(baseColorGray, baseColorGray, baseColorGray);
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<SpriteRenderer>().color = Color.LerpUnclamped(baseColor, white, Mathf.PingPong(Time.time, speed));

    }
}
