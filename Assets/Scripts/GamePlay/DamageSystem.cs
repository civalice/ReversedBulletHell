using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class DamageSystem : MonoBehaviour
    {
        public static DamageSystem Instance = null;

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
            }
        }

        #endregion
    }
}