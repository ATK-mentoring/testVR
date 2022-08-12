using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
1. raycast ^
2. if it hit ^ 
3. check normal of surface hit ^
4. if normal outside 45 deg of Vector3.Up, give debug.log feedback "cannot teleport there" ^
5. fade out and in 
6. teleport to new position with same rotation ^
*/



public class CustomTeleporter : MonoBehaviour
{
    [SerializeField] GameObject leftController;
    [SerializeField] LineRenderer lr;
    GameObject tiPrefab; // Teleport Indicator 
    GameObject ti;
    List<GameObject> FadedObjects;

    Wand wand;

    private float maxTeleportDistance = 20f;
    private float maxNormalAngle = 45f;
    // Start is called before the first frame update
    void Start()
    {
        tiPrefab = Resources.Load<GameObject>("Prefab/TeleportIndicator");
        ti = Instantiate(tiPrefab,Vector3.zero,Quaternion.identity);
        wand = FindObjectOfType<Wand>();
        FadedObjects = new List<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {
        HandleTeleporter();
    }

    private void HandleTeleporter()
    {
        // Fade to normal
        foreach (GameObject g in FadedObjects)
        {
            g.GetComponent<HouseObject>().ResetMyMaterials();
            g.GetComponent<MeshCollider>().enabled = true;

        }
        FadedObjects.Clear();

        Teleport();

        // Fade Green
        foreach (GameObject g in FadedObjects)
        {
            wand.SetFaded(g);
            g.GetComponent<MeshCollider>().enabled = false;
        }
    }

    private void Teleport()
    {
        ti.SetActive(false);


        RaycastHit[] hits;
        hits = Physics.RaycastAll(leftController.transform.position, leftController.transform.TransformDirection(Vector3.forward), maxTeleportDistance);
        if (hits.Length > 0) // layerMask
        {
            System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].transform.gameObject.tag == "Wanded")
                {
                    FadedObjects.Add(hits[i].transform.gameObject);
                    continue;
                }
                if (Vector3.Angle(Vector3.up, hits[i].normal) < maxNormalAngle)
                {
                    ChangeLineRendererColor(Color.green);
                    ti.SetActive(true);
                    ti.gameObject.transform.position = hits[i].point;

                    if (Input.GetButtonDown("XRI_Left_TriggerButton"))
                    {
                        gameObject.transform.position = hits[i].point;
                    }
                    break;
                }
                else
                {
                    //cannot teleport there
                    ChangeLineRendererColor(Color.red);
                    break;
                }
            }
        }
        else
        {
            // could not find a hit
            ChangeLineRendererColor(Color.red);
        }
    }


    private void ChangeLineRendererColor(Color color)
    {
        lr.startColor = color;
        lr.endColor = color;
    }
   
}
