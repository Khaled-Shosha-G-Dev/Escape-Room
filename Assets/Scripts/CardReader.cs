using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class CardReader : XRSocketInteractor
{
    private float timeEntered;
    private float timeExited;

    private Transform m_KeycardTransform;
    private Vector3 m_HoverEntry;
    private bool m_SwipeIsValid = true;

    public float AllowedUprightErrorRange = 0.1f;
    public GameObject VisualLockToHide;
    public Collider HandleToEnable;
    public Renderer activatedCardColor;

    public float swipeSpeedLimit;

    public bool doorIsOpened = false;

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        m_KeycardTransform = args.interactableObject.transform;
        m_HoverEntry = m_KeycardTransform.position;
        m_SwipeIsValid = true;
        timeEntered = Time.time;
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        if (m_KeycardTransform != null)
        {
            Vector3 entryToExit = m_KeycardTransform.position - m_HoverEntry;
            timeExited = Time.time;

            if (m_SwipeIsValid && entryToExit.y < -0.15f && activatedCardColor.material.color == Color.green && swipeSpeedLimit < Vector3.Distance(entryToExit,m_HoverEntry)/(timeExited-timeEntered))
            {
                VisualLockToHide.SetActive(false);
                doorIsOpened=true;
                HandleToEnable.enabled = true;
            }

            m_KeycardTransform = null;
        }
    }

    private void Update()
    {
        if (m_KeycardTransform != null)
        {
            Vector3 keycardUp = m_KeycardTransform.forward;
            float dot = Vector3.Dot(keycardUp, Vector3.up);

            if (dot < 1 - AllowedUprightErrorRange)
            {
                m_SwipeIsValid = false;
            }
        }

    }
}
