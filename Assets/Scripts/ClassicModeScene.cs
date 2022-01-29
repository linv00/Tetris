using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassicModeScene: MonoBehaviour
{
    public void Awake()
    {
        CellData.mode = true;
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
