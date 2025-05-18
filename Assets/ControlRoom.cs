using UnityEngine;
using DG.Tweening;

public class ControlRoom : MonoBehaviour,IEntityComponent
{
    public GameObject model;
    private Player _player;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _player.InputReader.OnWingEvent += Roll;
        _player.InputReader.OnXMoveEvent += MoveHandle;
    }

    private void MoveHandle(float obj)
    {

        Sequence s = DOTween.Sequence().OnStart(() =>
        {
        });
        s.Append(model.transform.DOLocalRotate(new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y, 10 * obj), .4f).SetEase(Ease.Linear))
        .Append(model.transform.DOLocalRotate(new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y, 0), .4f).SetEase(Ease.InOutExpo));
    }

    public void Roll(int dir)
    {
        Sequence s = DOTween.Sequence().OnStart(()=>
        {

        });
        s.Append(model.transform.DOLocalRotate(new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y, 20 * -dir), .4f).SetEase(Ease.InOutExpo))
        .Append(model.transform.DOLocalRotate(new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y, 0), .4f).SetEase(Ease.OutSine));
    }

}
