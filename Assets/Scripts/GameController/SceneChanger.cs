using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void LoadCombat1()
    {
        SceneManager.LoadScene("Combat 1");
    }
    public void LoadCombat2()
    {
        SceneManager.LoadScene("Combat 2");
    }
}
