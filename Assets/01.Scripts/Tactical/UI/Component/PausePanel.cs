using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Map;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public Panel panel;
    public Panel contentPanel;

    public void Show()
    {
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(0, () => panel.SetPosition(PanelStates.Show, true, 0.25f));
        sequence.InsertCallback(0.25f, () => contentPanel.SetPosition(PanelStates.Show, true, 0.25f));
    }
    
    public void Hide()
    {
        panel.SetPosition(PanelStates.Hide, true, 0.25f);
        contentPanel.SetPosition(PanelStates.Hide, true, 0.25f);
    }
    
    public void GiveUp()
    {
        GameManager.Inst.currentNodeType = NodeType.None;
        DataManager.Inst.Delete();
        Fade.Inst.FadeOut("CharacterSelection");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
