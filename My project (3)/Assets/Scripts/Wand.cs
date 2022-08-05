using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{ 

    [SerializeField] LineRenderer lr;

    [SerializeField] GameObject rightController; 
    
    private float maxWandDistance = 1.6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UseWand();
    }

 private void UseWand() 
    { 
            RaycastHit hit;
           
            if (Physics.Raycast(rightController.transform.position,  rightController.transform.TransformDirection(Vector3.forward), out hit, maxWandDistance)) // layerMask
            {
                // Debug.DrawRay(rightController.transform.position, rightController.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                // Debug.Log("Did Hit");
                // Debug.DrawRay(hit.point, hit.normal  * 1000, Color.yellow);
                lr.startColor = Color.blue;
                lr.endColor = Color.green;
                DestroyCollider(hit);

            }
            else
            {
                // Debug.DrawRay(rightController.transform.position, rightController.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                // Debug.Log("Did not Hit");
                lr.startColor = Color.blue;
                lr.endColor = Color.red;
            }
    }
  
    private void DestroyCollider(RaycastHit hit) 
    {
      if(Input.GetButtonDown("XRI_Right_TriggerButton"))
        {
          if(hit.transform.gameObject.GetComponent<MeshCollider>()!= null) 
          {
           // MakeObjectTransparent(hit.transform.gameObject);
            SetFaded(hit.transform.gameObject);
            Destroy(hit.transform.gameObject.GetComponent<MeshCollider>());
            // Debug.Log("Collider Destroyed");
          } 
        }
    }

    private void MakeObjectTransparent(GameObject houseObject)
    {
    
      MeshRenderer mr = houseObject.GetComponent<MeshRenderer>();
      Material[] newMaterials = new Material[mr.materials.Length];
      for(int i = 0; i < mr.materials.Length;i++)
      {
        Material m = houseObject.GetComponent<MeshRenderer>().materials[i];
        Color c = m.color;
        c.a = 125;
        m.color = c;
        
          m.SetOverrideTag("RenderType", "Transparent");
        m.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
        m.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        m.SetInt("_ZWrite", 0);
        m.DisableKeyword("_ALPHATEST_ON");
        m.EnableKeyword("_ALPHABLEND_ON");
        m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        m.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;
          newMaterials[i] = m;
      
      }
       mr.materials = newMaterials;
    
    }
    [SerializeField]
    Material TransparentDefault;
private void SetFaded (GameObject houseObject) 
{
 MeshRenderer mr = houseObject.GetComponent<MeshRenderer>();
 Material[] newMaterials = new Material[mr.materials.Length];
      for(int i = 0; i < mr.materials.Length;i++)
      {
        newMaterials[i] = TransparentDefault;
      }
      mr.materials = newMaterials;
}



/* to do list
1. Create new list of type materials*
2. for each current material, create a new material that copies old materials with lowered alpha and add it to the new list, 
    for loop of size x where x is current number of materials*
      create a new material that copies old material*
      lower the alpha *
      add it to the new list*
3. apply new list to gameobject materials list(for loop)
*/

}
