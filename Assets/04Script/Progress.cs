using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Progress : MonoBehaviour
{

    [SerializeField]
    private Slider sliderProgress;
    
    [SerializeField]
    private TextMeshProUGUI textProgess;

    [SerializeField]
    private float progressTime; // 로딩바 재생 시간

    public void Play(UnityAction action = null)
    {
        StartCoroutine(OnProgress(action));
    }

        private IEnumerator OnProgress(UnityAction action)
        {
            float current = 0f;
            float percent = 0f;

            while(percent  < 1)
            {
                current += Time.deltaTime;
                percent = current / progressTime;

                // 로딩바 퍼센테이지 표시
                textProgess.text = $"Now Loading... {sliderProgress.value * 100:F0}%";
                // slider 값 변경
                sliderProgress.value = Mathf.Lerp(0,1,percent);

                yield return null;
            }

            // action이 null이 아니면 실행
            action?.Invoke();

        }


}
