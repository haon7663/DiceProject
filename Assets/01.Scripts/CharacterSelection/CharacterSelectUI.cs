using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private List<UnitSO> characters;
    [SerializeField] private float interval;

    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text description;

    private int _index;
    
    public void Left()
    {
        if (_index <= 0) return;
        
        _index--;
        UpdatePosition();
        UpdateInfo(characters[_index]);
    }

    public void Right()
    {
        if (_index >= characters.Count - 1) return;
        
        _index++;
        UpdatePosition();
        UpdateInfo(characters[_index]);
    }
    
    private void UpdatePosition()
    {
        DOTween.Kill(this);
        rect.DOAnchorPosX(_index * interval, 0.5f);
    }

    private void UpdateInfo(UnitSO unitSO)
    {
        nameLabel.text = unitSO.name;
        description.text = unitSO.description;
    }
}
