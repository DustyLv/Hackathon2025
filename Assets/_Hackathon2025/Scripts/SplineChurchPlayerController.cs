using UnityEngine;
using UnityEngine.Splines;

public class SplineChurchPlayerController : MonoBehaviour
{
    public Transform playerTransform;
    public SplineContainer splineContainer;
    public Transform revenantTransform;
    public void SetSplineEndpoint()
    {
        Vector3 target = playerTransform.position;
        Vector3 pos = revenantTransform.position;
        Vector3 posRelToTarget = pos - target;
        Vector3 stop = posRelToTarget.normalized;
        Vector3 stopGlobal = target + stop;
        int id = splineContainer.Spline.Count - 1;
        BezierKnot knot = splineContainer.Spline.ToArray()[id];
        knot.Position = splineContainer.transform.InverseTransformPoint(new Vector3(stopGlobal.x, 0, stopGlobal.z));
        knot.Rotation = Quaternion.Euler(0, playerTransform.rotation.eulerAngles.y, 0);
        splineContainer.Spline.SetKnot(id, knot);
    }
}
