using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Urxxx.GamePlay
{

    public class SimpleBullet : MonoBehaviour
    {
        public GameObject HitEffect;
        protected Ray2D BulletRay;
        protected Vector3 BulletStartPosition;
        protected Vector3 BulletDirection;
        protected bool IsBulletLaunch = false;
        public float BulletSpeed = 1;
        public float BulletRange = 10;
        public float BulletAccuracy = 100f;
        public float Damage = 1;

        private Vector3 m_previousFramePosition;

        // Start is called before the first frame update
        void Start()
        {
        }

        public void SetBulletTarget(Vector3 startPosition, Vector3 targetPosition)
        {
            var randomVal = (100 - BulletAccuracy) / 100f;
            var randomVec = new Vector3(Random.Range(-randomVal, randomVal), Random.Range(-randomVal, randomVal), 0);

            transform.position = startPosition;
            BulletStartPosition = startPosition;
            BulletDirection = ((targetPosition - BulletStartPosition).normalized + randomVec).normalized;
            float angle = Mathf.Atan2(BulletDirection.y, BulletDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            BulletRay = new Ray2D(BulletStartPosition, BulletDirection);
            IsBulletLaunch = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsBulletLaunch) return;
            m_previousFramePosition = transform.position;
            transform.position += BulletDirection * BulletSpeed * Time.deltaTime;
            LayerMask hitLayer = LayerMask.GetMask("ITarget");
            RaycastHit2D hit = Physics2D.Raycast(BulletStartPosition, BulletDirection, BulletRange, hitLayer);
            // If it hits something...
            if (hit.collider != null)
            {
                //calculate collider range
                if (IsBetweenPreviousFrame(hit.point) || IsPointInsideCollider(hit.collider))
                {
                    var rotation = transform.rotation.eulerAngles;
                    rotation.z += 180;
                    var randomVal = 0.02f;
                    var hitEffect = Instantiate(HitEffect,
                        hit.point + new Vector2(Random.Range(-randomVal, randomVal),
                            Random.Range(-randomVal, randomVal)), Quaternion.Euler(rotation));
                    Destroy(gameObject);
                    //var enemy = hit.transform.parent?.GetComponent<IEnemy>();
                    //if (enemy != null)
                    //{
                    //    enemy.DamageTaken(Damage);
                    //}
                }
            }

            if ((transform.position - BulletStartPosition).magnitude > BulletRange)
            {
                Destroy(gameObject);
            }
        }

        private bool IsBetweenPreviousFrame(Vector3 position)
        {
            var previousRange = (m_previousFramePosition - BulletStartPosition).magnitude;
            var currentRange = (transform.position - BulletStartPosition).magnitude;
            var testRange = (position - BulletStartPosition).magnitude;
            return previousRange <= testRange && testRange <= currentRange;
        }

        private bool IsPointInsideCollider(Collider2D collider)
        {
            return collider.OverlapPoint(m_previousFramePosition) || collider.OverlapPoint(transform.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(BulletStartPosition, BulletStartPosition + BulletDirection * 5);
            Handles.Label(BulletStartPosition, $"{BulletDirection}");
        }
    }

}