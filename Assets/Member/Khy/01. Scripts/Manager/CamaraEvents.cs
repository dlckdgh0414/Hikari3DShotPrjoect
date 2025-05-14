using Unity.Cinemachine;
using UnityEngine;

public enum CameraEffectEnum
{
    FOV,
    DUTCH,
    Impulse
}

public static class CamaraEvents
{
    public static SwapCameraEvent SwapCameraEvent = new();
    public static ShakeEvent CameraShakeEvent = new();
    public static CameraEffectEvent CameraEffectEvent = new();
}

public class SwapCameraEvent : GameEvent
{
    public CinemachineCamera leftCamera;
    public CinemachineCamera rightCamera;
}

public class CameraEffectEvent : GameEvent
{
    public CameraEffectEnum cameraEffect;
    public float value;
    public float second;
}

public class ShakeEvent : GameEvent
{
    public float intensity;
}