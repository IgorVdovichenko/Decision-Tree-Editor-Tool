using System;
using System.Collections.Generic;
using Decision_Tree_Maker.Model;
using Decision_Tree_Maker.Save;
using UnityEngine;

namespace Decision_Tree_Maker
{
    [CreateAssetMenu(fileName = "Decision Tree", menuName = "Decision Tree Maker/Decision Tree Asset", order = 0)]
    [Serializable]
    public class DecisionTreeAsset : ScriptableObject
    {
        public List<NodeSaveData> nodes = new List<NodeSaveData>();
        public List<EdgeSaveData> edges = new List<EdgeSaveData>();

        public DecisionTree BuildTree()
        {
            Debug.LogError($"Built a decision tree from {name}");
            
            var trueAction = new ActionNode("True");
            var falseAction = new ActionNode("False");
            
            var decision = new DecisionNode(trueAction, falseAction, true);
            
            
            
            return new DecisionTree(decision);
        }
    }
}