using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    public Transform target;

    public bool x = false;
    public bool y = false;
    public bool z = false;

    public Vector3 pos;

    void LateUpdate()
    {
        pos = target.position;

        if (!x) pos.x = transform.position.x;
        if (!y) pos.y = transform.position.y;
        if (!z) pos.z = transform.position.z;
        transform.position = pos;
    }
}
