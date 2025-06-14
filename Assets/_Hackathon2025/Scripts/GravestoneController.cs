using System;
using NaughtyAttributes;
using UnityEngine;

public class GravestoneController : MonoBehaviour
{
    public GameObject normalGravestone, talkingGravestone;
    public SplineChurchPlayerController splineChurchPlayerController;

    private void Start()
    {
        normalGravestone.SetActive(true);
        talkingGravestone.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
       ChangeState();
    }

    [Button]
    private void ChangeState()
    {
        GlobalStateManager.Instance.TransitionTo(GlobalStateManager.ListeningState);
        normalGravestone.SetActive(false);
        talkingGravestone.SetActive(true);
        splineChurchPlayerController.SetSplineEndpoint();
    }
}
