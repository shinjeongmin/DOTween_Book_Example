using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyStatus : MonoBehaviour
{
    Transform targetPlayerPosition; // 플레이어의 위치
    SpriteRenderer enemySprite; // 적 오브젝트의 Sprite Renderer 컴포넌트를 담을 변수

    int kindOfEnemy = 0; // 적의 종류
    float StopMoveTimeSpeed = 2.1f; // 적의 속도가 변하기 까지 시간
    float StopMoveTimeSize = 1.3f; // 적의 크기가 변하기 까지 시간
    float UpSize = 4f; // 적의 크기변화값
    float ChangeSpeed = .5f; // 적의 속도변화값 기본 속도는 1f
    void Start()
    {
        // 적 오브젝트의 Sprite Renderer 컴포넌트를 받기
        enemySprite = GetComponent<SpriteRenderer>();
        // 적의 종류를 임의로 설정
        kindOfEnemy = Random.Range(0, 3); // 숫자가 2 -> 3으로 변경

        switch (kindOfEnemy)
        {
            case 1: // 적의 종류가 1이면 속도를 변화
                Invoke("StopMove", StopMoveTimeSpeed); // 잠시동안 움직임을 멈추는 함수호출
                Invoke("SpeedUp", StopMoveTimeSpeed + .5f); // 속도를 변화시키는 함수호출
                break;
            case 2: // 적의 종류가 2이면 크기를 변화
                enemySprite.DOColor(Color.cyan, .5f); // 시안 색으로 변경
                Invoke("SizeUp", StopMoveTimeSize); // 크기를 증가시키는 함수 호출
                break;
        }
    }

    void StopMove()
    {
        // 현재 자신의 위치로 1초동안 이동하므로 정지한 상태로 1초동안 같은 위치에 있음
        transform.DOMove(new Vector3(transform.position.x, transform.position.y, transform.position.z), 1f);
        // 노란색으로 색 변경
        enemySprite.DOColor(Color.yellow, .5f);
    }

    void SizeUp()
    {
        // DOScale함수로 크기를 변경
        transform.DOScale(new Vector3(UpSize, UpSize, UpSize), .5f); 
    }

    void SpeedUp()
    {
        // 플레이어의 위치를 받음
        targetPlayerPosition = GameObject.Find("Player").GetComponent<Transform>();

        // 현재 플레이어의 위치로 다른 속도로 날아감
        transform.DOMove(new Vector3(targetPlayerPosition.position.x, targetPlayerPosition.position.y, 0f), ChangeSpeed) //속도 변경
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    void OnBecameInvisible() //화면밖으로 나가 보이지 않게 되면 호출이 된다.
    {
        Destroy(this.gameObject); //객체를 삭제한다.
    }
}
