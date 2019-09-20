using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class VideoCartao : MonoBehaviour, ITrackableEventHandler{
    public float start;
    public float fadeSpeed;
    private Material material;
    private TrackableBehaviour m_Trackable;
    private ParticleSystem particleSys;

    Coroutine routine;

    public void Start(){
        material = GetComponent<MeshRenderer>().material;
        particleSys = GetComponent<ParticleSystem>();
    }

    private IEnumerator FadeOut(){
        var alpha = material.GetColor("_Color").a;
        while(alpha > 0){
            alpha -= fadeSpeed * Time.deltaTime;
            var color =  material.GetColor("_Color");
            var newColor = new Color(color.r, color.g, color.b, alpha);
            color = new Color(color.r, color.g, color.b, color.a - fadeSpeed);
            material.SetColor("_Color", color);
            yield return null;
        }
        
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
			routine = StartCoroutine(Timer());
        }
        else
        {
			if(routine != null) StopCoroutine(routine);
        }
    }
    
    private IEnumerator Timer(){
        yield return new WaitForSeconds(start);
        Play();

    }

    void Play(){
        particleSys.Play();
    }

    void Stop(){

    }
}