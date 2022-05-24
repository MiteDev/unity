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
    private NowState nowState = 0;

    // 현재 게임 진행 상태를 보여줄 텍스트
    public Text nowStateText;

    // 플레이어들이 낸 카드를 저장할 변수
    // 0 = 가위, 1 = 바위, 2 = 보
    private int player01Card;
    private int player02Card;

    // 플레이어들이 이긴 라운드를 셀 변수
    private int player01WonRoundNum = 0;
    private int player02WonRoundNum = 0;

    void Start()
    {
        nowStateText.text = "플레이어1의 차례입니다.";
    }

    // 카드가 인식 되면 값을 전달받는 메소드
    public void FilpCard(int _inputCard)
    {
        SetPlayersCard(_inputCard);
        SetMessage();
        Debug.Log("Flipcard_Called");
    }

    // 해당 턴인 플레이어의 카드를 설정
    private void SetPlayersCard(int _pCard)
    {
        Debug.Log("SetPlayersCardCalled");
        if ((int)nowState == 0)
        {
            player01Card = _pCard;
            nowState++;
        }
        else if((int)nowState == 1)
        {
            player02Card = _pCard;
            nowState++;
        }
    }

    // 메시지 변경
    private void SetMessage()
    {
        Debug.Log("SetMesageCalled");
        if ((int)nowState == 0)
        {
            nowStateText.text = "현재 플레이어1의 차례입니다.";
        }
        else if ((int)nowState == 1)
        {
            nowStateText.text = "현재 플레이어2의 차례입니다.";
        }
        else if ((int)nowState == 2)
        {
            // 누가 승리했는지 계산하는 메소드
            CalcRoundWinner();
        }
    }

    // 승자 계산 메소드
    // 플레이어1 승리 -> 0,
    // 플레이어2 승리 -> 1,
    // 비김 -> 2
    private void CalcRoundWinner()
    {
        int winner = 0;

        // 플레이어1이 가위를 낸 경우
        if(player01Card == 0)
        {
            if (player02Card == 0)
                winner = 2;
            else if (player02Card == 1)
                winner = 1;
            else if (player02Card == 2)
                winner = 0;
        }
        // 플레이어2가 바위를 낸 경우
        else if (player01Card == 1)
        {
            if (player02Card == 0)
                winner = 0;
            else if (player02Card == 1)
                winner = 2;
            else if (player02Card == 2)
                winner = 1;
        }
        // 보
        else if(player01Card == 2)
        {
            if (player02Card == 0)
                winner = 1;
            else if (player02Card == 1)
                winner = 0;
            else if (player02Card == 2)
                winner = 2;
        }

        ApplyRoundWinner(winner);


        nowState = 0;
    }

    // 이긴 플레이어의 메시지를 출력하고
    // 라운드 승리 횟수를 더함
    private void ApplyRoundWinner(int _winner)
    {
        if(_winner == 0)
        {
            nowStateText.text = "플레이어1 승리";
            player01WonRoundNum++;
        }

        if(_winner == 1)
        {
            nowStateText.text = "플레이어2 승리";
            player02WonRoundNum++;
        }

        if(_winner == 2)
        {
            nowStateText.text = "비김";
            player01WonRoundNum++;
            player02WonRoundNum++;
        }
    }
}
