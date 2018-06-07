using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class startclientserver : MonoBehaviour {

	// Use this for initialization
	void Start () {
        manager = GetComponent<NetworkManager>();
        manager.networkAddress = scenes.ipaddress;
        if (scenes.shouldStartServer == true)
        {
            manager.StartHost();
        }
        if (scenes.shouldStartClient == true)
        {
            manager.StartClient();
        }
    }

    NetworkManager manager;
	
	// Update is called once per frame
	void Update () {

	}
}
