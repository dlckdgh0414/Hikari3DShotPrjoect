using DG.Tweening;
using UnityEngine;

public class DodgeSkill : ActiveSkill
{
    [field: SerializeField] public GameEventChannelSO CameraChannel { get; private set; }
    [field: SerializeField] public float SpinCameraEffectSec;
    [field: SerializeField] public float SpinCameraEffectIntensity;
    private readonly string dodgeSkill = "DodgeVFX";

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        _player.InputReader.OnWingEvent += OnWingHandle;
    }

    private void OnWingHandle(int dir)
    {
        if(AttemptUseSkill())
        {
            if (!DOTween.IsTweening(_player.model.transform))
            {
                entityVFX.PlayVfx(dodgeSkill, Vector3.zero, Quaternion.identity);
                _mover.StopImmediately();
                _player.IsInvin = true;
                _mover.CanManualMove = false;
                _mover.SetAutoMovement(new Vector3(dir * 25, 0, 0));
                CameraDutchEffect(dir);
                isUsingSkill = false;

                _player.model.transform.DOLocalRotate(new Vector3(_player.transform.localEulerAngles.x, _player.transform.localEulerAngles.y, 360 * -dir), .4f, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.OutSine).OnComplete(() => {
                        _mover.CanManualMove = true;
                        _player.IsInvin = false;
                    });
            }
        }
    }
    private void OnDestroy()
    {
        _player.InputReader.OnWingEvent -= OnWingHandle;
    }

    private void CameraDutchEffect(int dir)
    {
        CameraEffectEvent effectCamera = CamaraEvents.CameraEffectEvent;
        effectCamera.cameraEffect = CameraEffectEnum.DUTCH;
        effectCamera.second =SpinCameraEffectSec;
        effectCamera.value = SpinCameraEffectIntensity * -dir;

        CameraChannel.RaiseEvent(effectCamera);
    }

    protected override void Update()
    {
        base.Update();
    }
}
