using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundMaterial { Ground, Rail, Air }

public class AudioManager : MonoBehaviour
{
    public static AudioManager i;

    private const float MAX_SPEED = 5.1f;

    private static FMOD.Studio.EventInstance skateboardNoise;
    
    void Awake()
    {
        if(i != null)
            GameObject.Destroy(i);
        else
            i = this;

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        skateboardNoise = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Skateboard/Skateboard");
        StartSkateboardNoise();
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = PlayerMovement.currentSpeed/MAX_SPEED; //1 is the speed after which the sfx doesn't change
        //Debug.Log(speedPercent);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Speed", speedPercent);
    }

    public void StartSkateboardNoise() {
        skateboardNoise.start();
        skateboardNoise.release();
    }

    public void StopSkateboardNoise() {
        skateboardNoise.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SkateboardJump() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Skateboard/Jump");
        skateboardNoise.setParameterByName("Ground Type", 2);
    }

    public void SkateboardLand(GroundMaterial ground) {
        int groundParam = 0;
        switch(ground) {
            case GroundMaterial.Ground:
                groundParam = 0;
                break;
            case GroundMaterial.Rail:
                groundParam = 1;
                break;
            case GroundMaterial.Air:
                groundParam = 2;
                break;
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Skateboard/Land");
        skateboardNoise.setParameterByName("Ground Type", groundParam);
    }

}
