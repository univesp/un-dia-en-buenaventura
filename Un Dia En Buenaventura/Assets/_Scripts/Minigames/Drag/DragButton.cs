using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Variáveis do Drag And Drop
    private Camera _mainCamera;
    [SerializeField] private Canvas _canvas;

    private bool _isIn;
    private bool _isDragging;

    //Variáveis dos dados do botão
    [SerializeField] private Transform _buttonHolder;
    public bool IsInSpace;
    public bool IsCorrect;
    public int RightLinkIndex;
    public DragBlankSpace CurrentBlankSpace;

    //Cores do botão
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Color _errorColor;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _rightColor;

    [SerializeField] private AudioClip _clickSFX;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        DragAndDrop();
    }

    private void DragAndDrop()
    {
        //Quando clica no botão
        if (_isIn && Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Som de clique
            AudioPlayer.Instance.PlaySFX(_clickSFX);

            _isDragging = true;

            //Liga o raycast do espaço em branco se houver e avisa o controlador que tem um espaço livre
            if(CurrentBlankSpace != null)
            {
                CurrentBlankSpace.BlankSpaceImage.raycastTarget = true;
                DragController.Instance.CheckIfFinished();
            }

            //Desliga o Raycast para poder verificar a colisão com o texto
            _buttonImage.raycastTarget = false;

            //Tira ele do objeto que segura os botões
            transform.SetParent(_buttonHolder.parent.transform);
            DragController.Instance.RemoveFromDelegate(this);
            //Chega se todos os espaços estão usados
            DragController.Instance.CheckIfFinished();

            DragController.Instance.SetCurrentButton(this);            
        }

        //Enquanto está arrastando
        if (_isDragging)
        {
            transform.position = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _canvas.planeDistance));
        }

        //Quando solta o botão
        if (Input.GetKeyUp(KeyCode.Mouse0) && _isDragging)
        {
            _isDragging = false;

            //Reativa o Raycast do espaço em branco
            _buttonImage.raycastTarget = true;            

            //Se não estiver dentro do espaço em branco
            if (!IsInSpace)
            {
                //Devolve o botão pro objeto do layout
                transform.SetParent(_buttonHolder);
                //Tira esse botão do delegate
                DragController.Instance.RemoveFromDelegate(this);
                //Tira a referência desse botão do controlador
                DragController.Instance.SetCurrentButton(null);
                if (CurrentBlankSpace != null)
                {
                    CurrentBlankSpace.SetNormalColor();
                }
                //Tira a referência do espaço em branco
                CurrentBlankSpace = null;
            }
            //Se estiver dentro do espaço em branco
            else
            {
                DragController.Instance.AddToDelegate(this);
                //Coloca esse botão na mesma posição do espaço
                transform.position = CurrentBlankSpace.transform.position;
                //Desliga o raycast do espaço em branco
                CurrentBlankSpace.BlankSpaceImage.raycastTarget = false;
                //Chega se todos os espaços estão usados
                DragController.Instance.CheckIfFinished();
                CurrentBlankSpace.SetNormalColor();
                //Tira a referência desse botão do controlador
                DragController.Instance.SetCurrentButton(null);
                //Já verifica se o botão foi colocado ou não no espaço correto
                if (CurrentBlankSpace.RightIndex == RightLinkIndex)
                {
                    IsCorrect = true;
                }
                else
                {
                    IsCorrect = false;
                }
            }
        }
    }

    public void CheckIfCorrect()
    {
        if(!IsCorrect)
        {
            StartCoroutine(ChangeErrorColor());
        }
        else
        {
            _buttonImage.color = _rightColor;
            _buttonImage.raycastTarget = false;
            StartCoroutine(RightAnswer());
        }
    }

    private IEnumerator ChangeErrorColor()
    {
        //Pinta com a cor errada
        _buttonImage.color = _errorColor;
        CurrentBlankSpace.BlankSpaceImage.raycastTarget = true;
        //Limpa a referência ao espaço em branco
        CurrentBlankSpace = null;        

        yield return new WaitForSeconds(1.0f);

        //Pinta com a cor normal
        _buttonImage.color = _normalColor;
        //Devolve o botão para o objeto com o layout
        transform.SetParent(_buttonHolder);
        //Tira esse botão do delegate
        DragController.Instance.RemoveFromDelegate(this);
    }

    private IEnumerator RightAnswer()
    {
        yield return new WaitForSeconds(1.0f);        
        DragController.Instance.BlankSpaceQuantity--;
        //Tira esse botão do delegate
        DragController.Instance.RemoveFromDelegate(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isIn = false;
    }
}