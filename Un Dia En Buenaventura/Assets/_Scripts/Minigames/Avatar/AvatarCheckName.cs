using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class AvatarCheckName : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _errorSFX;
    [SerializeField] private AudioClip _rightSFX;

    [SerializeField] private UnityEvent _actions;

    public void CheckName()
    {
        if(_nameInput.text == "" || _nameInput == null)
        {
            _animator.Play("nameInput_error");
            AudioPlayer.Instance.PlaySFX(_errorSFX);
        }
        else
        {
            AudioPlayer.Instance.PlaySFX(_rightSFX);
            _actions.Invoke();
        }
    }
}
