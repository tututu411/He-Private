
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>WebSocket链接</summary>
public class WebSocketLink
{
    public WebSocketLink(string url)
    {
        m_uri = new Uri(url);
        m_client = new ClientWebSocket();
        m_cToken = new CancellationToken();
    }

    protected readonly Uri m_uri = null;
    /// <summary>WebSocket客户端对象</summary>
    protected readonly ClientWebSocket m_client = null;
    protected readonly CancellationToken m_cToken;
    /// <summary>接收WebSocket返回的信息数据</summary>
    protected WebSocketReceiveResult m_websocketReceiveResult = null;
    /// <summary>byte数组，用于接收WebSocket返回的数据</summary>
    protected byte[] m_byteArrBuffer = null;
    /// <summary>接收WebSocket返回的字符串数据</summary>
    protected string m_result = null;

    /// <summary>是否循环（链接处于打开状态）</summary>
    protected bool Loop { get { return m_client.State == WebSocketState.Open; } }

    /// <summary>获取缓冲区的byte数组段</summary>
    /// <param name="arr">byte数组内容</param>
    /// <returns>结果byte数组段</returns>
    protected ArraySegment<byte> GetBuffer(byte[] arr)
    {
        return new ArraySegment<byte>(arr);
    }
    /// <summary>获取缓冲区的byte数组段</summary>
    /// <param name="str">字符串内容</param>
    /// <returns>结果byte数组段</returns>
    protected ArraySegment<byte> GetBuffer(string str)
    {
        return GetBuffer(Encoding.UTF8.GetBytes(str));
    }
    /// <summary>接收信息</summary>
    /// <returns>返回值为WebSocketReceiveResult的Task</returns>
    protected async Task<WebSocketReceiveResult> ReceiveMessage()
    {
        m_byteArrBuffer = new byte[1024];
        WebSocketReceiveResult wsrResult = await m_client.ReceiveAsync(GetBuffer(m_byteArrBuffer), new CancellationToken());//接受数据
        //Debug.Log(wsrResult.Count + "---" + wsrResult.EndOfMessage + "---" + wsrResult.MessageType);
        m_result += Encoding.UTF8.GetString(m_byteArrBuffer, 0, wsrResult.Count);
        return wsrResult;
    }
    /// <summary>解析结果</summary>
    protected virtual void ParseResult()
    {

    }
    /// <summary>网络报错</summary>
    /// <param name="ex">错误信息</param>
    protected virtual void WebSocketError(Exception ex)
    {
        Debug.LogError(ex.Message + "\n" + ex.StackTrace + "\n" + ex.Source + "\n" + ex.HelpLink);
    }

    /// <summary>连接、接收</summary>
    public async void ConnectAuthReceive()
    {
        try
        {
            await m_client.ConnectAsync(m_uri, m_cToken);//连接
            await SendRequest("Hello Server");
            while (Loop)
            {//遍历接受信息
                m_websocketReceiveResult = await ReceiveMessage();
                if (m_websocketReceiveResult.EndOfMessage)
                {//接收完一条完整信息，解析
                    //Debug.Log("完整一条信息：" + m_result);
                    if (string.IsNullOrEmpty(m_result))
                    {//正规闭包的返回值
                        break;
                    }
                    ParseResult();
                }
            }
        }
        catch (Exception ex)
        {
            WebSocketError(ex);
        }
    }
    /// <summary>发送请求</summary>
    /// <param name="text">请求信息内容</param>
    public async Task SendRequest(string text)
    {
        if (m_client.State == WebSocketState.None) { Debug.Log("未建立链接！"); return; }
        await m_client.SendAsync(GetBuffer(text), WebSocketMessageType.Text, true, m_cToken);//发送数据
    }
    /// <summary>关闭</summary>
    public async void Close()
    {
        if (m_client.State == WebSocketState.None) { Debug.Log("未建立链接！"); return; }
        await m_client.CloseAsync(WebSocketCloseStatus.NormalClosure, "正规闭包", m_cToken);
    }
    /// <summary>终止</summary>
    public void Abort()
    {
        if (m_client.State == WebSocketState.None) { Debug.Log("未建立链接！"); return; }
        m_client.Abort();
    }
}

