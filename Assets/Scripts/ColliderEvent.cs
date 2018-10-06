using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ColliderEvent : MonoBehaviour
{
    public GameObject model;

    Hand handController;

    bool isValid
    {
        get
        {
            return handController.controller != null;
        }
    }

    void Start()
    {
        handController = GetComponent<Hand>();
    }

    void Update()
    {
        if (isValid && handController.controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            if (GetComponent<SpringJoint>())
            {
                //model.SetActive(true);
                SpringJoint fx = GetComponent<SpringJoint>();
                fx.connectedBody.velocity = handController.controller.velocity;
                fx.connectedBody.angularVelocity = handController.controller.angularVelocity;
                fx.connectedBody = null;
                Destroy(fx);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!isValid)
        {
            return;
        }

        if (handController.controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (GetComponent<SpringJoint>() == null && other.gameObject.layer == LayerMask.NameToLayer("Parts"))
            {
                SpringJoint fx = gameObject.AddComponent<SpringJoint>();
                fx.spring = 600.0f;
                fx.damper = 100.0f;
                fx.breakForce = 20000;
                fx.breakTorque = 20000;
                fx.connectedBody = other.attachedRigidbody;

                /*if (other.attachedRigidbody.isKinematic == false)
                { 
                    other.gameObject.transform.parent = transform;
                    //other.gameObject.transform.localPosition = Vector3.zero;
                    other.attachedRigidbody.isKinematic = true;
                }
                else
                {
                    other.attachedRigidbody.isKinematic = false;
                    other.gameObject.transform.parent = null;
                    other.attachedRigidbody.velocity = GetComponent<Rigidbody>().velocity;
                    other.attachedRigidbody.angularVelocity = GetComponent<Rigidbody>().velocity;
                }*/
            }
            /*
            else if (gameObject.GetComponent<FixedJoint>())
            {
                other.attachedRigidbody.isKinematic = false;
                model.SetActive(true);
                FixedJoint fx = gameObject.GetComponent<FixedJoint>();
                fx.connectedBody.velocity = handController.device.velocity;
                fx.connectedBody.angularVelocity = handController.device.angularVelocity;
                fx.connectedBody = null;
                Destroy(fx);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Instruments"))
            {
                FixedJoint fx = gameObject.AddComponent<FixedJoint>();
                fx.breakForce = 20000;
                fx.breakTorque = 20000;
                fx.connectedBody = other.attachedRigidbody;
                model.SetActive(false);
                other.attachedRigidbody.isKinematic = true;
                //other.gameObject.transform.parent = transform;
                //other.gameObject.transform.localPosition = Vector3.zero;
            }*/

        }
    }
}
