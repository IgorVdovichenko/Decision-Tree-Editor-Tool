using System;
using UnityEngine;

namespace Decision_Tree_Maker.Save
{
    [Serializable]
    public class NodeSaveData
    {
        public string id;
        public string description;
        public NodeType type;
        public Vector2 position;
    }
}