using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLogic : LuaMono
{
    // Start is called before the first frame update
    public override void Init(string luaScpritName)
    {
        base.Init(luaScpritName);

        luaTable.Get("OnSceneInit", out LuaInit);
        luaTable.Get("OnSceneAwake", out LuaAwake);
        luaTable.Get("OnSceneStart", out LuaStart);
        luaTable.Get("OnSceneUpdate", out LuaUpdate);
        luaTable.Get("OnSceneFixUpdate", out LuaFixUpdate);
        luaTable.Get("OnSceneLateUpdate", out LuaLateUpdate);
        LuaInit?.Invoke();
    }
    public GameObject ui;
    void Start()
    {      
        LuaStart?.Invoke();
        
        ui = GameObject.FindGameObjectWithTag("UI");
        var temp = ui.transform.GetChild(0).GetComponent<Image>();
             
        Action acyio = Update;
        //AssetBundle ab = HotFix.Instance.GetAsset("player");
        /*
        GameObject go;
        //go = ab.LoadAsset<GameObject>("player");
        go = HotFix.Instance.GetPrefab("player");
        if(go == null)
        {
            Debug.LogError("GameObject player is null");
            return;
        }    
        go.AddComponent<Entity>();
        var temp = Instantiate(go, this.transform);
        temp.GetComponent<Entity>().Init("Entity");
        */
    }

    // Update is called once per frame
    void Update()
    {
        LuaUpdate?.Invoke();
    }
}
