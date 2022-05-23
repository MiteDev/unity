using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    // 메인 씬을 불러오는 메소드
    public void MovetoNextScene()
    {
        SceneManager.LoadScene("Main_Scene");
    }

    // 게임 종료 메소드
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
