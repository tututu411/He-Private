using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common_Venues.UI
{
    public class PopWindowLighting : StatusUI
    {
        public TextMeshProUGUI stateText;
        public LightingEquipment currentEquipment;
        public Image FillAmountImage;
        public SpriteGroup ArrowSpriteGroup;
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
            FillAmountImage.fillAmount = v;
        }
        private void OnDisable()
        {
            normalPanel.LightSlider.onValueChanged.RemoveAllListeners();
            normalPanel.openButton.SelectedButtonAction -= JuncButtonSelectedAction;
            normalPanel.closeButton.SelectedButtonAction -= JuncButtonUnSelectedAction;
        }

        public override void ShowEquipmentStatus(string value)
        {
            LightingData data = JsonConvert.DeserializeObject<LightingData>(value);
            Debug.Log(data);
        }

        public void ReciveNormalEquipment(Equipment equipment, ref LightingData data)
        {
            currentEquipment = (LightingEquipment)equipment;
            normalPanel.NormalPanelGameObject.transform.position = currentEquipment.transform.position + new Vector3(0, 2f, -02f);
            normalPanel.NormalPanelGameObject.SetActive(true);
            normalPanel.LightSlider.value = data.Brightness;
            normalPanel.openButton.Selected = data.IsOn;
            normalPanel.closeButton.Selected = !data.IsOn;
            normalPanel.LightSlider.onValueChanged.AddListener(currentEquipment.ChangeValue);
            normalPanel.openButton.SelectedButtonAction += JuncButtonSelectedAction;
            normalPanel.closeButton.SelectedButtonAction += JuncButtonUnSelectedAction;
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