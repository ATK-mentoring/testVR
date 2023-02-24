using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSceneManager : MonoBehaviour
{

    [SerializeField] GameObject dollhouseParent;
    GameObject dollhouse;
    [SerializeField] Camera dollCam;
    public float perspectiveCompensation = 0.95f;
    Vector3 dummyPosition = new Vector3(550f,250,10);

    void Start()
    {
        SetupHouseDummy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupHouseDummy() {
        dollhouse = FindObjectOfType<DataManager>().GetHouse();
        GameObject dh = Instantiate(dollhouse, dummyPosition, Quaternion.identity);
        dh.transform.parent = dollhouseParent.transform;
        dh.transform.localPosition = new Vector3(0, 0, 0);
        SetHouseObjectsLayers(dh);

        // position dollhouse in front of camera
        dollhouseParent.transform.position = dummyPosition;

        Bounds bounds = getBounds(dollhouse);
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        //Get the position on screen.
        Vector2 screenPosition = dollCam.WorldToScreenPoint(bounds.center);
        //Get the position on screen from the position + the bounds of the object.
        Vector2 sizePosition = dollCam.WorldToScreenPoint(bounds.center + bounds.size);
        //By subtracting the screen position from the size position, we get the size of the object on screen.
        Vector2 objectSize = sizePosition - screenPosition;
        //Calculate how many times the object can be scaled up.
        Vector2 scaleFactor = screenSize / objectSize;
        //The maximum scale is the one form the longest side, with the lowest scale factor.
        float maximumScale = Mathf.Min(scaleFactor.x, scaleFactor.y);

        if (dollCam.orthographic)
        {
            //Scale the orthographic size.
            dollCam.orthographicSize = dollCam.orthographicSize / maximumScale;
        }
        else
        {
            //Set the scale of the object.
            transform.localScale = transform.localScale * maximumScale * perspectiveCompensation;
        }
    }

    void SetHouseObjectsLayers(GameObject parent) {
        parent.layer = 3;

        foreach (Transform child in parent.transform)
        {
            SetHouseObjectsLayers(child.gameObject);
        }
    }

    Bounds getBounds(GameObject objeto)
    {
        Bounds bounds;
        Renderer childRender;
        bounds = getRenderBounds(objeto);
        if (bounds.extents.x == 0)
        {
            bounds = new Bounds(objeto.transform.position, Vector3.zero);
            foreach (Transform child in objeto.transform)
            {
                childRender = child.GetComponent<Renderer>();
                if (childRender)
                {
                    bounds.Encapsulate(childRender.bounds);
                }
                else
                {
                    bounds.Encapsulate(getBounds(child.gameObject));
                }
            }
        }
        return bounds;
    }

    Bounds getRenderBounds(GameObject objeto)
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        Renderer render = objeto.GetComponent<Renderer>();
        if (render != null)
        {
            return render.bounds;
        }
        return bounds;
    }
}
