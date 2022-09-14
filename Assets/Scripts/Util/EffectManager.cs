using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // Start is called before the first frame update
    ColorLerp colorLerp;
    LerpPosition lerpPosition;
    void Start()
    {
        colorLerp = GetComponent<ColorLerp>();
        lerpPosition = GetComponent<LerpPosition>();
        colorLerp.enabled = false;
        lerpPosition.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelected() {

        colorLerp.enabled = !colorLerp.enabled;
        lerpPosition.enabled = !lerpPosition.enabled;
    }
}
