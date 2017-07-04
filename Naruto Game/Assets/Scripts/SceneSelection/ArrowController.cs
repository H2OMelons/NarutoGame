using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public bool isRightArrow;
    public BackgroundController controller;

    private void OnMouseDown()
    {
        if (isRightArrow)
        {
            Debug.Log("Going right");
            controller.GoRight();
        }
        else
        {
            Debug.Log("Going Left");
            controller.GoLeft();
        }
    }
}
