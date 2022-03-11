using System;
using UnityEngine.Serialization;

namespace Common_Venues
{
    public class VenuesSystem : Singleton<VenuesSystem>
    {
        public bool clearLogsBeforeRunning = true;

        public CameraRotateAround cameraControl;
        internal FloorManager floorManager;

        private void Awake()
        {
            ClearSystemLog();
            cameraControl = FindObjectOfType<CameraRotateAround>();
        }

        private void ClearSystemLog()
        {
            if(clearLogsBeforeRunning)
                LogWriteRead.ClearGeneralLog();
        }

    }
}