using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class VideoPlayerController : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private UnityEvent _actions;

    private void Start()
    {
        _videoPlayer.url = "https://apps.univesp.br/teste_buenaventura/StreamingAssets/Cena5_trabalhando_v2.m4v";
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        yield return new WaitForSeconds(2.0f);
        _videoPlayer.Play();
        yield return new WaitForSeconds((float)_videoPlayer.length);
        _animator.Play("video_exit");
        yield return new WaitForSeconds(1.0f);
        _actions.Invoke();
    }
}
