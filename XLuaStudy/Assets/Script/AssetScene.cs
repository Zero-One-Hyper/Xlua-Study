using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetScene : MonoBehaviour
{
    private void Awake() 
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        

        //AssetBundle ab = HotFix.Instance.GetAsset("player");

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
    }

}
