using DG.Tweening;
using MoreMountains.Feedbacks;
using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Ami.BroAudio;

public class SpaceLogic : MonoBehaviour
{
    [SerializeField]
    private SoundID mainmenuBGM;
    [SerializeField]
    private CinemachineCamera camera;
    [SerializeField] private InputReader InputReader;

    public GameObject[] fields;
    public GameObject[] fieldObj;

    private int StageField;
    private int prevStageField;

    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private TextMeshProUGUI header;

    [SerializeField]
    private TextMeshProUGUI WarningText;

    [SerializeField]
    private CinemachineCamera cinemachine;

    [SerializeField]
    private MMF_Player mmf_Player;
    [SerializeField]
    private Image _black;

    //시간 없다ㅏㅏㅏㅏㅏㅏㅏㅏ이낙ㅎ뫃뮤ㅣㄴ닫류하덕ㅈ디쀼ㅣㄹ
    private void Awake()
    {
        InputReader.OnWingEvent += MoveStage;
    }
    private void Start()
    {
        BroAudio.Play(mainmenuBGM);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(StageField == 0)
            {
                Sequence seq = DOTween.Sequence();
                mmf_Player.PlayFeedbacks();
                seq.Append(DOTween.To(
                    () => cinemachine.Lens.FieldOfView,
                    x =>
                    {
                        var lens = cinemachine.Lens;
                        lens.FieldOfView = x;
                        cinemachine.Lens = lens;
                    },
                    180f,
                    1f
                )).Append(DOTween.To(
                    () => cinemachine.Lens.FieldOfView,
                    x =>
                    {
                        var lens = cinemachine.Lens;
                        lens.FieldOfView = x;
                        cinemachine.Lens = lens;
                    },
                    0f,
                    0.3f
                )).Join(DOTween.To(
                    () => cinemachine.Lens.Dutch,
                    x =>
                    {
                        var lens = cinemachine.Lens;
                        lens.Dutch = x;
                        cinemachine.Lens = lens;
                    },
                    360f,
                    0.3f)).Join(_black.DOFade(1, 0.3f))
                    .OnComplete(() => {
                        SceneManager.LoadScene("ShipStation");
                        BroAudio.Stop(mainmenuBGM);
                    });
            }
            else if(StageField == 1)
            {
                WarningText.DOKill();
                WarningText.DOFade(1f,1f).OnComplete(()=>WarningText.DOFade(0f,1f));
            }
        }
    }

    private void MoveStage(int obj)
    {//모르것담ㅇㄶㅍ지;ㅗㅎㅁ;ㅈ
        prevStageField = StageField;
        if (StageField + obj >= fields.Length && obj > 0) StageField = 0;
        else if (StageField + obj < 0) StageField = fields.Length -1;
        else StageField += obj;
        camera.transform.DOKill();
        camera.transform.DOMove(fields[StageField].transform.position,0.2f).SetEase(Ease.InOutSine);

        if (fieldObj[StageField].name == "Space Station")
        {
            header.text = "우주 정거장";
            description.text = "히카리호.\n 곧 지구로 떠날 예정이다.";
        }
        if (fieldObj[StageField].name == "???")
        {
            header.text = "???";
            description.text = "아직 공개되지 않았습니다.";
        }
    }

        private void OnDestroy()
    {
        InputReader.OnWingEvent -= MoveStage;
    }
}
