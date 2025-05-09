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

        public UnityEvent OnClear;

        private Coroutine _coroutine;
        
        private void Awake()
        {
            SliderInit();
        }

        private void SliderInit()
        {
            _slider.minValue = 0;
            _slider.maxValue = enemyCountData.StageEnemyCount;
            _slider.value = 0;
        }


        public void HandleEnemyDeadCount()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(UpProgress());
        }

        public IEnumerator UpProgress()
        {
            float percent = 0.01f;
            float target = _slider.value + 1;
            while (percent < 1)
            {
                _slider.value = Mathf.Lerp(_slider.value, target, (percent += 0.01f));
                yield return null;
            }
            
            if (_slider.value >= _slider.maxValue)
            {
                Debug.Log("클리어!");
                OnClear?.Invoke();
                Time.timeScale = 0;
            }
        }
    }
}