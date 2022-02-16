using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NumberOfPlayers : MonoBehaviour
{
    public void TwoPlayers()
    {
        CharacterSelection.numOfPressableButtons = 2;
    }

    public void ThreePlayers()
    {
        CharacterSelection.numOfPressableButtons = 3;
    }

    public void FourPlayers()
    {
        CharacterSelection.numOfPressableButtons = 4;
    }

}
