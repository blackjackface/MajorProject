using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    bool outOfTheGame = false;
    public void ChangeToBossCombat1() {
        SceneManager.LoadScene("BossBattle1");
    }
    public void ChangeToBossCombat2()
    {
        SceneManager.LoadScene("BossBattle2");
    }
    public void ChangeToBasicCombat2()
    {
        SceneManager.LoadScene("BasicBattle");
    }

    public void ChangeToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (outOfTheGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ChangeToMainMenu();
            }


        }

    }
}
