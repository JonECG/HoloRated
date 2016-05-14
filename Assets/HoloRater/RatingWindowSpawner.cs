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

            _currentWindow = GameObject.Instantiate(RatingWindowTemplate.gameObject).GetComponent<RatingWindow>();
            _currentWindow.gameObject.SetActive(true);
        }

        public void DismissWindow()
        {
            if (!_currentWindow)
                return;

            _currentWindow.Dismiss();
            _currentWindow = null;
        }
    }
}