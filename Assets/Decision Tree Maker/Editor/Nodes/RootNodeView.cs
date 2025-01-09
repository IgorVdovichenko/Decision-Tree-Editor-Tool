using UnityEditor.Experimental.GraphView;

namespace Decision_Tree_Maker.Editor.Nodes
{
    public class RootNodeView: NodeView
    {
        public RootNodeView()
        {
            CreatePort(Direction.Output, "Output");
            capabilities -= Capabilities.Deletable;
        }

        protected override string GetTitle() => "Root";
    }
}