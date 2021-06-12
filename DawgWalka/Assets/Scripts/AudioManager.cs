using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GroundMaterial { Ground, Rail, Air }

public class AudioManager : MonoBehaviour
{
    public static AudioManager i;


    private const float MAX_SPEED = 5.1f;

    private static FMOD.Studio.EventInstance skateboardNoise;

    private static FMOD.Studio.EventInstance music;

    private static FMOD.Studio.EventInstance jingle;
    
    private static FMOD.Studio.EventInstance horde;

    void Awake()
    {
        if(i != null)
            GameObject.Destroy(this);
        else
            i = this;

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        skateboardNoise = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Skateboard/Skateboard");
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Level Music");
        jingle = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Intro Jingle");
        horde = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Zombie Horde");
        if(SceneManager.GetActiveScene().name == "ProgrammingScene") {
            StartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = (PlayerMovement.currentSpeed + RoadController.moveSpeed)/MAX_SPEED; //1 is the speed after which the sfx doesn't change
        //Debug.Log(speedPercent);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Speed", speedPercent);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Zombie Distance", ZombieHorde.distance);
    }

    public void StartGame() {
        StopJingle();
        StartSkateboardNoise();
        StartMusic();
        StartHordeNoise();
    }

    public void StartJingle() {
        jingle.start();
    }

    public void StopJingle() {
        if(jingle.isValid()) {
            jingle.release();
            jingle.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }  
    }

    public void StartMusic() {
        music.start();
        music.release();
    }

    public void StopMusic() {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void OnPause() {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Paused", 1);
    }

    public void OnUnpause() {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Paused", 0);
    }

    public void OnDeath() {
        StopSkateboardNoise();
        StopMusic();
    }

    public void StartSkateboardNoise() {
        skateboardNoise.start();
    }

    public void StopSkateboardNoise() {
        skateboardNoise.release();
        skateboardNoise.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StartHordeNoise() {
        horde.start();
    }

    public void StopHordeNoise() {
        horde.release();
        horde.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
