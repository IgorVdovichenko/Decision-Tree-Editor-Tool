using System.Collections.Generic;
using System.Linq;
using Decision_Tree_Maker.Editor.Nodes;
using Decision_Tree_Maker.Save;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor
{
    public class GridField: GraphView
    {
        public new class UxmlFactory : UxmlFactory<GridField, UxmlTraits> { }

        public DecisionTreeAsset DecisionTreeAsset;
        
        public GridField()
        {
            // Insert grid background element
            Insert(0, new GridBackground());
            
            // Add manipulators
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            // Apply stylesheet
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    @"Assets\Decision Tree Maker\Editor\DecisionTreeWindowStylesheet.uss");
            styleSheets.Add(styleSheet);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList();
        }

        public override EventPropagation DeleteSelection()
        {
            foreach (var selectable in selection)
            {
                // if the selected element is a node, delete the node
                var element = (GraphElement)selectable;
                var node = DecisionTreeAsset.nodes.FirstOrDefault(n => n.id == element.viewDataKey);
                DecisionTreeAsset.nodes.Remove(node);
                
                // if the selected element is node, delete all the edges attached to this node
                DecisionTreeAsset.edges.RemoveAll(e => 
                    e.input.nodeId == element.viewDataKey || e.output.nodeId == element.viewDataKey);

                // delete edge if the selected element is an edge
                if (element is Edge)
                {
                    var edge = DecisionTreeAsset.edges.FirstOrDefault(e => e.id == element.viewDataKey);
                    DecisionTreeAsset.edges.Remove(edge);
                }
            }
            return base.DeleteSelection();
        }


        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            var menu = evt.menu;
            menu.AppendAction("Create Decision Node", action => CreateNode(NodeType.Decision, evt.localMousePosition));
            menu.AppendAction("Create Action Node", action => CreateNode(NodeType.Action, evt. localMousePosition));
            base.BuildContextualMenu(evt);
        }

        private void CreateNode(NodeType nodeType, Vector2 pos)
        {
            var node = InstantiateNode(nodeType, pos);
            
            //save node to the tree asset
            DecisionTreeAsset.nodes.Add(new NodeSaveData
            {
                id = node.viewDataKey,
                description = node.Description,
                type = nodeType,
                position = GetElementPosition(node)
            });
            
            EditorUtility.SetDirty(DecisionTreeAsset);
        }

        private NodeView InstantiateNode(NodeType nodeType, Vector2 position)
        {
            NodeView node = NodeViewFactory.CreateNode(nodeType);
            
            node.style.top = position.x;
            node.style.left = position.y;
            
            AddElement(node);
            
            node.OnDescriptionFocusOut += SetNodeDescription;

            return node;
        }

        private void SetNodeDescription(NodeView node)
        {
            var data = DecisionTreeAsset.nodes.FirstOrDefault(n => n.id == node.viewDataKey);
            if(data != null) data.description = node.Description;
            EditorUtility.SetDirty(DecisionTreeAsset);
        }
        
        private static Vector2 GetElementPosition(VisualElement element)
        {
            return new Vector2(element.style.top.value.value, element.style.left.value.value);
        }

        public void Initialize()
        {
            // Add decision and action nodes
            foreach (var node in DecisionTreeAsset.nodes)
            {
                var n = InstantiateNode(node.type, node.position);
                n.viewDataKey = node.id;
                n.Description = node.description;
            }
            
            // Add root mode
            if(DecisionTreeAsset.nodes.Count == 0)
                CreateNode(NodeType.Root, new Vector2(100, 100));
            
            //Add edges
            foreach (var edge in DecisionTreeAsset.edges)
            {
                var inPort = GetPort(edge.input);
                var outPort = GetPort(edge.output);
                var e = inPort.ConnectTo(outPort);
                edge.id = e.viewDataKey;
                AddElement(e);
            }
        }

        private Port GetPort(PortSaveData data)
        {
            var inputNode = nodes.FirstOrDefault(n => n.viewDataKey == data.nodeId);
            return inputNode.Q<Port>(data.name);
        }
    }
}