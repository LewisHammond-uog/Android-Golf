using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour {

    [SerializeField]
    private BallController ball;

    [SerializeField]
    private Slider powerBar;


	void Start () {
        //Here we need to set power bar min / max values
        powerBar.minValue = 0f;
        powerBar.maxValue = ball.MaxPower;
        powerBar.value = 0f;
	}

    // Update is called once per frame
    void Update()
    {
        //Update power display based on ball controller pending power value
        powerBar.value = ball.pendingPower;

        //Update slider colour
        powerBar.image.color = Color.Lerp(Color.red, Color.green, powerBar.value / powerBar.maxValue);

    }
}
