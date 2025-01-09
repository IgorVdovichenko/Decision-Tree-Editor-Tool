namespace Decision_Tree_Maker.Model
{
    public class DecisionTree
    {
        private Node _rootNode;

        public DecisionTree(Node rootNode)
        {
            _rootNode = rootNode;
        }

        public void Run()
        {
            _rootNode.Evaluate();
        }
    }
}