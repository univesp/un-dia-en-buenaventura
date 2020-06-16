using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ChoicesFiller : MonoBehaviour
{ 
    [SerializeField] private Animator _titleAnimator;
    [SerializeField] private TextMeshProUGUI _npcName;
    [SerializeField] private TextMeshProUGUI _npcText;

    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private Animator[] _buttonsAnimator;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    [SerializeField] private UnityEvent[] _actions;

    public static ChoicesFiller Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetActive(false);
        }
    }

    public void FillData(ChoicesHolder choices)
    {
        //Pega os textos do NPC        
        _npcName.text = choices.NpcName;
        if (_npcName.text.Contains("NOME_JOGADOR"))
        {
            _npcName.text = _npcName.text.Replace("NOME_JOGADOR", PlayerPrefs.GetString("nome_jogador", "Jogador"));
        }
        _npcText.text = choices.NpcText;

        StartCoroutine(SetButtons(choices));
    }

    private IEnumerator SetButtons(ChoicesHolder choices)
    {
        //Aguarda animação do título entrando
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < choices.Choices.Length; i++)
        {
            //Inicia o botão
            _buttons[i].SetActive(true);
            //Atribui a função ao botão            
            _actions[i] = choices.Choices[i].Actions;
            //Pega o texto do botão
            _buttonsText[i].text = choices.Choices[i].ChoiceText;

            yield return new WaitForSeconds(0.3f);
        }
    }

    public void InvokeButtonAction(int index)
    {
        StartCoroutine(CloseButtons(index));        
    }

    private IEnumerator CloseButtons(int index)
    {
        for (int i = _buttons.Length - 1; i >= 0 ; i--)
        {
            if (!_buttons[i].activeSelf)
            {
                continue;
            }
            _buttonsAnimator[i].Play("choiceButton_exit");
            yield return new WaitForSeconds(0.3f);
        }

        _titleAnimator.Play("minigameTitle_exit");

        yield return new WaitForSeconds(0.5f);      

        _actions[index].Invoke();
        gameObject.SetActive(false);
    }
}
