using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class AIScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform goal; // Reference to the goal Transform
    [SerializeField] GameObject player; // Reference to the player GameObject
    NavMeshAgent agent;
    Animator animatorController;
    bool isWalking = false;


    void Start()
    {
        NavMeshSurface surface = FindObjectOfType<NavMeshSurface>();
        agent = GetComponent<NavMeshAgent>();
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, this.transform.position) < 10f)
        {
            agent.destination = player.transform.position;
            isWalking = true;
            animatorController.SetFloat("Vitesse", 1.0f);
        }
        if(Vector3.Distance(player.transform.position, this.transform.position) <= 1f)
        {
            isWalking = false;
            agent.destination = this.transform.position;
            animatorController.SetFloat("Vitesse", 0.0f);
        }
    }
}
