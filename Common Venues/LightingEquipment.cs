using Common_Venues.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common_Venues
{
    public class LightingEquipment : Equipment
    {
        [Range(0, 1)] [SerializeField] private float lightValue;

        public float LightValue
        {
            get => lightValue;
        }

        [SerializeField] private bool isOn = false;
        [SerializeField] private StatusUI statusUI;
        private PopWindowLighting _popUpWindow;
        [SerializeField] private Material airMaterial;
        LightingData _data;
        public void ChangeValue(float value)
        {
            lightValue = Mathf.Clamp(lightValue, 0.04f, 1.0f);
            lightValue = value;
            airMaterial.SetFloat("_LiangDu", lightValue);
        }

        protected override void EquipmentStart()
        {
            base.EquipmentStart();
            _popUpWindow = (PopWindowLighting)statusUI;
            airMaterial = GetComponent<MeshRenderer>().materials[0];
            _data.IsOn = IsOn;
            _data.IsOnline = IsConnection;
            _data.Brightness = lightValue;
        }

        protected override void SwitchNormalStatus()
        {
            base.SwitchNormalStatus();
            airMaterial.EnableKeyword("_ZHENG_GU_ON");
            airMaterial.DisableKeyword("_ZHENG_GU_OFF");
            airMaterial.DisableKeyword("_ZHENG_GU_GUZHNAG");
        }

        protected override void SwitchErrorStatus()
        {
            base.SwitchErrorStatus();
            airMaterial.DisableKeyword("_ZHENG_GU_ON");
            airMaterial.DisableKeyword("_ZHENG_GU_OFF");
            airMaterial.EnableKeyword("_ZHENG_GU_GUZHNAG");
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
            airMaterial.EnableKeyword("_ZHENG_GU_ON");
            airMaterial.DisableKeyword("_ZHENG_GU_OFF");
            airMaterial.DisableKeyword("_ZHENG_GU_GUZHNAG");
        }

        protected override void SwitchOffStatus()
        {
            base.SwitchOffStatus();
            isOn = false;
            airMaterial.DisableKeyword("_ZHENG_GU_ON");
            airMaterial.EnableKeyword("_ZHENG_GU_OFF");
            airMaterial.DisableKeyword("_ZHENG_GU_GUZHNAG");
        }
        protected override void ClickEquipment()
        {
            _popUpWindow.ReciveNormalEquipment(this,ref _data);
            VenuesSystem.Instance.cameraControl.SetTarget(transform, 5f, null);
        }

    }
}