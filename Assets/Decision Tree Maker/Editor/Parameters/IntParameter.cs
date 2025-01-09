using UnityEngine;

namespace Decision_Tree_Maker.Editor.Parameters
{
    public class IntParameter: Parameter
    {
        [SerializeField] private int _value;
        
        protected override void SetValue(object value)
        {
            _value = (int) value;
        }

        protected override object GetValue()
        {
            return _value;
        }
    }
}