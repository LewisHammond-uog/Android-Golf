using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScorecardUI : MonoBehaviour {

    [SerializeField]
    private Text[] scoreTextItems = new Text[18];
    [SerializeField]
    private Text[] parTextItems = new Text[18];

    [SerializeField]
    private Text totalScoreText;
    [SerializeField]
    private Text totalParText;

    private const string blankingCharacter = "-";

    //Every time this UI is enabled make sure we have 
    //the most up to date scores
    void OnEnable()
    {
        FillInScores();
        FillInPars();
    }

    /// <summary>
    /// Fills in the scores to the score card
    /// </summary>
    private void FillInScores()
    {
        //Get the dictonary of current scores
        //Dictonary is Hole Number, Score
        Dictionary<int, int> currentScores = ScoreKeeper.GetScoreDictonary();

        //Go through each text item and set it to have a score if,
        //the user has completed that hole, else fill it with a blank character
        for(int textItemIndex = 0; textItemIndex < scoreTextItems.Length; textItemIndex++)
        {
            if (scoreTextItems[textItemIndex] != null)
            {
                //Holes are 1 indexed so we must add one to access the right
                //hole for this text item
                int currentHoleIndex = textItemIndex + 1;

                //If we have a score fill it in, else use a blanking character
                if (currentScores.ContainsKey(currentHoleIndex))
                {
                    scoreTextItems[textItemIndex].text = currentScores[currentHoleIndex].ToString();
                }
                else
                {
                    scoreTextItems[textItemIndex].text = blankingCharacter;
                }
            }
        }

        //Fill in total score
        if (totalScoreText != null)
        {
            int totalScore = GetTotalScore();
            totalScoreText.text = totalScore.ToString();
        }
    }

    private void FillInPars()
    {
        //Get the dictonary of current scores
        //Dictonary is Hole Number, Score
        Dictionary<int, int> pars = ScoreKeeper.GetParDictonary();

        //Go through each text item and set it to have a score if,
        //the user has completed that hole, else fill it with a blank character
        for (int textItemIndex = 0; textItemIndex < parTextItems.Length; textItemIndex++)
        {
            if (parTextItems[textItemIndex] != null)
            {
                //Holes are 1 indexed so we must add one to access the right
                //hole for this text item
                int currentHoleIndex = textItemIndex + 1;

                //If we have a par fill it in, else use a blanking character
                if (pars.ContainsKey(currentHoleIndex))
                {
                    parTextItems[textItemIndex].text = pars[currentHoleIndex].ToString();
                }
                else
                {
                    parTextItems[textItemIndex].text = blankingCharacter;
                }
            }
        }

        //Fill in total score
        if (totalParText != null)
        {
            int totalPar = GetCurrentPar();
            totalParText.text = totalPar.ToString();
        }
    }

    /// <summary>
    /// Calcuates the total score
    /// </summary>
    /// <returns>Total Score</returns>
    private int GetTotalScore()
    {
        int totalScore = 0;

        //Flatten the dictonary to extract all values
        int[] scoreArray = ScoreKeeper.GetScoreDictonary().Values.ToArray();

        //Add up all of the scores from the array
        foreach(int score in scoreArray)
        {
            totalScore += score;
        }

        //Return the score
        return totalScore;
    }

    /// <summary>
    /// Gets the par of all of the holes that the player has currently completed
    /// </summary>
    /// <returns></returns>
    private int GetCurrentPar()
    {
        //Get the number of completed holes
        int holesCompleted = ScoreKeeper.GetScoreDictonary().Values.ToArray().Length;

        //Get par list
        int[] pars = ScoreKeeper.GetParDictonary().Values.ToArray();

        int parSoFar = 0;

        //Add up the number of pars
        for(int i = 0; i < holesCompleted; i++)
        {
            parSoFar += pars[i];
        }

        return parSoFar;
    }
}
