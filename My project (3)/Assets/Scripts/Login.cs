
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Login : MonoBehaviour
{
   public string username;
   public  string password;
   public TMP_Text displayText;
   public TMP_Text displayText2;
   public TMP_Text inputField;
   public TMP_Text inputField2;

        public void StoreName()
    { 
        displayText.text = inputField.text;
        displayText2.text = inputField2.text;


       // Debug.Log("error");
       // password = inputField.GetComponent<Text>().text;
       // Debug.Log("eor");
        //displayText.GetComponent<Text>().text = password ;
    }
}
