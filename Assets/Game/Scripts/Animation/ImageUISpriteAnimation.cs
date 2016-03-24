using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class ImageUISpriteAnimation : BaseSpriteAnimation
{
    private Image imageRenderer;
    protected override void Init()
    {
        imageRenderer = GetComponent<Image>();
    }
    protected override void SetSprite(Sprite sprite)
    {
        imageRenderer.sprite = sprite;
    }
}