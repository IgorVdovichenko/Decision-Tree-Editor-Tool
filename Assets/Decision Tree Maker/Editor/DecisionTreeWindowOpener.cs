using UnityEditor;
using UnityEditor.Callbacks;

namespace Decision_Tree_Maker.Editor
{
    public class DecisionTreeWindowOpener
    {
        [OnOpenAsset]
        public static bool OnOpenAssetAction(int instanceId, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceId);
            
            if(obj is DecisionTreeAsset)
                DecisionTreeWindow.ShowWindow((DecisionTreeAsset)obj);
            
            return false;
        }
    }
}