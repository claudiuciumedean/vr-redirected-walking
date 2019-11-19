using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private Interactable m_CurrentInteractable = null;
    private List<Interactable> m_ContactInteractables = new List<Interactable>();

    // Start is called before the first frame update
    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    private void Update()
    {

        // down 

        if (m_GrabAction.GetLastStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger down");
            Pickup();
        }

        // pick up
        if (m_GrabAction.GetLastStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {

        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());

    }

    public void Pickup()
    {
        // Get nerest interactable
        m_CurrentInteractable = GetNearestInteractable();

        // null check
        if (!m_CurrentInteractable)
        {
            return;
        }

        // already held check
        if (m_CurrentInteractable.m_ActiveHand)
        {
            m_CurrentInteractable.m_ActiveHand.Drop();
        }

        // position
        m_CurrentInteractable.transform.position = transform.position;

        // attach
        Rigidbody targeBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targeBody;

        // set active hand
        m_CurrentInteractable.m_ActiveHand = this;
    }

    public void Drop()
    {

        // null check
        if (!m_CurrentInteractable)
        {
            return;
        }

        // apply velocity
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetVelocity();


        // detach
        m_Joint.connectedBody = null;

        // clear
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach(Interactable interactable in m_ContactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance) {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}
