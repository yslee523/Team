using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//애니메이션 클립을 저장할 클래스 
[System.Serializable]
//AnimationClip 9개
public class Anim
{
    public AnimationClip idle;
    public AnimationClip walk;
    public AnimationClip run;
    public AnimationClip attack1;
    public AnimationClip attack2;
    public AnimationClip hit1;
    public AnimationClip hit2;
    public AnimationClip die1;
    public AnimationClip die2;
}

public class EnemyCtrl : MonoBehaviour
{
    //인스펙터뷰에 노출시킬 Anim 클래스 변수 
    public Anim anims;
    //하위에 있는 모델의 Animation 컴포넌트에 접근하기 위한 레퍼런스
    private Animation _anim;

    //NavMeshAgent 연결 레퍼런스
    private NavMeshAgent enemyAgent;

    //enemyTr과 traceTr Transform 참조 변수  
    private Transform enemyTr;
    private Transform traceTr;

    //플레이어를 찾기 위한 배열 
    private GameObject[] players;
    private Transform playerTarget;

    //애니메이션 셀렉트(랜덤한 연출)
    private float randAnimTime;
    private int randAnim;

    //enemy 랜덤 위치 point
    private Vector3 point;
    //point 위치 찾기 위한 반경
    public float range = 1;

    //enemy 죽었는지 상태변수
    [SerializeField] private bool isDie;
    //enemy Hit 상태변수
    [SerializeField] private bool isHit;
    //enemy 추적 상태변수
    [SerializeField] private bool isTrace;
    //enemy walk 상태변수
    [SerializeField] private bool isWalk;

    // Enemy의 현재 상태정보를 위한 Enum 자료형 선언  
    public enum ENEMY_STATE
    {
        walk = 1, run, attack, hit, die
    };

    // Enemy의 종류 정보를 위한 Enum 자료형 선언  
    public enum ENEMY_KIND
    {
        enemy1 = 1, enemy2, enemyBoss
    };

    //인스펙터의 헤더의 표현을 위한 어트리뷰트 선언
    [Header("ENEMY_STATE")]
    //Enemy의 상태 셋팅
    public ENEMY_STATE enemyState = ENEMY_STATE.walk;

    //인스펙터의 헤더의 표현을 위한 어트리뷰트 선언
    [Header("SETTING")]
    //Enemy의 종류 셋팅
    public ENEMY_KIND enemyKind = ENEMY_KIND.enemy1;

    //인스펙터의 헤더의 표현을 위한 어트리뷰트 선언
    [Header("몬스터 인공지능")]
    [Range(0, 1000)] public int hp = 100;
    //[Range(0f, 5f)] public float speed = 5f;
    [Range(0f, 5f)][SerializeField] float traceDist = 32.5f;
    [Range(0f, 5f)][SerializeField] float attackDist = 17.0f;



    void Awake()
    {
        //Transform 연결
        enemyTr = gameObject.GetComponent<Transform>();
        //레퍼런스 할당 
        enemyAgent = GetComponent<NavMeshAgent>();
        //자신의 자식에 있는 Animation 컴포넌트를 찾아와 레퍼런스에 할당 
        _anim = GetComponentInChildren<Animation>();

    }


    IEnumerator Start()
    {
        //Animation 컴포넌트의 clip속성에 walk 애니메이션 클립 지정 
        _anim.clip = anims.walk;
        //지정한 애니메이션 클립(애니메이션) 실행 
        _anim.Play();

        // 일정 간격으로 주변의 가장 가까운 플레이어를 찾는 코루틴 
        StartCoroutine(TargetSetting());
        // 정해진 시간 간격으로 Enemy의 Ai 변화 상태를 셋팅하는 코루틴
        StartCoroutine(ModeSet());
        // Enemy의 상태 변화에 따라 일정 행동을 수행하는 코루틴
        StartCoroutine(ModeAction());
        yield return null;
    }


    void Update()
    {
        //랜덤 애니메이션 선택 (5초)
        if (Time.time > randAnimTime)
        {
            randAnim = Random.Range(0, 2);
            randAnimTime = Time.time + 5.0f;

            //걷는 상태일때만 포인트로 이동
            if (enemyState == ENEMY_STATE.walk)
            {
                if (RandomPoint(transform.position, range, out point))
                {
                    //SetDestination 포인트 지점까지 이동하는 함수
                    enemyAgent.SetDestination(point);
                }
            }
        }
    }

