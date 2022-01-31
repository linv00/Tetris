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
        foreach (Tetraminos tetramino in System.Enum.GetValues(typeof(Tetraminos)))
        {
            if (!CellData.Available.Contains(tetramino.ToString()))
            {
                CellData.Available.Add(tetramino.ToString());
            }
        }
        CellData.Available.Reverse();
    }
    public void ClassicModeStart()
    {
        SceneManager.LoadScene(1);
        CellData.mode = true;
        CellData.blockMode = false;
    }
    public void ChaoticModeStart()
    {
        SceneManager.LoadScene(2);
        CellData.mode = false;
        CellData.blockMode = false;
    }
    public void BlockModeStart()
    {
        CellData.blockMode = true;
        CellData.mode = true;
        SceneManager.LoadScene(3);
    }
}
