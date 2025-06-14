using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class RevenantRiseMaster : MonoBehaviour
{
    public List<RevenantRise> revenantRiseOrderList = new List<RevenantRise>();
    public Transform playerTransform;
public Transform revenantTransform;
public AudioSource chantAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     // RiseRevenants();
    // }
    public static RevenantRiseMaster Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GlobalStateManager.PossessedState.OnEnter += _PossessedState_OnEnter;
        Init();
    }

    private void OnDisable()
    {
        GlobalStateManager.PossessedState.OnEnter -= _PossessedState_OnEnter;
    }

    private void _PossessedState_OnEnter(GlobalStateManager._GlobalStateManager._State state)
    {
        RiseRevenants();
    }

    private void Init()
    {
        foreach (RevenantRise revenantRise in revenantRiseOrderList)
        {
            revenantRise.gameObject.SetActive(false);
        }
    }

    [Button]
    public void RiseRevenants()
    {
        StartCoroutine("Sequence_RiseRevenants");
    }

    public IEnumerator Sequence_RiseRevenants()
    {
        transform.position = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);
        transform.forward = -revenantTransform.forward;

        yield return new WaitForSeconds(10f);

        foreach (RevenantRise revenantRise in revenantRiseOrderList)
        {
            revenantRise.gameObject.SetActive(true);
            revenantRise.Rise();
            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(0.5f);
        foreach (RevenantRise revenantRise in revenantRiseOrderList)
        {
            revenantRise.Chant();
        }
        yield return new WaitForSeconds(1.5f);
        chantAudio.Play();
    }

    public void StopChanting()
    {
        foreach (RevenantRise revenantRise in revenantRiseOrderList)
        {
            revenantRise.Chant();
        }
        chantAudio.Stop();
    }
}