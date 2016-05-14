using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace HoloRater
{ 
    public class RatingEntry : MonoBehaviour {

        [SerializeField]
        private UnityEvent _onHoverEnter;
        [SerializeField]
        private UnityEvent _onHoverExit;
        [SerializeField]
        private UnityEvent _onSelected;
        [SerializeField]
        private UnityEvent _onDeselected;

        public UnityEvent OnHoverEnter { get { return _onHoverEnter; } }
        public UnityEvent OnHoverExit { get { return _onHoverExit; } }
        public UnityEvent OnSelected { get { return _onSelected; } }
        public UnityEvent OnDeselected { get { return _onDeselected; } }
        
    }

}
