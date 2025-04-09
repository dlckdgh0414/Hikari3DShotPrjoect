using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillTreeEditor : EditorWindow
{
    private const int Height = 5;
    private const int Width = 7;

    private readonly string _fruitsBtnName = "fruitsButton";

    private bool _fruitsSelectMode = false;
    
    private FruitsButtonData[,] fruitsButtonDatas = new FruitsButtonData[Height, Width];
    
    [SerializeField] private VisualTreeAsset _treeAsset;
    private VisualElement _background;
    private VisualElement _connectBackground;
    private VisualElement _previewBackground;
    private Button _createBtn;
    private Button _saveFruitsSOBtn;
    private Button _selectFruitsBtn;
    private TextField _intField; 
    private TextField _floatField;
    private TextField _treeNameField; 
    private DropdownField _fruitsTypeField;
    
    private Color _backgroundColor = new Color(0.15f, 0.15f, 0.15f);
    private Color _selectModeColor = new Color(0.15f, 0.15f, 0.5f);
    private Color hpColor = new Color(0.4f, 0.8f, 0.0f);
    private Color atkDmgColor = new Color(0.8f, 0f, 0.0f);
    private Color speedColor = new Color(0.0f, 0.5f, 1f);
    private Color skillColor = new Color(0.8f, 0.6f, 0.0f);
    private Color atkSpeedColor = new Color(0f, 0f, 0f);

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
        _treeNameField = tree.Q<TextField>("TreeNameField");
        _previewBackground = tree.Q<VisualElement>("PreviewBackground");
        
        #endregion
        
        for (int y = 0; y < Height; y++) {   
            for (int x = 0; x < Width; x++) {
                int localX = x;
                int localY = y;
                Button button = new Button();
                fruitsButtonDatas[localY, localX] = new FruitsButtonData(button);
                fruitsButtonDatas[localY, localX].Position = new Vector2(localX, localY);
                    
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
        _saveFruitsSOBtn.clicked += SaveFruitsData;
        _createBtn.clicked += GenerateTree;
        _fruitsTypeField.RegisterValueChangedCallback(evt => OnFruitsTypeFieldChanged());
    }

    private void OnFruitsTypeFieldChanged()
    {
        Color buttonCol = Color.white;

        switch (Enum.Parse(typeof(FruitsType), _fruitsTypeField.value))
        {
            case FruitsType.HP:
                buttonCol = hpColor;
                break;
            case FruitsType.AttackDamage:
                buttonCol = atkDmgColor;
                break;
            case FruitsType.Speed:
                buttonCol = speedColor;
                break;
            case FruitsType.Skill:
                buttonCol = skillColor;
                break;
            case FruitsType.AttackSpeed:
                buttonCol = atkSpeedColor;
                break;
        }
        
        _currentData.Button.style.backgroundColor = buttonCol;
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
        if(_fruitsSelectMode) {
            AddFruitsData(data);
            return;
        }
        
        _currentData = data;

        if (_currentData.isActive == false) return;
        
        if (_currentData.FruitsButtonSO == null)
            _currentData.FruitsButtonSO = ScriptableObject.CreateInstance<FruitsSO>();

        _fruitsTypeField.choices.Clear();

        foreach (FruitsType value in Enum.GetValues(typeof(FruitsType)))
            _fruitsTypeField.choices.Add(value.ToString());
        
        LoadFruitsData(_currentData);
    }

    private void SaveFruitsData() //변경된 SO 데이터 저장
    {
        _currentData.FruitsButtonSO.fruitsType = Enum.Parse<FruitsType>(_fruitsTypeField.value);
        _currentData.FruitsButtonSO.intValue = int.Parse(_intField.value);
        _currentData.FruitsButtonSO.floatValue = float.Parse(_floatField.value);
    }
    
    private void LoadFruitsData(FruitsButtonData data) //선택된 Fruits에 저장된 SO 데이터 로드
    {
        _fruitsTypeField.value = data.FruitsButtonSO.fruitsType.ToString();
        _intField.value = data.FruitsButtonSO.intValue.ToString();
        _floatField.value = data.FruitsButtonSO.floatValue.ToString();
        
        _connectBackground.Clear();
        data.FruitsButtonDataList.ForEach(fruits => _connectBackground.Add(CopyFruitsButton(fruits.Button))); // <= 이새끼 일단 문제 있음 농구 갔다와서 고쳐라
    }
    
    //Tree자식 선택모드로 변환
    private void ToggleSelectMode()
    {
        _fruitsSelectMode = !_fruitsSelectMode;
        _background.style.backgroundColor = _fruitsSelectMode ? _selectModeColor : _backgroundColor;
    }

    private void AddFruitsData(FruitsButtonData data) //자식으로 선택된 Fruits를 추가
    {
        _currentData.AddFruitsButtonData(data);
        _connectBackground.Add(CopyFruitsButton(data.Button));
    }

    private Button CopyFruitsButton(Button button)
    {
        Button newButton = new Button();
        newButton.AddToClassList(_fruitsBtnName);
        newButton.style.backgroundColor = button.style.backgroundColor;
        
        return newButton;
    }

    private void GenerateTree()
    {
        GameObject prefab = new GameObject();
        prefab.name = _treeNameField.value;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (fruitsButtonDatas[y, x].isActive)
                {
                    GenerateUI(fruitsButtonDatas[y, x]);
                }
            }
        }
        
        string path = $"Assets/Member/Kmin/TestTree/{_treeNameField.value}.prefab";
        PrefabUtility.SaveAsPrefabAsset(prefab, path);
        DestroyImmediate(prefab);
    }

    private void GenerateUI(FruitsButtonData data)
    {
        if (data.FruitsButtonDataList.Count > 0)
        {
            data.FruitsButtonDataList.ForEach(fruits => DrawLine(data.Button, fruits.Button));
        }
    }
    
    private void DrawLine(Button from, Button to)
    {
        Vector2 fromPos = from.worldBound.center;
        Vector2 toPos = to.worldBound.center;

        VisualElement line = new VisualElement();
        line.style.position = Position.Absolute;
        line.style.backgroundColor = Color.white;
        line.style.width = 2;
        line.style.height = Vector2.Distance(fromPos, toPos);

        float angle = Mathf.Atan2(toPos.y - fromPos.y, toPos.x - fromPos.x) * Mathf.Rad2Deg;
        //line.style.transformOrigin = new StyleTransformOrigin(Length.Percent(50f), Length.Percent(0));
        line.style.rotate = new StyleRotate(new Rotate(angle));

        line.style.left = fromPos.x;
        line.style.top = fromPos.y;

        _connectBackground.Add(line);
    }
}
