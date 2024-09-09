using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : NetworkBehaviour
{
    [SerializeField] private BlocksSO rectangleSO;
    [SerializeField] private BlocksSO squareSO;
    [SerializeField] private BlocksSO triangleSO;

    public static string blockKey;
    private bool isOverlapping = false;
    private float floorPosition = -4.4f;

    //public int ownerId;
    private GameObject newTransperentBlock;

    private Dictionary<string, BlocksSO> ScriptableObjectBlocksDic = new Dictionary<string, BlocksSO>();

    private void OnEnable()
    {
        //EventBus.OnPlayerSpawn += SetOwnerID;
        EventBus.OnBuild += BuildNewBlock;
        //EventBus.OnSetBuilding += SpawnTransperentBlock;
        EventBus.OnDestroyTransperentBlock += DestroyOldBlock;
        EventBus.OnTransperentBlockOverlap += SetIsOverlapping;
    }

    private void OnDisable()
    {
        //EventBus.OnPlayerSpawn -= SetOwnerID;
        EventBus.OnBuild -= BuildNewBlock;
       // EventBus.OnSetBuilding -= SpawnTransperentBlock;
        EventBus.OnDestroyTransperentBlock -= DestroyOldBlock;
        EventBus.OnTransperentBlockOverlap -= SetIsOverlapping;
    }

    private void Awake()
    {
        ScriptableObjectBlocksDic = new Dictionary<string, BlocksSO>
        {
            { "Rectangle",  rectangleSO },
            { "Square", squareSO },
            { "Triangle", triangleSO },
        };
    }

    private void Start()
    {
        SpawnTransperentBlock();
        
    }

    private void Update()
    {
        if (!IsOwner) return;

        DragTranserentBlock();
    }


    private void BuildNewBlock()
    {
        if (CanBuild() && IsOwner)
        {
            SpawnNewBlockServerRpc(newTransperentBlock.transform.position, blockKey, newTransperentBlock.transform.rotation);
            Destroy(newTransperentBlock);
            EndTurnServerRpc();
        }
    }

    private void DestroyOldBlock()
    {
        if (!IsOwner && newTransperentBlock == null)  return;

        Destroy(newTransperentBlock);        
    }

    public void SpawnTransperentBlock()
    {
        if (!IsOwner) return;

        if (newTransperentBlock != null)
        {
            Destroy(newTransperentBlock);
        }

        Debug.Log("SpawnTransperent" + blockKey);
        GameObject newBlockPrefab = ScriptableObjectBlocksDic[blockKey].transperentBlockPrefab;
        newTransperentBlock = Instantiate(newBlockPrefab, new Vector2(0, 4), Quaternion.identity);
        //StartCoroutine(SpawnTransperentBlockRoutine());
    }

    //private IEnumerator SpawnTransperentBlockRoutine()
    //{
    //    yield return new WaitForFixedUpdate();

    //    Debug.Log("Routine");
    //    GameObject newBlockPrefab = ScriptableObjectBlocksDic[blockKey].transperentBlockPrefab;
    //    newTransperentBlock = Instantiate(newBlockPrefab, new Vector2(0, 4), Quaternion.identity);
    //}

    private void DragTranserentBlock()
    {
        //if (newTransperentBlock == null)
        //{ 
        //    GameObject newBlockPrefab = ScriptableObjectBlocksDic[blockKey].transperentBlockPrefab;
        //    newTransperentBlock = Instantiate(newBlockPrefab, new Vector2(0, 4), Quaternion.identity);            
        //}

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Moved)
        {
            Vector2 transperentSpawnPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (transperentSpawnPos.y < floorPosition)
            {
                return;
            }

            if (GameStateManager.GAME_STATE.Build == GameStateManager.Instance.gameState)
            {
                if (newTransperentBlock != null)
                {
                    newTransperentBlock.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                }
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnNewBlockServerRpc(Vector2 spawnPos, string blockName, Quaternion rotation)
    {
        GameObject newBlockPrefab = ScriptableObjectBlocksDic[blockName].blockPrefab;
        GameObject newBlock = Instantiate(newBlockPrefab, spawnPos, rotation);
        newBlock.GetComponent<NetworkObject>().Spawn();
    }

    private bool CanBuild()
    {
        Debug.Log(isOverlapping);
        return GameStateManager.GAME_STATE.Build == GameStateManager.Instance.gameState && TurnManager.Instance.IsYourTurn((int)OwnerClientId) && !isOverlapping;
    }

    [ServerRpc(RequireOwnership = false)]
    private void EndTurnServerRpc()
    {
        EndTurnClientRpc();
    }

    [ClientRpc]
    private void EndTurnClientRpc()
    {
        EventBus.OnTurnEnd?.Invoke();
    }

    public void SetIsOverlapping(bool _isOverlapping)
    {
        if (!IsOwner) return;

        isOverlapping = _isOverlapping;
    }
}
