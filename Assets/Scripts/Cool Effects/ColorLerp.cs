using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    // Start is called before the first frame update
    public Color baseColor;
    public Color white = new Color(1, 1, 1,255);
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float baseColorGray = 0.6f;


    private void Awake()
    {
        baseColor = this.gameObject.GetComponent<SpriteRenderer>().color;

    }

    void Start()
    {
    //    baseColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<SpriteRenderer>().color = Color.LerpUnclamped(baseColor, white, Mathf.PingPong(Time.time, speed));

    }
}
