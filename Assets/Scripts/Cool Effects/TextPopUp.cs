using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{

    public float seconds = 2;
    public float timer = 0;
    public Vector3 Point;
    public Vector3 Difference;
    public Vector3 start;
    public float percent;
    void Start()
    {
        start = transform.position;
        Point = new Vector3(transform.position.x, transform.position.y + 1.2f , transform.position.z);
        Difference = Point - start;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2f)
        {
            Destroy(this.gameObject);

        }
        if (timer <= seconds)
        {
            // basic timer
            timer += Time.deltaTime;
            // percent is a 0-1 float showing the percentage of time that has passed on our timer!
            percent = timer / seconds;
            // multiply the percentage to the difference of our two positions
            // and add to the start
            transform.position = start + Difference * percent;
        }
    }
}