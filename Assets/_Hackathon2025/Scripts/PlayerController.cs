using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerTransform;

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }


    public void ResetTransform()
    {
        playerTransform.position = Vector3.zero;
        playerTransform.forward = Vector3.forward;
        
    }
}
