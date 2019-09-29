using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;
using System.Collections;
using Vuforia;

public class VideoCartao2 : MonoBehaviour, ITrackableEventHandler{
    public VideoPlayer videoPlayer;
    public ParticleSystem m_particleSystem;
    public Transform plane;
    private Material material;

    void Awake(){
        material = plane.GetComponent<MeshRenderer>().material;
        //FadeIn();
        videoPlayer.loopPointReached += PlayParticles;
        videoPlayer.Prepare();
        var m_TrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (m_TrackableBehaviour) {
            m_TrackableBehaviour.RegisterTrackableEventHandler(this);

        }
    }

    private void PlayParticles(VideoPlayer vp){
        material.DOFade(0, .5f);
        m_particleSystem.Play();
    }

    public void Update(){
        transform.LookAt(Camera.main.transform, Vector3.up);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {        
            StartCoroutine(FadeIn());
            
        }
        else
        {
            videoPlayer.Stop();
        }
    }

    IEnumerator FadeIn(){
        videoPlayer.Prepare();
        yield return new WaitUntil(()=> videoPlayer.isPrepared);
        //yield return new WaitForEndOfFrame();
        videoPlayer.frame = 0;
        videoPlayer.Play();
        
        var color =  material.GetColor("_Color");
        material.SetColor("_Color", new Color(color.r, color.g, color.b, 0));
        material.DOFade(1f, 1f);
    }
}