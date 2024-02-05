using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDrop : MonoBehaviour
{
    public GameObject player;
    public Transform crosshair;
    public float pickUpRange = 20f;
    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private bool canDrop = true;
    public GameObject Pickup;
    public GameObject Drop;

    private void Start()
    {
        Pickup.SetActive(true);
        Drop.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    DropObject();
                }
            }
        }
        if (heldObj != null)
        {
            MoveObject();
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
                {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = crosshair.transform;
            Drop.SetActive (true);
            Pickup.SetActive(false);
        }
    }
    void DropObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null;
        Drop.SetActive(false);
        Pickup.SetActive(true);
    }
    void MoveObject()
    {
        heldObj.transform.position = crosshair.transform.position;
    }
}
