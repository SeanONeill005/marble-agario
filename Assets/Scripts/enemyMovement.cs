using UnityEngine;
using UnityEngine.AI;


public class enemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 destination;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        generateNewDestination();
        var scale = Random.Range(0.1f, 1);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        gameObject.GetComponent<Rigidbody>().mass = scale;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = navMeshAgent.remainingDistance;
        if (dist > 0)
        {
            navMeshAgent.SetDestination(destination);
        }
        else
        {
            generateNewDestination();
            navMeshAgent.SetDestination(destination);
        }
        
    }

    void generateNewDestination()
    { 
        destination = new Vector3(Random.Range(gameObject.transform.position.x - 5, gameObject.transform.position.x + 5), 0, Random.Range(gameObject.transform.position.y - 5, gameObject.transform.position.y + 5));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {


            var enemyScale = collision.gameObject.transform.localScale;
            var playerScale = gameObject.transform.localScale;
            if (playerScale.x > enemyScale.x)
            {
                playerScale += enemyScale;
                gameObject.transform.localScale = playerScale;
                Destroy(collision.gameObject);
            }

            var EnemyMass = collision.gameObject.GetComponent<Rigidbody>().mass;
            var PlayerMass = gameObject.GetComponent<Rigidbody>().mass;

            if (PlayerMass > EnemyMass)
            {
                PlayerMass += EnemyMass;
                gameObject.GetComponent<Rigidbody>().mass = PlayerMass;
            }
        }
    }
}
