using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HoloRater
{
    public class TransitionsAndEffects : MonoBehaviour
    {
        private Dictionary<string, Vector3> positionEffectors = new Dictionary<string, Vector3>();
        private Dictionary<string, Quaternion> rotationEffectors = new Dictionary<string, Quaternion>();
        private Dictionary<string, Vector3> scaleEffectors = new Dictionary<string, Vector3>();

        private bool _floating = false;
        private bool _wobbling = false;

        private Transform _container;

        void Start()
        {
            // On start, we make a containing game object purely to manipulate transform
            bool useRect = GetComponent<RectTransform>() != null;

            GameObject go;
            if (useRect)
            {
                go = new GameObject("Transition Container", new System.Type[] { typeof(RectTransform) });
                var rectTrans = go.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
            }
            else
            {
                go = new GameObject("Transition Container");
            }
            _container = go.transform;
            _container.SetParent(transform.parent, false);
            transform.SetParent(_container, false);
        }

        void Update()
        {
            _container.localPosition = new Vector3();
            _container.localRotation = Quaternion.identity;

            RectTransform rect = _container.GetComponent<RectTransform>();
            if( rect )
            {
                rect.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                _container.localScale = new Vector3(1, 1, 1);
            }

            foreach (var pair in positionEffectors)
            {
                _container.localPosition += pair.Value;
            }

            foreach (var pair in rotationEffectors)
            {
                _container.localRotation *= pair.Value;
            }

            foreach(var pair in scaleEffectors)
            {
                if (rect)
                {
                    rect.localScale.Set(rect.localScale.x * pair.Value.x, rect.localScale.y * pair.Value.y, rect.localScale.z * pair.Value.z);
                }
                else
                {
                    _container.localScale.Set(_container.localScale.x * pair.Value.x, _container.localScale.y * pair.Value.y, _container.localScale.z * pair.Value.z);
                }
            }
        }

        private void SetPosition( string key, Vector3 position )
        {
            positionEffectors[key] = position;
        }
        private void SetRotation(string key, Quaternion rotation)
        {
            rotationEffectors[key] = rotation;
        }
        private void SetScale(string key, Vector3 scale)
        {
            scaleEffectors[key] = scale;
        }

        private Vector3 GetPosition( string key )
        {
            if( positionEffectors.ContainsKey( key ) )
            {
                return positionEffectors[key];
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
        }
        private Quaternion GetRotation(string key)
        {
            if (rotationEffectors.ContainsKey(key))
            {
                return rotationEffectors[key];
            }
            else
            {
                return Quaternion.identity;
            }
        }
        private Vector3 GetScale(string key)
        {
            if (scaleEffectors.ContainsKey(key))
            {
                return scaleEffectors[key];
            }
            else
            {
                return new Vector3(1, 1, 1);
            }
        }

        public void Grow(float duration)
        {

            SetScale("Grow", new Vector3(0, 0, 0));
            StopCoroutine("GrowCoroutine");
            StartCoroutine(GrowCoroutine(duration));
        }

        private IEnumerator GrowCoroutine(float duration)
        {
            float startTime = Time.time;
            float endTime = Time.time + duration;
            while (Time.time < endTime)
            {   
                float ratio = (Time.time - startTime) / duration;
                SetScale("Grow", new Vector3(ratio, ratio, ratio));
                yield return null;
            }
            SetScale("Grow", new Vector3(1, 1, 1) );
        }


        public void Shrink(float duration)
        {
            StopCoroutine("ShrinkCoroutine");
            SetScale("Shrink", new Vector3(1, 1, 1));
            StartCoroutine(ShrinkCoroutine(duration));
        }

        private IEnumerator ShrinkCoroutine(float duration)
        {
            float startTime = Time.time;
            float endTime = Time.time + duration;
            while (Time.time < endTime)
            {
                float ratio = (Time.time - startTime) / duration;
                SetScale("Shrink", new Vector3(1 - ratio, 1 - ratio, 1 - ratio));
                yield return null;
            }
            SetScale("Shrink", new Vector3(0F, 0F, 0F));
        }
        

        public void StartFloat(float distance)
        {
            _floating = true;
            StopCoroutine("StartFloatCoroutine");
            SetPosition("Float", new Vector3(0, 0, 0));
            StartCoroutine(StartFloatCoroutine(distance));
        }

        public void StopFloat()
        {
            _floating = false;
        }


        private IEnumerator StartFloatCoroutine(float distance)
        {
            float floatingRatio = 0;

            while (floatingRatio >= 0)
            {
                float positionZ = floatingRatio * ( distance * ( 1 + 0.25f * Mathf.Sin(Time.time) ) );
                SetPosition("Float", new Vector3(0, 0, -positionZ) );
                floatingRatio = Mathf.Min(1, floatingRatio +  ( (_floating) ? Time.deltaTime : -Time.deltaTime ) );
                yield return null;
            }

            SetPosition("Float", new Vector3(0, 0, 0));
        }
        
        public void StartWobbling(float frequency)
        {
            _wobbling = true;
            SetRotation("Wobble", Quaternion.identity);
            StopCoroutine("StartWobblingCoroutine");
            StartCoroutine(StartWobblingCoroutine(frequency));
        }

        public void StopWobbling()
        {
            _wobbling = false;
        }

        private IEnumerator StartWobblingCoroutine(float frequency)
        {
            float wobblingRatio = 0;

            float amplitude = 1;
            while (wobblingRatio > 0)
            {
                float wobbleZ = amplitude * Mathf.Sin(frequency * Time.time * 2 * Mathf.PI);
                Quaternion zRotationQuat = Quaternion.Euler(0, 0, wobbleZ);
                SetRotation("Wobble", zRotationQuat);
                wobblingRatio = Mathf.Min(1, wobblingRatio + ((_wobbling) ? Time.deltaTime : -Time.deltaTime));
                yield return null;
            }

            SetRotation("Wobble", Quaternion.identity);
        }


        public void Pulse(float duration)
        {
            SetScale("Pulse", new Vector3(1, 1, 1));
            StopCoroutine("PulseCoroutine");
            StartCoroutine(PulseCoroutine(duration));
        }

        private IEnumerator PulseCoroutine(float duration)
        {
            float startTime = Time.time;
            float endTime = startTime + duration;

            float amplitude = 0.5f;
            while (Time.time < endTime)
            {
                float ratio = (Time.time - startTime) / duration;
                float scale = 1F - amplitude * Mathf.Sin(ratio * Mathf.PI * 2);
                SetScale("Pulse", new Vector3(scale,scale,scale));
                yield return null;
            }

            SetScale("Pulse", new Vector3(1, 1, 1));
        }
    }
}