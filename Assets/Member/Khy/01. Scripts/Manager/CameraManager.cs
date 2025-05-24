using System;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera currentCamera;
    [SerializeField] private int activeCameraPriority = 15;
    [SerializeField] private int disableCameraPriority = 10;
    [SerializeField] private GameEventChannelSO cameraChannel;

    public UnityEvent OnCameraShakeing;

    private void Awake()
    {
        cameraChannel.AddListener<SwapCameraEvent>(HandleSwapCamera);
        cameraChannel.AddListener<ShakeEvent>(HandleShakeCamera);
        cameraChannel.AddListener<CameraEffectEvent>(HandleEffectCamera);

        currentCamera = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None)
                        .FirstOrDefault(cam => cam.Priority == activeCameraPriority);
        Debug.Assert(currentCamera != null, $"Check camera priority, there is no active camera");
        ChangeCamera(currentCamera);
    }

    private void HandleEffectCamera(CameraEffectEvent obj)
    {
        if(obj.cameraEffect == CameraEffectEnum.DUTCH)
        {
            DOTween.To(() => currentCamera.Lens.Dutch, x => currentCamera.Lens.Dutch = x, obj.value, obj.second).SetEase(Ease.InFlash).OnComplete(() => 
                DOTween.To(() => currentCamera.Lens.Dutch, x => currentCamera.Lens.Dutch = x, 0, obj.second))
                    .SetEase(Ease.OutFlash);
        }
    }

    private void HandleShakeCamera(ShakeEvent obj)
    {
        Debug.Assert(currentCamera.GetComponent<CinemachineImpulseSource>() != null, $"Check camera priority, there is no active camera");
        OnCameraShakeing?.Invoke();
    }

    private void HandleSwapCamera(SwapCameraEvent swapEvt)
    {
        if (currentCamera != swapEvt.changeCamera)
            ChangeCamera(swapEvt.changeCamera);
    }
    public void ChangeCamera(CinemachineCamera newCamera)
    {
        currentCamera.Priority = disableCameraPriority;
        currentCamera = newCamera;
        currentCamera.Priority = activeCameraPriority;
    }
    private void OnDestroy()
    {
        cameraChannel.RemoveListener<ShakeEvent>(HandleShakeCamera);
        cameraChannel.RemoveListener<SwapCameraEvent>(HandleSwapCamera);
        cameraChannel.RemoveListener<CameraEffectEvent>(HandleEffectCamera);
    }
}
