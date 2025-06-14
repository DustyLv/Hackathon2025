using NaughtyAttributes;
using UnityEngine;

public class ChurchController : MonoBehaviour
{
    public AudioSource churchSoundEffect;

    private void _PstKidState_OnEnter(GlobalStateManager._GlobalStateManager._State state) => Ding();

    private void Start()
    {
        GlobalStateManager.PstKidState.OnEnter += _PstKidState_OnEnter;
    }

    private void OnDisable()
    {
        GlobalStateManager.PstKidState.OnEnter -= _PstKidState_OnEnter;
    }

    [Button]
    public void Ding()
    {
        churchSoundEffect.Play();
        GlobalStateManager.Instance.TransitionTo(GlobalStateManager.WaitingForPlayerToTurnAroundState);
    }
}
