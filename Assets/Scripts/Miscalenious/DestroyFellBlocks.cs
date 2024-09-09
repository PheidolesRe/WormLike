using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DestroyFellBlocks : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BlockHP block = collision.GetComponent<BlockHP>();

        if (block)
        { 
            block.DestroySelf();
        }
    }
}
