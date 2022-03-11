using UnityEngine;

namespace Common_Venues
{
    public class EmergencyPlan : MonoBehaviour
    {
        public PlanType planType;

        
        public enum PlanType
        {
            火灾应急,
            踩踏事件应急
        }
    }
}