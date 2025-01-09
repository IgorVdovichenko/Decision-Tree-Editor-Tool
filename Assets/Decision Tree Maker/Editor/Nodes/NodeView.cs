using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor.Nodes
{
    public abstract class NodeView: Node
    {
        public string Description
        {
            get => textField.value;
            set => textField.value = value;
        }

        public event Action<NodeView> OnDescriptionFocusOut;

        private TextField textField;

        protected NodeView()
        {
            title = GetTitle();
            textField = new TextField(20, true, false, Char.MaxValue);
            textField.value = "Some description...";
        }
        
        protected abstract string GetTitle();

        protected void CreatePort(Direction direction, string portName)
        {
            var container = direction == Direction.Input
                ? inputContainer
                : outputContainer;
            
            var port = Port.Create<Edge>(Orientation.Horizontal, direction, Port.Capacity.Single, typeof(Port));

            port.portName = portName;
            port.name = portName;
            container.Add(port);
        }

        protected void AddDescriptionTextField()
        {
            mainContainer.Add(textField);
            textField.RegisterCallback<FocusOutEvent>(OnInputEvent);
        }
        
        private void OnInputEvent(FocusOutEvent evt)
        {
            OnDescriptionFocusOut?.Invoke(this);
        }
    }
}