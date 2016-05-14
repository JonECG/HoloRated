using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HoloRater
{ 
    public class RatingWindow : MonoBehaviour {

        [SerializeField]
        private UnityEvent _onShown = new UnityEvent();
        [SerializeField]
        private UnityEvent _onDismissed = new UnityEvent();
        [SerializeField]
        private UnityEvent _onSubmitted = new UnityEvent();

        [SerializeField]
        private RatingWidget _ratingWidget;

        [SerializeField]
        private Button _button;

	    // Use this for initialization
	    void Start () {
            _onShown.Invoke();
            _button.onClick.AddListener(Submit);
	    }
	
        public void Dismiss()
        {
            _onDismissed.Invoke();
        }

        public void Submit()
        {
            _onSubmitted.Invoke();
        }
    }

}
