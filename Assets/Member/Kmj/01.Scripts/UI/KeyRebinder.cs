using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing.MiniJSON;
using TMPro.Examples;

public class KeyRebinder : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI bindingDisplayName;
    [SerializeField] private Button rebindButton;

    [Header("Binding Info")]
    [SerializeField] InputReader _inputreader;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string actionMapName = "PlayerMap";
    [SerializeField] private string actionName;
    [SerializeField] private int bindingIndex = 0;

    private InputAction _actionToRebind;

    private void Awake()
    {
        if (rebindButton != null)
        {
            rebindButton.onClick.AddListener(() => StartRebind());
        }

        var map = inputActions.FindActionMap(actionMapName);
        if (map == null)
        {
            return;
        }

        _actionToRebind = map.FindAction(actionName);
        if (_actionToRebind == null)
        {
            return;
        }

        string json = PlayerPrefs.GetString("rebinds", string.Empty);
        if (!string.IsNullOrEmpty(json))
        {
            _inputreader._controlls.LoadBindingOverridesFromJson(json);
        }
        _inputreader.Initialize(_inputreader._controlls);
    }

    private void OnEnable()
    {
        LoadBindingOverride();
        UpdateBindingDisplay();
    }

    private void StartRebind()
    {
        if (_actionToRebind == null) return;

      
        
        _actionToRebind.Disable();

        bindingDisplayName.text = "입력 대기중...";
        rebindButton.interactable = false;

        _actionToRebind.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(operation =>
            {
                if (IsBindingDuplicate(path))
                {
                    Debug.LogWarning($"'{path}'는 이미 다른 액션에 사용 중입니다.");
                    bindingDisplayName.text = "중복된 키입니다!";  
                    return;
                }
                
                UpdateBindingDisplay();
                operation.Dispose();
                _actionToRebind.Enable();
                
                string json = PlayerPrefs.GetString("rebinds", string.Empty);
                if (!string.IsNullOrEmpty(json))
                {
                    _inputreader._controlls.LoadBindingOverridesFromJson(json);
                    _inputreader.Initialize(_inputreader._controlls);
                }
                

                rebindButton.interactable = true;
                UpdateBindingDisplay();
                SaveBindingOverride();
                
                print("실행됨");
                
            })
            .OnCancel(operation =>
            {
                operation.Dispose();
                _inputreader._controlls.Enable();
                _inputreader.Initialize(_inputreader._controlls);

                rebindButton.interactable = true;
                UpdateBindingDisplay();
                
            })
            .Start();
    }

    private void UpdateBindingDisplay()
    {
        if (bindingDisplayName != null && _actionToRebind != null)
        {
            bindingDisplayName.text = InputControlPath.ToHumanReadableString(
                _actionToRebind.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice
            );
        }
    }

    private void SaveBindingOverride()
    {
        string rebindJson = inputActions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebindJson);
        PlayerPrefs.Save();
    }

    private void LoadBindingOverride()
    {
        string rebindJson = PlayerPrefs.GetString("rebinds", string.Empty);
        if (!string.IsNullOrEmpty(rebindJson))
        {
            inputActions.LoadBindingOverridesFromJson(rebindJson);
        }
    }
    
    private bool IsBindingDuplicate(string path)
    {
        foreach (var map in inputActions.actionMaps)
        {
            foreach (var action in map.actions)
            {
                if (action == _actionToRebind) continue;

                foreach (var binding in action.bindings)
                {
                    if (binding.effectivePath == path)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}