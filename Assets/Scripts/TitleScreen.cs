using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene을 조작하기 위한 네임 
using UnityEngine.UI; // UI를 조작하기 위한 네임스페이스
using DG.Tweening;

public class TitleScreen : MonoBehaviour
{
    public string SceneToLoad; // 불러올 Scene의 이름
    public Text txt; // 화면에 띄울 텍스트 오브젝트
    bool GameStart = false; // 게임을 시작했는지 확인하는 함수
    private void Start()
    {
        Invoke("ChangeColor", 0.1f);
    }

    void ChangeColor()
    {
        // 색을 주기적으로 변경
        txt.DOColor(Color.red, .5f);
        txt.DOColor(Color.yellow, .5f)
            .SetDelay(.5f);
        txt.DOColor(Color.green, .5f)
            .SetDelay(1f);
        txt.DOColor(Color.blue, .5f)
            .SetDelay(1.5f);
        txt.DOColor(Color.cyan, .5f)
            .SetDelay(2f);

        // 2.5초 주기로 색 변경 반복
        if (!GameStart)
            Invoke("ChangeColor", 2.5f);
    }
    void Update()
    {
        // 마우스 좌클릭 시 게임을 시작함
        if(Input.GetMouseButton(0))
        {
            // SceneToLoad에 담긴 이름의 Scene으로 변경
            SceneManager.LoadScene(SceneToLoad);
            GameStart = true;
        }
    }
}