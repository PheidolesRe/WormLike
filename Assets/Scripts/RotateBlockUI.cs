using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateBlockUI : MonoBehaviour
{
    private void Awake()
    {
        transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            EventBus.OnBloockRotated?.Invoke();
        });
    }
}
