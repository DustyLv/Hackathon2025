using Meta.XR.MRUtilityKit;
using UnityEngine;

public class RoomRescan : MonoBehaviour
{
    // public MRUK mruk;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RescanRoom();
    }
    
    public void RescanRoom()
    {
        // mruk.ClearScene();
        MRUK.Instance.ClearScene();
        OVRScene.RequestSpaceSetup();
        MRUK.Instance.LoadSceneFromDevice();
    }


}
