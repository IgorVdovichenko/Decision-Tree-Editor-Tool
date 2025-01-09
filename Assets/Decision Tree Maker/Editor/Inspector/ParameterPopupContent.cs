using System;
using Decision_Tree_Maker.Editor.Parameters;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Decision_Tree_Maker.Editor.Inspector
{
    public class ParameterPopupContent: PopupWindowContent
    {
        private readonly ParametersManager _parametersManager;
        private VisualElement _root;
        private Color _defaultColor;

        public ParameterPopupContent(ParametersManager parametersManager)
        {
            _parametersManager = parametersManager;
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(120, 60);
        }

        public override void OnGUI(Rect rect){}
        
        public override void OnOpen()
        {
            _root = editorWindow.rootVisualElement;

            _defaultColor = _root.style.backgroundColor.value;
            
            _root.style.alignContent = Align.Center;
            
            CreateLabel("Bool Parameter", ParameterType.Bool);
            CreateLabel("Int Parameter", ParameterType.Int);
            CreateLabel("Float Parameter", ParameterType.Float);

            _root.RegisterCallback<MouseLeaveEvent>(evt => editorWindow.Close());
        }

        private void CreateLabel(string title, ParameterType parameterType)
        {
            var label = new Label(title);
            SetStyle(label);
            label.RegisterCallback<ClickEvent>(evt =>
            {
                GetAction(_parametersManager, parameterType).Invoke();
                editorWindow.Close();
            });
            _root.Add(label);
        }
        
        private static Action GetAction(ParametersManager parametersManager, ParameterType parameterType)
        {
            return parameterType switch
            {
                ParameterType.Bool => () => parametersManager.CreateNewParameter<BoolParameter>(),
                ParameterType.Int => () => parametersManager.CreateNewParameter<IntParameter>(),
                ParameterType.Float => () => parametersManager.CreateNewParameter<FloatParameter>(),
                _ => throw new ArgumentOutOfRangeException(nameof(parameterType), parameterType, null)
            };
        }

        private void SetStyle(VisualElement element)
        {
            var style = element.style;
            style.height = 20;
            style.paddingLeft = 7;
            style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
            element.RegisterCallback<MouseOverEvent>((evt => style.backgroundColor = Color.gray));
            element.RegisterCallback<MouseLeaveEvent>(evt => style.backgroundColor = _defaultColor);
        }
    }
}