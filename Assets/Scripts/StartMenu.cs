using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void Start()
    {
        Screen.fullScreen = false;
    }
    public void ClassicModeStart()
    {
        SceneManager.LoadScene(1);
        CellData.mode = true;
    }
    public void ChaoticModeStart()
    {
        SceneManager.LoadScene(2);
        CellData.mode = false;
    }
    public void BlockModeStart()
    {
        SceneManager.LoadScene(3);
    }
}
