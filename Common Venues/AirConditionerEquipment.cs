using System;
using Common_Venues.UI;
using LitJson;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common_Venues
{
    public class AirConditionerEquipment : Equipment
    {
        [Range(16, 30)] [SerializeField] private float temperatureValue;

        public float TmperatureValue
        {
            get => temperatureValue;
        }

        [SerializeField] private bool state = false;
        [SerializeField] private StatusUI statusUI;
        private PopUpWindowAirConditioner _popUpWindow;
        private AirConditionerData _data;

        public void ChangeValue(float value)
        {
            temperatureValue = Mathf.Clamp(temperatureValue, 16.0f, 30.0f);
            temperatureValue = value;
        }


        protected void OnValidate()
        {
            SetParticleStatus(state);
        }

        protected override void EquipmentStart()
        {
            base.EquipmentStart();
            _popUpWindow = (PopUpWindowAirConditioner)statusUI;
            _data = new AirConditionerData();
            _data.IsOn = IsOn;
            _data.IsOnline = IsConnection;
            _data.Temperature = temperatureValue;
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
            state = true;
            SetParticleStatus(true);
        }

        private void SetParticleStatus(bool state)
        {
            ParticleSystem[] particles = transform.parent.GetComponentsInChildren<ParticleSystem>();
            foreach (var item in particles)
            {
                if (state)
                    item.Play();
                else
                    item.Stop();
            }
        }

        protected override void SwitchOffStatus()
        {
            base.SwitchOffStatus();
            state = false;
            SetParticleStatus(false);
        }

        protected override void ClickEquipment()
        {
            _popUpWindow.ReciveNormalEquipment(this,_data);
        }

        protected override void EnterEquipment()
        {
        }

        protected override void ExitEquipment()
        {
        }
    }
}