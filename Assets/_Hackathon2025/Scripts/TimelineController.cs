using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector director;

    [Button]
    public void PlayTimeline()
    {
        director.Play();
    }
}