using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birdflycontroller : MonoBehaviour
{
    public Transform[] Waypoints;
    private int lastindx = -1;
    public int currentIndex = 0;
    private Transform m_currentNode;
    public float speed = 1;
    public float rotateSpeed = 20;
    public bool canmove = true;
    public Transform lookat;
    int GetPointIndex()
    {
        int ran = Random.Range(0, Waypoints.Length);
        if (ran == lastindx)
        {
            return GetPointIndex();
        }
        else
        {
            lastindx = ran;
            return ran;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetData(Waypoints);
    }

    public void SetData(Transform[] wap)
    {
        Waypoints = wap;
        int idx = GetPointIndex();
        m_currentNode = Waypoints[currentIndex];
    }

    public void SetTarget(int idx)
    {
        m_currentNode = Waypoints[idx];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!canmove)
            return;
        if (m_currentNode != null)
        {
            RotateTo();
            MoveTo();
        }
    }

    public void RotateTo()
    {
        float current = transform.eulerAngles.y;
        // this.transform.LookAt(lookat);
        this.transform.LookAt(m_currentNode.transform);
        Vector3 target = this.transform.eulerAngles;
        float next = Mathf.LerpAngle(current, target.y, rotateSpeed * Time.deltaTime);
        
        this.transform.eulerAngles = new Vector3(target.x, next, target.z);
        
    }

    public void MoveTo()
    {
        Vector3 post1 = this.transform.position;
        Vector3 post2 = m_currentNode.transform.position;
        float dist = Vector2.Distance(new Vector2(post1.x, post1.z), new Vector2(post2.x, post2.z));
        if (dist < 1.0f)
        {
            if (currentIndex < Waypoints.Length - 1)
                currentIndex++;
            else
            {
                currentIndex = 0;
            }

            m_currentNode = Waypoints[currentIndex];
        }

        this.transform.position = Vector3.MoveTowards(this.transform.position, m_currentNode.transform.position,
            speed * Time.deltaTime);
        //this.transform.position += transform.forward * speed * Time.deltaTime;
    }
}