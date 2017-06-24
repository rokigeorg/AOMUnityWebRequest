using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TicketScript : MonoBehaviour
{
    public GameObject TicketMenu;
    public Ticket jsonReqResult;

    void Start()
    { StartRequest();
    }

        void StartRequest()
    {
        StartCoroutine(Request());
    }

    IEnumerator Request()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://poc.apiomat.enterprises/yambas/rest/apps/TTApp/models/TTModul/Ticket?hrefs=false&withClassnameFilter=true&withReferencedHrefs=false");
        www.SetRequestHeader("X-apiomat-system","LIVE");
        www.SetRequestHeader("X-apiomat-sdkVersion","2.5.2");
        www.SetRequestHeader("Accept", "application/json");
        www.SetRequestHeader("Authorization", "Basic aG9sb2xlbnM0QGFwaW9tYXQuY29tOmFwaW5hdXQxMjM=");
        www.SetRequestHeader("X-apiomat-apikey", "1934812927473269849");



        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.error);

        }

        else
        {
            var dtoAsString = www.downloadHandler.text;
            Debug.Log(dtoAsString);

            var t = JsonUtility.FromJson<Ticket>(dtoAsString);

            Debug.Log(t.Name);
        }
    }

    [Serializable]
    public class Ticket
    {
        public string Name;
    }
}
