using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPanelController : MonoBehaviour
{
    [SerializeField] private Panel panel;

    private void Start()
    {
        panel.SetPosition(PanelStates.Show);
    }

    public void Show()
    {
        panel.SetPosition(PanelStates.Show, true, 0.5f);
    }
    
    public void Hide()
    {
        panel.SetPosition(PanelStates.Hide, true, 1f);
    }
}
