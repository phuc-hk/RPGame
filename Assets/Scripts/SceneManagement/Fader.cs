using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            //StartCoroutine(FadeOutIn());
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeIn(3);
            print("Fade in");
            yield return FadeOut(1);
            print("Fade out");
        }
        public IEnumerator FadeIn(float time)
        {
            while(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;  
            }
            
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }

        }

    }
}

