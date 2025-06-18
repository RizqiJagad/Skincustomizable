using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionedSprite
{
    public Sprite Sprite;
    public Vector3 PositionModifier;
}

public class CustomizableElement : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private List<PositionedSprite> _spriteOptions;

    [field: SerializeField]
    public int SpriteIndex { get; private set; }

    [SerializeField]
    private List<Color> _colorOptions;

    [field: SerializeField]
    public int ColorIndex { get; private set; }

    public int TotalSprites => _spriteOptions.Count;

    [ContextMenu("Next Sprite")]
    public PositionedSprite NextSprite()
    {
        SpriteIndex = Mathf.Min(SpriteIndex + 1, _spriteOptions.Count - 1);
        UpdateSprite();
        return _spriteOptions[SpriteIndex];
    }

    [ContextMenu("Previous Sprite")]
    public PositionedSprite PreviousSprite()
    {
        SpriteIndex = Mathf.Max(SpriteIndex - 1, 0);
        UpdateSprite();
        return _spriteOptions[SpriteIndex];
    }

    [ContextMenu("Next Color")]
    public Color NextColor()
    {
        ColorIndex = Mathf.Min(ColorIndex + 1, _colorOptions.Count - 1);
        UpdateColor();
        return _colorOptions[ColorIndex];
    }

    [ContextMenu("Previous Color")]
    public Color PreviousColor()
    {
        ColorIndex = Mathf.Max(ColorIndex - 1, 0);
        UpdateColor();
        return _colorOptions[ColorIndex];
    }

    private void UpdateSprite()
    {
        if (_spriteOptions == null || _spriteOptions.Count == 0)
        {
            Debug.LogWarning($"{name}: _spriteOptions is empty. Skipping UpdateSprite.");
            return;
        }

        SpriteIndex = Mathf.Clamp(SpriteIndex, 0, _spriteOptions.Count - 1);
        var positionedSprite = _spriteOptions[SpriteIndex];
        _spriteRenderer.sprite = positionedSprite.Sprite;
        transform.localPosition = positionedSprite.PositionModifier;
    }

    private void UpdateColor()
    {
        if (_colorOptions.Count == 0) return;

        var selectedColor = _colorOptions[ColorIndex];

        if (selectedColor.a == 0f)
        {
            selectedColor.a = 1f;
        }

        _spriteRenderer.color = selectedColor;
    }

    public void SetSpriteIndex(int index)
    {
        SpriteIndex = Mathf.Clamp(index, 0, _spriteOptions.Count - 1);
        UpdateSprite();
    }

    public void SetColor(Color color)
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = color;
        }
    }

    public void OnNextSpriteButton()
    {
        NextSprite();
    }

    public void OnPreviousSpriteButton()
    {
        PreviousSprite();
    }

    public void OnNextColorButton()
    {
        NextColor();
    }

    public void OnPreviousColorButton()
    {
        PreviousColor();
    }
}
