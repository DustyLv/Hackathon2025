using NaughtyAttributes;
using UnityEngine;

public class ChurchController : MonoBehaviour
{
    private void _PstKidState_OnEnter(GlobalStateManager._GlobalStateManager._State state) => Ding();

    private void Start()
    {
        GlobalStateManager.PstKidState.OnEnter += _PstKidState_OnEnter;
    }

    private void OnDisable()
    {
        GlobalStateManager.PstKidState.OnEnter -= _PstKidState_OnEnter;
    }

    [Button]
    public void Ding() { Debug.Log("DING"); }
}
