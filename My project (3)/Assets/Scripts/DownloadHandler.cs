using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadHandler : MonoBehaviour {

    void Start () {
        StartCoroutine(DownloadFile());
    }

    IEnumerator DownloadFile() {
        string houseName = "House.fbx";
        var uwr = new UnityWebRequest("https://github.com/ATK-mentoring/fbx-examples/raw/main/Hillside_House.fbx", UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.dataPath +"/Resources", houseName);
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(uwr.error);
            yield break;
        }
        else
            Debug.Log("File successfully downloaded and saved to " + path);
        GameObject housePrefab = Resources.Load<GameObject>(houseName);
        GameObject house = Instantiate(housePrefab, Vector3.zero, Quaternion.identity);
    }
}