using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContinueButton : MonoBehaviour {

    public GameManager manager;
    public BackgroundController bgController;

    private void OnMouseUp()
    {
        Debug.Log("Pressed button");
        manager.sceneSelection = bgController.GetCurrentBGIndex();
        manager.TransitionToCharacterSelection();
    }
}
