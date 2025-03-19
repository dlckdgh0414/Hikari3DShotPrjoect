using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class SkillTreeEditor : EditorWindow
{
    private readonly int Height = 12;
    private readonly int Width = 15;   

    private VisualElement _background;
    private Button _createBtn;
    private TextField _intField; 
    private TextField _floatField; 
    private DropdownField _skillTypeField;
    
    [MenuItem("Window/UI Toolkit/SkillTreeEditor")]
    public static void ShowExample()
    {
        SkillTreeEditor wnd = GetWindow<SkillTreeEditor>();
        wnd.titleContent = new GUIContent("SkillTreeEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        VisualTreeAsset asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/SkillTreeEditor.uxml");
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
                _datas[y, x] = new SeatData(false, button);
                _datas[y, x].Position = new Vector2(x, y);
                
                button.name = "seat";
                button.userData = _datas[y, x];
                button.clicked += () => OnSeatClicked(button);
                button.RegisterCallback<MouseDownEvent>((e) => OnSeatClicked(button, false));
                
                _background.Add(button);
            }
        }
    }
}
