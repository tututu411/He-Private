using System;

namespace Common_Venues
{
    public class EquipmentDataClass
    {
    }

    public struct AirConditionerData
    {
        public bool IsOn;
        public bool IsOnline;
        public float Temperature;

        public override string ToString()
        {
            return $"空调开关状态：{IsOn},空调在线状态：{IsOnline},空调当前温度：{Temperature}";
        }
    }

    public struct MonitoringData
    {
        public bool IsOn;
        public bool IsOnline;
        public string MonitorUrl;
        public string TextureBase64;
        public string Position;
        public string LastCheckTime;

        public override string ToString()
        {
            return
                $"监控开关状态：{IsOn},监控在线状态：{IsOnline},监控位置：{Position},上次维护时间：{LastCheckTime},监控视频流地址：{MonitorUrl},假图片{TextureBase64}";
        }
    }

    public struct LightingData
    {
        public bool IsOn;
        public bool IsOnline;
        public float Brightness;

        public override string ToString()
        {
            return $"灯光开关状态：{IsOn},灯光在线状态：{IsOnline},灯光亮度：{Brightness}";
        }
    }
    public struct FireInductorData
    {
        public string Position;
        public string LastCheckTime;
        public override string ToString()
        {
            return $"烟感位置：{Position},烟感上次维护时间：{LastCheckTime}";
        }
    }
    public struct EntranceGuardData
    {
        public bool IsOn;
        public bool IsOnline;
        public string Position;
        public string LastCheckTime;

        public override string ToString()
        {
            return $"门禁开关状态：{IsOn},门禁在线状态：{IsOnline},门禁位置{Position},上次维护时间{LastCheckTime}";
        }
    }
}