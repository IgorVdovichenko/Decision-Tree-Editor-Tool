using UnityEngine;

namespace Decision_Tree_Maker.Editor.Parameters
{
    public class BoolParameter: Parameter
    {
        [SerializeField] private bool _value;
        
        protected override void SetValue(object value)
        {
            _value = (bool) value;
        }

        protected override object GetValue()
        {
            return _value;
        }
    }
}