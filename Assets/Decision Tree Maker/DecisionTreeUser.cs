using Decision_Tree_Maker.Model;
using UnityEngine;

namespace Decision_Tree_Maker
{
    public class DecisionTreeUser: MonoBehaviour
    {
        [SerializeField] private DecisionTreeAsset decisionTree;

        private DecisionTree _tree;

        private void Awake()
        {
            _tree = decisionTree.BuildTree();
        }

        public void RunTree()
        {
            _tree.Run();
        }
    }
}