    //특정 위치에 네비게이션 존재 true 아니면 false
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }


    //Enemy의 변화 상태에 따라 일정 행동을 수행하는 코루틴
    IEnumerator ModeSet()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);

            //player가 1명 이상일때
            if (players.Length != 0)
            {
                //자신과 Player의 거리 셋팅
                float dist = Vector3.Distance(enemyTr.position, traceTr.position);

                //공격 받았을시
                if (isHit)
                {
                    enemyState = ENEMY_STATE.hit;
                }
                //attack 사거리에 들어왔을시
                else if (dist <= attackDist)
                {
                    enemyState = ENEMY_STATE.attack;
                }
                //추적 중일 때
                else if (isTrace)
                {
                    enemyState = ENEMY_STATE.run;
                }
                //추적 사거리 안에 들어왔을때
                else if (dist <= traceDist)
                {
                    enemyState = ENEMY_STATE.run;
                }
                else
                {
                    enemyState = ENEMY_STATE.walk;
                }
            }
            //player가 0명일때
            else
            {
                enemyState = ENEMY_STATE.walk;
            }
        }
    }

    //Enemy의 상태 변화에 따라 일정 행동을 수행하는 코루틴
    IEnumerator ModeAction()
    {
        while (!isDie)
        {
            switch (enemyState)
            {
                //Enemy가 walk 상태 일때
                case ENEMY_STATE.walk:
                    //네비게이션 추적시작
                    enemyAgent.isStopped = false;
                    enemyAgent.speed = 0.5f;
                    if (randAnim == 0 || randAnim == 1)
                    {
                        //walk 애니메이션
                        _anim.CrossFade(anims.walk.name);
                        //remainingDistance 도착까지 남은 거리 반환
                        if (enemyAgent.remainingDistance <= 0.5)
                        {
                            //walk 애니메이션
                            _anim.CrossFade(anims.idle.name);
                        }
                    }
                    break;

                case ENEMY_STATE.run:
                    //네비게이션 재시작(추적)
                    enemyAgent.isStopped = false;
                    //추적대상 설정
                    enemyAgent.destination = traceTr.position;
                    //네베게이션의 추적 속도 업
                    enemyAgent.speed = 1.0f;
                    //run 애니메이션
                    _anim.CrossFade(anims.run.name, 0.3f);
                    break;

                case ENEMY_STATE.attack:
                    //네비게이션 멈추고 (추적 중지) 
                    enemyAgent.isStopped = true;
                    //공격할때 적을 봐라 봐야함 
                    Quaternion enemyLookRotation = Quaternion.LookRotation(traceTr.position - enemyTr.position); // - 해줘야 바라봄  
                    enemyTr.rotation = Quaternion.Lerp(enemyTr.rotation, enemyLookRotation, Time.deltaTime * 10.0f);

                    if (randAnim == 0)
                    {
                        //attack1 애니메이션
                        _anim.CrossFade(anims.attack1.name, 0.3f);
                    }
                    else if (randAnim == 1)
                    {
                        //attack2 애니메이션
                        _anim.CrossFade(anims.attack2.name, 0.3f);
                    }
                    break;

                case ENEMY_STATE.hit:
                    //네비게이션 멈추고 (추적 중지)
                    enemyAgent.isStopped = true;

                    if (randAnim == 0)
                    {
                        //hit1 애니메이션
                        _anim.CrossFade(anims.hit1.name, 0.3f);
                    }
                    else if (randAnim == 1)
                    {
                        //hit2 애니메이션
                        _anim.CrossFade(anims.hit2.name, 0.3f);
                    }
                    break;
            }
            yield return null;
        }
    }

    //자신과 가장 가까운 적을 찾음
    IEnumerator TargetSetting()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);

            //자신과 가장 가까운 플레이어 찾음
            players = GameObject.FindGameObjectsWithTag("Player");

            //플레이어가 있을경우
            if (players.Length != 0)
            {   //sqrMagnitude 두 점간의 거리의 제곱에 루트를 한 값
                playerTarget = players[0].transform;
                float dist1 = (playerTarget.position - enemyTr.position).sqrMagnitude;
                foreach (GameObject _players in players)
                {
                    if ((_players.transform.position - enemyTr.position).sqrMagnitude < dist1)
                    {
                        playerTarget = _players.transform;
                        dist1 = (playerTarget.position - enemyTr.position).sqrMagnitude;
                    }
                }
            }
            traceTr = playerTarget;
        }
    }





    // //오브젝트 풀 테스트
    // private void OnEnable()
    // {
    //     StartCoroutine("DisableEnemy");
    // }


    // IEnumerator DisableEnemy()
    // {
    //     yield return new WaitForSeconds(5.0f);
    //     gameObject.SetActive(false);
    // }
}
