
local ownerBag = {}
local itemInfo = {}

local itemIcon = {}
local itemCount = {}

local itemButton = {}

function OnScrollItemInit()
    
end

function OnScrollItemEnable()
    --enabley在init之前会调用一次 如何保证enable正常使用的同时，在init之后走逻辑？
    if isFirstOpen then
        print('第一次Enable')
        isFirstOpen = false
        return
    end
    print('非第一次OnEnable')
    --逻辑主体

end
local string name = {}
function OnScrollItemStart()
    --print("OnEntityStart")
    itemInfo = self.item
    ownerBag = self.owner
    --记得setactive
    itemButton = self.transform:GetComponent(typeof(CS.UnityEngine.UI.Button))
    itemIcon = self.transform:GetChild(0):GetComponent(typeof(CS.UnityEngine.UI.Image))
    itemCount = self.transform:GetChild(1):GetComponent(typeof(CS.UnityEngine.UI.Text))
   
    --设置每一个
    if itemInfo.count ~= -1 then
        print(itemInfo.name)
        itemIcon.transform.gameObject:SetActive(true)        
        print(typeof(itemInfo.name))

        --Lua中不存在as 强制类型转化的方法，不能直接使用
        --itemIcon.overrideSprite = CS.UnityEngine.Resources.Load(CS.UnityEngine.Sprite, itemInfo.name)
        --建立一个复制类来从Resoureces文件夹加载内容
        itemIcon.overrideSprite = CS.ResouceLoader.LoadSprite(itemInfo.name)
        itemCount.text = itemInfo.count        
    else
        print('asjkdghjkahsjkdhasjk')

    end
    itemButton.onClick:AddListener(OnSelected)
end


function OnScrollItemUpdate()

    
end


function OnScrollItemDisable()
    
end


function OnPointerClick(eventData)
    
    
end

function OnPointerEnter(eventData)
    
end

function OnPointerExit(eventData)
    
end

function SetItemInfo(owner, info)
    ownerBag = owner
    itemInfo = info
end

function OnSelected()
    print("Click了欸")
    ownerBag:OnSelectedItem(itemInfo)
end