using System.Collections;
using System.IO;
using UnityEngine;
using System.Text;
using Dummiesman;
using UnityEngine.Networking;
using System.Net;
using System;
using System.IO.Compression;
using System.ComponentModel;

public class DownloadHandler : MonoBehaviour {

    public event System.ComponentModel.AsyncCompletedEventHandler? DownloadFileCompleted;
    string path = "";


    void Start () 
    {
        // GameObject loadedObject = new OBJLoader().Load(Application.dataPath + "/Resources/House 1.obj");
       StartCoroutine(DownloadFile());

       // var loadedObj = new OBJLoader().Load(Path.Combine(Application.dataPath + "/Resources", "Test1.obj"));
       
    }
 


 

     IEnumerator DownloadFile() {



         WebClient client = new WebClient();


        path = Application.persistentDataPath + "/" + "ArchicadObjSize.zip";
        Debug.Log(Application.persistentDataPath);


        client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);
        Uri uri = new Uri("https://github.com/ATK-mentoring/fbx-examples/raw/main/ArchicadObjSize.zip");
        client.DownloadFileAsync(uri, path);
        
       
        yield return null; 

        /* Stream data = client.OpenRead(@"https://github.com/ATK-mentoring/fbx-examples/blob/main/ArchicadObjSize.zip");
         StreamReader reader = new StreamReader(data);
         string s = reader.ReadToEnd();
         Console.WriteLine(s);
         data.Close();
         reader.Close(); */





        /* string houseName = "archicad.zip";
        var www = new UnityWebRequest("https://drive.google.com/file/d/1dVefOGTcxVF8nJgDli5LsFB_S6AyXVRi/view?usp=sharing");
        string path = Path.Combine(Application.dataPath , houseName);
        www.downloadHandler = new DownloadHandlerFile(path);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            yield break;
        }
        else 
            Debug.Log("File successfully downloaded and saved to " + path);
        ZipFile.ExtractToDirectory(path, Application.persistentDataPath); */
        //GameObject housePrefab = Resources.Load<GameObject>(houseName);
        // GameObject house = Instantiate(housePrefab, Vector3.zero, Quaternion.identity);
      //  var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.downloadHandler.text)); **
       // var textStream = new MemoryStream(www.downloadHandler.data);
       // var loadedObj = new OBJLoader().Load(textStream); **
        //GameObject loadedObject = new OBJLoader().Load(Application.dataPath + "/Resources/" + houseName); 

    }
    void DownloadFileCallback(object sender, AsyncCompletedEventArgs e)
    {
        ZipFile.ExtractToDirectory(path, Application.persistentDataPath);
        var loadedObject = new OBJLoader().Load(Application.persistentDataPath + "/TEST 8/" + "Vacation Home.obj");

        loadedObject.gameObject.transform.Rotate(-90f, 0f, 0f, Space.World);
        WorldManager.ApplyCollidersToHouse(loadedObject);

        FindObjectOfType<Wand>().setHouse(loadedObject);

    }
    
}