using System;
using UnityEngine;

public class IKController : MonoBehaviour
{

    public bool ikActive = true;
    public Transform IKTarget_RightHand = null;
    public Transform IKTarget_LeftHand = null;
    public Transform IKTarget_Head = null;

    public Transform Player_RightHand = null;
    public Transform Player_LeftHand = null;
    public Transform Player_Head = null;

    void Start ()
    {
        // animator = GetComponent<Animator>();
    }


    public void Update()
    {
        if (ikActive)
        {
            IKTarget_RightHand.localPosition = Player_RightHand.localPosition;
            IKTarget_LeftHand.localPosition = Player_LeftHand.localPosition;
            IKTarget_Head.localPosition = Player_Head.localPosition;
        }
    }
}
