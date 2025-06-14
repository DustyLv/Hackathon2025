using RootMotion.FinalIK;
using UnityEngine;

public class IKController : MonoBehaviour
{
    public RevenantController revenant;
    public bool ikActive = true;
    public Transform IKTarget_RightHand = null;
    public Transform IKTarget_LeftHand = null;
    public Transform IKTarget_Head = null;

    public Transform Player_RightHand = null;
    public Transform Player_LeftHand = null;
    public Transform Player_Head = null;

    public FullBodyBipedIK FullBodyBipedIK = null;
    public FBBIKHeadEffector FBBIKHeadEffector = null;
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
        Invoke(nameof(StopPossessedState), 50f);
    }

    public void LateUpdate()
    {
        if (!ikActive) return;

        Vector3 right = Player_LeftHand.localPosition;
        Vector3 left = Player_RightHand.localPosition;
        Vector3 head = Player_Head.localPosition;

        Quaternion rightR = Player_LeftHand.localRotation;
        Quaternion leftR = Player_RightHand.localRotation;
        Quaternion headR = Player_Head.localRotation;

        IKTarget_RightHand.localPosition = new Vector3(right.x, right.y, right.z);
        IKTarget_LeftHand.localPosition = new Vector3(left.x, left.y, left.z);
        IKTarget_Head.localPosition = new Vector3(-head.x, head.y, head.z);

        IKTarget_RightHand.localRotation = Quaternion.Euler(rightR.eulerAngles.x + 90f, -rightR.eulerAngles.y, -rightR.eulerAngles.z);
        IKTarget_LeftHand.localRotation = Quaternion.Euler(leftR.eulerAngles.x + 90f, -leftR.eulerAngles.y, -leftR.eulerAngles.z);
        IKTarget_Head.localRotation = Quaternion.Euler(headR.eulerAngles.x, -headR.eulerAngles.y, -headR.eulerAngles.z);
    }

    public void StopPossessedState()
    {
        ikActive = false;
        FullBodyBipedIK.solver.IKPositionWeight = 0f;
        FBBIKHeadEffector.positionWeight = 0f;
        FBBIKHeadEffector.rotationWeight = 0f;
        revenant.animator.SetTrigger("Attack");
    }
}
