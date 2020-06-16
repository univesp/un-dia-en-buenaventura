using UnityEngine;
using UnityEngine.UI;

public class FontSizeController : MonoBehaviour
{
    [SerializeField] private Slider _fontSlider;
    [SerializeField] private float _fontSliderMinValue;
    [SerializeField] private float _fontSliderMaxValue;

    public delegate void OnSizeChange();
    public OnSizeChange SizeChangeDelegate;

    public static FontSizeController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartSlider();
    }

    //Inicializa o valor mínimo e máximo do slider e o coloca no valor definido
    private void StartSlider()
    {
        _fontSlider.minValue = _fontSliderMinValue;
        _fontSlider.maxValue = _fontSliderMaxValue;

        _fontSlider.value = PlayerPrefs.GetFloat("font_size", 46.5f);
    }

    public void ChangeFontSize()
    {
        //Atualiza o PlayerPrefs com o valor atual
        PlayerPrefs.SetFloat("font_size", _fontSlider.value);
        //Executa o delegate que contém os métodos que alteram o tamanho das fontes
        SizeChangeDelegate?.Invoke();
    }
}