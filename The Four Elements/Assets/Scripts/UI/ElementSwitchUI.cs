using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElementSwitchUI : MonoBehaviour
{
    private PlayerElementManager switcher;
    [SerializeField] private Image fireSprite;
    [SerializeField] private Image fireSpriteBackGround;
    private Color originalColorFire;
    
    [SerializeField] private Image waterSprite;
    [SerializeField] private Image waterSpriteBackGround;
    private Color originalColorWater;
    
    [SerializeField] private Image windSprite;
    [SerializeField] private Image windSpriteBackGround;
    private Color originalColorWind;
    
    [SerializeField] private Image soilSprite;
    [SerializeField] private Image soilSpriteBackGround;
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
        Debug.LogWarning(type);
        switch (type)
        {
            case ElementalType.FIRE:
                fireSpriteBackGround.color = originalColorFire;
                windSpriteBackGround.color = backGroundColor;
                waterSpriteBackGround.color =backGroundColor;
                soilSpriteBackGround.color = backGroundColor;
                break;
            case ElementalType.WATER:
                fireSpriteBackGround.color = backGroundColor;
                windSpriteBackGround.color = backGroundColor;
                waterSpriteBackGround.color = originalColorWater;
                soilSpriteBackGround.color = backGroundColor;
                break;
            case ElementalType.WIND:
                fireSpriteBackGround.color = backGroundColor;
                windSpriteBackGround.color = originalColorWind;
                waterSpriteBackGround.color = backGroundColor;
                soilSpriteBackGround.color = backGroundColor;
                break;
            case ElementalType.SOIL:
                fireSpriteBackGround.color = backGroundColor;
                windSpriteBackGround.color = backGroundColor;
                waterSpriteBackGround.color = backGroundColor;
                soilSpriteBackGround.color = originalColorSoil;
                break;
        }
    }
    
    



}
