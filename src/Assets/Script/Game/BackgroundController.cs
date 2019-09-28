using System.Collections;
using UnityEngine;

namespace Script.Game
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private Color[] colors;
        [SerializeField] private ParticleSystem particleSystem;

        private Coroutine transitionCoroutine;
        [SerializeField] private float transitionDuration;

        public void SetRandomColor()
        {
            var newColor = colors[Random.Range(0, colors.Length)];
            if (transitionCoroutine != null)
                StopCoroutine(transitionCoroutine);
            transitionCoroutine = StartCoroutine(SetRandomColorTransition(newColor));
        }

        private IEnumerator SetRandomColorTransition(Color newColor)
        {
            var module = particleSystem.main;
            float currentLerpTime = 0;
            var currColor = module.startColor.color;
            while (currColor != newColor)
            {
                currentLerpTime += Time.deltaTime;
                if (currentLerpTime > transitionDuration)
                    currentLerpTime = transitionDuration;

                currColor = Color.Lerp(currColor, newColor, currentLerpTime / transitionDuration);
                module.startColor = currColor;
                yield return null;
            }
        }
    }
}