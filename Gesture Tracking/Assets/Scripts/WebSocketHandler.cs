using System;
using System.Text;
using UnityEngine;
using WebSocketSharp;
using System.Threading;
using WebSocketSharp.Server;
using PimDeWitte.UnityMainThreadDispatcher;

public class WebSocketHandler : MonoBehaviour
{
    private WebSocketServer wss;

    void Start()
    {
        // Start WebSocket server on port 8080
        wss = new WebSocketServer("ws://localhost:8080");
        wss.AddWebSocketService<GestureBehavior>("/Gesture");
        wss.Start();
        Debug.Log("WebSocket server started on ws://localhost:8080");
    }

    void OnDestroy()
    {
        if (wss != null)
        {
            wss.Stop();
            wss = null;
        }
    }
}

public class GestureBehavior: WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        string gesture = e.Data.Trim(); // Clean up data
        Debug.Log($"Received gesture: '{gesture}'");

        // Run UI updates on the main thread
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            UIManager.Instance.ActivateImage(gesture);
        });

        // Optionally, send a response back to the client
        Send($"Gesture {e.Data} received by Unity!");
    }
}
