using System;
using System.Linq;
using Decision_Tree_Maker.Editor.Inspector;
using Decision_Tree_Maker.Editor.Parameters;
using Decision_Tree_Maker.Editor.Parameters.Views;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor
{
    public class DecisionTreeWindow : EditorWindow
    {
        private static DecisionTreeAsset _decisionTreeAsset;

        private ParametersManager _parametersManager;
        private Parameter _selectedParameter;
        
        public static void ShowWindow(DecisionTreeAsset decisionTreeAsset)
        {
            _decisionTreeAsset = decisionTreeAsset;
            var window = GetWindow<DecisionTreeWindow>();
            window.titleContent = new GUIContent($"Decision Tree - {decisionTreeAsset.name}");

            window.Show();
        }
        
        private void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/Decision Tree Maker/Editor/DecisionTreeWindowLayout.uxml");
            visualTree.CloneTree(root);

            var grid = root.Q<GridField>();
            grid.DecisionTreeAsset = _decisionTreeAsset;
            var graphSaver = new GraphSaver(_decisionTreeAsset);
            grid.graphViewChanged += graphSaver.Save;
            grid.Initialize();
            
            var tabsController = new TabsController(root);
            
            _parametersManager = new ParametersManager(_decisionTreeAsset);

            var inspectorContainer = root.Q<VisualElement>("inspector-content");
            var searchFieldToolbar = new SearchFieldToolbar(_parametersManager);
            
            inspectorContainer.Add(searchFieldToolbar);
            
            var removeButton = new Button(RemoveSelectedParameter);
            removeButton.text = "Remove";
            inspectorContainer.Add(removeButton);

            Func<VisualElement> makeItem = () =>
            {
                var label = new ParaView();
                return label;
            };

            Action<VisualElement, int> bindItem = (element, i) =>
            {
                var para = _parametersManager.Assets[i];
                
                
                var label = element as ParaView;
                label.SetName(para.name);
                label.AddInputField(GetInputField(para));
                var so = new SerializedObject(para);
                label.Bind(so);
            };

            var listView = new ListView(_parametersManager.Assets, 25, makeItem, bindItem);
            listView.style.marginLeft = 5;
            listView.style.marginRight = 5;
            listView.showBorder = true;
            listView.onSelectionChange += objects =>
            {
                _selectedParameter = (Parameter) objects.First();
            };
            inspectorContainer.Add(listView);

            _parametersManager.OnParameterAdded += parameter => listView.RefreshItems();
            _parametersManager.OnParameterDeleted += parameter => listView.RefreshItems();
        }

        private void RemoveSelectedParameter()
        {
            if(_parametersManager == null || _selectedParameter == null) return;
            _parametersManager.RemoveParameter(_selectedParameter);
        }

        private VisualElement GetInputField(Parameter parameter)
        {
            return parameter switch
            {
                BoolParameter _ => new Toggle(),
                IntParameter _ => new IntegerField(),
                FloatParameter _ => new FloatField(),
                _ => null
            };
        }
    }
}