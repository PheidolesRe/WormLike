using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        Solid solid = collision.gameObject.GetComponent<Solid>();
        BlockHP block = collision.gameObject.GetComponent<BlockHP>();

        if (block)
        {
            block.TakeDamage(damage);
        }

        if (solid)
        {
            Vector2 closestPoint = collision.bounds.ClosestPoint(transform.position);
            Vector2 forceDirection = (closestPoint - (Vector2)transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(forceDirection * 300, closestPoint);  //AddForce(Vector2.up * 1000); // Magic Number
        }
        Destroy(gameObject);
    }
}
