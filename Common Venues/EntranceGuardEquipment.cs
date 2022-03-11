using System;
using Common_Venues.UI;
using Newtonsoft.Json;
using UnityEngine;

namespace Common_Venues
{
    /// <summary>
    /// 门禁设备
    /// </summary>
    public class EntranceGuardEquipment : Equipment
    {
        [SerializeField] private string AlarmType;

        [SerializeField] private StatusUI statusUI;
        private PopWindowEntranceGuard _popUpWindow;
        private EntranceGuardData _data;
        protected override void EquipmentStart()
        {
            base.EquipmentStart();
            _popUpWindow = (PopWindowEntranceGuard) statusUI;
            _data = new EntranceGuardData();
            _data.IsOn = IsOn;
            _data.IsOnline = IsConnection;
            _data.Position = equipmentPosition;
            _data.LastCheckTime=DateTime.Now.ToString("u");
        }

        protected override void SwitchNormalStatus()
        {
            Debug.Log("正常");
            base.SwitchNormalStatus();
        }

        protected override void SwitchErrorStatus()
        {
            Debug.Log("报警");
            base.SwitchErrorStatus();
        }

        protected override void SwitchConnectedStatus()
        {
            base.SwitchConnectedStatus();
        }

        protected override void SwitchDisConnectedStatus()
        {
            base.SwitchDisConnectedStatus();
        }

        protected override void SwitchOnStatus()
        {
            base.SwitchOnStatus();
        }

        protected override void SwitchOffStatus()
        {
            base.SwitchOffStatus();
        }

        protected override void ClickEquipment()
        {
            base.ClickEquipment();
            _popUpWindow.ReciveNormalEquipment(this);
            string json = JsonConvert.SerializeObject(_data);
            _popUpWindow.ShowEquipmentStatus(json);
        }

        private struct ErrorInformation
        {
            internal string Message;
            internal string Reportmessage;
            internal string ReportPersonName;
            internal string RepairState;
        }

        private struct IllegalInformation
        {
            internal string message;
            internal Texture2D MonitorPhoto;
        }
    }
}