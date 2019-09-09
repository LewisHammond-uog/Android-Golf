using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueOutput : MonoBehaviour {

    //Text
    [SerializeField]
    private Text outputText;

    //Linked Slider
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        UpdateValue();
    }

    /// <summary>
    /// Update the value of the slider
    /// </summary>
	public void UpdateValue()
    {
        outputText.text = System.Math.Round(slider.value,2).ToString();
    }
}
