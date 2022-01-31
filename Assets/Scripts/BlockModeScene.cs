using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockModeScene : MonoBehaviour
{
    public void ReturnChooseBlock()
    {
        SceneManager.LoadScene(3);
    }

}
