using UnityEngine;

public class GravestoneController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GlobalStateManager.Instance.TransitionTo(GlobalStateManager.ListeningState);
    }
}
