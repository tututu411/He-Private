using System;
using DG.Tweening;
using UnityEngine;

namespace Common_Venues
{
    public class AutomaticRoaming : MonoBehaviour
    {
        public Transform[] roamingTransforms;
        private Vector3[] _roamingPoints;
        private Tweener _dotweenPathTweener;
        private float _time;
        private void Start()
        {
            SetRoamingTransforms(roamingTransforms);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseRoaming();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                ContinueRoaming();
            }
        }

        private void SetRoamingTransforms(Transform[] trans)
        {
            roamingTransforms = trans;
            InitRoamingPoint();
        }

        private void InitRoamingPoint()
        {
            _roamingPoints = new Vector3[roamingTransforms.Length];
            for (var i = 0; i < roamingTransforms.Length; i++)
            {
                _roamingPoints[i] = roamingTransforms[i].position;
            }

            StartRoaming();
        }

        public void PauseRoaming()
        {
            _dotweenPathTweener.Pause();
        }

        public void ContinueRoaming()
        {
            _dotweenPathTweener.Play();
        }

        private void StartRoaming()
        {
            _dotweenPathTweener = transform.DOPath(_roamingPoints, 5f, PathType.CatmullRom, PathMode.Full3D, 10, Color.green)
                .OnWaypointChange(p => { Move(_roamingPoints); });
            _dotweenPathTweener.SetLoops(-1);
            _dotweenPathTweener.OnComplete(() => { _pathIndex = 0; });
        }

        private int _pathIndex = 0;

        private void Move(Vector3[] path)
        {
            _pathIndex += 1;
            transform.DOLookAt(path[_pathIndex], 2f);
        }
    }
}