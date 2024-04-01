using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : XRBaseInteractable
{
    [SerializeField]
    private CardReader cardReader;
    [SerializeField]
    private Transform door;
    [SerializeField]
    private float doorMaxDrageDistance=.35f;
    [SerializeField]
    private int doorWeight=20;
    [SerializeField]
    private AudioSource doorAudioSource;
    [SerializeField]
    private AudioClip doorSound;
    private Vector3 localDragDirection;
    private Vector3 doorStartPosition;
    private Vector3 doorEndPosition;
    private Vector3 worledDragDirection;
    float speed;



    private void Awake()
    {
        base.Awake();
        localDragDirection = new Vector3(-1,0,0);
        worledDragDirection =transform.TransformDirection(localDragDirection).normalized;
        doorStartPosition = door.position;
        doorEndPosition = doorStartPosition + worledDragDirection*doorMaxDrageDistance;
        doorAudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(!isSelected)
            DecreaseSound();
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(isSelected&&cardReader.doorIsOpened)
        {
            var interactorTransform = firstInteractorSelecting.GetAttachTransform(this);
            Vector3 selfToInteractor = interactorTransform.position - transform.position;
            float forceInDirectionOfDrag = Vector3.Dot(selfToInteractor, worledDragDirection);

            bool dragToEnd= forceInDirectionOfDrag > 0;

            float absoluteForce = Mathf.Abs(forceInDirectionOfDrag);

            speed = absoluteForce / Time.deltaTime / doorWeight ;

            door.position = Vector3.MoveTowards(door.position
                ,dragToEnd?doorEndPosition:doorStartPosition
                , speed*Time.deltaTime);
            DoorSound(speed);
            if (door.position == doorEndPosition || door.position == doorStartPosition)
            {
                doorAudioSource.volume -= .2f*Time.deltaTime;
            }
        }
    }

    private void DoorSound(float soundVolume)
    {
        doorAudioSource.clip = doorSound;
        doorAudioSource.volume = soundVolume;

    }
    private void DecreaseSound()
    {
        doorAudioSource.volume -= 0.3f * Time.deltaTime;
        if(door.position == doorEndPosition ||  door.position == doorStartPosition)
        {
            doorAudioSource.volume -= .2f * Time.deltaTime;
        }
    }
}
