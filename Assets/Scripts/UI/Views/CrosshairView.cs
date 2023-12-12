using System.Collections;
using UnityEngine;

namespace UI.Views
{
    public class CrosshairView : MonoBehaviour
    {
        [SerializeField] private RectTransform _crosshair;
        [SerializeField] private GameObject _crosshairHit;
        [SerializeField] private float _hitShowTime = 0.25f;

        private Coroutine _coroutine;
        
        public void OnHit()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(HitAnimation());
        }

        private IEnumerator HitAnimation()
        {
            _crosshairHit.SetActive(true);
            yield return new WaitForSeconds(_hitShowTime);
            _crosshairHit.SetActive(false);

            _coroutine = null;
        }

        public void SetSpread(float spread)
        {
            _crosshair.sizeDelta = new Vector2(spread, spread);
        }

        public void SetRectTransform(Vector2 transformPosition)
        {
            _crosshair.position = transformPosition;
            _crosshairHit.transform.position = transformPosition;
        }
    }
}