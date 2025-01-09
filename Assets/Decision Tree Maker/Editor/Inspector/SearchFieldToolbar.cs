using Decision_Tree_Maker.Editor.Parameters;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor.Inspector
{
    public class SearchFieldToolbar: VisualElement
    {
        public SearchFieldToolbar(ParametersManager parametersManager)
        {
            AddToClassList("searchFieldPanel");
            
            var button = new Button();
            button.clicked += ()
                => UnityEditor.PopupWindow.Show(button.worldBound, new ParameterPopupContent(parametersManager));
            button.style.width = 60;
            button.text = "Add";
            Add(button);
            
            
            var searchField = new ToolbarSearchField();
            searchField.style.flexShrink = new StyleFloat(1);
            Add(searchField);
        }
    }
}