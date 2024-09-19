using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private List<CharacterStanding> characters;

    [SerializeField] private float interval;

    private int _index;
    private int _saveIndex;
    
    public void Left()
    {
        _index--;
        UpdatePosition();
    }

    public void Right()
    {
        _index++;
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        DOTween.Kill(this);

        int count = characters.Count;

        for (int i = 0; i < count; i++)
        {
            print($"{characters[i].name} / {(_index + i) % count}");
            if ((_index + i) % count == 0)
                characters[i].rect.anchoredPosition = new Vector2(_index - _saveIndex > 0 ? -interval : interval, 0);
            
            
            characters[i].rect.DOAnchorPosX((_index + i) % count * interval, 0.5f);
        }

        _saveIndex = _index;
    }

}
