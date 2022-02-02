using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CombatTextTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string descriptionString = "";
    public GameObject combatText;
    // Start is called before the first frame update
    void Start()
    {
        combatText = GameObject.Find("CombatText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {   

        if(this.gameObject.GetComponent<Button>().interactable)
        combatText.GetComponent<Text>().text = descriptionString;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.gameObject.GetComponent<Button>().interactable)
            combatText.GetComponent<Text>().text = "Select a command";
    }
}
