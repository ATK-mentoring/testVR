using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void ApplyCollidersToHouse(GameObject parent)
    {
        if (house == null)
        {
            house = parent;
        }

        parent.AddComponent<MeshCollider>();

        foreach (Transform child in parent.transform)
        {
            ApplyCollidersToHouse(child.gameObject);
        }
    }

    GameObject house = null;
    [SerializeField] GameObject user;

    public void PositionPlayer()
    {
        RaycastHit hit;
        Vector3 direction = house.transform.position - user.transform.position;
        if (Physics.Raycast(user.transform.position, direction, out hit, Mathf.Infinity))
        {
            //Vector3 closestPoint = hit.transform.gameObject.GetComponent<MeshCollider>().ClosestPoint(user.transform.position);
            Debug.Log(hit.transform.gameObject.name);
            Vector3[] vertices = hit.transform.gameObject.GetComponent<MeshFilter>().mesh.vertices;

            Vector3 bestPoint = new Vector3(0, 0, 0);
            float bestHeight = -Mathf.Infinity;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].y > bestHeight)
                {
                    bestPoint = vertices[i];
                }
            }

            user.transform.position = bestPoint;
        }
        else
        {
            Debug.Log("nohit");
        }
    }
}
