﻿using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    private void Start()
    {
        ButtonImage = GetComponent<Image>();
    }

    public int RightIndex;
    public Image ButtonImage;
}
