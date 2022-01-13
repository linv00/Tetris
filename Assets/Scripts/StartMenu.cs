using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void ClassicModeStart()
    {
        SceneManager.LoadScene(1);
    }
    public void ChaoticModeStart()
    {
        SceneManager.LoadScene(2);
    }
    public void BlockModeStart()
    {
        SceneManager.LoadScene(3);
    }
}
