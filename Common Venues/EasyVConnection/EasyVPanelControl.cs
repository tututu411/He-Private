using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyV;

public class EasyVPanelControl : SetConfigComponentBase
{
    public override void UpdateSetting()
    {
        
    }
    [EasyVGetConfig("SetPanelEasy", "设置底部Panel")]
    public string GetPanel()
    {
        return ":Panel";
    }
    [EasyVSetConfig("SetPanelEasy")]
    public void SetPanel(object data)
    {
        string panel = (string)data;
        Debug.Log(panel);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
