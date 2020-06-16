using UnityEngine;

public class EventSwitch : MonoBehaviour
{
    //Os bools são inseridos manualmente de acordo com a necessidade ao longo do projeto.
    //Não esquecer de inserir novos bools no GetEvent, no SetEvent e no ResetEvents
    private bool event1;

    public static EventSwitch Instance;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    //Retorna o estado do evento que o outro script está tentando acessar
    public bool GetEvent(string eventName)
    {
        switch(eventName)
        {
            case "event1":
                return event1;

            default:
                Debug.LogError("There's no event with this name: " + eventName);
                return false;
        }        
    }

    //Define o estado do evento determinado
    public void SetEvent(string eventName, bool eventState)
    {
        switch(eventName)
        {
            case "event1":
                event1 = eventState;
                break;

            default:
                Debug.LogError("There's no event with this name: " + eventName);
                break;
        }
    }

    public void ResetEvents()
    {
        event1 = false;
    }
}