using System;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera currentCamera;
    [SerializeField] private int activeCameraPriority = 15;
    [SerializeField] private int disableCameraPriority = 10;
    [SerializeField] private GameEventChannelSO cameraChannel;

    private void Awake()
    {
        cameraChannel.AddListener<SwapCameraEvent>(HandleSwapCamera);
        cameraChannel.AddListener<PerlinShakeEvent>(HandleShakeCamera);
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

    private void HandleShakeCamera(PerlinShakeEvent obj)
    {

    }

    private void HandleSwapCamera(SwapCameraEvent swapEvt)
    {
        if (currentCamera == swapEvt.leftCamera)
            ChangeCamera(swapEvt.rightCamera);
        else if (currentCamera == swapEvt.rightCamera)
            ChangeCamera(swapEvt.leftCamera);
    }
    public void ChangeCamera(CinemachineCamera newCamera)
    {
        currentCamera.Priority = disableCameraPriority;
        currentCamera = newCamera;
        currentCamera.Priority = activeCameraPriority;
    }
    private void OnDestroy()
    {
        cameraChannel.RemoveListener<PerlinShakeEvent>(HandleShakeCamera);
        cameraChannel.RemoveListener<SwapCameraEvent>(HandleSwapCamera);
    }
}
