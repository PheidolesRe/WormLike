using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLifeTime : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
