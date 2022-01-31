using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseBlockScene : MonoBehaviour
{
    public Toggle[] toggles;

    public void Awake()
    {
        CellData.mode = true;
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Button_Play()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (!toggles[i].isOn)
            {
                CellData.Available.Remove(((Tetraminos)i).ToString());
            }
            else
            {
                if (!CellData.Available.Contains(((Tetraminos)i).ToString()))
                {
                    CellData.Available.Add(((Tetraminos)i).ToString());
                }
            }
        }
        if (CellData.Available.Count != 0)
        {
            SceneManager.LoadScene(4);
        }
    }
}
