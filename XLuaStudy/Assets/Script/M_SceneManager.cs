using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_SceneManager : MonoBehaviour
{
    private static M_SceneManager instance;
    public static M_SceneManager Instance
    {
        get
        {
            if(instance == null)
                instance = (M_SceneManager)FindObjectOfType<M_SceneManager>();
            return instance;
        }

    }
    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartLoadScene(string name)
    {
        StartCoroutine(LoadScene(name));

    }
    IEnumerator LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        yield return null;
        //加载场景后 给场景挂上场景的脚本 启动场景的脚本
        var scene = SceneManager.GetActiveScene();
        
        foreach(var gg in scene.GetRootGameObjects())
        {
            Debug.Log(gg.name);
            if(gg.name == "MapRoot")
            {
                //如何通过场景名 给MapRoot添加对应的脚本？
                //配置文件？
                gg.AddComponent<SceneLogic>();
                gg.GetComponent<SceneLogic>().Init("SceneLogic");
                yield break;
            }
        }
        Debug.Log("没有MapRoot");
        yield return null;
    }
}
