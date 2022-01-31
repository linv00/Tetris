using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaoticModeScene : MonoBehaviour
{
    public void Awake()
    {
        CellData.mode = false;
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
