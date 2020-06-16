using UnityEngine;
using System.Collections;
using TMPro;

//Responsável por instanciar os espaços em branco
public class DragTextFields : MonoBehaviour
{
    //Variáveis para operações do TextMesh Pro
    [SerializeField] private TextMeshProUGUI _text;

    //Variáveis do espaço em branco    
    [SerializeField] private GameObject _blankSpacePrefab;
    
    private Vector2 _linkSpaceSize; 

    private void OnEnable()
    {
        StartCoroutine(SpawnBlankSpaces());
    }

    private Vector2 CalcLinkCenterPosition(int linkIndex)
    {
        Transform m_Transform = gameObject.GetComponent<Transform>();

        Vector3 bottomLeft = Vector3.zero;
        Vector3 topRight = Vector3.zero;

        float maxAscender = -Mathf.Infinity;
        float minDescender = Mathf.Infinity;

        TMP_TextInfo textInfo = _text.textInfo;
        TMP_LinkInfo linkInfo = textInfo.linkInfo[linkIndex];
        TMP_CharacterInfo currentCharInfo = textInfo.characterInfo[linkInfo.linkTextfirstCharacterIndex + linkInfo.linkTextLength / 2];

        maxAscender = Mathf.Max(maxAscender, currentCharInfo.ascender);
        minDescender = Mathf.Min(minDescender, currentCharInfo.descender);

        bottomLeft = new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.descender, 0);

        bottomLeft = m_Transform.TransformPoint(new Vector3(bottomLeft.x, minDescender, 0));
        topRight = m_Transform.TransformPoint(new Vector3(currentCharInfo.topRight.x, maxAscender, 0));

        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        _linkSpaceSize = new Vector2(width * (linkInfo.linkTextLength - 1), height);

        Vector2 centerPosition = bottomLeft;
        
        centerPosition.x += width / 2;
        centerPosition.y += height / 2;

        return centerPosition;
    }

    public IEnumerator SpawnBlankSpaces()
    {
        //Espera um tempo para gerar as meshes do texto
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _text.textInfo.linkCount; i++)
        {
            //Instancia o espaço em branco
            GameObject blankSpaceObject = Instantiate(_blankSpacePrefab, CalcLinkCenterPosition(i), Quaternion.identity);
            //Coloca ele dentro do canvas
            blankSpaceObject.transform.SetParent(gameObject.transform.parent);
            //Coloca ele numa lista para acessar os dados depois
            DragBlankSpace blankSpace = blankSpaceObject.GetComponent<DragBlankSpace>();
            //Define o indice dele
            blankSpace.RightIndex = i;
            //Arruma o tamanho de acordo com o link
            blankSpace.BlankSpaceImage.rectTransform.sizeDelta = _linkSpaceSize;
            //Atualiza a quantidade de espaços em branco
            DragController.Instance.BlankSpaceQuantity = i + 1;
        }
    }    
}