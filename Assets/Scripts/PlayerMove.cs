using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    private Camera mainCamera; // 메인 카메라 오브젝트
    public Vector3 targetMovePos; // 움직일 위치
    public bool playerDie = false; // 플레이어의 죽음 상태
    Material playerMaterial; // 플레이어의 Material 컴포넌트를 담는 변수
    void Start()
    {
        // Main Camera라는 이름의 오브젝트를 찾아서 Camera 컴포넌트를 집어 넣음
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        // 플레이어의 Material 컴포넌트
        playerMaterial = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if(!playerDie) // 플레이어가 죽음 상태가 아니면 함수를 실행
            FollowMouse();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
            // Enemy라는 태그
        {
            // 플레이어를 죽음 상태로 만듬
            playerDie = true;

            // DOFade함수로 플레이어를 사라지게 하기
            playerMaterial.DOFade(0f, .5f);
        }
    }

    void FollowMouse() // 마우스를 따라다니게 만드는 함수
    {
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 클릭을 하면 마우스의 위치로 이동함
        {
            // mouseposition에 마우스의 위치를 담음
            targetMovePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // 마우스의 위치로 좌표를 이동시키기
            transform.DOMove(new Vector3(targetMovePos.x, targetMovePos.y, 0f), .5f)
                .SetEase(Ease.OutCubic);
        }
    }
}