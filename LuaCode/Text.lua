
print("require了Text.lua")

local UnityEngine = CS.UnityEngine

xlua.hotfix(CS.Cube, 'Update', function (self)
    if UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.S) then
        --Addforce是Rigidbody里的方法，lua中调用需要指定self（rigidbody）所以使用：连接
        self.rigb:AddForce(UnityEngine.Vector3.up * 500)
    end
    CS.Cube.Text();

end)

xlua.hotfix(CS.LoadManager, 'Start', function (self)
    print('Start方法')
    self.hotfix:LoadResource('Sphere', 'gameobject\\Sphere.ab')   
    
end)
xlua.hotfix(CS.LoadManager, 'Update', function (self)
    if UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.P) then
        go = self.hotfix:GetPrefab('Sphere')
        UnityEngine.Object.Instantiate(go, self.transform)
    end
end)