using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Common_Venues.UI
{
    public class PopUpWindowFireInductor : StatusUI
    {
        private FireInductorEquipment currentEquipment;
        [Serializable]
        public class NormalPanel
        {
            public GameObject NormalPanelGameObject;
            public Text FireInductorName;
            public TextMeshProUGUI FireInductorID;
            public TextMeshProUGUI FireInductorOnlineStatus;
            public TextMeshProUGUI Position;
            public TextMeshProUGUI Time;
        }



        public NormalPanel normalPanel;

        public ErrorPanel errorPanel;
        [Serializable]
        public class ErrorPanel
        {
            public GameObject ErrorPanelGameObject;
            public Text FireInductorName;
            public TextMeshProUGUI FireInductorID;
            public TextMeshProUGUI FireInductorOnLineStatus;
            public TextMeshProUGUI ErrorInfoMessage;
            public TextMeshProUGUI MaintenancePerson;
            public TextMeshProUGUI MaintenanceStatus;
            public TextMeshProUGUI ErrorTime;
            public Button ErrorSendButton;
        }
        public override void ShowEquipmentStatus(string value)
        {
            FireInductorData data = JsonConvert.DeserializeObject<FireInductorData>(value);
            Debug.Log(data);
        }
        public void ReciveNormalEquipment(Equipment equipment, FireInductorData data)
        {
            currentEquipment = (FireInductorEquipment)equipment;
            normalPanel.FireInductorID.text = currentEquipment.EID;
            normalPanel.FireInductorOnlineStatus.text = currentEquipment.IsConnection ? "<color=green>在线" : "<color=green>离线";
            normalPanel.Position.text = $"设备位置：<color=#31cffc>{currentEquipment.EPosition}";
            normalPanel.Time.text = $"上次维护时间：<color=#31cffc>{data.LastCheckTime}";
            normalPanel.NormalPanelGameObject.transform.position = currentEquipment.transform.position + new Vector3(0, 2f, -02f);
            normalPanel.NormalPanelGameObject.SetActive(true);
        }

        public override void ClearWindow()
        {
            normalPanel.NormalPanelGameObject.SetActive(false);
            errorPanel.ErrorPanelGameObject.SetActive(false);
        }
    }
}