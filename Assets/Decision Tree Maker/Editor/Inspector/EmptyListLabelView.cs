using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor.Inspector
{
    public class EmptyListLabelView: Label
    {
        private const string StyleClassName = "emptyListLabel";
        
        public EmptyListLabelView()
        {
            text = "List is Empty";
            AddToClassList(StyleClassName);
        }
    }
}