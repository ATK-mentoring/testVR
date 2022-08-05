using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class WorldManager : MonoBehaviour
{
    [SerializeField] GameObject house;

    // Start is called before the first frame update
    void Start()
    {
        ApplyCollidersToHouse(house);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    1. add mesh collider to house
    2. create list of children
    3. call applycolliderstohouse on each child
    */
    private void ApplyCollidersToHouse(GameObject parent)
    {
        parent.AddComponent<MeshCollider>();
       
       

        foreach(Transform child in parent.transform)
        {
            ApplyCollidersToHouse(child.gameObject);
        }
    }
}
