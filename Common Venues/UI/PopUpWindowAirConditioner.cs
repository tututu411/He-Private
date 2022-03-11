using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Common_Venues.UI
{
    public class PopUpWindowAirConditioner : StatusUI
    {
        public Image FillAmountImage;
        public SpriteGroup ArrowSpriteGroup;
        public TextMeshProUGUI stateText;
        public AirConditionerEquipment currentEquipment;
        [Serializable]
        public class NormalPanel
        {
            public GameObject NormalPanelGameObject;
            public Text LightName;
            public TextMeshProUGUI LightID;
            public TextMeshProUGUI LightOnlineStatus;
            public Image ArrowImage;
            public TextMeshProUGUI Power;
            public TextMeshProUGUI Position;
            public TextMeshProUGUI Temperature;
            public TextMeshProUGUI Time;
            public Slider LightSlider;
            public Image FillImage;
            public Button addButton;
            public Button subButton;
            public Junc_Button openButton;
            public Junc_Button closeButton;
        }
        public NormalPanel normalPanel;

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

        private void OnEnable()
        {
            normalPanel.LightSlider.onValueChanged.AddListener(ImageFillAmount);
        }
        private void ImageFillAmount(float v)
        {
            normalPanel.FillImage.fillAmount = v;
        }
        private void OnDisable()
        {
            normalPanel.LightSlider.onValueChanged.RemoveAllListeners();
            normalPanel.openButton.SelectedButtonAction -= JuncButtonSelectedAction;
            normalPanel.closeButton.SelectedButtonAction -= JuncButtonUnSelectedAction;
        }

        public override void ShowEquipmentStatus(string value)
        {
            AirConditionerData data = JsonConvert.DeserializeObject<AirConditionerData>(value);
            Debug.Log(data);
        }

        public void ReciveNormalEquipment(Equipment equipment, AirConditionerData data)
        {
            currentEquipment = (AirConditionerEquipment)equipment;
            normalPanel.LightSlider.value = currentEquipment.TmperatureValue;
            normalPanel.NormalPanelGameObject.transform.position = currentEquipment.transform.position + new Vector3(0, 2f, -02f);
            normalPanel.NormalPanelGameObject.SetActive(true);
            normalPanel.LightSlider.onValueChanged.AddListener(currentEquipment.ChangeValue);
            normalPanel.openButton.SelectedButtonAction += JuncButtonSelectedAction;
            normalPanel.closeButton.SelectedButtonAction += JuncButtonUnSelectedAction;

            normalPanel.LightID.text = currentEquipment.EID;
            normalPanel.LightOnlineStatus.text = currentEquipment.IsConnection ? $"<color=green>在线" : "<color=red>离线";
            normalPanel.Position.text = $"设备位置：<color=#31cffc>{ currentEquipment.EPosition}";
            normalPanel.Temperature.text =$"设备当前温度：<color=#31cffc>{ currentEquipment.TmperatureValue}";
        }

        public override void ClearWindow()
        {
            normalPanel.NormalPanelGameObject.SetActive(false);
            errorPanel.ErrorPanelGameObject.SetActive(false);
        }

        private void JuncButtonSelectedAction()
        {
            currentEquipment.IsOn = true;
            if (stateText != null)
                stateText.text = "开启";
        }
        private void JuncButtonUnSelectedAction()
        {
            currentEquipment.IsOn = false;
            if (stateText != null)
                stateText.text = "关闭";
        }
        public void AddValue(float step)
        {
            normalPanel.LightSlider.value += step;
        }
    }
}