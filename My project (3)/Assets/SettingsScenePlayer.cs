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

    [Header("dollhouse")]
    public GameObject dollhouse;

    [Header("Activation Settings")]
    public float activationDistance;

    private bool dollhouseGrabbed = false;

    // record positions on click
    private Vector3 startingPositionOfController;
    private Quaternion startingRotationOfController;
    private Quaternion startingRotationOfDollhouse;

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
        if (IsHoveringButton())
        {
            dollhouseGrabbed = true;
            startingPositionOfController = controller.transform.position;
            startingRotationOfController = controller.transform.rotation;
            startingRotationOfDollhouse = dollhouse.transform.rotation;

        }

        controller.GetComponent<LineRenderer>().positionCount = 2;
        controller.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, -20, 0));
    }
    private void GrabRelease(InputAction.CallbackContext context) {
        dollhouseGrabbed = false;
    }
    private void ResetButton(InputAction.CallbackContext context) {
        dollhouse.transform.eulerAngles = Vector3.zero;
    }

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        if (!dollhouseGrabbed) { return; }

        Vector3 positionDifference = controller.transform.position - startingPositionOfController;
        Quaternion rotationDifference = controller.transform.rotation * Quaternion.Inverse(startingRotationOfController);

        dollhouse.transform.rotation = startingRotationOfDollhouse * rotationDifference;
    }

    bool IsHoveringButton()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(controller.transform.position, controller.transform.TransformDirection(Vector3.down), Mathf.Infinity);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].transform.gameObject.GetComponent<RawImage>() != null) {
                return true;
            }
        }

        return false;
    }
}
