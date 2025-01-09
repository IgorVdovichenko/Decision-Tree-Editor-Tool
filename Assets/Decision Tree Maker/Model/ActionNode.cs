using UnityEngine;

namespace Decision_Tree_Maker.Model
{
    public class ActionNode: Node
    {
        private string _title;
        public ActionNode(string title)
        {
            _title = title;
        }
        
        public override Node Evaluate()
        {
            Debug.LogError($"Executed an action {_title}!");
            return this;
        }
    }
}