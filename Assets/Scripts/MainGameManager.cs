using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class MainGameManager : MonoBehaviour
{
    enum NowState
    {
        Player01Turn,
        Player02Turn,
        Result
    }
    private NowState nowState;

    public Text nowStateText;

    // Start is called before the first frame update
    void Start()
    {
        nowState = NowState.Player01Turn;
    }

    public void FilpCard(string _inputCard)
    {
        nowStateText.text = _inputCard;
    }
}
