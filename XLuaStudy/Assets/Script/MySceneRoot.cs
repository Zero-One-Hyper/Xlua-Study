using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneRoot : MonoBehaviour
{
    public void StartHotFix()
    {
        SceneManager.LoadScene(1);
    }
}
