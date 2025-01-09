namespace Decision_Tree_Maker.Model
{
    public class DecisionNode: Node
    {
        private Node _trueNode;
        private Node _falseNode;

        private bool _decision;

        public DecisionNode(Node trueNode, Node falseNode, bool decision)
        {
            _decision = decision;
            
            _trueNode = trueNode;
            _falseNode = falseNode;
        }

        public override Node Evaluate()
        {
            var node = _decision ? _trueNode : _falseNode;
            return node.Evaluate();
        }
    }
}