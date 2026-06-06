using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NPC : MonoBehaviour
{
    [SerializeField] private Collider[] colliders;
    [SerializeField] private Transform bubble;
    [SerializeField] private float maxIdleTime;
    [SerializeField] AudioSource _AudioSourceBalloon;
    [SerializeField] AudioSource _AudioSourceOuch;

    private NavMeshAgent agent;
    private NpcState npcState;
    private float timer;
    private float cooldown;
    private Animator animator;
    private bool _isHittable;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isHittable = true;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(npcState == NpcState.Dead)
        {
            return;
        }

        if(agent.velocity.magnitude <= 0.0f)
        {
            npcState = NpcState.Idle;
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                SetDestination();
            }
        }
        else
        {

            npcState = NpcState.Walking;
        }

        animator.SetFloat("NormalizedWalkSpeed", agent.velocity.magnitude.Remap(0, agent.speed, 0, 1));
    }

    private void StartTimer()
    {
        cooldown = Random.Range(2, maxIdleTime);
        timer = cooldown;
    }

    private void SetDestination()
    {
        agent.SetDestination(RandomPointOnNavMesh());
    }

    private Vector3 RandomPointOnNavMesh()
    {
        bool searchPoint = true;
        Vector3 result = Vector3.zero;

        while (searchPoint)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * 10.0f;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                searchPoint = false;
            }

        }

        return result;
    }

    public void ReceiveCollision(string tag)
    {
        if (tag == "Head" && npcState != NpcState.Dead && _isHittable == true)
        {
            bubble.transform.DOScale(Vector3.one * Random.Range(0.5f, 5.0f), 2.5f);
            _AudioSourceBalloon.Play();
            ScoreManager.Instance.IncreaseScore();
            _isHittable = false;
            Invoke("SetHittable", 20);
        }
        else if(tag == "Body")
        {
            animator.SetBool("Die", true);
            npcState = NpcState.Dead;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.ResetPath();
            _AudioSourceOuch.Play();
            Invoke("StandUp", 15);
        }
    }

    private void StandUp()
    {
        animator.SetBool("Die", false);
        npcState = NpcState.Idle;
    }

    private void SetHittable()
    {
        _isHittable = true;
        bubble.transform.DOScale(Vector3.zero, 1);
    }
}
