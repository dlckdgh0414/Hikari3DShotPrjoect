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
        [SerializeField]
        public UnityEvent OnArriveMiddlePoint;
        private bool isOneTime;

        private TestBoss currentBoss;

        private void Awake()
        {
            SliderInit();
            
        }

        private void OnDestroy()
        {
            if(currentBoss != null)
                currentBoss.OnDead.RemoveListener(CheatClear);
            currentBoss = null;
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
        private void Update()
        {
            if(Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.P))
            {
                CheatClear();
            }
        }

        public void CheatClear()
        {
            Time.timeScale = 0f;
            StartDialogueEvent dialogueEvent = UIEvents.StartDialogueEvent;
            dialogueEvent.dialogue = _clearDialogue;
            uiManager.RaiseEvent(dialogueEvent);
            Entity.IsGameStart = false;
            ClearGame.IsCLEAR = true;

            OnClear?.Invoke();
        }

        public void HandleEnemyDeadCount()
        {
            if(currentEnemyCount + 1 == maxEnemyCount)
            {
                CheatClear();
            }
            //else if(currentEnemyCount == maxEnemyCount / 2)
            else if(currentEnemyCount == maxEnemyCount / 2 && !isOneTime)
            {
                isOneTime = true;
                OnArriveMiddlePoint?.Invoke();
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