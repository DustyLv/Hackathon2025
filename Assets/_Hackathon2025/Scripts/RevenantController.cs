using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines;

public class RevenantController : MonoBehaviour
{
    public Transform playerTransform;

    public Animator animator;
    public SplineAnimate splineToPlayer;
    public AudioSource audioSource;

    public AudioClip audioClip_HowYouDoin;

    private void _ListeningState_OnEnter(GlobalStateManager._GlobalStateManager._State state) => Sequence_WalkToPlayer();

    private void Start()
    {
        splineToPlayer.Completed += Sequence_StandingAtPlayer;
        GlobalStateManager.ListeningState.OnEnter += _ListeningState_OnEnter;
        Debug.Log("REMOVE ME!!!");
        GlobalStateManager.Instance.TransitionTo(GlobalStateManager.ListeningState);
    }

    private void OnDisable()
    {
        GlobalStateManager.ListeningState.OnEnter -= _ListeningState_OnEnter;
        splineToPlayer.Completed -= Sequence_StandingAtPlayer;
    }

    [Button]
    public void Sequence_WalkToPlayer()
    {
        Anim_Walk();
        WalkToPlayer();
    }

    public void Sequence_StandingAtPlayer()
    {
        GlobalStateManager.Instance.TransitionTo(GlobalStateManager.PstKidState);
        Anim_Idle();
        audioSource.clip = audioClip_HowYouDoin;
        audioSource.Play();
    }

    public void Anim_Walk()
    {
        animator.SetBool("IsWalking", true);
    }

    public void Anim_Idle()
    {
        animator.SetBool("IsWalking", false);
    }

    public void WalkToPlayer()
    {
        splineToPlayer.Play();
    }
}
