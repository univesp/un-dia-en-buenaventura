using UnityEngine;
using UnityEngine.UI;

public class ScrollFix : MonoBehaviour
{
    [SerializeField] private Scrollbar _sb;

    private void Start()
    {
        //Pega o componente no game object
        _sb = gameObject.GetComponent<Scrollbar>();
        //Começa a barra no topo
        _sb.value = 1;
        //Força o tamanho da área de rolagem
        _sb.size = 0;
    }

    //Isso é chamado pelo Scroll Rect
    public void ForceSize()
    {
        _sb.size = 0;
    }
}