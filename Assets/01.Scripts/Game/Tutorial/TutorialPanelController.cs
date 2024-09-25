using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Tutorial
{
    public string title;
    [TextArea]
    public string description;

    public RectTransform source;
    public SkipOption skipOption;

    [DrawIf("skipOption", SkipOption.Button)]
    public Button skipButton;

    public bool onTop;
    public bool onHideAtEnd = true;
}

public enum SkipOption
{
    Click,
    Button,
}

public class TutorialPanelController : MonoBehaviour
{
    [SerializeField] private TutorialPanel tutorialPanel;
    [SerializeField] private Panel panel;
    
    public List<Tutorial> tutorials;
    
    private Tutorial _currentTutorial;

    public void TryToShow(string title)
    {
        var tutorialData = TutorialDataContainer.Inst.tutorialData;
        if (!tutorialData.isPlayed) return;
        
        var index = tutorialData.tutorialIndex;
        
        if (index >= tutorials.Count) return;
        if (tutorials[index].title != title) return;
        
        var tutorial = tutorials[index];
        
        ShowAndFocus(tutorial);
    }

    public void TryNext()
    {
        var tutorialData = TutorialDataContainer.Inst.tutorialData;
        if (!tutorialData.isPlayed) return;
        
        var index = tutorialData.tutorialIndex;
        if (index >= tutorials.Count) return;
        
        var tutorial = tutorials[index];
        
        ShowAndFocus(tutorial);
    }

    public void ShowAndFocus(Tutorial tutorial)
    {
        _currentTutorial = tutorial;
            
        panel.SetPosition(PanelStates.Show, true, 0.5f);
        tutorialPanel.MoveTransform(tutorial.source);
        tutorialPanel.SetLabel(tutorial.title, tutorial.description);
        StartCoroutine(tutorialPanel.SetLabelRect(tutorial.source, tutorial.onTop));

        switch (tutorial.skipOption)
        {
            case SkipOption.Click:
                StartCoroutine(WaitingForSkip());
                break;
            case SkipOption.Button:
                tutorial.skipButton.onClick.AddListener(Skip);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator WaitingForSkip()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        
        Skip();
    }

    private void Skip()
    {
        TutorialDataContainer.Inst.tutorialData.tutorialIndex++;
        if (_currentTutorial.onHideAtEnd)
            panel.SetPosition(PanelStates.Hide, true, 0.5f);
        else
            TryNext();
    }
}
