using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.Video;

public class RevenantController : MonoBehaviour
{
    public Transform playerTransform;

    public Animator animator;
    public SplineAnimate splineToPlayer;
    public AudioSource audioSource;
    public AudioClip audioClip_HowYouDoin;
    public Camera mainCamera;
    public Renderer revenantRenderer;

    public VideoPlayer videoPlayer;

    private void _ListeningState_OnEnter(GlobalStateManager._GlobalStateManager._State state) => Sequence_WalkToPlayer();

    private void _SpottedState_OnEnter(GlobalStateManager._GlobalStateManager._State state) => Sequence_Spotted();

    private void Start()
    {
        splineToPlayer.Completed += Sequence_StandingAtPlayer;
        GlobalStateManager.SpottedState.OnEnter += _SpottedState_OnEnter;
        GlobalStateManager.ListeningState.OnEnter += _ListeningState_OnEnter;
        revenantRenderer.enabled = false;
        // Debug.Log("REMOVE ME!!!");
        // GlobalStateManager.Instance.TransitionTo(GlobalStateManager.ReadyState);
        // GlobalStateManager.Instance.TransitionTo(GlobalStateManager.ListeningState);
    }

    private void OnDisable()
    {
        GlobalStateManager.ListeningState.OnEnter -= _ListeningState_OnEnter;
        GlobalStateManager.SpottedState.OnEnter -= _SpottedState_OnEnter;
        splineToPlayer.Completed -= Sequence_StandingAtPlayer;
    }

    private void FixedUpdate()
    {
        if (!IsVisible()) return;
        // if (GlobalStateManager.State == GlobalStateManager.ListeningState)
        // { GlobalStateManager.Instance.TransitionTo(GlobalStateManager.SpottedState); }
        // else
        if (GlobalStateManager.State == GlobalStateManager.WaitingForPlayerToTurnAroundState)
        {
            GlobalStateManager.Instance.TransitionTo(GlobalStateManager.PossessedState);
        }
    }

    public bool IsVisible() => GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), revenantRenderer.bounds);

    public void Sequence_Spotted()
    {
        Anim_Idle();
        Debug.Log("NOTHING TO SEE HERE");
    }

    [Button]
    public void Sequence_WalkToPlayer()
    {
        Anim_Walk();
        StartCoroutine("WalkToPlayer");
        // WalkToPlayer();
    }

    public void Sequence_StandingAtPlayer()
    {
        GlobalStateManager.Instance.TransitionTo(GlobalStateManager.PstKidState);
        Anim_Idle();
        // audioSource.clip = audioClip_HowYouDoin;
        // audioSource.Play();
    }

    public void Anim_Walk()
    {
        animator.SetBool("IsWalking", true);
    }

    public void Anim_Idle()
    {
        animator.SetBool("IsWalking", false);
    }

    public IEnumerator WalkToPlayer()
    {
        yield return new WaitForSeconds(5f);
        revenantRenderer.enabled = true;
        splineToPlayer.Play();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        // Invoke(nameof(DelayedScreenFade), 5.4f);
        StartCoroutine(DelayedScreenFade());
    }

    private IEnumerator DelayedScreenFade()
    {
        yield return new WaitForSeconds(5.6f);
        OVRScreenFade.instance.FadeOut();
        yield return new WaitForSeconds(1.2f);
        videoPlayer.Stop();
        // Invoke(nameof(DelayedLoadScene), 13f);
    }

    private void DelayedLoadScene()
    {
        PlayerController.Instance.ResetTransform();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
