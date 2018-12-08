using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class EventObject : Photon.PunBehaviour {
    string Message;

    private void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnEvent;
    }

    public void DoRaiseEvent(byte _evCode, string message)
    {
        byte evCode = _evCode;
        bool reliable = true;
        byte[] content = Encoding.UTF8.GetBytes(message);
        //send to server plugin
        PhotonNetwork.RaiseEvent(evCode, content, reliable, null);
    }

    // receive from server
    private void OnEvent(byte eventCode, object content, int senderID)
    {
        if((eventCode==1)&&(senderID<=0))
        {
            Message = (string)content;
            Debug.Log(string.Format("Message from server: {0}", Message));
        }
    }
    private void OnGUI()
    {
        GUILayout.Label(string.Format("I am {0}. Message from Server: {1}", PhotonNetwork.playerName, Message));
    }
}
