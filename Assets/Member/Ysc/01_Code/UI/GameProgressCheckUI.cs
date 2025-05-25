using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Member.Ysc._01_Code.UI
{
    public class GameProgressCheckUI : MonoBehaviour
    {
        [SerializeField] private EnemySpawnListSO enemyCountData;

        [SerializeField] private Slider _slider;
        [SerializeField] private Slider _backSlider;

        public UnityEvent OnClear;
        
        private int currentEnemyCount;
        private int maxEnemyCount;
        [SerializeField]
        private string[] _clearDialogue;
        [SerializeField]
        private GameEventChannelSO uiManager;

        private void Awake()
        {
            SliderInit();
        }

        private void SliderInit()
        {
            maxEnemyCount = enemyCountData.StageEnemyCount;
            currentEnemyCount = 0;
            _slider.maxValue = maxEnemyCount;
            _slider.minValue = 0;
            _slider.value = 0;
            if (_backSlider != null)
            {
                _backSlider.maxValue = enemyCountData.StageEnemyCount;
                _backSlider.minValue = 0;
                _backSlider.value = 0;
            }
        }


        public void HandleEnemyDeadCount()
        {
            if(currentEnemyCount + 1 == maxEnemyCount)
            {
                StartDialogueEvent dialogueEvent = UIEvents.StartDialogueEvent;
                dialogueEvent.dialogue = _clearDialogue;
                uiManager.RaiseEvent(dialogueEvent);

                OnClear?.Invoke();
            }
            else
            {
                currentEnemyCount += 1;
                UpProgress();
            }
        }

        public void UpProgress()
        {
            _slider.value = currentEnemyCount;

            if (_backSlider != null && _backSlider.value > _slider.value)
            {
                DOTween.Sequence()
                    .AppendInterval(0.2f)
                    .Append(_backSlider.DOValue(currentEnemyCount, 0.5f).SetEase(Ease.OutCubic));
            }
            else if (_backSlider != null && _backSlider.value < _slider.value)
            {
                _backSlider.value = currentEnemyCount;
            }
        }
    }
}