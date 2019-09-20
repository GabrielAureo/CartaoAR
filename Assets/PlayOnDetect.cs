using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;
public class PlayOnDetect : MonoBehaviour, ITrackableEventHandler
{
    [SerializeField] VideoPlayer player;
    private TrackableBehaviour mTrackableBehaviour;

    // Start is called before the first frame update
    void Awake()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if(newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED){
            print("reeee");
            player.Play();
        }
    }

    
}
