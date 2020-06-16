using UnityEngine;
using TMPro;

public class ChangePlayerNameTag : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;

    private void Start()
    {
        if (_textMesh.text.Contains("NOME_JOGADOR"))
        {
            _textMesh.text.Replace("NOME_JOGADOR", PlayerPrefs.GetString("nome_jogador", "jogador"));
        }
    }
}
