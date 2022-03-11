using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Common_Venues.UI
{
    public class PopWindowMonitor : StatusUI
    {
        public Junc_Button button;
        public RawImage monitorImage;
        public MonitoringEquipment currentEquipment;
        public TextMeshProUGUI stateText;
        private string[] spritePaths = new string[] { "monitor1", "monitor2", "monitor3" };

        [Serializable]
        public class NormalPanel
        {
            public GameObject PanelGameobject;
            public Text MonitorName;
            public TextMeshProUGUI MonitorId;
            public RawImage MonitorRawImage;
        }

        public NormalPanel normalPanel;

        [Serializable]
        public class ErrorPanel
        {
            public GameObject PanelGameObject;
            public Text MonitorName;
            public TextMeshProUGUI MonitorId;
            public TextMeshProUGUI ErrorInfo;
            public TextMeshProUGUI ErrorPeople;
            public TextMeshProUGUI ErrorStatus;
            public TextMeshProUGUI ErrorTime;
            public Button ErrorSendButton;
        }

        public ErrorPanel errorPanel;
        [Serializable]
        public class AlarmPanel
        {
            public GameObject PanelGameObject;
            public Text MonitorName;
            public TextMeshProUGUI MonitorId;
            public TextMeshProUGUI AlarmInfo;
            public TextMeshProUGUI AlarmPos;
            public TextMeshProUGUI ErrorStatus;
            public TextMeshProUGUI ErrorTime;
            public Button ErrorSendButton;
        }
        public AlarmPanel alarmPanel;

        private void Start()
        {

        }

        private void OnEnable()
        {
            int random = Random.Range(0, spritePaths.Length);
            normalPanel.MonitorRawImage.texture = Resources.Load<Texture2D>("MonitorSprites/" + spritePaths[random]);
        }

        private void OnDisable()
        {
        }

        public override void ShowEquipmentStatus(string value)
        {
            MonitoringData data = JsonConvert.DeserializeObject<MonitoringData>(value);
            Debug.Log(data);
            if (stateText != null)
                stateText.text = data.IsOnline ? "在线" : "离线";
            // byte[] bytes = Convert.FromBase64String(data.TextureBase64);
            // Texture2D texture = new Texture2D(100, 100);
            // texture.LoadImage(bytes);
            // Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        public void ReciveNormalEquipment(Equipment equipment)
        {
            currentEquipment = (MonitoringEquipment)equipment;
            normalPanel.PanelGameobject.transform.position = currentEquipment.transform.position + new Vector3(0, 2f, -02f);
            normalPanel.PanelGameobject.SetActive(true);
            //normalPanel.MonitorName.text = currentEquipment.EName;
            normalPanel.MonitorId.text = currentEquipment.EID;
        }

        public override void ClearWindow()
        {
            normalPanel.PanelGameobject.SetActive(false);
            errorPanel.PanelGameObject.SetActive(false);
            alarmPanel.PanelGameObject.SetActive(false);
        }
    }
}