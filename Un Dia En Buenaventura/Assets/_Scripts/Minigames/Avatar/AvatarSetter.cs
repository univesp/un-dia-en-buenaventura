using UnityEngine;
using UnityEngine.UI;

public class AvatarSetter : MonoBehaviour
{
    [Header("Body")]
    [SerializeField] private Image _bodyImage;
    [SerializeField] private Image _mouthImage;
    [SerializeField] private Sprite[] _bodySprite; //0 - homem, 1 - mulher
    [SerializeField] private Sprite[] _mouthSprite; //0 - homem, 1 - mulher
    [SerializeField] private Color[] _skinColor;

    [Header("Eyes")]
    [SerializeField] private Image _eyesImage;
    [SerializeField] private Image _irisImage;
    [SerializeField] private Sprite[] _eyesSprite; //0 - puxados, 1 - saltados, 2 - separados
    [SerializeField] private Sprite[] _irisSprite; //0 - puxados, 1 - saltados, 2 - separados
    [SerializeField] private Color[] _eyesColor; //0 - verdes, 1 - azuis, 2 - castanhos, 3 - mel, 4 - pretos, 5 - cinzas

    [Header("Hair")]
    [SerializeField] private Image _hairImage;
    [SerializeField] private Sprite[] _hairSprite;
    [SerializeField] private Color[] _hairColor; //0 - loiro, 1 - ruivo, 2 - castanho, 3 - preto, 4 - grisalho

    [Header("Clothing")]
    [SerializeField] private Image _clothingImage;
    [SerializeField] private Sprite[] _clothingSprite; //0 - M social, 1 - M casual, 2 - M esporte, 3 - F social, 4 - F casual, 5 - F esporte

    [Header("Other")]
    [SerializeField] private Image _otherImage;
    [SerializeField] private Sprite[] _otherSprite; //0 - Nada, 1 - óculos, 2 - barba, 3 - bigode

    private void Start()
    {
        ChangeAvatar();
    }

    private void ChangeAvatar()
    {
        //Troca o corpo e a boca de acordo com o gênero
        _bodyImage.sprite = _bodySprite[PlayerPrefs.GetInt("player_gender", 0)];
        _mouthImage.sprite = _mouthSprite[PlayerPrefs.GetInt("player_gender", 0)];

        //Troca a cor da pele
        _bodyImage.color = _skinColor[PlayerPrefs.GetInt("player_skin", 0)];

        //Troca o sprite dos olhos e a cor da íris
        _eyesImage.sprite = _eyesSprite[PlayerPrefs.GetInt("player_eyes", 0)];
        _irisImage.sprite = _irisSprite[PlayerPrefs.GetInt("player_eyes", 0)];
        _irisImage.color = _eyesColor[PlayerPrefs.GetInt("player_eyeColor", 0)];

        //Troca o penteado e a cor do cabelo
        _hairImage.sprite = _hairSprite[PlayerPrefs.GetInt("player_hairStyle", 0)];
        _hairImage.color = _hairColor[PlayerPrefs.GetInt("player_hairColor", 0)];

        //Troca a roupa do personagem
        _clothingImage.sprite = _clothingSprite[PlayerPrefs.GetInt("player_clothing", 0)];

        //Troca o acessório do personagem e define a cor
        _otherImage.sprite = _otherSprite[PlayerPrefs.GetInt("player_other", 0)];
        if (PlayerPrefs.GetInt("player_other", 0) == 1)
        {
            _otherImage.color = Color.white;
        }
        else
        {
            _otherImage.color = _hairColor[PlayerPrefs.GetInt("player_hairColor", 0)];
        }

    }
}