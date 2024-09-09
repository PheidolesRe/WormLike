using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRotated : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.OnBloockRotated += RotateBlock;
    }

    private void OnDisable()
    {
        EventBus.OnBloockRotated -= RotateBlock;        
    }

    private void RotateBlock()
    {
        transform.rotation *= Quaternion.Euler(0, 0, -30);
    }
}
