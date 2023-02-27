using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public static class ResouceLoader
{
    public static Sprite LoadSprite(string path)
    {
        Sprite go = Resources.Load<Sprite>(path);
        return go;
    }
}
