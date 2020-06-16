using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TransitionEffect : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _exitAnimationName;

    [SerializeField] private TextMeshProUGUI _transitionTextMesh;
    [SerializeField] private string _transitionText;

    [SerializeField] private UnityEvent _transitionActions;

    private void OnEnable()
    {
        _transitionTextMesh.text = "";
        if(_transitionText != "")
        {
            _transitionTextMesh.text = _transitionText;
        }
    }

    public void CallTransition()
    {
        StartCoroutine(TransitionSequence());
    }

    private IEnumerator TransitionSequence()
    {                    
        //Deixa o texto um tempo na tela
        yield return new WaitForSeconds(3.0f);

        //Toca a animação de saída
        _animator.Play(_exitAnimationName);

        //Espera a animação de entrada terminar
        yield return new WaitForSeconds(0.5f);

        //Invoca as ações depois que a animação acabar
        _transitionActions.Invoke();        
        //Desativa essa transição no final da animação de saída
        _animator.gameObject.SetActive(false);
    }
}
