using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevenantRiseMaster : MonoBehaviour
{
    public List<RevenantRise> revenantRiseOrderList = new List<RevenantRise>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RiseRevenants();
    }


    
    public void RiseRevenants()
    {
        StartCoroutine("Sequence_RiseRevenants");
    }

    public IEnumerator Sequence_RiseRevenants()
    {
        foreach (RevenantRise revenantRise in revenantRiseOrderList)
        {
            revenantRise.Rise();
            yield return new WaitForSeconds(1.5f);
        }
    }
}
