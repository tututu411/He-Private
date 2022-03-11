using Common_Venues;
using EasyV;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Common_Venues.TimeWeatherManager;

public class EasyVWeatherControl : SetConfigComponentBase
{
    public TimeWeatherManager weatherManager;
    public override void UpdateSetting()
    {
        Debug.Log("天气刷新完成");
    }
    string currentWether = "晴天";
    [EasyVSetConfig("SetWeatherEasy")]
    public void SetWeather(object data)
    {
        try
        {
            string weather = (string)data;
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
    [EasyVGetConfig("SetWeatherEasy", "修改天气")]
    private string GetWeatherEasy()
    {
        return ":Weather";
    }
}
