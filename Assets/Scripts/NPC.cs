using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NPC : MonoBehaviour
{
    [SerializeField] private Collider[] colliders;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bubble;

    private NavMeshAgent agent;
    private bool isWalking;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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

    // Update is called once per frame
    void Update()
    {
        if(agent.velocity.magnitude <= 0.0f)
        {
            isWalking = false;
        }
        if(!isWalking)
        {
            agent.SetDestination(RandomPointOnNavMesh());
            isWalking = true;
        }
    }

    public void ReceiveCollision(string tag)
    {
        if (tag == "Head")
        {
            bubble.transform.DOScale(Vector3.one * Random.Range(0.5f, 5.0f), 2.5f);
        }
        else if(tag == "Body")
        {
            animator.SetBool("Die", true);
        }
    }
}
