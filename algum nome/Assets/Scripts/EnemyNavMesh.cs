using UnityEngine;
using UnityEngine.AI;
public class EnemyNavMesh : MonoBehaviour
{
    public Transform player;
    public Transform GameObject;
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public LayerMask layerMask;
    public float losePlayerDistamce = 15f;

    // Update is called once per frame

    public enum EnemyState
    {
        WayPatrol, RandomPatrol, Pursuit, Fugir
    }
    public EnemyState currentState = EnemyState.WayPatrol;

    void Update()
    {
        FiniteStateMachine();
    }
    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }
    void FiniteStateMachine()
    {
        switch (currentState)
        {
            case EnemyState.Pursuit:
                Pursuit();
                break;
            case EnemyState.WayPatrol:
                WayPatrol();
                break;
            case EnemyState.Fugir:
                Fugir();
                break;
            default:
                break;
        }
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 20f, layerMask))
        {
            ChangeState(EnemyState.Pursuit);
        }
        else
        {
            ChangeState(EnemyState.WayPatrol);
        }
    }
    void Pursuit()
    {
        if (player != null)
        {
            agent.stoppingDistance = 6f;
            agent.SetDestination(player.position);
            transform.LookAt(player);
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > losePlayerDistamce)
        {
            ChangeState(EnemyState.WayPatrol);
        }
    }
    void WayPatrol()
    {
        agent.stoppingDistance = 0f;
        if (waypoints.Length != 0 && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            int randomIndex = Random.Range(0, waypoints.Length);
            agent.SetDestination(waypoints[randomIndex].position);
        }
        SearchPlayer();
    }
    void SearchPlayer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 20f, layerMask))
        {
            ChangeState(EnemyState.Pursuit);
            CallBackup();
        }
    }
    void CallBackup()
    {
        EnemyNavMesh[] todosInimigos = FindObjectsByType<EnemyNavMesh>();
        foreach  (EnemyNavMesh aliado in todosInimigos)
        {
            float distancia = Vector3.Distance(transform.position, aliado.transform.position);
            if (distancia < 10)
            {
                aliado.ChangeState(EnemyState.Pursuit);
            }
        }

  

    }
    void Fugir()
    {
        if (GameObject != null)
        {
            agent.stoppingDistance = 6f;
            agent.SetDestination(GameObject.position);
            transform.LookAt(GameObject);
        }
        float distanceToGameObject = Vector3.Distance(transform.position, GameObject.position);
        if (distanceToGameObject > 0.5f)
        {
            ChangeState(EnemyState.WayPatrol);
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GameObject"))
        {
            ChangeState(EnemyState.Fugir);
        }
    }
}