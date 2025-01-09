using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor.Parameters.Views
{
    public class ParaView: VisualElement
    {
        private readonly Label _label;
        private VisualElement _inputField;
        
        public ParaView()
        {
            style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            
            _label = new Label();
            
            Add(_label);
        }

        public void SetName(string newName)
        {
            _label.text = newName;
        }

        public void AddInputField(VisualElement field)
        {
            ((IBindable)field).bindingPath = "_value";
            if(_inputField != null) return;
            _inputField = field;
            Add(field);
        }
    }
}