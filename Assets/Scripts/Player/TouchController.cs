using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class TouchController{

    /// <summary>
    /// Gets the phase of the first touch on the screen
    /// </summary>
    /// <returns>Phase of Touch</returns>
    static public TouchPhase GetTouchPhase()
    {
        //If the screen is being touched
        if (Input.touchCount > 0)
        {
            //Get the current touch
            Touch currentTouch = Input.GetTouch(0);
            return currentTouch.phase;
        }

        return TouchPhase.Canceled;
    }


    static public bool ScreenTouched()
    {
        if(Input.touchCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
	
}
