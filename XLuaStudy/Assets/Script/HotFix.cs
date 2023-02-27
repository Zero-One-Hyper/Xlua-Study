//using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using XLua;


public class HotFix : MonoBehaviour
{
    private static HotFix instance;
    public static HotFix Instance
    {
        get
        {
            if(instance == null)
                instance =(HotFix)FindObjectOfType<HotFix>();
            return instance;
        }
    }
    private LuaEnv luaEnv;//开启了一个虚拟环境

    private Dictionary<string, byte[]> luaScript = new Dictionary<string, byte[]>();
    private Dictionary<string, AssetBundle> AssetPrefabs = new Dictionary<string, AssetBundle>();
    private Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();
    private void Awake() 
    {
        DontDestroyOnLoad(this.transform.parent.gameObject);
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        //luaEnv.DoString("require 'Text'");
    }
    byte[] MyLoader(ref string filePath)
    {
        string absPath = @"E:\UnityStudy\XluaHotFix\LuaCode\TempCode\" + filePath + ".lua";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }
    //在停止play时，只是直接回收了luaEnv的内存，而Text.lua中对update的修改内存还没有处理，这里直接
    //这里直接调用LuaDispose 使用 xlua.hotfix(CS.Cube, 'Update', nil) 将update设置为nil来回收 
    private void OnDisable() 
    {
        //luaEnv.DoString("require 'LuaDispose'");
    }
    private void OnDestroy() //开启了一个虚拟环境，需要回收
    {
        luaEnv = null;
        luaEnv?.Dispose();
    }
    //
    
    public void LoadResource(string resName, string filePath)
    {
        StartCoroutine(Load(resName, filePath));
    }

    IEnumerator Load(string resName, string filePath)
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(@"http://localhost/AssetsBundles/" + filePath);
        yield return request.SendWebRequest();
        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        if(ab == null)
        {
            Debug.Log("ab是null");
            yield break;
        }
        GameObject go = ab.LoadAsset<GameObject>(resName);
    
        this.Prefabs.Add(resName, go);
    }
    public GameObject GetPrefab(string goName)
    {
        
        AssetBundle ab = GetAsset(goName);
        GameObject go;
        go = ab.LoadAsset<GameObject>(goName);
        if(go == null)
            Debug.Log("aaaaaaaaa");
        return go;
    }
    AssetBundle GetAsset(string resName)
    {
        AssetBundle go = null;
        /*
        Debug.Log(AssetPrefabs.Count);
        foreach(var tt in  AssetPrefabs)
        {
            Debug.Log(tt.Key);
        }
        */
        if(AssetPrefabs.TryGetValue(resName, out go))
        {
            Debug.Log("成功load到了GameObject");
            return go;
        }
        else
            Debug.Log("nnnnnnn");
        if(go == null)
            Debug.Log("kkk");
        return go;
    }
    public void AddAsset(AssetBundle go, string name)
    {
        if(go == null)
        {
            Debug.Log("GameObject is null");
            return;
        }
        this.AssetPrefabs.Add(name, go);
    }
    public LuaEnv GetLuaEnv()
    {
        return luaEnv;
    }
    public void ReSetLuaEnv()
    {
        luaEnv = null;
        luaEnv.Dispose();
    }
    //存着lua脚本的
    public byte[] GetLuaScript(string name)
    {
        //需要在之前拿到所有lua脚本的引用
        byte[] m_script = null;
        //缓存。判断这个lua是否加载过
        if (!luaScript.TryGetValue(name, out m_script))
            Debug.LogError("lua script is not exist:" + name);
        return m_script;
    }
}
