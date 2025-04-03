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
    private VisualElement _connectBackground;
    private Button _createBtn;
    private Button _saveFruitsSOBtn;
    private Button _selectFruitsBtn;
    private TextField _intField; 
    private TextField _floatField; 
    private DropdownField _fruitsTypeField;
    
    private Color _backgroundColor = new Color(0.15f, 0.15f, 0.15f);
    private Color _selectModeColor = new Color(0.15f, 0.15f, 0.5f);

    private FruitsButtonData _currentData;
    private FruitsButtonData _prevData;
    
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
        _connectBackground = tree.Q<VisualElement>("connectBackground");
        #endregion
        
        for (int y = 0; y < Height; y++) {   
            for (int x = 0; x < Width; x++) {
                int localX = x;
                int localY = y;
                Button button = new Button();
                fruitsButtonDatas[localY, localX] = new FruitsButtonData(button);

                button.AddToClassList(_fruitsBtnName);
                button.style.backgroundColor = Color.grey;
                _background.Add(button);
                
                fruitsButtonDatas[localY, localX].Button.clicked += () 
                    => OnFruitsSelect(fruitsButtonDatas[localY, localX]);
                
                fruitsButtonDatas[localY, localX].Button.RegisterCallback<MouseDownEvent>
                    ((e) => OnFruitsChange(fruitsButtonDatas[localY, localX]));
            }
        }

        _selectFruitsBtn.clicked += ToggleSelectMode;
    }

    private void OnFruitsChange(FruitsButtonData data)
    {
        //if (_currentData.isActive == false) return;

        _currentData = data;
        _currentData.isActive = !_currentData.isActive;
        _currentData.Button.style.backgroundColor = _currentData.isActive ? Color.white : Color.grey;
    }

    private void OnFruitsSelect(FruitsButtonData data) //Fruits 버튼을 눌렀을 때
    {
        if (_currentData != null)
            _prevData = _currentData;
        _currentData = data;

        if (_currentData.isActive == false) return;
        
        if(_fruitsSelectMode) {
            AddFruitsData(data);
            return;
        }
        
        if (_currentData.FruitsButtonSO == null)
            _currentData.FruitsButtonSO = ScriptableObject.CreateInstance<FruitsSO>();

        foreach (FruitsType value in Enum.GetValues(typeof(FruitsType)))
            _fruitsTypeField.choices.Add(value.ToString());
        
        _fruitsTypeField.choices.Clear();


        SaveFruitsData(_currentData);
    }

    private void SaveFruitsData(FruitsButtonData data) //변경된 SO 데이터 저장
    {
        Debug.Log(data.FruitsButtonSO.intValue);
        //data.FruitsButtonSO.fruitsType = Enum.Parse<FruitsType>(_fruitsTypeField.value);
        data.FruitsButtonSO.intValue = int.Parse(_intField.value);
        data.FruitsButtonSO.floatValue = float.Parse(_floatField.value);
    }
    
    private void LoadFruitsData(FruitsButtonData data) //선택된 Fruits에 저장된 SO 데이터 로드
    {
       // _fruitsTypeField.value = data.FruitsButtonSO.fruitsType.ToString();
        _intField.value = data.FruitsButtonSO.intValue.ToString();
        _floatField.value = data.FruitsButtonSO.floatValue.ToString();
        
        data.FruitsButtonDataList.ForEach(fruits =>  _connectBackground.Add(fruits.Button));
    }
    
    //Tree자식 선택모드로 변환
    private void ToggleSelectMode()
    {
        _fruitsSelectMode = !_fruitsSelectMode;
        _background.style.backgroundColor = _fruitsSelectMode ? _selectModeColor : _backgroundColor;
    }

    private void AddFruitsData(FruitsButtonData data) //자식으로 선택된 Fruits를 추가
    {
        Button newButton = new Button();
        newButton.style.backgroundColor = data.Button.style.backgroundColor;
        newButton.clicked += () => OnFruitsSelect(data);

        _connectBackground.Add(newButton);
    }
}
