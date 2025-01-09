using System;

namespace Decision_Tree_Maker.Save
{
    [Serializable]
    public class EdgeSaveData
    {
        public string id;
        public PortSaveData input;
        public PortSaveData output;
    }
}