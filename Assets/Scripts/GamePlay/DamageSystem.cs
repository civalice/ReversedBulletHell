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

        public void DamagingTarget(IDamageDealer dealer, ITarget target)
        {
            float damage = dealer.DealDamage();
            if (target != null)
            {
                target.DamageTaken(damage);
                PopupDamageText(damage, target.GetTargetTransform().position);
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