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

    // 카드 인식 일시 정지
    private bool isGameOn = true;

    // 현재 게임 진행 상태를 보여줄 텍스트
    public Text nowStateText;
    public Text player01WonRoundNum_Text;
    public Text player02WonRoundNum_Text;

    // 플레이어들이 낸 카드를 저장할 변수
    // 0 = 가위, 1 = 바위, 2 = 보
    private int player01Card;
    private int player02Card;

    // 플레이어들이 이긴 라운드를 셀 변수
    private int player01WonRoundNum = 0;
    private int player02WonRoundNum = 0;

    // 최종 결과 창
    public GameObject Canvas_Result;

    void Start()
    {
        nowStateText.text = "플레이어1의 차례입니다.";
        Canvas_Result.SetActive(false);
    }

    private void Update()
    {
        player01WonRoundNum_Text.text = player01WonRoundNum.ToString();
        player02WonRoundNum_Text.text = player02WonRoundNum.ToString();
    }

    // 카드가 인식 되면 값을 전달받는 메소드
    public void FilpCard(int _inputCard)
    {
        Debug.Log("not in if");
        if (isGameOn)
        {
            Debug.Log("in if");
            SetPlayersCard(_inputCard);
            SetMessage();
        }
    }

    // 해당 턴인 플레이어의 카드를 설정
    private void SetPlayersCard(int _pCard)
    {
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

        FindFinalWinner();
    }

    private void FindFinalWinner()
    {
        bool isGameOver = false;

        // 마지막 라운드에 비겨서 2대2로 끝남
        if (player01WonRoundNum >= 2 && player02WonRoundNum >= 2)
        {
            nowStateText.text = "최종 결과: 비김";
            isGameOver = true;
        }
        // 플레이어1 최종 승리
        else if (player01WonRoundNum >= 2)
        {
            nowStateText.text = "최종 결과: 플레이어 1 승리";
            isGameOver = true;
        }
        // 플레이어2 최종 승리
        else if(player02WonRoundNum >= 2)
        {
            nowStateText.text = "최종 결과: 플레이어 2 승리";
            isGameOver = true;
        }

        if (isGameOver)
        {
            isGameOn = false;
            // 최종게임 결과 화면
            Canvas_Result.SetActive(true);
        }
        else
        {
            nowState = 0;
            // 2초 정도 후에 다음 라운드 시작하는 코루틴 넣으면 좋을듯??
        }
    }

    public void ResetGame()
    {
        isGameOn = true;
        nowState = 0;
        player01WonRoundNum = 0;
        player02WonRoundNum = 0;
        SetMessage();
    }
}
