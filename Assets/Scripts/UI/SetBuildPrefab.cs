using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetBuildPrefab : MonoBehaviour //IPointerDownHandler
{
    //[SerializeField] private GameObject blockPrefab;
    [SerializeField] private BlockType blockType;

    private Button buildButton;

    private enum BlockType // Add new type in BuildManager as well
    { 
        Rectangle,
        Square,
        Triangle,
    }

    private void Awake()
    {
        buildButton = GetComponent<Button>();
        buildButton.onClick.AddListener(() =>
        {
            Debug.Log("SetBuildPrefab");
            BuildManager.blockKey = blockType.ToString();
            //EventBus.OnSetBuilding?.Invoke();
        });

    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    BuildManager.blockKey = blockType.ToString();
    //}
}
