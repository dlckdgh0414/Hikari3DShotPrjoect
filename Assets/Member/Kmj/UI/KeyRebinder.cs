using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing.MiniJSON;

public class KeyRebinder : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI bindingDisplayName;
    [SerializeField] private Button rebindButton;

    [Header("Binding Info")]
    [SerializeField] InputActionAsset _inputreader;
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
            Debug.LogError($"ActionMap '{actionMapName}' not found.");
            return;
        }

        _actionToRebind = map.FindAction(actionName);
        if (_actionToRebind == null)
        {
            Debug.LogError($"Action '{actionName}' not found in map '{actionMapName}'.");
            return;
        }

        string json = PlayerPrefs.GetString("rebinds", "");
        if (!string.IsNullOrEmpty(json))
        {
            _inputreader.LoadBindingOverridesFromJson(json);
            Debug.Log("오버라이드 적용 완료!");
        }
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
                operation.Dispose();
                _actionToRebind.Enable();
                rebindButton.interactable = true;
                UpdateBindingDisplay();
                print(_actionToRebind.ToString());
                SaveBindingOverride();
                string json = PlayerPrefs.GetString("rebinds", "");
                if (!string.IsNullOrEmpty(json))
                {
                    _inputreader.LoadBindingOverridesFromJson(json);
                    Debug.Log("오버라이드 적용 완료!");
                }
            })
            .OnCancel(operation =>
            {
                operation.Dispose();
                _actionToRebind.Enable();
                rebindButton.interactable = true;
                UpdateBindingDisplay();
                string json = PlayerPrefs.GetString("rebinds", "");
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
}