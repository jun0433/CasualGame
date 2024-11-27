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
    private float progressTime; // �ε��� ��� �ð�

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

                // �ε��� �ۼ������� ǥ��
                textProgess.text = $"Now Loading... {sliderProgress.value * 100:F0}%";
                // slider �� ����
                sliderProgress.value = Mathf.Lerp(0,1,percent);

                yield return null;
            }

            // action�� null�� �ƴϸ� ����
            action?.Invoke();

        }


}
