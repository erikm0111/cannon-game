using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	    if (isServer)
        {
            hitpoints = 100;
        }	
	}

    [SyncVar]
    float hitpoints;
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetHitpoints()
    {
        return hitpoints;
    }

    [Command]
    public void CmdChangeHealth(float amount)
    {
        hitpoints += amount;

        if (hitpoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isServer == false)
        {
            Debug.LogError("Client called DIE");
            return;
        }
        Debug.Log("DIE");
        scenes.is_over = 1;
        Destroy(gameObject);
    }

    void OnGUI()
    {

        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);

        GUI.Box(new Rect(targetPos.x - 20, Screen.height - targetPos.y - 80, 60, 20), hitpoints + "/" + 100);

    }
}
