﻿using UnityEngine;
using System.Collections;
using System;

public class EnemyMeleeDMG : MonoBehaviour {

    public int damage = 1;
    public int damageRate = 1;
    //public bool doDMGAtStart = true;
    public float pushBackForce = 5;
    private float dmgTimer = 0;

    void OnTriggerStay2D(Collider2D colision)
    {
        if (colision.CompareTag("Player") && dmgTimer < Time.time)
        {
           HealthManager PlayerHealth =  colision.gameObject.GetComponent<HealthManager>();
            PlayerHealth.TakeDemage(damage);
            dmgTimer = Time.time + damageRate;
            PushBackPlayer(colision.transform);
        }
    }

    private void PushBackPlayer(Transform PushedObjectTransform)
    {
        Vector2 pushDirection = new Vector2(0, (PushedObjectTransform.position.y - 1)).normalized;
        pushDirection *= pushBackForce;

        Rigidbody2D ObjectRB = PushedObjectTransform.gameObject.GetComponent<Rigidbody2D>();
        ObjectRB.velocity = Vector2.zero;
        ObjectRB.AddForce(pushDirection, ForceMode2D.Impulse);
    }
}