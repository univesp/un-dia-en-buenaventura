using UnityEngine;
using TMPro;

public class FontSizeChanger : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        //Pega o componente do texto
        _text = GetComponent<TextMeshProUGUI>();
        
        //Inicializa com o último tamanho definido nas opções
        _text.fontSize = PlayerPrefs.GetFloat("font_size", 46.5f);

        //Salva o método de atualizar o tamanho no delegate
        FontSizeController.Instance.SizeChangeDelegate += UpdateSize;
    }

    private void UpdateSize()
    {
        _text.fontSize = PlayerPrefs.GetFloat("font_size", 46.5f);
    }

    private void OnDestroy()
    {
        //Remove o método de atualizar o tamanho no delegate
        FontSizeController.Instance.SizeChangeDelegate -= UpdateSize;
    }
}