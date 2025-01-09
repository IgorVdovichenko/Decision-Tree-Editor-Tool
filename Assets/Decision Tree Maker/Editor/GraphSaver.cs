using System.Linq;
using Decision_Tree_Maker.Save;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor
{
    public class GraphSaver
    {
        private readonly DecisionTreeAsset _treeAsset;

        public GraphSaver(DecisionTreeAsset treeAsset)
        {
            _treeAsset = treeAsset;
        }

        public GraphViewChange Save(GraphViewChange change)
        {
            SaveNodeChanges(change);
            SaveEdgeChanges(change);
            SaveRemovedElements(change);
            EditorUtility.SetDirty(_treeAsset);
            return change;
        }

        private void SaveNodeChanges(GraphViewChange change)
        {
            if (change.movedElements == null) return;
            
            foreach (var element in change.movedElements)
            {
                var node = _treeAsset.nodes.FirstOrDefault(n => n.id == element.viewDataKey);
                if (node == null) continue;
                node.position = GetElementPosition(element);
            }
        }
        
        private void SaveRemovedElements(GraphViewChange change)
        {
            if (change.elementsToRemove == null) return;

            foreach (var element in change.elementsToRemove)
            {
                if(!(element is Edge)) continue;
                var edge = _treeAsset.edges.FirstOrDefault(e => e.id == element.viewDataKey);
                _treeAsset.edges.Remove(edge);
            }
        }
        
        private void SaveEdgeChanges(GraphViewChange change)
        {
            if (change.edgesToCreate == null) return;

            foreach (var edge in change.edgesToCreate)
            {
                var inputPort = new PortSaveData
                {
                    nodeId = edge.input.node.viewDataKey,
                    name = edge.input.portName
                };
                
                var outputPort = new PortSaveData
                {
                    nodeId = edge.output.node.viewDataKey,
                    name = edge.output.portName
                };

                _treeAsset.edges.Add(new EdgeSaveData
                {
                    input = inputPort,
                    output = outputPort,
                    id = edge.viewDataKey
                });
            }
        }
        
        private static Vector2 GetElementPosition(VisualElement element)
        {
            return new Vector2(element.style.top.value.value, element.style.left.value.value);
        }
    }
}