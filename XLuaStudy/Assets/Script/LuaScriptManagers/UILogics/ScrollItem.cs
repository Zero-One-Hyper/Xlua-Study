using System.Xml.Serialization;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XLua;

[CSharpCallLua]
public class Item
{
    public Item(string m_name, int m_count)
    {
        if(m_name != "-1")
            name = m_name;
        else
            name = null;
        count = m_count;
    }
    public string name;
    public int count;
}
public class ScrollItem : LuaMono, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Action<PointerEventData> LuaPointerEnter;
    Action<PointerEventData> LuaPointerExit;
    Action<PointerEventData> LuaPointerClick;

    
    public Item item;
    public ScrollRectLogic owner;
    public void Init(string luaScpritName, ScrollRectLogic m_owner, string name, int count)
    {
        owner = m_owner;
        item = new Item(name, count);
        this.Init(luaScpritName);
    }
    public override void Init(string luaScpritName)
    {

        base.Init(luaScpritName);
                
        //this.Init("Entity");
        luaTable.Get("OnScrollItemInit", out LuaInit);        
        luaTable.Get("OnScrollItemEnable", out LuaOnEnable);
        luaTable.Get("OnScrollItemStart", out LuaStart);
        luaTable.Get("OnScrollItemUpdate", out LuaUpdate);        
        luaTable.Get("OnScrollItemDisable", out LuaOnDisable);
        luaTable.Get("OnPointerClick", out LuaPointerClick);
        luaTable.Get("OnPointerEnter", out LuaPointerEnter);
        luaTable.Get("OnPointerExit", out LuaPointerExit);

        LuaInit?.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        LuaStart?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        LuaUpdate?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LuaPointerClick?.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.LuaPointerEnter?.Invoke(eventData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.LuaPointerExit?.Invoke(eventData);
    }

    protected override void Clear()
    {
        this.LuaPointerClick = null;
        this.LuaPointerEnter = null;
        this.LuaPointerExit = null;
        base.Clear();
    }
}
