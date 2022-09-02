using System.Collections;
using System.IO;
using UnityEngine;
using System.Text;
using Dummiesman;
using UnityEngine.Networking;

public class DownloadHandler : MonoBehaviour {

    void Start () 
    {
        // GameObject loadedObject = new OBJLoader().Load(Application.dataPath + "/Resources/House 1.obj");
        StartCoroutine(DownloadFile());

        var loadedObj = new OBJLoader().Load(Path.Combine(Application.dataPath + "/Resources", "Test1.obj"));

    }

    IEnumerator DownloadFile()
    {
        var www = new WWW("https://drive.google.com/uc?export=view&id=1jNmntP7cxcZXilTe5S4NX-8Q-QkhXpXx");
        yield return www;
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
        var loadedObj = new OBJLoader().Load(textStream);
        FindObjectOfType<WorldManager>().ApplyCollidersToHouse(loadedObj);
    }

    /* IEnumerator DownloadFile() {
        string houseName = "House.obj";
        var www = new UnityWebRequest("https://drive.google.com/file/d/1jNmntP7cxcZXilTe5S4NX-8Q-QkhXpXx/view?usp=sharing");
        string path = Path.Combine(Application.dataPath +"/Resources", houseName);
        www.downloadHandler = new DownloadHandlerFile(path);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            yield break;
        }
        else
            Debug.Log("File successfully downloaded and saved to " + path);
        //GameObject housePrefab = Resources.Load<GameObject>(houseName);
        // GameObject house = Instantiate(housePrefab, Vector3.zero, Quaternion.identity);
         var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.downloadHandler.text));
       // var textStream = new MemoryStream(www.downloadHandler.data);
        var loadedObj = new OBJLoader().Load(textStream);
        //GameObject loadedObject = new OBJLoader().Load(Application.dataPath + "/Resources/" + houseName);
    }
    */
}