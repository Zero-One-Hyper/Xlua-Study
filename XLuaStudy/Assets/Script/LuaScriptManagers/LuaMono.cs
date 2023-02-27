using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

public abstract class LuaMono : MonoBehaviour
{
    protected Action LuaInit;
    protected Action LuaAwake;
    protected Action LuaOnEnable;
    protected Action LuaStart;
    protected Action LuaUpdate;
    protected Action LuaFixUpdate;
    protected Action LuaLateUpdate;
    protected Action LuaOnDisable;
    protected LuaTable luaTable;

    private string FilePath = "E://UnityStudy/XluaHotFix/LuaCode/TempCode/";
    protected LuaEnv luaEnv
    {
        get
        {
            return HotFix.Instance.GetLuaEnv();
        }
    }

    public virtual void Init(string luaScpritName)
    {
        //Debug.Log("Lua 的 Init" + luaScpritName);
        
        luaTable = luaEnv.NewTable();
        
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        luaTable.SetMetaTable(meta);
        meta.Dispose();
        //设置self
        luaTable.Set("self", this);
        //luaTable.Set("__newindex", this);
        string script = File.ReadAllText(FilePath + luaScpritName + ".lua");
        //加载lua脚本                    传入lua脚本的内容  ，脚本名？       运行环境
        luaEnv.DoString(script, luaScpritName, luaTable);
        
    }
    
    protected virtual void Clear()
    {
        LuaInit = null;
        LuaAwake = null;
        LuaOnEnable = null;
        LuaStart = null;
        LuaUpdate = null;
        LuaFixUpdate = null;
        LuaLateUpdate = null;
        LuaOnDisable = null;
    }
}
