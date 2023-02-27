print("require 了 Entity")

local rig = {}

function OnEntityAwake()
    print('OnEntityAwake')
    local camera = CS.UnityEngine.Camera.main
    
    camera.transform:SetParent(self.transform:GetChild(0).transform)
    local cameraPosition = camera.transform.parent.transform.position
    local cameraRotation = camera.transform.parent.transform.rotation
    camera.transform:SetPositionAndRotation(cameraPosition, cameraRotation)
    rig = self.gameObject:GetComponent(typeof(CS.UnityEngine.Rigidbody))
end

function OnEntityStart()
    print("OnEntityStart")
end

local up = CS.UnityEngine.KeyCode.W
local down = CS.UnityEngine.KeyCode.S
local right = CS.UnityEngine.KeyCode.D
local left = CS.UnityEngine.KeyCode.A
local run = CS.UnityEngine.KeyCode.LeftShift

local baseSpeed = 8

local Dforward
local Dright
local Drun
local M_forward --正面移动速度
local M_mag --椭圆平面上二维移动向量
local DMoveForwardNormal --player应该移动的方向向量单位向量
local DMove --移动方向向量

function OnEntityUpdate()
    print('OnEntityUpdate')
    local tempup = 0
    if CS.UnityEngine.Input.GetKey(up) then
        tempup = tempup + 1
    else 
        tempup = tempup + 0
    end
    if CS.UnityEngine.Input.GetKey(down) then
        tempup = tempup - 1
    else 
        tempup = tempup - 0
    end
    
    Dforward = tempup

    local tempright = 0
    if CS.UnityEngine.Input.GetKey(right) then
        tempright = tempright + 1
    else 
        tempright = tempright + 0
    end
    if CS.UnityEngine.Input.GetKey(left) then
        tempright = tempright - 1
    else 
        tempright = tempright - 0
    end    
    Dright = tempright

    local temprun = 1
    if CS.UnityEngine.Input.GetKey(run) then
        temprun = 2.0
    else
        temprun = 1.0
    end
    Drun = temprun

    --角色在x z平面上的移动方向 转化成椭圆平面
    M_mag = SquareToCircle(Dforward, Dright)

    M_forward = math.sqrt(Dforward * Dforward + Dright * Dright) * Drun

    if M_forward <= 0.01 then--小于0.01认为停止
        DMove = CS.UnityEngine.Vector3(0, 0, 0)
        DMoveForwardNormal = CS.UnityEngine.Vector3(0, 0, 0)
        return
    end

    DMoveForwardNormal = (self.transform.forward * M_mag.x + self.transform.right * M_mag.y).normalized
    DMove = M_forward * DMoveForwardNormal * Drun * baseSpeed

end

function OnEntityFixUpdate()
    print('OnEntityFixUpdate')
    local temp = rig.position
    rig.position = temp + DMove * CS.UnityEngine.Time.fixedDeltaTime

end

function OnEntityLateUpdate()
    print('OnEntityLateUpdate')
end

function SquareToCircle(a, b)
    x = a * math.sqrt(1 - (b * b) / 2.0)
    y = b * math.sqrt(1 - (a * a) / 2.0)
    return CS.UnityEngine.Vector2(x, y)
end