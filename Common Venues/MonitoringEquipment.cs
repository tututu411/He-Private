using System;
using Common_Venues.UI;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common_Venues
{
    [DisallowMultipleComponent]
    public class MonitoringEquipment : Equipment
    {
        [SerializeField] private bool isOn = false;
        [SerializeField] private StatusUI statusUI;
        private PopWindowMonitor _popUpWindow;
        public string moitorUrl = "http://cctvalih5ca.v.myalicdn.com/live/cctvchild_2/index.m3u8";
        private MonitoringData _data;

        protected override void EquipmentStart()
        {
            base.EquipmentStart();
            _popUpWindow = (PopWindowMonitor)statusUI;
            _data = new MonitoringData();
            _data.IsOn = IsOn;
            _data.IsOnline = IsConnection;
            _data.Position = equipmentPosition;
            _data.LastCheckTime = DateTime.Now.ToString("u");
        }

        protected override void SwitchNormalStatus()
        {
            base.SwitchNormalStatus();

        }

        protected override void SwitchErrorStatus()
        {
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
            isOn = true;
        }

        protected override void SwitchOffStatus()
        {
            base.SwitchOffStatus();
            isOn = false;
        }
        protected override void ClickEquipment()
        {
            _popUpWindow.ReciveNormalEquipment(this);
            string json = JsonConvert.SerializeObject(_data);
            _popUpWindow.ShowEquipmentStatus(json);
            VenuesSystem.Instance.cameraControl.SetTarget(transform, 5f, null);
        }


        private enum ErrorType
        {
            非法闯入,
            人流拥挤,
            寻衅滋事,
            设备故障
        }
    }
}