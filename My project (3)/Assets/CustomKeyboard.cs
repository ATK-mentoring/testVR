using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomKeyboard : MonoBehaviour
{
    string inputText = "";
    [SerializeField] GameObject targetTextbox;

    void Start()
    {
        SetChildren(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetChildren(Transform parent) {
        if (parent.gameObject.GetComponent<CustomKey>() != null) {
            parent.gameObject.GetComponent<CustomKey>().SetParentKeyboard(this);
        }
        foreach (Transform child in parent) {
            SetChildren(child);
        }
    }

    public void AddCharacter(char c) {
        // inputText += c;
        targetTextbox.GetComponent<TextMeshProUGUI>().text += c;
    }
    public void BackspaceCharacter()
    {
        TextMeshProUGUI text = targetTextbox.GetComponent<TextMeshProUGUI>();
        int l = text.text.Length;
        text.text = text.text.Substring(0, l - 2);
    }
}
