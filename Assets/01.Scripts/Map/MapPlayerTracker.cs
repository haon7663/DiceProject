using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Map
{
    public class MapPlayerTracker : Singleton<MapPlayerTracker>
    {
        public float enterNodeDelay = 0.5f;
        public MapController mapController;
        public MapView view;
        
        public void SelectNode(MapNode mapNode)
        {
            if (mapController.CurrentMap.path.Count == 0)
            {
                if (mapNode.Node.point.Y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
            else
            {
                var currentPoint = mapController.CurrentMap.path[^1];
                var currentNode = mapController.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
        }
        
        private void SendPlayerToNode(MapNode mapNode)
        {
            mapNode.OnSelect();
            mapController.CurrentMap.path.Add(mapNode.Node.point);
            view.SetNodeColor();
            view.SetLineColor();
            //mapNode.ShowSwirlAnimation();*/

            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private static void EnterNode(MapNode mapNode)
        {
            // we have access to blueprint name here as well
            Debug.Log("Entering node: " + mapNode.Node.blueprintName + " of type: " + mapNode.Node.nodeType);
            // load appropriate scene with context based on nodeType:
            // or show appropriate GUI over the map: 
            // if you choose to show GUI in some of these cases, do not forget to set "Locked" in MapPlayerTracker back to false

            GameManager.Inst.currentNodeType = mapNode.Node.nodeType;
            Fade.Inst.FadeOut("Battle");
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }
    }
}