using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    // 현재 게임 진행 상태를 저장할 열거형
    enum NowState
    {
        Player01Turn,
        Player02Turn,
        Result
    }
    private NowState nowState;

    // 현재 게임 진행 상태를 보여줄 텍스트
    public Text nowStateText;

    void Start()
    {
        nowState = NowState.Player01Turn;
    }

    // 카드가 인식 되면 값을 전달받는 메소드
    public void FilpCard(string _inputCard)
    {
        // 메인씬 가운데에 있는 텍스트에 입력된 카드 값을 표시
        nowStateText.text = _inputCard;
    }
}
