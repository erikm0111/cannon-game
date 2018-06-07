using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class btnhostgame : MonoBehaviour {

    public Button btnHost;

	// Use this for initialization
	void Start () {
        Button btn = btnHost.GetComponent<Button>();
        btn.onClick.AddListener(HostGame);
	}

    void HostGame()
    {
        scenes.shouldStartServer = true;
        SceneManager.LoadScene("_SCENE_GAMPLAY_");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
