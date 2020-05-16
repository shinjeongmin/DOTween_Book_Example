using DG.Tweening;
using UnityEngine;
using UnityEngine.UI; // --수정--

public class EnemySpawn : MonoBehaviour
{
    public GameObject SpawnEnemyObj; // 생성할 적의 오브젝트
    public PlayerMove PlayerStatus; // --수정-- PlayerMove 스크립트
    public Text GameOver; // --수정-- 게임오버 텍스트

    float EnemySpawnTimeMin = 0f; // 적을 재생성하는데 걸리는 최소시간
    float EnemySpawnTimeMax = .5f; // 적을 재생성하는데 걸리는 최대시간
    float EnemySpeed = 1f; // 적의 이동속도 단계 (모든 적이 동일한 속도로 움직이지 않음)

    Transform targetPlayerPosition; // 플레이어의 위치

    void Start()
    {
        // 플레이어오브젝트를 찾아 플레이어의 Transform 컴포넌트를 대입
        targetPlayerPosition = GameObject.Find("Player").GetComponent<Transform>();

        // 적 생성 함수를 시간을 두고 호출
        Invoke("RandomEnemySpawn", EnemySpawnTimeMax);

        // --수정-- 플레이어의 죽음 여부 변수를 가져오기 위헤 PlayerMove 스크립트를 가져옴
        PlayerStatus = GameObject.Find("Player").GetComponent<PlayerMove>();

        // --수정-- GameOver 오브젝트를 가져옴
        GameOver = GetComponentInChildren<Text>();

        // --수정-- 시작 상태에서는 GameOver텍스트를 안보이도록
        GameOver.DOFade(0f, 0f);
        GameOver.rectTransform.anchoredPosition = new Vector3(0, 300, 0);
    }
    void RandomEnemySpawn()
    {
        // 화면 밖의 임의의 위치들을 담은 배열
        Vector3[] EnemySpawnPos = {
            new Vector3(Random.Range(-18.0f, 18.0f), -11f, 0f)
            ,new Vector3(Random.Range(-18.0f, 18.0f), 11f, 0f)
            ,new Vector3(-18f, Random.Range(-11.0f, 11.0f), 0f)
            ,new Vector3(18f, Random.Range(-11.0f, 11.0f), 0f) };

        // 배열 안의 값 중 임의로 하나를 선택하여 저장
        int SpawnPosNum = Random.Range(0, 4);

        // 적 오브젝트를 화면 밖 임의의 위치에서 생성
        GameObject enemy = (GameObject)Instantiate(SpawnEnemyObj,
            EnemySpawnPos[SpawnPosNum],
            Quaternion.identity);

        // Rotate함수를 이용하여 적이 회전하면서 이동하게 만듦
        enemy.transform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);

        // Move함수를 이용하여 생성당시 플레이어의 현재 위치를 목표로 직선이동
        enemy.transform.DOMove(new Vector3(targetPlayerPosition.position.x, targetPlayerPosition.position.y, 0f), EnemySpeed)
            .SetDelay(1f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);

        // 재생성하는 시간을 최소시간과 최대시간사이의 임의의 실수로 설정
        float RespawnDelay = Random.Range(EnemySpawnTimeMin, EnemySpawnTimeMax);

        // --수정-- 플레이어 오브젝트가 죽음 상태일 때 실행
        if (PlayerStatus.playerDie)
        {
            GameOver.DOFade(1f, 1f);
            GameOver.rectTransform.DOAnchorPosY(0, 2)
                .SetEase(Ease.OutElastic);
        }

        // --수정-- 플레이어 오브젝트가 죽지 않았을 때만 다시 호출
        if (!PlayerStatus.playerDie)
        {
            // 적을 반복해서 생성할 수 있도록 자기자신함수를 다시 호출
            Invoke("RandomEnemySpawn", RespawnDelay);
        }
    }
}