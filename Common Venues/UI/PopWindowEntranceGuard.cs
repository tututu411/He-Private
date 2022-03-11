using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Common_Venues.UI
{
    public class PopWindowEntranceGuard : StatusUI
    {
        public Button button;
        public TextMeshProUGUI stateText;
        public EntranceGuardEquipment currentEquipment;
        public NormalPanel normalPanel;
        
        [Serializable]
        public class NormalPanel
        {
            public GameObject NormalPanelGameObject;
            public Text EntranceName;
            public TextMeshProUGUI EntranceID;
            public TextMeshProUGUI EntranceOnLineStatus;
            public TextMeshProUGUI EnterPeopleNumber;
            public TextMeshProUGUI ExitPeopleNumber;
            public TextMeshProUGUI MaintenancePeople;
            public TextMeshProUGUI MaintenanceTime;
            public TextMeshProUGUI EquipmentPos;
        }

        public ErrorPanel errorPanel;
        [Serializable]
        public class ErrorPanel
        {
            public GameObject ErrorPanelGameObject;
            public Text EntranceName;
            public TextMeshProUGUI EntranceID;
            public TextMeshProUGUI EntranceOnLineStatus;
            public TextMeshProUGUI ErrorInfoMessage;
            public TextMeshProUGUI MaintenancePerson;
            public TextMeshProUGUI MaintenanceStatus;
            public TextMeshProUGUI ErrorTime;
            public Button ErrorSendButton;
        }
        public override void ShowEquipmentStatus(string value)
        {
            EntranceGuardData data = JsonConvert.DeserializeObject<EntranceGuardData>(value);
            Debug.Log(data);
        }

        public void ReciveNormalEquipment(Equipment equipment)
        {
            currentEquipment = (EntranceGuardEquipment) equipment;
        }

        public override void ClearWindow()
        {
            normalPanel.NormalPanelGameObject.SetActive(false);
            errorPanel.ErrorPanelGameObject.SetActive(false);
        }
    }
}