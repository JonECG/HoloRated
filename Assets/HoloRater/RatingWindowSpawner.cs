using UnityEngine;
using System.Collections;

namespace HoloRater
{
    public class RatingWindowSpawner : MonoBehaviour
    {

        [SerializeField]
        private RatingWindow _ratingWindowTemplate;
        public RatingWindow RatingWindowTemplate { get { return _ratingWindowTemplate; } }

        private RatingWindow _currentWindow = null;
        // Use this for initialization
        public void ShowWindow()
        {
            if (_currentWindow)
                return;

            GameObject windowObject = Instantiate(RatingWindowTemplate.gameObject, new Vector3(), Quaternion.identity) as GameObject;
            windowObject.SetActive(true);
            windowObject.transform.SetParent(transform, false);
            _currentWindow = windowObject.GetComponent<RatingWindow>();
        }

        public void DismissWindow()
        {
            if (!_currentWindow)
                return;

            _currentWindow.Dismiss();
            _currentWindow = null;   
        }

        void OnDrawGizmos()
        {
            if (!_currentWindow)
            {
                Gizmos.matrix = transform.localToWorldMatrix;

                Gizmos.color = Color.white;
                Gizmos.DrawCube(new Vector3(0, 0, 0.01f), new Vector3(1, 1, 0.01f));

                Gizmos.color = Color.blue;
                Gizmos.DrawFrustum(new Vector3(), 130, -0.2f, 0, 1);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(new Vector3(), new Vector3(1, 0, 0));
                Gizmos.color = Color.green;
                Gizmos.DrawLine(new Vector3(), new Vector3(0, 1, 0));
            }
        }
    }
}