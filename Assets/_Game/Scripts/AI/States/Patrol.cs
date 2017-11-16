using UnityEngine;
using UnityEngine.AI;

public class Patrol : IState
{

  private NavMeshAgent navMeshAgent;
  private float patrolSpeed;
  private int wayPointInd = 0;
  private GameObject[] wayPoints;
  private Transform newTransform;
  private Animator animator;

  public Patrol(NavMeshAgent navMeshAgent, float patrolSpeed, GameObject[] wayPoints, int wayPointInd, Transform newTransform, Animator animator)
  {

    this.navMeshAgent = navMeshAgent;
    this.patrolSpeed = patrolSpeed;
    this.wayPoints = wayPoints;
    this.wayPointInd = wayPointInd;
    this.newTransform = newTransform;
    this.animator = animator;

  }
  void IState.Enter()
  {

    // wayPointInd = Random.Range(0, wayPoints.Length);
    animator.SetBool("isWalking", true);
    // wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");

  }

  void IState.Execute()
  {

    if (Vector3.Distance(newTransform.position, wayPoints[wayPointInd].transform.position) >= 1)
    {
      navMeshAgent.SetDestination(wayPoints[wayPointInd].transform.position);
      navMeshAgent.destination = wayPoints[wayPointInd].transform.position;
    }
    else if (Vector3.Distance(newTransform.position, wayPoints[wayPointInd].transform.position) <= 1)
    {
      wayPointInd += 1;
      // wayPointInd = Random.Range(0, wayPoints.Length);
      if (wayPointInd >= wayPoints.Length)
      {
        wayPointInd = 0;
      }
    }
    else
    {
      navMeshAgent.destination = newTransform.position;
    }

  }

  void IState.Exit()
  {

    animator.SetBool("isWalking", false);
    navMeshAgent.destination = newTransform.position;

  }

}
