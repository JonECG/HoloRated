using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace HoloRater
{ 
    public abstract class RatingSubmissionHandler : MonoBehaviour {

        public struct SubmissionResult
        {
            public bool Succeeded;
            public string ErrorMessage;
        }
        
        public abstract void SubmitRating(RatingWindow window, RatingWidget widget);
    }

}
