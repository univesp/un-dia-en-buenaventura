using UnityEngine;
using UnityEngine.Events;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField] private ConversationScriptableObject[] _conversation;    
    [SerializeField] private UnityEvent[] _actions;
    private bool[] _actionExecuted;
    [SerializeField] private bool _repeatAction;
    private int _currentConversationIndex;

    [SerializeField] private bool _callAtStart;
    [SerializeField] private float _startDelay;

    private void Start()
    {
        if(_callAtStart)
        {
            Invoke("StartDialogue", _startDelay);
        }

        _actionExecuted = new bool[_actions.Length];
    }

    public void StartDialogue()
    {
        if(_repeatAction)
        {
            _actionExecuted[_currentConversationIndex] = false;
        }
        DialogueReader.Instance.SetStartDialogue(this);
    }

    public bool CheckIsPlayer(int dialogueIndex)
    {
        //Se o speaker não tiver sprite, é o jogador. Se ele tiver, é npc
        if(_conversation[_currentConversationIndex].Dialogue[dialogueIndex].Speaker.Sprite == null)
        {
            return true;
        }
        return false;
    }

    public bool CheckForExistingDialogue(int dialogueIndex)
    {
        if (dialogueIndex < _conversation[_currentConversationIndex].Dialogue.Length && dialogueIndex >= 0)
        {
            return true;
        }
        return false;
    }

    public int GetDialogueLenght()
    {
        return _conversation[_currentConversationIndex].Dialogue.Length;

    }

    public bool CheckForExistingLine(int dialogueIndex, int lineIndex)
    {
        if (dialogueIndex >= _conversation[_currentConversationIndex].Dialogue.Length)
        {
            dialogueIndex = _conversation[_currentConversationIndex].Dialogue.Length - 1;
        }
        
        if (lineIndex < _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines.Length && lineIndex >= 0)
        {            
            return true;
        }
        return false;
    }

    public string GetLine(int dialogueIndex, int lineIndex)
    {
        //Trata o texto para mudar os pronomes de gênero
        if(_conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Contains("NOME_JOGADOR"))
        {
            _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex] = _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Replace("NOME_JOGADOR", PlayerPrefs.GetString("nome_jogador", "Jogador"));
        }

        if (_conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Contains("SENHOR"))
        {
            if(PlayerPrefs.GetInt("player_gender", 0) == 0)
            {
                _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex] = _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Replace("SENHOR", "señor");
            }            
            else
            {
                _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex] = _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Replace("SENHOR", "señora");
            }
        }

        if (_conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Contains("SR"))
        {
            if (PlayerPrefs.GetInt("player_gender", 0) == 0)
            {
                _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex] = _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Replace("SR", "Sr.");
            }
            else
            {
                _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex] = _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Replace("SR", "Sra.");
            }
        }

        if (_conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Contains("NOME_COMIDA"))
        {
            _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex] = _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Replace("NOME_COMIDA", PlayerPrefs.GetString("food_name", "comida"));

        }

        if (_conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Contains("ELE"))
        {
            if (PlayerPrefs.GetInt("player_gender", 0) == 0)
            {
                _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex] = _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Replace("ELE", "el");
            }
            else
            {
                _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex] = _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Replace("ELE", "ella");
            }
        }

        if (_conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Contains("COR_ROUPA"))
        {
            _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex] = _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex].Replace("COR_ROUPA", PlayerPrefs.GetString("shirt_color", "cor"));

        }

        return _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines[lineIndex];
    }

    public int GetLineLenght(int dialogueIndex)
    {
        return _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Lines.Length;
    }

    public string GetSpeakerName(int dialogueIndex)
    {
        return _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Speaker.Name;
        
    }

    public Sprite GetSpeakerSprite(int dialogueIndex)
    {
        return _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Speaker.Sprite;        
    }

    public Vector2 GetSpeakerPosition(int dialogueIndex)
    {
        return new Vector2(_conversation[_currentConversationIndex].Dialogue[dialogueIndex].Speaker.SpriteXPos, _conversation[_currentConversationIndex].Dialogue[dialogueIndex].Speaker.SpriteYPos);               
    }

    public DialogueLine.SpeakerAction GetSpeakerAction(int dialogueIndex)
    {
        return _conversation[_currentConversationIndex].Dialogue[dialogueIndex].EndAction;
    }

    public bool CheckActions()
    {
        //Verifica se as ações existem no evento
        if (_actions.Length > 0 && _currentConversationIndex < _actions.Length && !_actionExecuted[_currentConversationIndex])
        {
            return true;
        }
        return false;
    }

    public void InvokeActions()
    {
        _actions[_currentConversationIndex].Invoke();
        _actionExecuted[_currentConversationIndex] = true;
    }

    public void NextConversationIndex()
    {
        _currentConversationIndex++;
    }
}