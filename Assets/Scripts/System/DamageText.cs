using TMPro;
using UnityEngine;

namespace Urxxx.System
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DamageText : MonoBehaviour
    {
        #region Private nonserialized fields

        private TextMeshProUGUI textObject;
        private Color currentColor;
        private float alphaValue = 1;
        private Vector3 textPosition;

        #endregion

        #region LifeCycle Method

        void Awake()
        {
            textObject = GetComponent<TextMeshProUGUI>();
            currentColor = textObject.color;
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            UpdateText();
        }

        #endregion

        #region Public Method

        public void SetupText(int damage, Vector3 position)
        {
            textObject.text = damage.ToString();
            textPosition = position;
        }

        #endregion

        #region Private Method

        private void UpdateText()
        {
            var uiPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, textPosition);

            alphaValue = Mathf.MoveTowards(alphaValue, 0, Time.deltaTime);
            uiPosition.y += (1 - alphaValue) * 40;
            transform.position = uiPosition;
            currentColor.a = alphaValue;
            textObject.color = currentColor;
            if (alphaValue <= 0.01f) Destroy(gameObject);
        }

        #endregion
    }
}