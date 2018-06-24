using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour {

    public GameManager manager;
    public PlayerSelectionManager[] selectionManager;

    private void OnMouseUp()
    {
        manager.SetPlayerSelections(selectionManager[0].GetCurrentSelection(), selectionManager[1].GetCurrentSelection());
        manager.TransitionToPlay();
    }
}
