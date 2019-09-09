using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreKeeper{

    //Dictonary of hole number, score
    private static Dictionary<int, int> scoresDictonary = new Dictionary<int, int>();

    //Dictonary of hole numbers and pars
    private static readonly Dictionary<int, int> parsDictonary = new Dictionary<int, int>()
    {
        {1 , 1}, //Hole 1, par 1
        {2 , 1}, //Hole 2, par 1
        {3 , 3}, //Hole 3, par 3
        {4 , 4}, //Hole 4, par 4
        {5 , 4}, //Hole 5, par 4
        {6 , 4}, //Hole 6, par 4
        {7 , 6}, //Hole 7, par 6
        {8 , 5}, //Hole 8, par 5
        {9 , 5}, //Hole 9, par 5
        {10 , 9}, //Hole 10, par 9
        {11 , 7}, //Hole 11, par 7
        {12 , 11}, //Hole 12, par 11
        {13 , 7}, //Hole 13, par 7
        {14 , 15}, //Hole 14, par 15
        {15 , 15}, //Hole 15, par 15
        {16 , 15}, //Hole 16, par 15
        {17 , 14}, //Hole 17, par 14
        {18 , 15}, //Hole 18, par 15
    };

    /// <summary>
    /// Adds a score to the dictonary of scores given a hole number and the score for that hole 
    /// </summary>
    /// <param name="holeNumber">Hole number to write score for</param>
    /// <param name="score">Score for given hole</param>
    public static void AddScore(int holeNumber, int score)
    {
        //Check that the score doesn't already exist
        if (!scoresDictonary.ContainsKey(holeNumber))
        {
            scoresDictonary.Add(holeNumber, score);
        }
    }

    /// <summary>
    /// Gets the current dictonary of holes and the scores that the player
    /// has scored for that hole
    /// </summary>
    /// <returns>Dictonary of int, int (hole number, score)</returns>
    public static Dictionary<int, int> GetScoreDictonary()
    {
        return scoresDictonary;
    }

    /// <summary>
    /// Gets the current dictonary of holes and the expected par
    /// for that hole
    /// </summary>
    /// <returns>Dictonary of int, int (hole number, par)</returns>
    public static Dictionary<int, int> GetParDictonary()
    {
        return parsDictonary;
    }

    /// <summary>
    /// Gets the par of a given hole
    /// </summary>
    /// <param name="hole">Hole to get par of</param>
    /// <returns>Par of hole</returns>
    public static int GetPar(int hole)
    {
        if (parsDictonary.ContainsKey(hole))
        {
            return parsDictonary[hole];
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Resets the score dictonary, deleting all scores
    /// </summary>
    public static void ResetScores()
    {
        scoresDictonary.Clear();
    }

}
