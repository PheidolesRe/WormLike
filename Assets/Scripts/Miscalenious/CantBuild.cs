using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantBuild : MonoBehaviour
{
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(0, 6);
    }

    private void Update()
    {
        if (GameStateManager.Instance.IsGameStateBuild())
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.3f);
        }
        else
        { 
            GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 0);            
        }
    }
}
