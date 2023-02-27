using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[CSharpCallLua]
public class ScrollRectLogic : LuaMono
{
    
    Action<Item> LuaSelectedItem;
    public override void Init(string luaScpritName)
    {
        base.Init(luaScpritName);
    }
    private void Awake() {
        
        this.LuaAwake?.Invoke();
    }
    private void OnEnable() 
    {
        this.LuaOnEnable?.Invoke();    
    }
    
    void Start()
    {
        Init("UIBag");

        luaTable.Get("OnUIScrollRectAwake", out LuaAwake);
        luaTable.Get("OnUIScrollRectEnable", out LuaOnEnable);
        luaTable.Get("OnUIScrollRectStart", out LuaStart);
        luaTable.Get("OnUIScrollRectUpdate", out LuaUpdate);
        luaTable.Get("OnUIScrollRectFixUpdate", out LuaFixUpdate);
        luaTable.Get("OnUIScrollRectLateUpdate", out LuaLateUpdate);
        luaTable.Get("OnUIScrollRectDisable", out LuaOnDisable);
        luaTable.Get("OnUIScrollRectLuaSelectedItem", out LuaSelectedItem);

        this.LuaStart?.Invoke();
        /*
        Image go = this.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        GameObject gp = null;
        var rr = Instantiate(gp, this.transform);
        gp.AddComponent<ScrollItem>();
        go.overrideSprite = Resources.Load<Sprite>("Items/Mushroom");
        */
    }

    // Update is called once per frame
    void Update()
    {
        this.LuaUpdate?.Invoke();
    }
    
    private void FixedUpdate() 
    {
        this.LuaFixUpdate?.Invoke(); 
    }
    private void LateUpdate() 
    {
        this.LuaLateUpdate?.Invoke();    
    }
    private void OnDisable() 
    {
        this.LuaOnDisable?.Invoke();    
    }

    [CSharpCallLua]
    public void OnSelectedItem(Item item)
    {
        LuaSelectedItem?.Invoke(item);
    }
}
