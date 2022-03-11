using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 本脚本挂到摄像机上
/// </summary>
public class WebSocketMessageCenter : MonoBehaviour
{
    public bool Connect = false;
    public string url = "ws://localhost:8888";
    public string testMes;
    WebSocketVuene web;

    private void Start()
    {
        if (Connect)
            StartCoroutine(LoadServerAddres());

    }
    IEnumerator LoadServerAddres()
    {
        UnityWebRequest request = UnityWebRequest.Get(Application.streamingAssetsPath + "/ServerTxt/ServerIPAddres.txt");
        yield return request.SendWebRequest();
        url = request.downloadHandler.text;
        web = new WebSocketVuene(url);
        web.messageHandle = this;
        web.ConnectAuthReceive();
    }
    internal void ReciveMessage(string mes)
    {
        Debug.Log($"网络消息中心接收到消息:{mes}");
    }

    private void OnDestroy()
    {
        web?.Close();
    }
    [ContextMenu("sendMessage")]
    public async void SendWebSocketMessage()
    {
        await web.SendRequest(testMes);
    }
    public async void SendWebSocketMessage(string message)
    {
        await web.SendRequest(message);
    }
}