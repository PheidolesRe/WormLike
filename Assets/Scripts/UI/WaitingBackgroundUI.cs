using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitingBackgroundUI : MonoBehaviour
{
    [SerializeField] private Button cancelButton;

    private void Start()
    {
        gameObject.SetActive(false);

        cancelButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void WiatingTillLoad(string waitText)
    {
        gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = waitText;
    }
}
