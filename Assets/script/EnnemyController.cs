using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnnemyAI{
    isChasing,
    isFinding,
}

public class EnnemyController : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    public List<GameObject> positions = new List<GameObject>();
    public int currentPos;
    public EnnemyAI ennemyAI;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(positions[currentPos].transform.position);


    }

    // Update is called once per frame
    void Update()
    {
        switch (ennemyAI)
        {
            case EnnemyAI.isChasing:
                // Implement chasing logic here
                agent.SetDestination(player.transform.position);
                break;

            case EnnemyAI.isFinding:
                // Implement finding logic here, for example, moving towards a predefined set of positions
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    currentPos = (currentPos + 1) % positions.Count;
                    agent.SetDestination(positions[currentPos].transform.position);
                }
                break;

            default:
                break;
        }


    }
    public void SetEnemyAI(EnnemyAI ennemyAIState)
    {
        ennemyAI = ennemyAIState;
    }
}
