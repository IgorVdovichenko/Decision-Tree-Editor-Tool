using UnityEngine;

namespace Decision_Tree_Maker.Editor.Parameters
{
    public abstract class Parameter : ScriptableObject
    {
        public object Value { get; protected set; }
        protected abstract void SetValue(object value);
        protected abstract object GetValue();
    }
}
