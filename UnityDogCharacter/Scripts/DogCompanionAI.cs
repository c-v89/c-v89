using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DogCompanionAI : MonoBehaviour
{
    private enum State { Idle, Following, Wandering, Sitting }

    [Header("Target")]
    [SerializeField] private Transform owner;
    [SerializeField] private float followDistance = 3f;
    [SerializeField] private float stopDistance = 1.5f;

    [Header("Wander")]
    [SerializeField] private float wanderRadius = 6f;
    [SerializeField] private float wanderInterval = 4f;

    [SerializeField] private Animator animator;

    private NavMeshAgent agent;
    private State state = State.Idle;
    private float wanderTimer;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int SitHash = Animator.StringToHash("Sit");

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (owner == null)
        {
            Wander();
        }
        else
        {
            float distanceToOwner = Vector3.Distance(transform.position, owner.position);
            if (distanceToOwner > followDistance)
                Follow();
            else if (distanceToOwner <= stopDistance)
                Sit();
            else
                state = State.Idle;
        }

        if (state != State.Sitting)
            animator.SetBool(SitHash, false);

        animator.SetFloat(SpeedHash, agent.velocity.magnitude);
    }

    private void Follow()
    {
        state = State.Following;
        agent.isStopped = false;
        agent.SetDestination(owner.position);
    }

    private void Sit()
    {
        if (state == State.Sitting) return;
        state = State.Sitting;
        agent.isStopped = true;
        animator.SetBool(SitHash, true);
    }

    private void Wander()
    {
        wanderTimer += Time.deltaTime;
        if (state == State.Wandering && wanderTimer < wanderInterval) return;

        wanderTimer = 0f;
        state = State.Wandering;
        agent.isStopped = false;

        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius + transform.position;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
            agent.SetDestination(hit.position);
    }
}
