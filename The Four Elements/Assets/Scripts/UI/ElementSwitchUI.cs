using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElementSwitchUI : MonoBehaviour
{
    private PlayerElementManager switcher;
    [SerializeField] private Image fireSprite;
    [SerializeField] private Image fireSpriteBackGround;
    [SerializeField] private Image fireFrame;
    private Color originalColorFire;
    
    [SerializeField] private Image waterSprite;
    [SerializeField] private Image waterSpriteBackGround;
    [SerializeField] private Image waterFrame;
    private Color originalColorWater;
    
    [SerializeField] private Image windSprite;
    [SerializeField] private Image windSpriteBackGround;
    [SerializeField] private Image windFrame;
    private Color originalColorWind;
    
    [SerializeField] private Image soilSprite;
    [SerializeField] private Image soilSpriteBackGround;
    [SerializeField] private Image soilFrame;
    private Color originalColorSoil;
    
    [SerializeField]private Color backGroundColor = Color.black;
    
    private void OnEnable()
    {
        switcher.OnElementSwitch += SwitchElementUI;
    }

    private void OnDisable()
    {
        switcher.OnElementSwitch -= SwitchElementUI;
    }

    void Awake()
    {
        switcher = FindFirstObjectByType<PlayerElementManager>();
        originalColorFire = fireSpriteBackGround.color;
        originalColorWater = waterSpriteBackGround.color;
        originalColorWind = windSpriteBackGround.color;
        originalColorSoil = soilSpriteBackGround.color;
        SwitchElementUI(ElementalType.FIRE);
    }


    void SwitchElementUI(ElementalType type)
    {
//        Debug.LogWarning(type);
        switch (type)
        {
            case ElementalType.FIRE:
                fireSpriteBackGround.color = originalColorFire;
                windSpriteBackGround.color = backGroundColor;
                waterSpriteBackGround.color =backGroundColor;
                soilSpriteBackGround.color = backGroundColor;

                fireFrame.color = originalColorFire;
                waterFrame.color = Color.white;
                windFrame.color = Color.white;
                soilFrame.color = Color.white;
                break;
            case ElementalType.WATER:
                fireSpriteBackGround.color = backGroundColor;
                windSpriteBackGround.color = backGroundColor;
                waterSpriteBackGround.color = originalColorWater;
                soilSpriteBackGround.color = backGroundColor;

                fireFrame.color = Color.white;
                waterFrame.color = originalColorWater;
                windFrame.color = Color.white;
                soilFrame.color = Color.white;
                break;
            case ElementalType.WIND:
                fireSpriteBackGround.color = backGroundColor;
                windSpriteBackGround.color = originalColorWind;
                waterSpriteBackGround.color = backGroundColor;
                soilSpriteBackGround.color = backGroundColor;

                fireFrame.color = Color.white;
                waterFrame.color = Color.white;
                windFrame.color = originalColorWind;
                soilFrame.color = Color.white;
                break;
            case ElementalType.SOIL:
                fireSpriteBackGround.color = backGroundColor;
                windSpriteBackGround.color = backGroundColor;
                waterSpriteBackGround.color = backGroundColor;
                soilSpriteBackGround.color = originalColorSoil;

                fireFrame.color = Color.white;
                waterFrame.color = Color.white;
                windFrame.color = Color.white;
                soilFrame.color = originalColorSoil;
                break;
        }
    }
    
    



}