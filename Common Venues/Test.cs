using System;
using System.Collections;
using System.Configuration;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Common_Venues
{
    public class Test : MonoBehaviour
    {
        private Tweener[] _tweeners;

        //[SerializeField] private Transform[] _waypoints;
        [SerializeField] private PointsInfo[] _waypoints;
        [SerializeField] private Transform tran;
        private Tweener _currentTweener;
        private int curIndex;
        [SerializeField] private float movespeed;
        [SerializeField] private float rotatespeed;

        [SerializeField] private Transform lookTarget;

        private void Start()
        {
            
        }

        private void Path()
        {
            Vector3[] points = new Vector3[_waypoints.Length];
            for (int i = 0; i < _waypoints.Length; i++)
            {
                points[i] = _waypoints[i].point.position;
            }

            tran.DOPath(points, 80f, PathType.Linear, PathMode.Full3D, 100, Color.cyan).OnWaypointChange(eeee);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Path();
            }
        }

        private void eeee(int b)
        {
            //lookTarget = _waypoints[b].target;
            Debug.Log(b);
        }

        private void LateUpdate()
        {
            tran.LookAt(lookTarget);
        }
    }

    [Serializable]
    public class PointsInfo
    {
        public Transform point;
        public Transform target;
    }
}