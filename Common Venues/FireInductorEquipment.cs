using UnityEngine;
using Common_Venues.UI;
using System;

namespace Common_Venues
{
    public class FireInductorEquipment : Equipment
    {
        public StatusUI statusUI;
        private PopUpWindowFireInductor _popUpWindow;
        private FireInductorData _data;

        protected override void EquipmentStart()
        {
            base.EquipmentStart();
            if (statusUI != null)
                _popUpWindow = (PopUpWindowFireInductor)statusUI;
            _data = new FireInductorData();
            _data.Position = EPosition;
            _data.LastCheckTime = DateTime.Now.ToString("u");
        }

        protected override void SwitchErrorStatus()
        {
            base.SwitchErrorStatus();
        }
        protected override void ClickEquipment()
        {
            base.ClickEquipment();
            _popUpWindow.ReciveNormalEquipment(this,_data);
            VenuesSystem.Instance.cameraControl.SetTarget(transform, 5f, null);

        }
        private enum EffectType
        {
            Spray, Fire, SmokeFog, Temperature, AreaRange
        }
    }
}