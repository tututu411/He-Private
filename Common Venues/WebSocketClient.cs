using System;
using System.Collections;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Common_Venues
{
    public class WebSocketClient : MonoBehaviour
    {
        public string message = "ceshji";
        ClientWebSocket ws;
        // Use this for initialization
        private async void Start()
        {
            try
            {
                ws = new ClientWebSocket();
                Uri url = new Uri("ws://localhost:8888");
                await ws.ConnectAsync(url, CancellationToken.None);

                await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("hello")), WebSocketMessageType.Binary, true, CancellationToken.None);

                while (ws.State == WebSocketState.Open)
                {
                    var @byte = new byte[1024];
                    var result = await ws.ReceiveAsync(new ArraySegment<byte>(@byte), CancellationToken.None);
                    var str = Encoding.UTF8.GetString(@byte, 0, @byte.Length);
                    Debug.Log("接收到：" + str);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("链接失败" + e.Message);
            }
        }
        private async void OnDestroy()
        {
            try
            {
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "正规闭包", CancellationToken.None);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        [ContextMenu("SendMessage")]
        private async void SendWebSocketMessage()
        {
            try
            {
                await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Binary, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Debug.Log("发送失败" + e.Message);
            }
        }
        [ContextMenu("ReciveMessaage")]
        private void ReciveMessage(string mes)
        {

        }
    }
}