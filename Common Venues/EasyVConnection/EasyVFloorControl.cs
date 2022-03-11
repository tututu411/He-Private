using EasyV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyVFloorControl : SetConfigComponentBase
{
    public FloorManager floorManager;
    public override void UpdateSetting()
    {
        Debug.Log("刷新楼层信息");
    }
    
    [EasyVGetConfig("SetFloorEasy", "改变楼层")]
    public string GetFloorEasy()
    {
        return ":Floor";
    }
    string currentFloor = "全景";
    [EasyVSetConfig("SetFloorEasy")]
    public void SetFloor(object data)
    {
        try
        {
            string floor = (string)data;
            if (floor == currentFloor)
                return;
            Debug.Log("接受到楼层信息：" + floor);
            if (floor == "全景")
            {
                floorManager?.SwitchFloor(EnumClass.FloorState.全楼);
            }
            else if(floor == "1F")
            {
                floorManager?.SwitchFloor(EnumClass.FloorState.一);
            }
            else if (floor == "2F") 
            {
                floorManager?.SwitchFloor(EnumClass.FloorState.二);
            }
            else if (floor == "3F")
            {
                floorManager?.SwitchFloor(EnumClass.FloorState.三);
            }
            else if (floor == "4F")
            {
                floorManager?.SwitchFloor(EnumClass.FloorState.四);
            }
            else
            {
                return;
            }

            currentFloor = floor;
        }
        catch (System.Exception e)
        {
            Debug.LogError("楼层转换失败：" + e.Message);
        }

    }
}
