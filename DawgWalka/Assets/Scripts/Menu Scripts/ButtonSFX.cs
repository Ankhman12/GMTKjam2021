using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public string mouseoverSfxPath;
    public string selectSfxPath;

    public void Start() {
        if(mouseoverSfxPath == "") {
            mouseoverSfxPath = "event:/SFX/UI/Mouseover";
        }
        if(selectSfxPath == "") {
            selectSfxPath = "event:/SFX/UI/Menu Select";
        }
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/Mouseover");
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/Menu Select");
    }
    
}
