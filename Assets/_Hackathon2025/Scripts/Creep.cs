using UnityEngine;

public class Creep : MonoBehaviour
{
    public float scaleValue = 0f;
    public AnimationCurve scaleCurve;
    public float timeScale = 0.5f;
    private float timer = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scaleValue = scaleCurve.Evaluate(timer);
        transform.localScale = Vector3.one + (Vector3.up * scaleValue);
        timer += Time.deltaTime * timeScale;
        if (timer >= 1f)
        {
            timer = 0f;
        }
    }
}
