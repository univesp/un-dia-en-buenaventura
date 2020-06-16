using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SencentesController : MonoBehaviour
{
    [SerializeField] private Animator _titleAnimator;
    [SerializeField] private TextMeshProUGUI _npcName;
    [SerializeField] private TextMeshProUGUI _npcText;

    [SerializeField] private Animator _sentenceAnimator;
    [SerializeField] private GameObject _buttonsPrefab;
    [SerializeField] private Transform _unnuedButtons;
    [SerializeField] private Transform _usedButtons;

    [SerializeField] private GameObject _endButton;
    private UnityEvent _rightAnswer;
    private UnityEvent[] _wrongAnswer;
    private int _wrongIndex;

    private List<SentencesButton> _sentenceButtons;
    private int _buttonCount;

    [SerializeField] private AudioClip _errorSFX;
    [SerializeField] private AudioClip _rightSFX;

    public static SencentesController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void FillData(SentencesHolder sentence)
    {
        //Pega o nome e o texto do NPC
        _npcName.text = sentence.NpcName;
        _npcText.text = sentence.NpcText;

        //Pega os eventos de certo e errado
        _rightAnswer = sentence.RightAnswer;
        _wrongAnswer = sentence.WrongAnswer;

        //Zera o contador de botões
        _buttonCount = 0;

        //Reseta o contador erros
        _wrongIndex = 0;

        //Inicializa a lista
        _sentenceButtons = new List<SentencesButton>();

        //Cria todos os botões que o jogador precisa usar
        for (int i = 0; i < sentence.ButtonsData.Length; i++)
        {
            //Instancia o botão no Transform fora dos botões não usados para criar o tamanho certo
            GameObject button = Instantiate(_buttonsPrefab, _unnuedButtons.parent);
            //Passa eles para o Transform certo
            button.transform.SetParent(_unnuedButtons);
            //Pega o texto do botão
            button.GetComponentInChildren<TextMeshProUGUI>().text = sentence.ButtonsData[i].ButtonText;
            //Pega o índice correto do botão na sentença
            button.GetComponent<SentencesButton>().IndexInSentence = sentence.ButtonsData[i].IndexInSentence;
            //Adiciona 1 na contagem de botões
            _buttonCount++;
        }
        StartCoroutine(FixButtonsPosition());
    }

    private IEnumerator FixButtonsPosition()
    {
        yield return new WaitForEndOfFrame();
        //Deixa os botões invisíveis
        _unnuedButtons.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();
        //Deixa os botões visíveis
        _unnuedButtons.gameObject.SetActive(true);
    }

    public void SelectWord(SentencesButton button)
    {
        button.transform.SetParent(_usedButtons);
        _sentenceButtons.Add(button);

        if(_sentenceButtons.Count == _buttonCount)
        {
            _endButton.SetActive(true);
        }
    }

    public void DeselectWord(SentencesButton button)
    {
        button.transform.SetParent(_unnuedButtons);
        foreach(SentencesButton sentenceButton in _sentenceButtons)
        {
            if(sentenceButton.IndexInSentence == button.IndexInSentence)
            {
                _sentenceButtons.Remove(sentenceButton);
                break;
            }
        }
        _endButton.SetActive(false);
    }

    public void CheckSentence()
    {
        for(int i = 0; i < _sentenceButtons.Count; i++)
        {
            if(i != _sentenceButtons[i].IndexInSentence)
            {
                StartCoroutine(WrongAnswer());
                return;
            }
        }
        StartCoroutine(RightAnswer());
    }

    private IEnumerator WrongAnswer()
    {
        //Toca som de erro
        AudioPlayer.Instance.PlaySFX(_errorSFX);
        //Toca as animações das janelas saindo
        _titleAnimator.Play("minigameTitle_exit");
        yield return new WaitForSeconds(0.1f);
        _sentenceAnimator.Play("sentence_exit");
        yield return new WaitForSeconds(0.5f);        

        //Limpa a jogada para o jogador tentar de novo
        _sentenceButtons.Clear();

        //Deixa o botão final invisível
        _endButton.SetActive(false);

        //Verifica se esse é o último erro que o jogador pode cometer
        if (_wrongIndex == _wrongAnswer.Length-1)
        {
            //Destrói todos os filhos que estão nos botões usados
            foreach (Transform usedButton in _usedButtons)
            {
                Destroy(usedButton.gameObject);
            }
        }
        else
        {
            //Fiz assim porque o foreach não estava funcionando aqui
            while(_usedButtons.childCount != 0)
            {
                _usedButtons.GetChild(0).SetParent(_unnuedButtons);
            }
        }

        //Invoca as ações do erro
        _wrongAnswer[_wrongIndex].Invoke();
        //Aumenta a contagem do erro em 1
        _wrongIndex++;

        gameObject.SetActive(false);
    }

    private IEnumerator RightAnswer()
    {
        //Toca som de acerto
        AudioPlayer.Instance.PlaySFX(_rightSFX);

        //Toca as animações das janelas saindo
        _titleAnimator.Play("minigameTitle_exit");
        yield return new WaitForSeconds(0.1f);
        _sentenceAnimator.Play("sentence_exit");
        yield return new WaitForSeconds(0.5f);

        //Invoca as ações do acerto
        _rightAnswer.Invoke();

        //Limpa a jogada para o jogador tentar de novo
        _sentenceButtons.Clear();

        //Destrói todos os filhos que estão nos botões usados
        foreach (Transform usedButton in _usedButtons)
        {
            Destroy(usedButton.gameObject);
        }

        //Deixa o botão final invisível
        _endButton.SetActive(false);

        gameObject.SetActive(false);
    }
}
