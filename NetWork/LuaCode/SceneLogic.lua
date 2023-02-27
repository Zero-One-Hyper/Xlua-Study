
local CanvsRoot = {}
local Bag = {}


function OnSceneStart()
    print("SceneLogic Start")
    
    --GameObject
    local go = CS.HotFix.Instance:GetPrefab('player')
    if go == nil then
        print("GameObject player is null")
    end        
    go:AddComponent(typeof(CS.Entity))

    local temp = CS.UnityEngine.GameObject.Instantiate(go, self.transform)
    local m_init = temp:GetComponent(typeof(CS.Entity))
    m_init:Init('Entity')

    CanvsRoot = CS.UnityEngine.GameObject.FindGameObjectWithTag("UI")
    if CanvsRoot ~= nil then
        Bag = CanvsRoot.transform:GetChild(0).gameObject
        local tamp = Bag:AddComponent(typeof(CS.ScrollRectLogic))
        
        tamp:Init('UIBag')
    else
        print("CanvsRoot 为nil")
    end
end

function OnSceneUpdate()
    if CS.UnityEngine.Input.GetKeyDown(CS.UnityEngine.KeyCode.Tab) then
        print('按下了Tab键')
        if Bag ~= nil then            
            Bag:SetActive(true)
        end
    end
end