using UnityEngine;
using UnityEngine.Video;

public class AvatarAnimationController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Animator animator;

    void Start()
    {
        if (!videoPlayer) Debug.LogError("Video Player missing!");
        if (!animator) Debug.LogError("Animator missing!");
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Play();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        vp.Play();
        animator.Play("DemoMovements"); // Generic doesn’t need layer/time
        Debug.Log("Video and animation started");
    }
}