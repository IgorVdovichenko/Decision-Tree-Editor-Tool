using UnityEditor.Experimental.GraphView;

namespace Decision_Tree_Maker.Editor.Nodes
{
    public class ActionNodeView: NodeView
    {
        public ActionNodeView()
        {
            CreatePort(Direction.Input, "Input");
            AddDescriptionTextField();
        }
        
        protected override string GetTitle() => "Action";
    }
}