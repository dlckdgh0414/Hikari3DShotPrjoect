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

    private bool _fruitsSelectMode = false;
    
    private FruitsButtonData[,] fruitsButtonDatas = new FruitsButtonData[Height, Width];

    [SerializeField] private VisualTreeAsset _treeAsset;
    private VisualElement _background;
    private Button _createBtn;
    private Button _saveFruitsSOBtn;
    private Button _selectFruitsBtn;
    private TextField _intField; 
    private TextField _floatField; 
    private DropdownField _fruitsTypeField;

    private FruitsButtonData _currentData;
    
    [MenuItem("Window/SkillTreeEditor")]
    public static void ShowWindow()
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
        _saveFruitsSOBtn = tree.Q<Button>("SaveFruitsSOButton");
        _fruitsTypeField = tree.Q<DropdownField>("TypeDropdown");
        _intField = tree.Q<TextField>("IntTextField");
        _floatField = tree.Q<TextField>("FloatTextField");
        _selectFruitsBtn = tree.Q<Button>("SelectFruitsData");
        #endregion
        
        for (int y = 0; y < Height; y++) {   
            for (int x = 0; x < Width; x++) {
                int localX = x;
                int localY = y;
                Button button = new Button();
                fruitsButtonDatas[localY, localX] = new FruitsButtonData(button);

                button.AddToClassList(_fruitsBtnName);
                button.style.backgroundColor = Color.grey;
                fruitsButtonDatas[localY, localX].Button.clicked += () 
                    => OnFruitsSelect(fruitsButtonDatas[localY, localX]);
                _background.Add(button);
            }
        }

        _saveFruitsSOBtn.clicked += SaveFruitsData;
        _selectFruitsBtn.clicked += ToggleSelectMode;
    }


    private void OnFruitsSelect(FruitsButtonData data) //Fruits 버튼을 눌렀을 때
    {
        _currentData = data;
        _currentData.isActive = !_currentData.isActive;

        if (!_fruitsSelectMode)
        {
            
        }
        
        _currentData.Button.style.backgroundColor = _currentData.isActive ? Color.white : Color.grey;
        
        if (_currentData.FruitsButtonSO == null)
            _currentData.FruitsButtonSO = ScriptableObject.CreateInstance<FruitsSO>();

        foreach (FruitsType value in Enum.GetValues(typeof(FruitsType)))
            _fruitsTypeField.choices.Add(value.ToString());
        
        _fruitsTypeField.choices.Clear();
        LoadFruitsData();
    }

    private void SaveFruitsData() //변경된 SO 데이터 저장
    {
        _currentData.FruitsButtonSO.fruitsType = Enum.Parse<FruitsType>(_fruitsTypeField.value);
        _currentData.FruitsButtonSO.intValue = int.Parse(_intField.value);
        _currentData.FruitsButtonSO.floatValue = float.Parse(_floatField.value);
    }
    
    private void LoadFruitsData() //선택된 Fruits에 저장된 SO 데이터 로드
    {
        FruitsButtonData data = _currentData;
        _fruitsTypeField.value = data.FruitsButtonSO.fruitsType.ToString();
        _intField.value = data.FruitsButtonSO.intValue.ToString();
        _floatField.value = data.FruitsButtonSO.floatValue.ToString();
    }
    
    //Tree자식 선택모드로 변환
    private void ToggleSelectMode() => _fruitsSelectMode = !_fruitsSelectMode;

    private void AddFruitsData() //자식으로 선택된 Fruits를 추가
    {
        
    }
}
