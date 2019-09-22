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
    float cutoff;

    public void Start(){
        material = GetComponent<MeshRenderer>().material;
        particleSys = GetComponent<ParticleSystem>();
        cutoff = material.GetFloat("_Cutoff");
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut(){
        yield return new WaitForSeconds(start);
        var _cutoff = cutoff;
        while(_cutoff < 1){
            _cutoff += fadeSpeed * Time.deltaTime;
            material.SetFloat("_Cutoff", _cutoff);
            yield return null;
        }
        
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            material.SetFloat("_Cutoff",cutoff);
			routine = StartCoroutine(FadeOut());
        }
        else
        {
			if(routine != null) StopCoroutine(routine);
        }
    }

    void Play(){
        particleSys.Play();
    }

    void Stop(){

    }
}