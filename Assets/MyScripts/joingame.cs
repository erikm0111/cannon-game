using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class joingame : MonoBehaviour {

    public InputField myInput;
    public Button btnJoin;

	// Use this for initialization
	void Start () {
        Button btn = btnJoin.GetComponent<Button>();
        btn.onClick.AddListener(JoinGame);
	}

    void JoinGame()
    {
        scenes.ipaddress = myInput.text;
        scenes.shouldStartClient = true;
        SceneManager.LoadScene("_SCENE_GAMPLAY_");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
