using Meta.XR;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class InstantPlacementController : MonoBehaviour
{
    public OVRHand rightHand;
    public Transform rightControllerAnchor;
    public GameObject prefabToPlace;
    public EnvironmentRaycastManager raycastManager;
    
    public SpatialAnchorManager anchorManager;

    private bool isPlaced = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaced) return;
        bool isPinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        // rightHand.transform.position
        if (isPinching)
        {
            if (MRUK.Instance?.IsWorldLockActive != true)
            {
                // objectToPlace.AddComponent<OVRSpatialAnchor>();
                anchorManager.CreateSpatialAnchor(0, rightHand.transform);
                anchorManager.SaveLastCreatedAnchor();
                isPlaced = true;
            }

            // TryPlace(ray);
        }
    }
    
    private void TryPlace(Ray ray)
    {
       
        
        if (raycastManager.Raycast(ray, out var hit))
        {
            // var objectToPlace = Instantiate(prefabToPlace);
            // objectToPlace.transform.SetPositionAndRotation(
            //     hit.point,
            //     Quaternion.LookRotation(hit.normal, Vector3.up)
            // );

            // If no MRUK component is present in the scene, we add an OVRSpatialAnchor component
            // to the instantiated prefab to anchor it in the physical space and prevent drift.
           
        }
    }
}
