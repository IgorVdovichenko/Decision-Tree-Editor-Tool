using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor.Nodes
{
    public class DecisionNodeView : NodeView
    { 
        public DecisionNodeView()
        {
            CreatePort(Direction.Input, "Input");
            CreatePort(Direction.Output, "True");
            CreatePort(Direction.Output, "False");

            AddDescriptionTextField();
        }

        protected override string GetTitle() => "Decision";
    }
}
