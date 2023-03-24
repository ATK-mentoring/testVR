using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsScenePlayer : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] InputActionReference rightX;
    [SerializeField] InputActionReference rightTrigger;
    [SerializeField] GameObject controller;
    //public OVRInput.Button grabButton;
    //public OVRInput.Button resetRotationButton;
    private Vector3 aimDirection;

    [Header("dollhouse")]
    public GameObject dollhouse;

    [Header("Activation Settings")]
    public float activationDistance;
    private bool sliderDragging = false;

    private void Start()
    {
        rightX.action.Enable();
        rightX.action.performed += GrabButton;
        rightX.action.canceled += GrabRelease;

        controller.GetComponent<LineRenderer>().positionCount = 2;
        controller.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, -20, 0));
        //rightTrigger.action.performed += ResetButton;
    }

    private void GrabButton(InputAction.CallbackContext context) {
        PressButton();

        controller.GetComponent<LineRenderer>().positionCount = 2;
        controller.GetComponent<LineRenderer>().SetPosition(1, aimDirection*20);
    }
    private void GrabRelease(InputAction.CallbackContext context) {
        sliderDragging = false;
    }
    private void ResetButton(InputAction.CallbackContext context) {
        dollhouse.transform.eulerAngles = Vector3.zero;
    }

    void Update()
    {
        DragSlider();
    }

    void DragSlider() {
        if (sliderDragging) {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(controller.transform.position, controller.transform.TransformDirection(aimDirection), Mathf.Infinity);
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].transform.gameObject.name == "SlidingArea") {
                    // P(x1, y1, z1) and Q(x2, y2, z2) = PQ = ?[(x2 – x1)2 + (y2 – y1)2 + (z2 – z1)2]
                    //Vector3 difference = hits[i].transform.position - hits[i].point;
                }
            }
        }
    }

    void PressButton()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(controller.transform.position, controller.transform.TransformDirection(aimDirection), Mathf.Infinity);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].transform.gameObject.GetComponent<Button>() != null) {
                hits[i].transform.gameObject.GetComponent<Button>().onClick.Invoke();
                break;
            }

            if (hits[i].transform.gameObject.name == "Handle")
            {
                sliderDragging = true;
                break;
            }
        }
    }
}
