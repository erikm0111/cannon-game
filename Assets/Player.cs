using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if (isServer == true)
        {
            SpawnTank();
        }
	}

    public GameObject TankPrefab;
    GameObject myTank;

    // Update is called once per frame
    void Update () {

	}

    public void SpawnTank()
    {
        if (isServer == false)
        {
            Debug.Log("Can only do from SERVER");
            return;
        }

        myTank = Instantiate(TankPrefab);
        NetworkServer.SpawnWithClientAuthority(myTank, connectionToClient);
    }
}
