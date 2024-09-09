using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInputArea : Singletone<NoInputArea>
{
    public bool isInputOff { get; private set; }
    private Vector2 touchPos;

    protected override void Awake()
    {
        base.Awake();

        isInputOff = false;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);            
        } 

        if (touchPos.y <= transform.position.y)
        {
            isInputOff = true;
        }
        else
        {
            isInputOff = false;
        }
    }

    public bool GetCanInteract()
    {
        if (transform.position.y <= Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //private void LateUpdate()
    //{
    //    if (Input.touchCount > 0)
    //    { 
    //        if (Input.GetTouch(0).phase == TouchPhase.Ended)
    //        {
    //            isInputOff = true;
    //            Debug.Log(isInputOff);
    //        }
    //    }
    //}
}
