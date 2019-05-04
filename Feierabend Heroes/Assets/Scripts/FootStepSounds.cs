using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSounds : MonoBehaviour {

    public AudioSource footstepWalk;
    public AudioSource footstepRun;


    public void FootStepWalk() {
        float rndPitch = Random.Range(0.8f, 1.2f);
        AudioSource newFootstep = Instantiate(footstepWalk);
        newFootstep.pitch = rndPitch;
    }

    public void FootStepRun() {
        float rndPitch = Random.Range(0.8f, 1.2f);
        AudioSource newFootstep = Instantiate(footstepRun);
        newFootstep.pitch = rndPitch;
    }

}
