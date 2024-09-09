using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlappingBlock : MonoBehaviour
{
    private int overlapCount = 0;
    private Color startColor;

    private void Awake()
    {
        startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Solid solid = collision.gameObject.GetComponent<Solid>();
        CantBuild noBuild = collision.gameObject.GetComponent<CantBuild>();

        if (solid)
        {
            Debug.Log("Enter Overlap");
            overlapCount++;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
            EventBus.OnTransperentBlockOverlap?.Invoke(true);
        }

        if (noBuild)
        {
            overlapCount++;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
            EventBus.OnTransperentBlockOverlap?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Solid solid = collision.gameObject.GetComponent<Solid>();
        CantBuild noBuild = collision.gameObject.GetComponent<CantBuild>();

        if (solid)
        {
            overlapCount--;

            if (overlapCount == 0)
            { 
                Debug.Log("Exit Overlap");
                gameObject.GetComponent<SpriteRenderer>().color = startColor;
                EventBus.OnTransperentBlockOverlap?.Invoke(false);
            }
        }

        if (noBuild) 
        {
            overlapCount--;

            if (overlapCount == 0)
            {
                Debug.Log("Exit Overlap");
                gameObject.GetComponent<SpriteRenderer>().color = startColor;
                EventBus.OnTransperentBlockOverlap?.Invoke(false);
            }
        }
    }
}
