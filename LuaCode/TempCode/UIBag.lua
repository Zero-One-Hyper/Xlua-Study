print("require 了 UIBag")

require 'Items'

local itemContent = {} --transform
local itemSelect = {}
local itemIcon = {}
local itemInfos = {}

local CloseButton = {}


local isFirstOpen = true
function OnUIScrollRectAwake()

end

function OnUIScrollRectEnable()
    --enabley在init之前会调用一次 如何保证enable正常使用的同时，在init之后走逻辑？
    if isFirstOpen then
        --print('第一次Enable')
        isFirstOpen = false
        return
    end
    --print('非第一次OnEnable')
    --逻辑主体

end

local item = {}
local itemslot = {}

function OnUIScrollRectStart()
    print("OnUIScrollStart")
    item = Items
    
    local temp = self.transform:GetChild(0).gameObject
    local temp2 = temp.transform:GetChild(0)
    itemContent = temp2:GetChild(0)
    local infoTemp = self.transform:GetChild(1)
    itemIcon = infoTemp:GetChild(0):GetChild(0):GetComponent(typeof(CS.UnityEngine.UI.Image))
    itemInfos = infoTemp:GetChild(1):GetChild(0):GetComponent(typeof(CS.UnityEngine.UI.Text))
    
    itemslot = CS.HotFix.Instance:GetPrefab('itemslot')
    
    CloseButton = self.transform:GetChild(2):GetComponent(typeof(CS.UnityEngine.UI.Button))

    CloseButton.onClick:AddListener(CloseBag)
    InitItems()
end

--初始化背包
function InitItems()
    if itemslot == nil then
        print('Itemslot 是空')
        return
    end
    for count = 0, 24, 1 do
        local temp = CS.UnityEngine.GameObject.Instantiate(itemslot, itemContent)
        tempslot = temp:AddComponent(typeof(CS.ScrollItem))
        if count < item.Bag.slotCount then
          
            tempslot:Init('UIBagItem', self, Items.Bag.item[count + 1].name, Items.Bag.item[count + 1].count)           
        else
            tempslot:Init('UIBagItem', self, "-1", -1)
        end
        
    end
end

function OnUIScrollRectUpdate()

    
end

function OnUIScrollRectDisable()
    
end


function OnUIScrollRectLuaSelectedItem(m_item)
    if m_item.count == -1 then
        itemIcon.transform.gameObject:SetActive(false)
        itemInfos.text = ''
        return
    end
    --设置图片Icon
    itemIcon.transform.gameObject:SetActive(true)
    itemIcon.overrideSprite = CS.ResouceLoader.LoadSprite(m_item.name)
    --设置描述name
    itemInfos.text = m_item.name
end

function CloseBag()
    self.transform.gameObject:SetActive(false)
   
end