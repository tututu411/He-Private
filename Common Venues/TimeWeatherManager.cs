using DG.Tweening;
using GlobalSnowEffect;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AzureSky;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Common_Venues
{
    public class TimeWeatherManager : MonoBehaviour
    {
        [SerializeField]
        private AzureTimeController TimeController;

        [SerializeField]
        private float currentTime;

        public float CurrentTimer { get => currentTime; }

        public GameObject WeatherLocalTrigger;

        public Transform SnowTrigger;
        public Transform RainTrigger;
        public Vector3 ClearTrigger;
        //日间触发时间，早上06.00
        public const float morningTime = 6f;
        //夜间触发时间，下午17.30
        public const float eveningTime = 17.0f;

        //时间状态 0为夜晚，1为白天
        public int TimeState = 0;

        public enum TimeType
        {
            白天, 夜晚
        }
        public enum WeatherType
        {
            晴天, 下雨, 下雪
        }

        [SerializeField]
        private GameObject NightLights;

        [SerializeField]
        private GameObject NightEffects;

        [SerializeField]
        private Material LuDeng;
        [SerializeField]
        private Material FloorLine;
        [SerializeField]
        private Material LuDengFront;

        #region UI部分属性
        public Junc_Button ri;
        public Junc_Button ye;
        
        [SerializeField]
        private bool isSystem = true;

        #endregion

        void Awake()
        {




        }
        private void Start()
        {
            ClearTrigger = WeatherLocalTrigger.transform.position;
            //计算当前时间在一天中的比例
            float tempTime = (System.DateTime.Now.Hour + System.DateTime.Now.Minute / 60f);
            currentTime = tempTime;
            TimeState = (tempTime > morningTime && tempTime < eveningTime) ? 1 : 0;
            DayNightChanged(TimeState == 0 ? TimeType.夜晚 : TimeType.白天);
            //ClearWeather();
        }

        private void Update()
        {
            //根据当前时间值判断日夜状态切换
            if (currentTime > morningTime && currentTime < eveningTime && TimeState == 0)
            {
                DayNightChanged(TimeType.白天);
                TimeState = 1;
                
            }
            else if ((currentTime > eveningTime || currentTime < morningTime) && TimeState == 1)
            {
                DayNightChanged(TimeType.夜晚);
                TimeState = 0;
               
            }
        }
        private void LateUpdate()
        {
            if (isSystem)
            {
                float tempTime = (System.DateTime.Now.Hour + System.DateTime.Now.Minute / 60f);
                currentTime = tempTime;
                TimeController.timeline = tempTime;
            }
        }
        private void DayNightChanged(TimeType type)
        {
            NightLights?.SetActive(type == TimeType.夜晚 ? true : false);
            NightEffects?.SetActive(type == TimeType.夜晚 ? true : false);

            if (type == TimeType.夜晚)
            {
                LuDeng?.EnableKeyword("_EMISSION");
                LuDengFront?.EnableKeyword("_EMISSION");
                FloorLine?.SetFloat("_LineSwitch", 1);
            }
            else
            {
                LuDeng?.DisableKeyword("_EMISSION");
                LuDengFront?.DisableKeyword("_EMISSION");
                FloorLine?.SetFloat("_LineSwitch", 0);
            }
            Shader.SetGlobalFloat("Night_TouMing", type == TimeType.夜晚 ? 1 : 0);


            return;
            if (type == TimeType.白天)
            {
                ri.SetSelected(true);
                ri.GetComponentInChildren<TimeWeatherTab>().SetState(true);
                ye.SetSelected(false);
                ye.GetComponentInChildren<TimeWeatherTab>().SetState(false);
            }
            else
            {
                ri.SetSelected(false);
                ri.GetComponentInChildren<TimeWeatherTab>().SetState(false);
                ye.SetSelected(true);
                ye.GetComponentInChildren<TimeWeatherTab>().SetState(true);
            }
        }


        internal void TimeChanged(float timer)
        {
            isSystem = false;
            if (timer < 0f)
                timer = 0f;
            currentTime = timer;
            TimeController.timeline = timer;
            StopAllCoroutines();
            StartCoroutine(WaitTimeToSystemTime());
        }
        #region UI部分控制函数

        private void InitUI()
        {

        }

        private void Junc_BtnClick(Junc_WeatherButton item)
        {
            StopAllCoroutines();
            StartCoroutine(WaitTimeToSystemTime());
        }

        WaitForSeconds waitSecond = new WaitForSeconds(60f);
        private IEnumerator WaitTimeToSystemTime()
        {
            yield return waitSecond;
            isSystem = true;
        }

        #endregion
        private void FixedUpdate()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SnowWeather();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                RainWeather();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ClearWeather();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                TimeToDay();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                TimeToNight();
            }
#endif
        }
        [SerializeField]
        private WeatherType currentID;
        [ContextMenu("snow", false, -1)]
        public void SnowWeather()
        {
            WeatherType weatherId = (WeatherType)2;
            if (weatherId == currentID) return;
            WeatherLocalTrigger.transform.DOMove(SnowTrigger.position, 2f);
            Camera.main.GetComponent<GlobalSnow>().enabled = true;
            currentID = (WeatherType)2;
        }
        [ContextMenu("rain", false, -1)]
        public void RainWeather()
        {
            WeatherType weatherId = (WeatherType)1;
            if (weatherId == currentID) return;
            WeatherLocalTrigger.transform.DOMove(RainTrigger.position, 2f);
            Camera.main.GetComponent<GlobalSnow>().enabled = false;
            Camera.main.GetComponent<GlobalSnow>().ClearSnow();
            currentID = (WeatherType)1;
        }
        [ContextMenu("clear", false, -1)]
        public void ClearWeather()
        {
            WeatherType weatherId = (WeatherType)0;
            if (weatherId == currentID) return;
            WeatherLocalTrigger.transform.DOMove(ClearTrigger, 2f);
            Camera.main.GetComponent<GlobalSnow>().enabled = false;
            Camera.main.GetComponent<GlobalSnow>().ClearSnow();
            currentID = 0;
        }
        [ContextMenu("Day", false, -1)]
        public void TimeToDay()
        {
            //ClearWeather();
            float cur = currentTime;
            float tar = 11;
            DOTween.To(() => cur, (value) =>
            {
                TimeChanged(value);
            }, tar, 3f).SetEase(Ease.Linear).SetAutoKill(false).SetTarget(this);
        }
        [ContextMenu("night", false, -1)]
        public void TimeToNight()
        {
            //ClearWeather();
            float cur = currentTime;
            float tar = 18;
            DOTween.To(() => cur, (value) =>
            {
                TimeChanged(value);
            }, tar, 3f).SetEase(Ease.Linear).SetAutoKill(false).SetTarget(this);
        }
    }
}