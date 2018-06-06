using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    Rigidbody2D rb;

    public float Radius = 2f;
    public float Damage = 10f;
    public bool DamageFallsOff = true;
    public Tank SourceTank;

    public bool RotatesWithVelocity;

	// Update is called once per frame
	void Update () {
        if (RotatesWithVelocity) { 
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }

        if (isServer == true)
        {

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);    
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("OnTriggerEnter2D");
        if (isServer == false)
        {
            return;
        }

        if (SourceTank != null && SourceTank.GetComponent<Rigidbody2D>() == collider.attachedRigidbody)
        {
            return;
        }

        Collider2D[] cols = Physics2D.OverlapCircleAll(this.transform.position, Radius);

        foreach(Collider2D col in cols)
        {
            if (col.attachedRigidbody == null)
            {
                continue;
            }
            Health h = col.attachedRigidbody.GetComponent<Health>();
            if (h != null)
            {
                h.CmdChangeHealth(-Damage);
            }
        }

        Destroy(gameObject);
    }
}
