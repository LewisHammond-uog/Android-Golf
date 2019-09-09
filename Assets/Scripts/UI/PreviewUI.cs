using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewUI : MonoBehaviour {

    [SerializeField]
    private HoleController holeController;

    [SerializeField]
    private Text holeNumberText, parNumberText, distanceNumberText;
    private const char distanceTextSuffix = 'M';

	// Use this for initialization
	void Start () {
        //Fillin Data
        int currentHoleNumber = holeController.HoleNumber;

        holeNumberText.text = currentHoleNumber.ToString();
        parNumberText.text = ScoreKeeper.GetPar(currentHoleNumber).ToString();

        //Round up hole distance
        int holeDistance = Mathf.CeilToInt(holeController.GetHoleDistance());
        distanceNumberText.text = holeDistance.ToString() + ' ' + distanceTextSuffix;

	}
}
