using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyV;

public class EasyVTimeControl : SetConfigComponentBase
{
    public Common_Venues.TimeWeatherManager timeWeatherManager;
    public override void UpdateSetting()
    {
        Debug.Log("时间刷新完成");
    }
    string currentTime = "白天";
    [EasyVSetConfig("SetTimeEasy")]
    public void SetTime(object data)
    {
        try
        {
            string time = (string)data;
            if (time == currentTime)
                return;
            Debug.Log("接受到时间信息：" + time);
            if (time == "白天")
            {
                timeWeatherManager?.TimeToDay();
            }
            else if (time == "黑夜")
            {
                timeWeatherManager?.TimeToNight();
            }
            else { }
            currentTime = time;
        }
        catch (System.Exception e)
        {
            Debug.LogError("时间转换失败：" + e.Message);
        }

    }
    [EasyVGetConfig("SetTimeEasy", "修改时间")]
    private string GetTimeEasy()
    {
        return ":Time";
    }
}
