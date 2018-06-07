using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    void Start()
    {
        text = GetComponent<Text>();
    }

    Text text;
    Health health;
	
	// Update is called once per frame
	void Update () {
		if (health == null)
        {
            if (Tank.LocalTank != null)
            {
                health = Tank.LocalTank.GetComponent<Health>();
            }
            if (health == null)
            {
                text.text = "DEAD";
                return;
            }
        }

        text.text = health.GetHitpoints().ToString("#");
	}
}
