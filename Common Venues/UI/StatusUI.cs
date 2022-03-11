using LitJson;
using System;
using UnityEngine;

namespace Common_Venues.UI
{
    public abstract class StatusUI : MonoBehaviour
    {
        public abstract void ShowEquipmentStatus(string value);

        public virtual void ClearWindow() { }
    }
}