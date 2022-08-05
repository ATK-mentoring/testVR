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
    

    private float maxTeleportDistance = 20f;
    private float maxNormalAngle = 45f;
    // Start is called before the first frame update
    void Start()
    {
        tiPrefab = Resources.Load<GameObject>("Prefab/TeleportIndicator");
        ti = Instantiate(tiPrefab,Vector3.zero,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
       Teleport();
    }

    private void Teleport() 
    {
            ti.SetActive(false);

            
            RaycastHit hit;
           
            if (Physics.Raycast(leftController.transform.position, leftController.transform.TransformDirection(Vector3.forward), out hit, maxTeleportDistance)) // layerMask
            {
                //Debug.DrawRay(leftController.transform.position, leftController.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
                if(Vector3.Angle(Vector3.up, hit.normal) < maxNormalAngle)
                {
                    //Debug.DrawRay(hit.point, hit.normal  * 1000, Color.yellow);
                    ChangeLineRendererColor(Color.green);
                    ti.SetActive(true);
                    ti.gameObject.transform.position = hit.point;

                    if(Input.GetButtonDown("XRI_Left_TriggerButton"))
                    {
                        gameObject.transform.position = hit.point;
                    }
                }
                else 
                {
                    //Debug.Log("cannot teleport there");
                    ChangeLineRendererColor(Color.red);
                }

            }
            else
            {
                //Debug.DrawRay(leftController.transform.position, leftController.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //Debug.Log("Did not Hit");
                ChangeLineRendererColor(Color.red);
                
            }

        

    }


    private void ChangeLineRendererColor(Color color)
    {
        lr.startColor = color;
        lr.endColor = color;
    }
   
}
