using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urxxx.System;

namespace Urxxx.GamePlay
{
    public class DamageSystem : MonoBehaviour
    {
        public static DamageSystem Instance = null;

        #region Protect serialized fields

        [SerializeField] protected GameObject DamageTextPrefab;

        #endregion

        #region LifeCycle Method

        void Awake()
        {
            if (Instance == null) Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region Public Method

        public void DamagingTarget(IDamageDealer dealer, Transform targetTransform)
        {
            float damage = dealer.DealDamage();
            var target = targetTransform.GetComponent<ITarget>();
            if (target != null)
            {
                target.DamageTaken(damage);
                PopupDamageText(damage, targetTransform.position);
            }
        }

        #endregion

        #region Private Method

        private void PopupDamageText(float damage, Vector3 position)
        {
            var uiPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, position);
            GameObject damageObj = Instantiate(DamageTextPrefab, uiPosition, Quaternion.identity, transform);
            damageObj.GetComponent<DamageText>()?.SetupText((int)damage, position);
        }

        #endregion
    }
}