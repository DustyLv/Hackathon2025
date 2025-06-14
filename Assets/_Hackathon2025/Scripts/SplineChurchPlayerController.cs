using UnityEngine;
using UnityEngine.Splines;

public class SplineChurchPlayerController : MonoBehaviour
{
    public Transform playerTransform;
    public SplineContainer splineContainer;
    public Transform revenantTransform;
    public void SetSplineEndpoint()
    {
        var length = splineContainer.Spline.Count - 1;
        var firstKnot = splineContainer.Spline.ToArray()[length];
        Vector3 playerPosition = new Vector3(playerTransform.position.x, 0f, playerTransform.position.z);
        Vector3 offset = revenantTransform.position - playerPosition;
        // offset.y = 0f;
        var normalizedOffset = offset.normalized;
        offset -= normalizedOffset;
        normalizedOffset *= 1.5f;
        
        
        // offset -= Vector3.one;
        offset.y = 0f;
        // normalizedOffset.y = 0f;
        firstKnot.Position = splineContainer.transform.InverseTransformPoint(playerPosition + normalizedOffset);
        firstKnot.Position.y = 0f;
        firstKnot.Rotation = Quaternion.Inverse(splineContainer.transform.rotation) * playerTransform.rotation;

        splineContainer.Spline.SetKnot(length, firstKnot);
    }
}