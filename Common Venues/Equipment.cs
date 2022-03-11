using System;
using UnityEngine;


namespace Common_Venues
{
    public class Equipment : MonoBehaviour
    {
        [Tooltip("设备ID")] [SerializeField] private string equipmentID;

        [Tooltip("设备名称")] [SerializeField] protected string equipmentName = "测试设备";

        [Tooltip("设备位置")] [SerializeField] protected string equipmentPosition;

        #region 设备状态信息

        [Tooltip("设备运行状态")] [SerializeField] protected EquipmentEnum.EquipmentOperatingStatus equipmentOperatingStatus;

        [Tooltip("设备连接状态")]
        [SerializeField]
        protected EquipmentEnum.EquipmentConnectionStatus equipmentConnectionStatus;

        [Tooltip("设备开启状态")] [SerializeField] protected EquipmentEnum.EquipmentOnStatus equipmentOnStatus;

        #endregion

        #region 状态变化委托事件

        private StatusWasChangedDelegate _operatingStatusWasChangedDelegate;
        private StatusWasChangedDelegate _connectionStatusWasChangedDelegate;
        private StatusWasChangedDelegate _onStatusWasChangedDelegate;

        #endregion

        public string EName
        {
            get => equipmentName;
            set => equipmentName = value;
        }

        public string EPosition
        {
            get => equipmentPosition;
            set => equipmentPosition = value;
        }

        public string EID { get => equipmentID; set => equipmentID = value; }
        public bool IsOperating => equipmentOperatingStatus == EquipmentEnum.EquipmentOperatingStatus.正常;

        public bool IsConnection => equipmentConnectionStatus == EquipmentEnum.EquipmentConnectionStatus.在线;

        public bool IsOn
        {
            get => equipmentOnStatus == EquipmentEnum.EquipmentOnStatus.开启;
            set => equipmentOnStatus = value == true ? EquipmentEnum.EquipmentOnStatus.开启 : EquipmentEnum.EquipmentOnStatus.关闭;
        }

        private EquipmentEnum.EquipmentOperatingStatus _currentOperatingStatus;
        private EquipmentEnum.EquipmentConnectionStatus _currenConnectionStatus;
        private EquipmentEnum.EquipmentOnStatus _currentOnStatus;

        private void Start()
        {
            //equipmentID = gameObject.GetInstanceID();
            InitEquipment();
            EquipmentStart();
        }

        protected virtual void EquipmentStart()
        {
        }

        private void Update()
        {
            if (_currentOperatingStatus != equipmentOperatingStatus)
                _operatingStatusWasChangedDelegate?.Invoke();
            if (_currenConnectionStatus != equipmentConnectionStatus)
                _connectionStatusWasChangedDelegate?.Invoke();
            if (_currentOnStatus != equipmentOnStatus)
                _onStatusWasChangedDelegate?.Invoke();
        }

        private void OnMouseEnter()
        {
            EnterEquipment();
        }

        private void OnMouseExit()
        {
            ExitEquipment();
        }

        private void OnMouseUpAsButton()
        {
            ClickEquipment();
        }

        protected virtual void ClickEquipment()
        {
            
        }

        protected virtual void EnterEquipment()
        {

        }

        protected virtual void ExitEquipment()
        {

        }

        /// <summary>
        /// 初始化设备
        /// </summary>
        protected virtual void InitEquipment()
        {
            _currentOperatingStatus = equipmentOperatingStatus;
            _currenConnectionStatus = equipmentConnectionStatus;
            _currentOnStatus = equipmentOnStatus;
            _operatingStatusWasChangedDelegate += OnOperatingStatusChanged;
            _connectionStatusWasChangedDelegate += OnConnectedStatusChanged;
            _onStatusWasChangedDelegate += OnStatusChanged;
            WriteLog(EquipmentEnum.EquimentMessageType.Message, "设备开启");
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        protected virtual void OpenEquipment()
        {
            equipmentOnStatus = EquipmentEnum.EquipmentOnStatus.开启;
            WriteLog(EquipmentEnum.EquimentMessageType.Message, "设备开启");
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        protected virtual void CloseEquipment()
        {
            equipmentOnStatus = EquipmentEnum.EquipmentOnStatus.关闭;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        private void WriteLog(EquipmentEnum.EquimentMessageType messageType, string log)
        {
            LogWriteRead.WriteLog($"（{messageType.ToString()}）设备名称：{equipmentName}---日志信息：{log}");
        }

        /// <summary>
        /// 发送LogMessage
        /// </summary>
        /// <param name="messageType">消息类型</param>
        protected virtual void SendMessage(EquipmentEnum.EquimentMessageType messageType)
        {
        }

        private void OnOperatingStatusChanged()
        {
            Debug.Log($"设备运行状态发生变化\r设备ID{equipmentID}\r当前设备运行状态:{equipmentOperatingStatus}");
            WriteLog(EquipmentEnum.EquimentMessageType.Message,
                $"设备运行状态发生变化---设备ID{equipmentID}---当前设备运行状态:{equipmentOperatingStatus}");
            switch (equipmentOperatingStatus)
            {
                case EquipmentEnum.EquipmentOperatingStatus.故障:
                    SwitchErrorStatus();
                    break;
                case EquipmentEnum.EquipmentOperatingStatus.正常:
                    SwitchNormalStatus();
                    break;
            }

            _currentOperatingStatus = equipmentOperatingStatus;
        }

        protected virtual void SwitchNormalStatus()
        {
        }

        protected virtual void SwitchErrorStatus()
        {
        }

        private void OnConnectedStatusChanged()
        {
            Debug.Log($"设备连接状态发生变化\r设备ID{equipmentID}\r当前设备连接状态:{equipmentConnectionStatus}");
            WriteLog(EquipmentEnum.EquimentMessageType.Message,
                $"设备连接状态发生变化---设备ID{equipmentID}---当前设备连接状态:{equipmentConnectionStatus}");
            switch (equipmentConnectionStatus)
            {
                case EquipmentEnum.EquipmentConnectionStatus.离线:
                    SwitchDisConnectedStatus();
                    break;
                case EquipmentEnum.EquipmentConnectionStatus.在线:
                    SwitchConnectedStatus();
                    break;
            }

            _currenConnectionStatus = equipmentConnectionStatus;
        }

        protected virtual void SwitchConnectedStatus()
        {
        }

        protected virtual void SwitchDisConnectedStatus()
        {
        }

        private void OnStatusChanged()
        {
            Debug.Log($"设备开启状态发生变化\r设备ID{equipmentID}\r当前设备开启状态:{equipmentOnStatus}");
            WriteLog(EquipmentEnum.EquimentMessageType.Message,
                $"设备开启状态发生变化---设备ID{equipmentID}---当前设备开启状态:{equipmentOnStatus}");
            switch (equipmentOnStatus)
            {
                case EquipmentEnum.EquipmentOnStatus.关闭:
                    SwitchOffStatus();
                    break;
                case EquipmentEnum.EquipmentOnStatus.开启:
                    SwitchOnStatus();
                    break;
            }

            _currentOnStatus = equipmentOnStatus;
        }

        protected virtual void SwitchOnStatus()
        {
        }

        protected virtual void SwitchOffStatus()
        {
        }
    }
}