namespace Common_Venues
{
    public static class EquipmentEnum
    {
        public enum EquipmentOperatingStatus
        {
            正常,
            故障
        }

        public enum EquipmentConnectionStatus
        {
            在线,
            离线
        }

        public enum EquipmentOnStatus
        {
            开启,
            关闭
        }
        
        public enum EquimentMessageType
        {
            Message,Warning,Error
        }
    }
}