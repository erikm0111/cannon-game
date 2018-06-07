﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Tank : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    float MovementPerTurn = 5;
    float MovementLeft;

    float Speed = 5;
    float TurretSpeed = 90; // Degrees per second
    float TurretPowerSpeed = 20;

    public GameObject CurrentBulletPrefab;
    public Transform TurretPivot;

    public Transform BulletSpawnPoint;

    [SyncVar (hook = "OnTurretAngleChange")]
    float turretAngle = 45f;

    float turretPower = 10f;

    [SyncVar]
    Vector3 serverPosition;
    Vector3 serverPositionSmoothVelocity;

    static public Tank LocalTank { get; protected set; }

    void NewTurn()
    {
        MovementLeft = MovementPerTurn;
    }
	
	// Update is called once per frame
	void Update () {
        if (isServer)
        {

        }

		if (hasAuthority)
        {
            LocalTank = this;
            AuthorityUpdate();
        }

        TurretPivot.rotation = Quaternion.Euler(0, 0, turretAngle);

        if (hasAuthority == false)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position, 
                serverPosition, 
                ref serverPositionSmoothVelocity, 
                0.25f);
        }
	}

    void AuthorityUpdate()
    {
        AuthorityUpdateMovement();
        AuthorityUpdateShooting();

        GameObject pn_go = GameObject.Find("PowerNumber");
        pn_go.GetComponent<UnityEngine.UI.Text>().text = turretPower.ToString("#.00");
    }

    void AuthorityUpdateMovement()
    {
        // Listen for keyboard commands for movement
        float movement = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            movement *= 0.1f;
        }

        transform.Translate(movement, 0, 0);

        CmdUpdatePosition(transform.position);
    }

    void AuthorityUpdateShooting()
    {
        float turretMovement = Input.GetAxis("TurretHorizontal") * TurretSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            turretMovement *= 0.1f;
        }

        turretAngle = Mathf.Clamp(turretAngle + turretMovement, 0, 180);
        CmdChangeTurretAngle(turretAngle);


        float powerChange = Input.GetAxis("Vertical") * TurretPowerSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            powerChange *= 0.1f;
        }

        turretPower = Mathf.Clamp(turretPower + powerChange, 0, 20);


        if (Input.GetKeyUp(KeyCode.Space))
        {
            Vector2 velocity = new Vector2(
                turretPower * Mathf.Cos(turretAngle * Mathf.Deg2Rad),
                turretPower * Mathf.Sin(turretAngle * Mathf.Deg2Rad)
            );
            CmdFireBullet(BulletSpawnPoint.position, velocity);
        }
    }

    [Command]
    void CmdChangeTurretAngle(float angle)
    {
        turretAngle = angle;
    }

    [Command]
    void CmdFireBullet( Vector2 bulletPosition, Vector2 velocity )
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        GameObject go = Instantiate(CurrentBulletPrefab, 
            bulletPosition, 
            Quaternion.Euler(0, 0, angle)    
        );
        go.GetComponent<Bullet>().SourceTank = this;

        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = velocity;

        NetworkServer.Spawn(go);
    }

    [Command]
    void CmdUpdatePosition(Vector3 newPosition)
    {
        serverPosition = newPosition;
    }

    [ClientRpc]
    void RpcFixPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    // SYNCVAR HOOKS
    void OnTurretAngleChange(float newAngle)
    {
        if (hasAuthority)
        {
            return;
        }
        turretAngle = newAngle;
    }
}