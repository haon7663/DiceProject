using System;
using System.Collections;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Map
{
    public class MapController : MonoBehaviour
    {
        public MapView view;
        public Panel panel;
        
        public Map CurrentMap { get; private set; }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => DataManager.Inst.playerData != null);
            
            GenerateNewMap(DataManager.Inst.playerData.map);
        }

        public void Show()
        {
            panel.SetPosition(PanelStates.Show, true);
        }
        
        public void Hide()
        {
            panel.SetPosition(PanelStates.Hide, true);
        }

        public void GenerateNewMap(Map map)
        {
            CurrentMap = map;
            view.ShowMap(map);
        }
    }
}