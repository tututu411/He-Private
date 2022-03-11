using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyV;
using Common_Venues;
using static Common_Venues.TimeWeatherManager;
using System;

public class EasyVMessage : SetConfigComponentBase
{
    public override void UpdateSetting()
    {
        Debug.Log("刷新属性完成");
        GC.Collect();
    }
    private void Start()
    {
        configupdated = true;
    }


    #region Loading
    int currentLoading = 0;
    public GameObject EasyPanel;
    [EasyVGetConfig("EasyVSetLoading", "页面加载完成回调")]
    public string GetLoading()
    {
        return ":Loading";
    }
    [EasyVSetConfig("EasyVSetLoading")]
    public void SetLoading(object data)
    {
        string mes = (string)data;
        if (currentLoading > 0)
            return;
        EasyPanel.SetActive(true);
        currentLoading += 1;
        Debug.Log(mes);
    }
    #endregion


    #region Time
    public TimeWeatherManager timeWeatherManager;
    string currentTime = "白天";
    [EasyVGetConfig("EasyVSetTime", "设置时间")]
    public string GetTime()
    {
        return ":Time";
    }
    [EasyVSetConfig("EasyVSetTime")]
    public void SetTime(object data)
    {
        try
        {
            string time = (string)data;
            if (time[0] == ':') return;
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
    #endregion


    #region Weather
    public TimeWeatherManager weatherManager;
    string currentWether = "晴天";
    [EasyVGetConfig("EasyVSetWeather", "设置天气")]
    public string GetWeather()
    {
        return ":Weather";
    }

    [EasyVSetConfig("EasyVSetWeather")]
    public void SetWeather(object data)
    {
        try
        {
            string weather = (string)data;
            if (weather[0] == ':') return;
            if (weather == currentWether)
                return;

            Debug.Log("接受到天气状态信息：" + weather);
            WeatherType type = (WeatherType)Enum.Parse(typeof(WeatherType), weather);
            switch (type)
            {
                case WeatherType.晴天:
                    weatherManager.ClearWeather();
                    break;
                case WeatherType.下雨:
                    weatherManager.RainWeather();
                    break;
                case WeatherType.下雪:
                    weatherManager.SnowWeather();
                    break;
                default:
                    break;
            }
            currentWether = weather;
        }
        catch (System.Exception e)
        {
            Debug.LogError("天气转换失败：" + e.Message);
        }
    }
    #endregion


    #region Floor
    public FloorManager floorManager;
    string currentFloor = "全景";
    [EasyVGetConfig("EasyVSetFloor", "设置楼层")]
    public string GetFloor()
    {
        return ":Floor";
    }
    [EasyVSetConfig("EasyVSetFloor")]
    public void SetFloor(object data)
    {
        try
        {
            string floor = (string)data;
            if (floor[0] == ':') return;
            if (floor == currentFloor)
                return;
            Debug.Log("接受到楼层信息：" + floor);
            if (floor == "全景")
            {
                floorManager?.SwitchFloor(EnumClass.FloorState.全楼);
            }
            else if (floor == "1F")
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
    #endregion


    #region Panel
    public Junc_ButtonGroup EquupmentPanel;
    public Junc_ButtonGroup PersonPanel;
    string currentPanel = "综合态势";
    [EasyVGetConfig("EasyVSetPanel", "设置页面")]
    public string GetPanel()
    {
        return ":Panel";
    }
    [EasyVSetConfig("EasyVSetPanel")]
    public void SetPanel(object data)
    {
        string panel = (string)data;
        if (panel[0] == ':') return;
        if (panel == currentPanel)
            return;
        Debug.Log(panel);

        EquupmentPanel.gameObject.SetActive(panel == "设备管控" ? true : false);
        PersonPanel.gameObject.SetActive(panel == "人员监管" ? true : false);
        if (panel == "设备管控")
        {
            Debug.Log("设备管控");
            PersonPanel.ResetAllJuncBtns();
        }
        else if (panel == "人员监管")
        {
            Debug.Log("人员监管");
            EquupmentPanel.ResetAllJuncBtns();
        }
        currentPanel = panel;
    }
    #endregion
}
