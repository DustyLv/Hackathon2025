using System;
using NaughtyAttributes;
using UnityEngine;

public class GravestoneController : MonoBehaviour
{
    public GameObject normalGravestone, talkingGravestone;
    public SplineChurchPlayerController splineChurchPlayerController;

    public GameObject flowersReal;
    public GameObject flowersGhost;

    private void Start()
    {
        flowersReal.SetActive(false);
        flowersGhost.SetActive(true);
        normalGravestone.SetActive(true);
        talkingGravestone.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Flowers")) return;
        other.gameObject.SetActive(false);
        ChangeState();
    }

    [Button]
    private void ChangeState()
    {
        GlobalStateManager.Instance.TransitionTo(GlobalStateManager.ListeningState);

        flowersReal.SetActive(true);
        flowersGhost.SetActive(false);

        normalGravestone.SetActive(false);
        talkingGravestone.SetActive(true);
        splineChurchPlayerController.SetSplineEndpoint();
    }
}