using UnityEngine;
using System.Collections;

public class transitionsAndEffects : MonoBehaviour {

    public void Grow(float duration)
    {
        transform.localScale = new Vector3();
        StartCoroutine(GrowCoroutine(duration));
    }

    private IEnumerator GrowCoroutine( float duration )
    {
        float startTime = Time.time;
        float endTime = Time.time + duration;
        while( Time.time < endTime )
        {
            float ratio = (Time.time - startTime) / duration;
            transform.localScale = new Vector3(ratio, ratio, ratio);
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);
    }

    //Shrink
    public void Shrink(float duration)
    {
        StartCoroutine(ShrinkCoroutine(duration));
    }

    private IEnumerator ShrinkCoroutine( float duration )
    {
        float startTime = Time.time;
        float endTime = Time.time + duration;
        while( Time.time < endTime )
        {
            float ratio = (Time.time - startTime) / duration;
            transform.localScale = new Vector3(1 - ratio, 1 - ratio, 1 - ratio);
            yield return null;
        }
        transform.localScale = new Vector3(0F, 0F, 0F);
    }

    //Float (Forward vec + sin?) Start/Stop
    bool floating = false;
    public void startFloat(float speed = 2F)
    {
        float amplitude = 0.15F;
        float returnDuration = 1F;
        floating = true;
        StartCoroutine(startFloatCoroutine(speed, amplitude, returnDuration));
    }

    public void stopFloat()
    {
        floating = false;
    }


    private IEnumerator startFloatCoroutine(float speed = 2F, float amplitude = 0.15F, float returnDuration = 1F)
    {
        //start floating
        float startPositionX = transform.position.x;
        float startPositionY = transform.position.y;
        float startPositionZ = transform.position.z;
        while( floating )
        {
            float positionZ = startPositionZ + amplitude * Mathf.Sin(speed * Time.time);
            transform.position = new Vector3(startPositionX, startPositionY, positionZ);
            yield return null;
        }

        //stop 
        float startTime = Time.time;
        float endTime = startTime + returnDuration;
        float distanceToStartPoistion = startPositionZ - transform.position.z;
        float currentZposition = transform.position.z;
        while( Time.time < endTime )
        {
            float ratio = (Time.time - startTime) / returnDuration;
            float positionZ = currentZposition + ratio * distanceToStartPoistion;
            transform.position = new Vector3(startPositionX, startPositionY, positionZ);
            yield return null;
        }
    }

    //Wobble (rotating back and forth) Start/Stop
    bool wobbling = false;
    public void startWobbling(float speed)
    {
        wobbling = true;
        StartCoroutine(startWobblingCoroutine(speed));
    }

    public void stopWobbling()
    {
        wobbling = false;
    }

    private IEnumerator startWobblingCoroutine(float speed = 10F, float endDuration = 5f, float amplitude = 20F)
    {
        Quaternion originalRotation = transform.rotation;
        float lastTime = Time.time ;
        float totalAmplitude = amplitude;
        while ( amplitude > 0 )
        {
            if (!wobbling)
            {
                amplitude -= totalAmplitude * ( Time.time - lastTime ) / endDuration;
            }
            lastTime = Time.time;
            float rotationZ = amplitude * Mathf.Sin(speed * Time.time);
            Quaternion zRotationQuat = Quaternion.Euler(0, 0, rotationZ);
            transform.rotation = originalRotation * zRotationQuat;
            yield return null;
        }

        transform.rotation = originalRotation;  
    }

    //Pulse
    public void Pulse(float duration = 0.1F)
    {
        transform.localScale = new Vector3();
        StartCoroutine(PulseCoroutine(duration));
    }

    private IEnumerator PulseCoroutine(float duration = 0.1F, float amplitude = .3F)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        while (Time.time < endTime)
        {
            float ratio = (Time.time - startTime) / duration;
            float scale = 1F - amplitude * Mathf.Sin(ratio * Mathf.PI * 2);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        transform.localScale = new Vector3(1, 1, 1);
    }
}
