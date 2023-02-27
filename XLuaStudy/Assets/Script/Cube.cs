using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//对于需要热更的脚本，引用Xlua 类添加[Hotfix]特性 需要由lua更改的代码添加[luaCallCSharp]特性
using XLua;

//hotfix只适用于演示，打包出来不会有反应，实际应该在Editor中使用static列表？

public class Cube : MonoBehaviour
{
    Rigidbody rigb;
    // Start is called before the first frame update
    void Start()
    {
        rigb = this.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
            rigb.AddForce(Vector3.up * 500);
    }
    public static void Text()
    {
        Debug.Log("测试lua");
    }
}
