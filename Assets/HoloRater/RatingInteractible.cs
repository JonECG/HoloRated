using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace HoloRater
{ 
    public class RatingInteractible : MonoBehaviour {

        private bool _focused = false;
        private bool _selected = false;

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

        void OnGazeEnter()
        {
            if (!_focused)
            {
                OnHoverEnter.Invoke();
                _focused = true;
            }
        }

        void OnGazeLeave()
        {
            if (_focused)
            {
                OnHoverExit.Invoke();
                _focused = false;
            }
        }

        public void OnSelect()
        {
            OnGazeLeave();
            _selected = true;
            OnSelected.Invoke();
        }

        public void OnDeselect()
        {
            if( _selected )
            {
                OnDeselected.Invoke();
                _selected = false;
            }
        }


        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) && _focused)
            {
                OnSelect();
            }
        }

    }

}
