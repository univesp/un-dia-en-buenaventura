using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueReader : MonoBehaviour
{
    [Header("Dialogue Box")]
    [SerializeField] private Animator _dialogueBoxAnimator;
    [SerializeField] private Image _lineBgImage;
    [SerializeField] private Color _lineBgNpcColor;
    [SerializeField] private Color _lineBgPlayerColor;
    [SerializeField] private GameObject _blurImage;

    [Header("NPC")]
    [SerializeField] private Animator _npcAnimator;
    [SerializeField] private Image _npcImage;
    private float _npcImageXPos;
    private float _npcImageYpos;

    [Header("Player")]
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Button[] _playerImage;
    [SerializeField] private ColorBlock _inactiveColor;
    [SerializeField] private ColorBlock _activeColor;

    [Header("Conversation")]
    [SerializeField] private DialogueHolder _dialogueHolder;
    private int _dialogueIndex;
    private int _lineIndex;
    [SerializeField] private TextMeshProUGUI _speakerName;
    [SerializeField] private TextMeshProUGUI _lineText;
    [SerializeField] private bool _isEnd;

    [Header("Letter by Letter")]
    private int _totalVisibleCharacters;

    [Header("Sound Effect")]
    [SerializeField] private AudioClip _clickSFX;

    public static DialogueReader Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetStartDialogue(DialogueHolder currentDialogue)
    {
        StopAllCoroutines();
        _isEnd = false;
        _dialogueHolder = currentDialogue;
        _dialogueIndex = 0;
        _lineIndex = -1;

        //Faz a caixa de diálogo aparecer
        _dialogueBoxAnimator.gameObject.SetActive(true);
        _blurImage.SetActive(true);

        //Define a aparência do personagem
        SetSpeakerAppearance();

        //Chama o método de imprimir o texto depois de meio segundo de delay, para dar tempo da animação tocar  
        _lineText.text = "";
        ShowLine(1);
    }

    public void ShowLine(int direction)
    {
        if (!_isEnd)
        {
            _lineIndex += direction;

            AudioPlayer.Instance.PlaySFX(_clickSFX);

            //Se a linha não existir, ele vai para o próximo diálogo
            if (!_dialogueHolder.CheckForExistingLine(_dialogueIndex, _lineIndex))
            {
                ChangeDialogue(direction);
            }
            else
            {
                //Imprime a linha na tela letra por letra            
                _lineText.text = _dialogueHolder.GetLine(_dialogueIndex, _lineIndex);
                //Deixa a quantidade inicial de caracteres visíveis no zero
                _lineText.maxVisibleCharacters = 0;

                StartCoroutine(ShowLetterByLetter(_lineText));
            }
            _lineIndex = _lineIndex < 0 ? 0 : _lineIndex;
        }
    }

    private void ChangeDialogue(int direction)
    {        
        _dialogueIndex += direction;
        StartCoroutine(SpeakerState(direction));

        if (_dialogueHolder.CheckForExistingDialogue(_dialogueIndex))
        {
            //Reseta a linha e chama para imprimir a linha de novo
            if (direction > 0)
            {
                _lineIndex = -1;
            }
            else
            {
                _lineIndex = _dialogueHolder.GetLineLenght(_dialogueIndex);
            }            
            ShowLine(direction);
        }

        //Não deixa o índice ser menor que 0
        _dialogueIndex = _dialogueIndex < 0 ? 0 : _dialogueIndex;

        //Impede o diálogo de continuar após acabar
        if (_dialogueIndex == _dialogueHolder.GetDialogueLenght())
        {
            _isEnd = true;
        }

        //Chama as ações cadastradas no diálogo
        if (_dialogueIndex == _dialogueHolder.GetDialogueLenght())
        {
            //Se não tiver ações para executar ele fecha a caixa de diálogo
            if (!_dialogueHolder.CheckActions())
            {            
                StartCoroutine(CloseDialogueBox());
            }
            else
            {
                _dialogueHolder.InvokeActions();
            }
        }
    }

    private void SetSpeakerAppearance()
    {
        //Verifica se a primeira fala é do jogador ou do NPC e atribui a aparência e o nome correto
        if (_dialogueHolder.CheckIsPlayer(_dialogueIndex))
        {
            _speakerName.text = PlayerPrefs.GetString("nome_jogador", "Jogador");
            _playerAnimator.gameObject.SetActive(true);            
            _lineBgImage.color = _lineBgPlayerColor;

            _npcImage.color = Color.gray;
            foreach(Button image in _playerImage)
            {
                image.colors = _activeColor;
            }
        }
        else
        {
            _speakerName.text = _dialogueHolder.GetSpeakerName(_dialogueIndex);
            _npcImage.sprite = _dialogueHolder.GetSpeakerSprite(_dialogueIndex);
            _npcImage.SetNativeSize();
            _npcImage.transform.localPosition = _dialogueHolder.GetSpeakerPosition(_dialogueIndex);
            _npcAnimator.gameObject.SetActive(true);
            _lineBgImage.color = _lineBgNpcColor;

            _npcImage.color = Color.white;
            foreach (Button image in _playerImage)
            {
                image.colors = _inactiveColor;
            }
        }        
    }

    private IEnumerator SpeakerState(int direction)
    {
        if (direction > 0)
        {
            switch (_dialogueHolder.GetSpeakerAction(_dialogueIndex - 1))
            {                
                //Troca de NPC
                case DialogueLine.SpeakerAction.Change:
                    _npcAnimator.Play("npc_exit");
                    yield return new WaitForSeconds(0.5f);
                    SetSpeakerAppearance();
                    _npcAnimator.Play("npc_enter");
                    break;

                //Apenas faz o jogador ou o NPC saírem da tela
                case DialogueLine.SpeakerAction.Leave:
                    SetSpeakerAppearance();
                    if (_dialogueHolder.CheckIsPlayer(_dialogueIndex-1))
                    {
                        _playerAnimator.Play("player_exit");
                    }
                    else
                    {
                        _npcAnimator.Play("npc_exit");
                    }                    
                    break;

                case DialogueLine.SpeakerAction.End:
                    StartCoroutine(CloseDialogueBox());
                    break;

                default:
                    //Verifica se é o último diálogo. Se for, ele não faz nada
                    if (_dialogueIndex != _dialogueHolder.GetDialogueLenght())
                    {
                        SetSpeakerAppearance();
                    }
                    break;
            }
        }
        else
        {
            SetSpeakerAppearance();
        }
    }

    private IEnumerator ShowLetterByLetter(TextMeshProUGUI currentTextMesh)
    {
        //Espera o fim do frame para pegar a quantidade certa de palavras na frase
        yield return new WaitForEndOfFrame();

        //Pega a quantidade total de caracteres do texto
        _totalVisibleCharacters = currentTextMesh.textInfo.characterCount;

        //Espera o tempo para tornar visível letra por letra
        for (int i = 0; i <= _totalVisibleCharacters; i++)
        {
            currentTextMesh.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.01f);
        }
    }    

    private IEnumerator CloseDialogueBox()
    {
        _dialogueBoxAnimator.Play("dialogueBox_exit");
        _npcAnimator.Play("npc_exit");
        _playerAnimator.Play("player_exit");

        yield return new WaitForSeconds(1.0f);

        _dialogueBoxAnimator.gameObject.SetActive(false);
        _blurImage.SetActive(false);
        _npcAnimator.gameObject.SetActive(false);
        _playerAnimator.gameObject.SetActive(false);
    }
}
