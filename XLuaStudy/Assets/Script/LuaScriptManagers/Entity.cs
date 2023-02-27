using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class Entity : LuaMono
{    
    
    //在Awake之前拿到lua脚本 场景切换时调用 此脚本不直接放入场景，而是场景切换后，新建GameObject
    //随后SceneManager.MoveGameObjectToScene(go, scene); 并将此脚本AddComponent，随后调用此方法
    public override void Init(string luaScpritName)
    {
        base.Init(luaScpritName);

        //this.Init("Entity");
        luaTable.Get("OnEntityInit", out LuaInit);
        luaTable.Get("OnEntityAwake", out LuaAwake);
        luaTable.Get("OnEntityStart", out LuaStart);
        luaTable.Get("OnEntityUpdate", out LuaUpdate);
        luaTable.Get("OnEntityFixUpdate", out LuaFixUpdate);
        luaTable.Get("OnEntityLateUpdate", out LuaLateUpdate);
        LuaInit?.Invoke();
    }
    void Awake() 
    {
        //DontDestroyOnLoad(this.gameObject);
        this.LuaAwake?.Invoke();
    }
    private void OnEnable() {
        Debug.Log("OnEnable");
    }
    void Start()
    {
        this.LuaStart?.Invoke();
        
    }

    // Update is called once per frame
    void Update()
    {
        this.LuaUpdate?.Invoke();

        //RotateCamera();
    }
    
    private void FixedUpdate() 
    {
        this.LuaFixUpdate?.Invoke(); 
        //RotateCamera();   
    }
    private void LateUpdate() 
    {
        this.LuaLateUpdate?.Invoke();    
    }

}
