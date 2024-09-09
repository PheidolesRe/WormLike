using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField] protected float maxForce;

    protected float powerCoef = 1f;
    public Vector2 target;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        //rb.velocity = target.normalized * moveSpeed * powerCoef;
        rb.AddForce(target.normalized * maxForce * powerCoef);
    }

    protected virtual void FixedUpdate()
    {
        float angle = GetProjectileAngle();
        transform.rotation = Quaternion. Euler(0, 0, angle);
    }

    private float GetProjectileAngle()
    {
        return Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;        
    }

    public void SetPowerCoef(float power)
    {
        if (power > 1)
        {
            powerCoef = 1;
        }
        else
        { 
            powerCoef = power;
        }

    }
}

