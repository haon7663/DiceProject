using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicPanelController : MonoBehaviour
{
    [SerializeField] private RelicPanel relicPanelPrefab;
    [SerializeField] private Transform parent;
    
    private List<RelicPanel> _relicPanels = new();

    private void Start()
    {
        _relicPanels = new List<RelicPanel>();
        UpdatePanels();
    }
    
    public void UpdatePanels()
    {
        _relicPanels.ForEach(relicPanel => Destroy(relicPanel.gameObject));
        _relicPanels = new List<RelicPanel>();

        foreach (var relic in DataManager.Inst.playerData.Relics.ToRelic())
        {
            var relicPanel = Instantiate(relicPanelPrefab, parent);
            relicPanel.Initialize(relic);
            _relicPanels.Add(relicPanel);
        }
    }
}
