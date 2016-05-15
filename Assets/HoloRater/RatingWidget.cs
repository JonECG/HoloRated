using UnityEngine;
using System.Collections;
using System;

namespace HoloRater
{ 
    public class RatingWidget : MonoBehaviour {

        [SerializeField]
        private RatingEntry[] _ratingEmblems;

        private bool _currentlySelecting = false;

        [SerializeField]
        private bool _autoSelectLower = true;

        private int? _currentRating;
        public int? CurrentRating { get { return _currentRating; } }

        public void SetSelected(RatingEntry ratingEntry)
        {
            if (_currentlySelecting) // As this is event based, we set a guard to prevent infinite loops when cascading
                return;

            _currentlySelecting = true;

            if (!ratingEntry.RatingValue.HasValue)
            {
                // The rating value wasn't set, assume its position in the array as its value

                int? rating = null;

                for( int i = 0; i < _ratingEmblems.Length; i++ )
                {
                    if ( _ratingEmblems[i] == ratingEntry )
                    {
                        rating = i + 1;
                        break;
                    }
                }

                for( int i = 0; i < _ratingEmblems.Length; i++ )
                {
                    if( rating.HasValue && ( i == rating - 1 ) || (_autoSelectLower && i < rating ) )
                    {
                        _ratingEmblems[i].GetComponent<RatingInteractible>().OnSelect();
                    }
                    else
                    {
                        _ratingEmblems[i].GetComponent<RatingInteractible>().OnDeselect();
                    }
                }
            }
            else
            {
                foreach (RatingEntry entry in _ratingEmblems)
                {
                    if (_autoSelectLower && entry.RatingValue <= ratingEntry.RatingValue)
                    {
                        entry.GetComponent<RatingInteractible>().OnSelect();
                    }
                    else
                    {
                        entry.GetComponent<RatingInteractible>().OnDeselect();
                    }
                }

                ratingEntry.GetComponent<RatingInteractible>().OnSelect();
            }
            
            _currentRating = ratingEntry.RatingValue;
            _currentlySelecting = false;
        }
    }

}
