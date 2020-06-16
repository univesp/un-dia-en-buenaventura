using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarCreation : MonoBehaviour
{
    [Header("Name")]
    [SerializeField] private TMP_InputField _inputField;

    [Header("Body")]
    [SerializeField] private Image _bodyImage;
    [SerializeField] private Image _mouthImage;
    [SerializeField] private Sprite[] _bodySprite; //0 - homem, 1 - mulher
    [SerializeField] private Sprite[] _mouthSprite; //0 - homem, 1 - mulher
    private int _genderIndex;
    [SerializeField] private TextMeshProUGUI _skinColorDescription;
    [SerializeField] private Color[] _skinColor;
    [SerializeField] private string[] _skinColorDescText;
    private int _skinColorIndex;

    [Header("Eyes")]    
    [SerializeField] private Image _eyesImage;
    [SerializeField] private Image _irisImage;
    [SerializeField] private TextMeshProUGUI _eyesDescription;
    [SerializeField] private Sprite[] _eyesSprite; //0 - puxados, 1 - saltados, 2 - separados
    [SerializeField] private Sprite[] _irisSprite; //0 - puxados, 1 - saltados, 2 - separados
    [SerializeField] private string[] _eyesDescText;
    private int _eyeIndex;
    [SerializeField] private TextMeshProUGUI _eyesColorDescription;
    [SerializeField] private Color[] _eyesColor; //0 - verdes, 1 - azuis, 2 - castanhos, 3 - mel, 4 - pretos, 5 - cinzas
    [SerializeField] private string[] _eyesColorDescText;
    private int _eyeColorIndex;

    [Header("Hair")]
    [SerializeField] private Image _hairImage;
    [SerializeField] private TextMeshProUGUI _hairDescription;
    [SerializeField] private Sprite[] _hairSprite;
    [SerializeField] private string[] _hairDescText;
    private int _hairIndex;
    [SerializeField] private TextMeshProUGUI _hairColorDescription;
    [SerializeField] private Color[] _hairColor; //0 - loiro, 1 - ruivo, 2 - castanho, 3 - preto, 4 - grisalho
    [SerializeField] private string[] _hairColorDescText;
    private int _hairColorIndex;

    [Header("Clothing")]
    [SerializeField] private Image _clothingImage;
    [SerializeField] private TextMeshProUGUI _clothingDescription;
    [SerializeField] private Sprite[] _clothingSprite; //0 - M social, 1 - M casual, 2 - M esporte, 3 - F social, 4 - F casual, 5 - F esporte
    [SerializeField] private string[] _clothingDescText;
    private int _clothingIndex;

    [Header("Other")]
    [SerializeField] private Image _otherImage;
    [SerializeField] private TextMeshProUGUI _otherDescription;
    [SerializeField] private Sprite[] _otherSprite; //0 - Nada, 1 - óculos, 2 - barba, 3 - bigode
    [SerializeField] private string[] _otherDescText;
    private int _otherIndex;

    private void Start()
    {
        StartAvatar();
    }

    private void StartAvatar()
    {
        PlayerPrefs.DeleteAll();

        ChangeGender(0);
        ChangeSkinColor(0);
        ChangeEyes(0);
        ChangeEyesColor(0);
        ChangeHairStyle(0);
        ChangeHairColor(0);
        ChangeClothing(0);
        ChangeOther(0);
    }

    public void ChangeName()
    {
        PlayerPrefs.SetString("nome_jogador", _inputField.text);
    }

    public void ChangeGender(int direction)
    {
        _genderIndex += direction;
        if(_genderIndex < 0)
        {
            _genderIndex = _bodySprite.Length - 1;
        }else if(_genderIndex > _bodySprite.Length - 1)
        {
            _genderIndex = 0;
        }

        //Atualiza a roupa
        ChangeClothing(0);
        //Atualiza o acessório
        ChangeOther(0);

        _bodyImage.sprite = _bodySprite[_genderIndex];
        _mouthImage.sprite = _mouthSprite[_genderIndex];

        //Salva no PlayerPrefs
        PlayerPrefs.SetInt("player_gender", _genderIndex);
    }

    public void ChangeSkinColor(int direction)
    {
        _skinColorIndex += direction;
        if (_skinColorIndex < 0)
        {
            _skinColorIndex = _skinColor.Length - 1;
        }
        else if (_skinColorIndex > _skinColor.Length - 1)
        {
            _skinColorIndex = 0;
        }

        _bodyImage.color = _skinColor[_skinColorIndex];
        _skinColorDescription.text = _skinColorDescText[_skinColorIndex];

        //Salva no PlayerPrefs
        PlayerPrefs.SetInt("player_skin", _skinColorIndex);
    }

    public void ChangeEyes(int direction)
    {
        _eyeIndex += direction;
        if (_eyeIndex < 0)
        {
            _eyeIndex = _eyesSprite.Length - 1;
        }
        else if (_eyeIndex > _eyesSprite.Length - 1)
        {
            _eyeIndex = 0;
        }

        _eyesImage.sprite = _eyesSprite[_eyeIndex];
        _irisImage.sprite = _irisSprite[_eyeIndex];
        _eyesDescription.text = _eyesDescText[_eyeIndex];

        //Salva no PlayerPrefs
        PlayerPrefs.SetInt("player_eyes", _eyeIndex);
    }

    public void ChangeEyesColor(int direction)
    {
        _eyeColorIndex += direction;
        if (_eyeColorIndex < 0)
        {
            _eyeColorIndex = _eyesColor.Length - 1;
        }
        else if (_eyeColorIndex > _eyesColor.Length - 1)
        {
            _eyeColorIndex = 0;
        }

        _irisImage.color = _eyesColor[_eyeColorIndex];
        _eyesColorDescription.text = _eyesColorDescText[_eyeColorIndex];

        //Salva no PlayerPrefs
        PlayerPrefs.SetInt("player_eyeColor", _eyeColorIndex);
    }

    public void ChangeHairStyle(int direction)
    {
        _hairIndex += direction;
        if (_hairIndex < 0)
        {
            _hairIndex = _hairSprite.Length - 1;
        }
        else if (_hairIndex > _hairSprite.Length - 1)
        {
            _hairIndex = 0;
        }

        _hairImage.sprite = _hairSprite[_hairIndex];
        _hairDescription.text = _hairDescText[_hairIndex];

        //Salva no PlayerPrefs
        PlayerPrefs.SetInt("player_hairStyle", _hairIndex);
    }

    public void ChangeHairColor(int direction)
    {
        _hairColorIndex += direction;
        if (_hairColorIndex < 0)
        {
            _hairColorIndex = _hairColor.Length - 1;
        }
        else if (_hairColorIndex > _hairColor.Length - 1)
        {
            _hairColorIndex = 0;
        }

        _hairImage.color = _hairColor[_hairColorIndex];
        _hairColorDescription.text = _hairColorDescText[_hairColorIndex];
        if (_otherIndex != 1)
        {
            _otherImage.color = _hairColor[_hairColorIndex];
        }
        //Salva no PlayerPrefs
        PlayerPrefs.SetInt("player_hairColor", _hairColorIndex);
    }

    public void ChangeClothing(int direction)
    {
        _clothingIndex += direction;
        //Se for homem
        if (_genderIndex == 0)
        {
            if (_clothingIndex < 0)
            {
                _clothingIndex = 2;
            }
            else if (_clothingIndex > 2)
            {
                _clothingIndex = 0;
            }
        }
        //Se for mulher
        else
        {
            if (_clothingIndex < 3)
            {
                _clothingIndex = 5;
            }
            else if (_clothingIndex > 5)
            {
                _clothingIndex = 3;
            }
        }

        _clothingImage.sprite = _clothingSprite[_clothingIndex];
        _clothingDescription.text = _clothingDescText[_clothingIndex];

        //Salva no PlayerPrefs
        PlayerPrefs.SetInt("player_clothing", _clothingIndex);
    }

    public void ChangeOther(int direction)
    {
        _otherIndex += direction;
        //Se for mulher
        if(_genderIndex == 1)
        {
            if (_otherIndex < 0)
            {
                _otherIndex = 1;
            }
            else if (_otherIndex > 1)
            {
                _otherIndex = 0;
            }
        }
        else
        {
            if (_otherIndex < 0)
            {
                _otherIndex = _otherSprite.Length - 1;
            }
            else if (_otherIndex > _otherSprite.Length - 1)
            {
                _otherIndex = 0;
            }
        }

        if(_otherIndex == 1)
        {
            _otherImage.color = Color.white;
        }
        else
        {
            _otherImage.color = _hairColor[_hairColorIndex];
        }
        _otherImage.sprite = _otherSprite[_otherIndex];
        _otherDescription.text = _otherDescText[_otherIndex];

        //Salva no PlayerPrefs
        PlayerPrefs.SetInt("player_other", _otherIndex);
    }
}