using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Decision_Tree_Maker.Editor.Parameters
{
    public class ParametersManager
    {
        private readonly DecisionTreeAsset _decisionTreeAsset;
        public readonly List<Parameter> Assets;
        
        public event Action<Parameter> OnParameterAdded;
        public event Action<Parameter> OnParameterDeleted;

        public ParametersManager(DecisionTreeAsset decisionTreeAsset)
        {
            _decisionTreeAsset = decisionTreeAsset;

            var decisionTreeAssetPath = AssetDatabase.GetAssetPath(decisionTreeAsset);
            Assets = AssetDatabase.LoadAllAssetRepresentationsAtPath(decisionTreeAssetPath)
                .Select(a => a as Parameter)
                .ToList();
        }

        public BoolParameter CreateNewBoolParameter()
        {
            return CreateNewParameter<BoolParameter>();
        }

        public T CreateNewParameter<T>() where T: Parameter
        {
            var asset = (T) ScriptableObject.CreateInstance(typeof(T));
            var baseParameterName = GetBaseParameterName(asset);
            asset.name = CreateUniqueName(baseParameterName);
            AssetDatabase.AddObjectToAsset(asset, _decisionTreeAsset);
            Assets.Add(asset);
            AssetDatabase.SaveAssets();
            OnParameterAdded?.Invoke(asset);

            return asset;
        }

        public void RemoveParameter(Parameter parameter)
        {
            if(Assets.Count == 0) return;
            
            AssetDatabase.RemoveObjectFromAsset(parameter);
            Assets.Remove(parameter);
            AssetDatabase.SaveAssets();
            OnParameterDeleted?.Invoke(parameter);
        }

        private string GetBaseParameterName(Parameter parameter)
        {
            return parameter switch
            {
                BoolParameter _ => "Bool Parameter",
                IntParameter _ => "Int Parameter",
                FloatParameter _ => "Float Parameter",
                _ => "Parameter"
            };
        }

        private string CreateUniqueName(string baseName)
        {
            var names = Assets.Select(a => a.name).ToArray();
            return ObjectNames.GetUniqueName(names, baseName);
        }
    }
}