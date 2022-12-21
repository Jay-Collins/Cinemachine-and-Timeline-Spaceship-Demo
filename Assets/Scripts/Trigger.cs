using UnityEngine;
using UnityEngine.Playables;

public class Trigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!_director) return;
        _director.Play();
    }
}
