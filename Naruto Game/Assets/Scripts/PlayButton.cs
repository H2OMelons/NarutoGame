using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour {

    public GameManager manager;
    public PlayerSelectionManager[] selectionManager;

    private void OnMouseUp()
    {
        manager.player1Choice = selectionManager[0].GetCurrentSelection();
        manager.player2Choice = selectionManager[1].GetCurrentSelection();
        manager.TransitionToPlay();
    }
}
