using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PairsController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PairsImageButton _currentPairsImageButton;
    private PairsNameButton _currentPairsNameButton;

    [SerializeField] private List<PairsImageButton> _imageButtons;
    [SerializeField] private List<PairsNameButton> _nameButtons;

    [SerializeField] private UnityEvent _actions;

    private int _pairsQuantity = 5;

    [SerializeField] private AudioClip _errorSFX;
    [SerializeField] private AudioClip _rightSFX;

    public static PairsController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCurrentImageButton(PairsImageButton imageButton)
    {
        if (_currentPairsImageButton != imageButton)
        {
            if(_currentPairsImageButton != null)
            {
                //Desseleciona a seleção anterior
                _currentPairsImageButton.SetNormalColor();
            }

            //Seleciona o botão
            _currentPairsImageButton = imageButton;
            _currentPairsImageButton.SetSelectedColor();

            //Verifica se tem par para ver se os pares batem
            if (_currentPairsNameButton != null)
            {
                CheckPairs();
            }
        }
        else
        {
            //Desseleciona o botão
            _currentPairsImageButton = null;
        }        
    }

    public void SetCurrentNameButton(PairsNameButton nameButton)
    {
        if (_currentPairsNameButton != nameButton)
        {
            if(_currentPairsNameButton != null)
            {
                //Desseleciona a seleção anterior
                _currentPairsNameButton.SetNormalColor();
            }

            //Seleciona o botão
            _currentPairsNameButton = nameButton;
            _currentPairsNameButton.SetSelectedColor();

            //Verifica se tem par para ver se os pares batem
            if (_currentPairsImageButton != null)
            {
                CheckPairs();
            }
        }
        else
        {
            //Desseleciona o botão
            _currentPairsNameButton = null;
        }
    }

    private void CheckPairs()
    {
        if(_currentPairsImageButton.PairIndex == _currentPairsNameButton.PairIndex)
        {
            //Toca som de acerto
            AudioPlayer.Instance.PlaySFX(_rightSFX);
            _pairsQuantity--;
            _currentPairsImageButton.SetCorrectColor();
            _currentPairsNameButton.SetCorrectColor();

            //Tira os botões das listas
            _imageButtons.Remove(_currentPairsImageButton);
            _nameButtons.Remove(_currentPairsNameButton);

            _currentPairsImageButton = null;
            _currentPairsNameButton = null;

            //Verifica se é o final do jogo. Se for, ele chama a ação
            if (_pairsQuantity == 0)
            {
                StartCoroutine(CallActions());
            }
        }
        else
        {
            //Funções do erro
            StartCoroutine(ChangeErrorColor());
        }        
    }

    private IEnumerator ChangeErrorColor()
    {
        //Toca som de erro
        AudioPlayer.Instance.PlaySFX(_errorSFX);

        _currentPairsImageButton.SetErrorColor();
        _currentPairsNameButton.SetErrorColor();

        //Trava os botões
        for(int i = 0; i < _imageButtons.Count; i++)
        {
            _imageButtons[i].FoodBackground.raycastTarget = false;
            _nameButtons[i].NameBackground.raycastTarget = false;
        }

        yield return new WaitForSeconds(1.0f);

        _currentPairsImageButton.SetNormalColor();
        _currentPairsNameButton.SetNormalColor();

        //Destrava os botões
        for (int i = 0; i < _imageButtons.Count; i++)
        {
            _imageButtons[i].FoodBackground.raycastTarget = true;
            _nameButtons[i].NameBackground.raycastTarget = true;
        }

        _currentPairsImageButton = null;
        _currentPairsNameButton = null;
    }

    private IEnumerator CallActions()
    {
        _animator.Play("restaurantMenu_exit");
        yield return new WaitForSeconds(1.0f);
        _actions.Invoke();
        gameObject.SetActive(false);
    }
}