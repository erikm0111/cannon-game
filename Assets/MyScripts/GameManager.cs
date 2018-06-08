using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    [SyncVar]
    public float TimeLeft = 180;
	
	// Update is called once per frame
	void Update () {

        if (isServer == false)
        {
            return;
        }
        
        if (scenes.is_over == 1)
        {
            SceneManager.LoadScene("menu");
        }
        TimeLeft -= Time.deltaTime;
        if (TimeLeft <= 0)
        {
            // Restart the match
        }
	}
}
