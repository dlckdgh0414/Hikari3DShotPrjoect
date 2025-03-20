using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

public class SkillTreeEditor : EditorWindow
{
    private const int Height = 6;
    private const int Width = 11;
    
    private readonly string _fruitsBtnName = "fruitsButton";
    
    private FruitsButtonData[,] fruitsButtonDatas = new FruitsButtonData[Height, Width];

    [SerializeField] private VisualTreeAsset _treeAsset;
    private VisualElement _background;
    private Button _createBtn;
    private TextField _intField; 
    private TextField _floatField; 
    private DropdownField _skillTypeField;
    
    [MenuItem("Window/SkillTreeEditor")]
    public static void ShowExample()
    {
        SkillTreeEditor wnd = GetWindow<SkillTreeEditor>();
        wnd.titleContent = new GUIContent("SkillTreeEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        VisualTreeAsset asset = _treeAsset;
        VisualElement tree = asset.Instantiate();
        root.Add(tree);

        #region Assignment
        _background = tree.Q<VisualElement>("Background");
        _createBtn = tree.Q<Button>("CreateButton");
        _skillTypeField = tree.Q<DropdownField>("TypeDropdown");
        _intField = tree.Q<TextField>("IntTextField");
        _floatField = tree.Q<TextField>("FloatTextField");
        #endregion
        
        for (int y = 0; y < Height; y++)
        {   
            for (int x = 0; x < Width; x++)
            {
                Button button = new Button();
                FruitsButtonData fruitsButtonData = new FruitsButtonData(button);
                fruitsButtonDatas[y, x] = fruitsButtonData;

                button.AddToClassList(_fruitsBtnName);
                button.clicked += () => OnFruitsSelect(fruitsButtonData);
                _background.Add(button);
            }
        }
    }

    private void OnFruitsSelect(FruitsButtonData data)
    {
        if (data.FruitsButtonSO == null)
        {
            FruitsSO newSO = ScriptableObject.CreateInstance<FruitsSO>();
            _skillTypeField.choices.Clear();
            data.FruitsButtonSO = newSO;
        }
        
        _skillTypeField.choices.Clear();
        
        foreach (FruitsType value in Enum.GetValues(typeof(FruitsType)))
        {
            _skillTypeField.choices.Add(value.ToString());
        }
    }
}
