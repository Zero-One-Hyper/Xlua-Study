using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;

public class LuaLoader : MonoBehaviour
{
    private void Start() 
    {
        StartLoad();
    }
    public void StartLoad()
    {
        StartCoroutine(LoadLua());
    }
    IEnumerator LoadLua()
    {
        /*
        UnityWebRequest request = UnityWebRequest.Get(@"http://localhost/LuaCode/" + "Text.lua");
        yield return request.SendWebRequest();
        string luaCode = request.downloadHandler.text;
        File.WriteAllText(@"D://StudyUnity/XluaHotFix/LuaCode/TempCode/Text.lua", luaCode);
        yield return null;
        
        UnityWebRequest request2 = UnityWebRequest.Get(@"http://localhost/LuaCode/" + "LuaDispose.lua");
        yield return request2.SendWebRequest();
        string luaCode2 = request2.downloadHandler.text;
        File.WriteAllText(@"D://StudyUnity/XluaHotFix/LuaCode/TempCode/LuaDispose.lua", luaCode2);
        yield return null;
        */
        UnityWebRequest request3 = UnityWebRequest.Get(@"http://localhost/LuaCode/" + "Entity.lua");
        yield return request3.SendWebRequest();
        string luaCode3 = request3.downloadHandler.text;
        File.WriteAllText(@"E://UnityStudy/XluaHotFix/LuaCode/TempCode/Entity.lua", luaCode3);
        yield return null;

        UnityWebRequest request4 = UnityWebRequest.Get(@"http://localhost/LuaCode/" + "SceneLogic.lua");
        yield return request4.SendWebRequest();
        string luaCode4 = request4.downloadHandler.text;
        File.WriteAllText(@"E://UnityStudy/XluaHotFix/LuaCode/TempCode/SceneLogic.lua", luaCode4);
        yield return null;

        UnityWebRequest request5 = UnityWebRequest.Get(@"http://localhost/LuaCode/" + "UIBag.lua");
        yield return request5.SendWebRequest();
        string luaCode5 = request5.downloadHandler.text;
        File.WriteAllText(@"E://UnityStudy/XluaHotFix/LuaCode/TempCode/UIBag.lua", luaCode5);
        yield return null;

        UnityWebRequest request6 = UnityWebRequest.Get(@"http://localhost/LuaCode/" + "Items.lua");
        yield return request6.SendWebRequest();
        string luaCode6 = request6.downloadHandler.text;
        File.WriteAllText(@"E://UnityStudy/XluaHotFix/LuaCode/TempCode/Items.lua", luaCode6);
        yield return null;

        UnityWebRequest request7 = UnityWebRequest.Get(@"http://localhost/LuaCode/" + "UIBagItem.lua");
        yield return request7.SendWebRequest();
        string luaCode7 = request7.downloadHandler.text;
        File.WriteAllText(@"E://UnityStudy/XluaHotFix/LuaCode/TempCode/UIBagItem.lua", luaCode7);
        yield return null;
        //SceneManager.LoadScene(1);
        StartCoroutine(LoadPlayer());
    }

    IEnumerator LoadPlayer()
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(@"http://localhost/AssetsBundles/gameobject/" + "player.ab");
        yield return request.SendWebRequest();
        while(!request.isDone)
        {
            Debug.Log(request.downloadProgress);
            yield return null;
        }
        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        if(ab == null)
        {
            Debug.Log("ab包为空");
            yield break;
        }
        HotFix.Instance.AddAsset(ab, "player");
        
        request = null;
        ab = null;

        UnityWebRequest request1 = UnityWebRequestAssetBundle.GetAssetBundle(@"http://localhost/AssetsBundles/ui/" + "itemslot.ab");
        yield return request1.SendWebRequest();
        while(!request1.isDone)
        {
            Debug.Log(request1.downloadProgress);
            yield return null;
        }
        ab = (request1.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        if(ab == null)
        {
            Debug.Log("加载的BagSlot为空");
            yield break;
        }
        HotFix.Instance.AddAsset(ab, "itemslot");

        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        //Debug.Log("加载场景");
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(@"http://localhost/AssetsBundles/" + "scene/testscene.ab");
        yield return request.SendWebRequest();
        while(!request.isDone)
        {
            Debug.Log(request.downloadProgress);
            yield return null;
        }
        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        if(ab == null)
        {
            Debug.LogError("ab包为空");
            yield break;
        }
        string[] strs = ab.GetAllScenePaths();
        M_SceneManager.Instance.StartLoadScene(strs[0]);
        
        //Debug.Log("加载场景完成");
        yield return null;
        
    }
}
