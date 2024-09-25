using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanelController : MonoBehaviour
{
    public Panel panel;

    public void Show()
    {
        panel.SetPosition(PanelStates.Show, true, 0.5f);
    }
}
