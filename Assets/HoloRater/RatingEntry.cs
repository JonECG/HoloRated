using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace HoloRater
{ 
    public class RatingEntry : MonoBehaviour {

        private RatingWidget _widget = null;
        private RatingWidget Widget
        {
            get
            {
                if ( !_widget )
                {
                    _widget = GetComponentInParent<RatingWidget>();
                }
                return _widget;
            }
        }

        [SerializeField]
        private int _ratingValue = 0;
        public int? RatingValue
        {
            get
            {
                if (_ratingValue != 0)
                    return _ratingValue;
                else
                    return null;
            }
        }

        public void DoSetRating()
        {
            Widget.SetSelected(this);
        }

    }

}
