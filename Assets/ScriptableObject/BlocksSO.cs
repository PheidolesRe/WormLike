using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockInfo")]
public class BlocksSO : ScriptableObject
{
    public GameObject blockPrefab;
    public GameObject transperentBlockPrefab;


    public GameObject GetBlockPrefab()
    { 
        return blockPrefab;
    }
    
    public GameObject GetTransperentBlockPrefab()
    { 
        return transperentBlockPrefab;
    }
    //{
    //    {"Square", blockPrefab },
    //    { }
    //};
}
