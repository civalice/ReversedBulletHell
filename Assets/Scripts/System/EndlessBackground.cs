using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


namespace Urxxx.System
{
    public class EndlessBackground : MonoBehaviour
    {
        public static List<EndlessBackground> BgList = new List<EndlessBackground>();

        #region Private serialized fields

        [SerializeField] private Vector3 tilingOffset = new Vector3(0.32f, 0.32f, 0);

        #endregion

        #region Private nonserialized fields

        private Transform cameraTransform = null;

        #endregion

        #region LifeCycle Method

        void Awake()
        {
            EndlessBackground.BgList.Add(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            if (cameraTransform == null) return;
            UpdateDiffTiling();
        }

        #endregion

        #region Public Method

        public static void SetAllTargetCamera(Transform camTransform)
        {
            foreach (var bg in BgList)
            {
                bg.SetTargetCamera(camTransform);
            }
        }
        public void SetTargetCamera(Transform camTransform)
        {
            cameraTransform = camTransform;
        }

        #endregion

        #region Private Method

        private void Initialize()
        {
            var camTransform= cameraTransform.position;
            camTransform.z = transform.position.z;
            transform.position = camTransform;
        }

        private void UpdateDiffTiling()
        {
            Vector3 diff = cameraTransform.position - transform.position;
            Vector3 nextTransform = transform.position;
            if (Mathf.Abs(diff.x) >= tilingOffset.x)
            {
                nextTransform.x += diff.x;
            }

            if (Mathf.Abs(diff.y) >= tilingOffset.y)
            {
                nextTransform.y += diff.y;
            }

            nextTransform.z = transform.position.z;
            transform.position = nextTransform;
        }

        #endregion
    }
}