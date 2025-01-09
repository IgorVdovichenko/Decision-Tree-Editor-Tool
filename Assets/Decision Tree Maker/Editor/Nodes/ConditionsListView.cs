using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor.Nodes
{
    public class ConditionsListView: VisualElement
    {
        public ConditionsListView()
        {
            var list = new List<string>();
            
            Func<VisualElement> makeItem = () =>
            {
                var label = new Label();
                return label;
            };

            Action<VisualElement, int> bindItem = (element, i) =>
            {
                var label = element as Label;
            };
            
            var listView = new ListView(list, 15, makeItem, bindItem);
            var header = new Label("Conditions");
            
            listView.showBorder = true;
            
            var container = new VisualElement();
            
            container.style.height = new StyleLength(50);
            //container.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            
            //container.Add(header);
            container.Add(listView);
        }
    }
}