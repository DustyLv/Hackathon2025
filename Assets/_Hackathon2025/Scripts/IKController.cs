using System;
using UnityEngine;
using UnityEngine.Animations;

public class IKController : MonoBehaviour
{

    public bool ikActive = true;
    public Transform IKTarget_RightHand = null;
    public Transform IKTarget_LeftHand = null;
    public Transform IKTarget_Head = null;

    public Transform Player_RightHand = null;
    public Transform Player_LeftHand = null;
    public Transform Player_Head = null;
    
    // public ParentConstraint RightHandConstraint;
    // public ParentConstraint LeftHandConstraint;
    // public ParentConstraint HeadConstraint;


    
    private void Start()
    {
        GlobalStateManager.PossessedState.OnEnter += _PossessedState_OnEnter;
        
    }

    private void OnDisable()
    {
        GlobalStateManager.PossessedState.OnEnter -= _PossessedState_OnEnter;
    }

    private void _PossessedState_OnEnter(GlobalStateManager._GlobalStateManager._State state)
    {
        ikActive = true;
    }


    public void LateUpdate()
    {
        if (ikActive)
        {
            IKTarget_RightHand.localPosition = new Vector3(Player_LeftHand.localPosition.x,Player_LeftHand.localPosition.y,-Player_LeftHand.localPosition.z);
            IKTarget_LeftHand.localPosition = new Vector3(Player_RightHand.localPosition.x,Player_RightHand.localPosition.y,-Player_RightHand.localPosition.z);
            IKTarget_Head.localPosition = new Vector3(-Player_Head.localPosition.x,Player_Head.localPosition.y,Player_Head.localPosition.z);
            
            
            IKTarget_RightHand.localRotation = Quaternion.Euler(Player_LeftHand.localRotation.eulerAngles.x + 90f, -Player_LeftHand.localRotation.eulerAngles.y, -Player_LeftHand.localRotation.eulerAngles.z);
            IKTarget_LeftHand.localRotation = Quaternion.Euler(Player_RightHand.localRotation.eulerAngles.x + 90f, -Player_RightHand.localRotation.eulerAngles.y, -Player_RightHand.localRotation.eulerAngles.z);
            IKTarget_Head.localRotation = Quaternion.Euler(Player_Head.localRotation.eulerAngles.x, -Player_Head.localRotation.eulerAngles.y, -Player_Head.localRotation.eulerAngles.z);

            // IKTarget_RightHand.localPosition = Player_LeftHand.localPosition;
            // IKTarget_LeftHand.localPosition = Player_RightHand.localPosition;
            // IKTarget_Head.localPosition = Player_Head.localPosition;
            //
            //
            // IKTarget_RightHand.localRotation = Player_LeftHand.localRotation;
            // IKTarget_LeftHand.localRotation = Player_RightHand.localRotation;
            // IKTarget_Head.localRotation = Player_Head.localRotation;
        }
    }
}
