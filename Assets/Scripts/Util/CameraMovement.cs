using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    bool idleState = false;
    public Vector3 InitialPosition;
    public Vector3 targetPosition;
    float targetXValue;
    float targetYValue;
    float timePassed = 0;
    public int randomMovementValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = GetComponent<Transform>().position;
        targetPosition = InitialPosition;
        targetXValue = targetPosition.x + 0.1f;
        targetYValue = targetPosition.y + 0.1f;
        randomMovementValue = Random.Range(1, 7);
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 10f)
        {
            timePassed = 0;
            randomMovementValue = Random.Range(0, 7);
            idleState = !idleState;
        }

        switch (randomMovementValue) {
            case 1:
                GetComponent<Transform>().position = Vector3.LerpUnclamped(InitialPosition, new Vector3(targetXValue, targetPosition.y, targetPosition.z), Mathf.PingPong(Time.time, 5f));
                break;
            case 2:
                GetComponent<Transform>().position = Vector3.LerpUnclamped(InitialPosition, new Vector3(targetPosition.x, targetYValue, targetPosition.z), Mathf.PingPong(Time.time, 5f));
                break;
            case 3:
                GetComponent<Transform>().position = Vector3.LerpUnclamped(InitialPosition, new Vector3(targetXValue, targetYValue, targetPosition.z), Mathf.PingPong(Time.time, 5f));
                break;
            case 4:
                GetComponent<Transform>().position = Vector3.LerpUnclamped(InitialPosition, new Vector3(targetXValue-0.2f, targetYValue-0.2f, targetPosition.z), Mathf.PingPong(Time.time, 5f));
                break;
            case 5:
                GetComponent<Transform>().position = Vector3.LerpUnclamped(InitialPosition, new Vector3(targetXValue - 0.2f, targetPosition.y, targetPosition.z), Mathf.PingPong(Time.time, 5f));
                break;
            case 6:
                GetComponent<Transform>().position = Vector3.LerpUnclamped(InitialPosition, new Vector3(targetPosition.x, targetYValue-0.2f, targetPosition.z), Mathf.PingPong(Time.time, 5f));
                break;
        }

    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(5f);
        idleState = !idleState;
    }
}
