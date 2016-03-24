using UnityEngine;
using System.Collections;
using System;


[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class SpriteAnimation : BaseSpriteAnimation
{
    public SpriteRenderer spriteRenderer;
    protected override void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}