using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class RevenantController : MonoBehaviour
{
    public Transform playerTransform;

    public Animator animator;
    public SplineAnimate splineToPlayer;
    public AudioSource audioSource;

    public AudioClip audioClip_HowYouDoin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        splineToPlayer.Completed += Sequence_StandingAtPlayer;
    }

    private void OnDisable()
    {
        splineToPlayer.Completed -= Sequence_StandingAtPlayer;
    }

    // Update is called once per frame
    void Update()
    {
    }

    [Button]
    public void Sequence_WalkToPlayer()
    {
        Anim_Walk();
        WalkToPlayer();
    }

    public void Sequence_StandingAtPlayer()
    {
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