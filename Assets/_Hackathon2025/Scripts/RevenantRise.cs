using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RevenantRise : MonoBehaviour
{
    public Animator animator;

    public Vector3 startPositionOffset;
    public AudioSource riseSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        riseSound = GetComponentInChildren<AudioSource>();
        transform.position = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);
        // Rise();
    }


    public void Rise()
    {
        transform.DOMoveY(-2f, 0f).OnStart(() =>
        {
            animator.SetTrigger("Rise");
            riseSound.PlayDelayed(2f);
        }).OnComplete(() => { transform.DOMoveY(0f, 7f).OnComplete(() => { riseSound.Stop(); }); });


        // StartCoroutine("IE_Rise");
    }

    IEnumerator IE_Rise()
    {
        transform.localPosition -= startPositionOffset;
        animator.SetTrigger("Rise");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Standing"))
        {
            yield return null;
        }

        transform.localPosition += startPositionOffset;
    }

    public void Chant()
    {
        animator.SetBool("Chanting", true);
    }
}