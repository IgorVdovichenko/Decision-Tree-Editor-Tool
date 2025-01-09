using System;

namespace Decision_Tree_Maker.Editor.Nodes
{
    public static class NodeViewFactory
    {
        public static NodeView CreateNode(NodeType nodeType)
        {
            return nodeType switch
            {
                NodeType.Decision => new DecisionNodeView(),
                NodeType.Action => new ActionNodeView(),
                NodeType.Root => new RootNodeView(),
                _ => throw new ArgumentOutOfRangeException(nameof(nodeType), nodeType, null)
            };
        }
    }
}