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
        private RatingSubmissionHandler _submissionHandler;

        [SerializeField]
        private Text _errorTextContainer = null;
        

	    // Use this for initialization
	    void Start () {
            _onShown.Invoke();
	    }
	
        public void Dismiss()
        {
            _onDismissed.Invoke();
        }

        public void Submit()
        {
            if(_submissionHandler)
            {
                _submissionHandler.SubmitRating(this, _ratingWidget);
            }
        }

        public void ProcessSubmissionResults( RatingSubmissionHandler.SubmissionResult results )
        {
            Debug.Break();
            if( results.Succeeded )
            {
                _onSubmitted.Invoke();
            }
            else
            {
                if( _errorTextContainer )
                {
                    _errorTextContainer.text = results.ErrorMessage;
                }
            }
        }
    }

}
