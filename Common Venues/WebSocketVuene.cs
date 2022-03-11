using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using UnityEngine;

public class WebSocketVuene : WebSocketLink
{
    public WebSocketVuene(string url) : base(url) { }

    public WebSocketMessageCenter messageHandle;

    protected override void ParseResult()
    {
        base.ParseResult();
        messageHandle?.ReciveMessage(m_result);
    }
}
