using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum PanelStates
{
    Hide,
    Show,
    Custom
}

public class Panel : MonoBehaviour
{
    [Serializable]
    public class Position
    {
        public PanelStates state;
        [DrawIf("state", PanelStates.Custom)]
        public string stateName;
        public Vector2 offset;
        public float alpha;
        public bool blockRay;

        public Position(PanelStates state)
        {
            this.state = state;
        }
        public Position(PanelStates state, Vector2 offset) : this(state)
        {
            this.offset = offset;
        }
        public Position(PanelStates state, Vector2 offset, float alpha, bool blockRay) : this(state, offset)
        {
            this.alpha = alpha;
            this.blockRay = blockRay;
        }
    }

    [SerializeField] private List<Position> positionList;
    public Position CurrentPosition { get; private set; }
    
    public Position this[PanelStates state] {
        get {
            return positionList.FirstOrDefault(p => p.state == state);
        }
    }
    
    public Position this[string stateName] {
        get {
            return positionList.FirstOrDefault(p => p.stateName == stateName);
        }
    }

    private RectTransform _rect;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        
        SetPosition(positionList[0]);
    }

    public void SetPosition(PanelStates state, bool useDotween = false, float dotweenTime = 0.2f)
    {
        SetPosition(this[state], useDotween, dotweenTime);
    }
    
    public void SetPosition(string stateName, bool useDotween = false, float dotweenTime = 0.2f)
    {
        SetPosition(this[stateName], useDotween, dotweenTime);
    }

    public void SetPosition(Position p, bool useDotween = false, float dotweenTime = 0.2f)
    {
        DOTween.Complete(_canvasGroup);
        DOTween.Complete(_rect);
        
        CurrentPosition = p;
        if (useDotween)
        {
            _rect.DOAnchorPos(p.offset, dotweenTime);
            if (_canvasGroup)
            {
                _canvasGroup.DOFade(p.alpha, dotweenTime);
                _canvasGroup.blocksRaycasts = p.blockRay;
            }
        }
        else
        {
            _rect.anchoredPosition = p.offset;
            if (_canvasGroup)
            {
                _canvasGroup.alpha = p.alpha;
                _canvasGroup.blocksRaycasts = p.blockRay;
            }
        }
    }
}
