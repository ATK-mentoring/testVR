using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomKey : MonoBehaviour
{
    string key;
    CustomKeyboard parentKeyboard;
    void Start()
    {
        key = gameObject.name;
        gameObject.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>().text = key;
    }

    void Update()
    {
        
    }

    public void SetParentKeyboard(CustomKeyboard ck) {
        parentKeyboard = ck;
    }

    public void OnPress() {
        if (key.Length == 1) {
            // is a character
            parentKeyboard.AddCharacter(key[0]);
        }
        if (key == "BACKSPACE")
        {

        }
        if (key == "ENTER")
        {

        }
        if (key == "TAB") {
        
        }
    }
}
