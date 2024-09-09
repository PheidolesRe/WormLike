using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Windows;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CopyCode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI codeText;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            string code = codeText.text.Substring(12);

            GUIUtility.systemCopyBuffer = code;
        });
    }
}
