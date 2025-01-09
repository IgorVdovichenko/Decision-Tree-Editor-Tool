using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor.Inspector
{
    public class TabsController
    {
        private const string SelectedTabClassName = "tab";
        private const string UnselectedTabClassName = "unselectedTab";
        private const string TabsContainer = "tabsContainer";

        private readonly VisualElement _root;
        
        public TabsController(VisualElement root)
        {
            _root = root;
            var labels = GetAllTabs();

            labels.ForEach(l => l.RegisterCallback<ClickEvent>(OnTabClick));
        }

        private UQueryBuilder<Label> GetAllTabs()
        {
            var container = _root.Q<VisualElement>(TabsContainer);
            return container.Query<Label>();
        }

        private void OnTabClick(ClickEvent evt)
        {
            var clickedTab = (Label)evt.currentTarget;

            if (IsSelected(clickedTab)) return;
            
            GetAllTabs()
                .Where(t => t != clickedTab && !IsSelected(clickedTab))
                .ForEach(DeselectTab);
                
            SelectTab(clickedTab);
        }

        private static bool IsSelected(Label tab)
        {
            return tab.ClassListContains(SelectedTabClassName);
        }

        private static void SelectTab(Label tab)
        {
            tab.RemoveFromClassList(UnselectedTabClassName);
            tab.AddToClassList(SelectedTabClassName);
        }

        private static void DeselectTab(Label tab)
        {
            tab.RemoveFromClassList(SelectedTabClassName);
            tab.AddToClassList(UnselectedTabClassName);
        }
    }
}
