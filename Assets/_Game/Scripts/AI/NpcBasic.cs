using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class NpcBasic : MonoBehaviour
{

  public enum State
  {

    IDLE,
    PATROL,
    SHEARCHING,
    INTERACT

  }

  public State state;

  [SerializeField] private Transform npcTransform;
  [SerializeField] private NavMeshAgent npcNavMeshAgent;
  [SerializeField] private Animator npcAnimator;
  [SerializeField] private GameObject[] wayPoints;

  private int wayPointInd = 0;
  private float patrolSpeed = 3f;

  Vector3 npcPosition;

  bool alive;
  bool idle;
  bool walking;
  bool looking;
  bool interacting;

  IEnumerator sm;

  void Start()
  {

    npcTransform = npcTransform.GetComponent<Transform>();
    npcNavMeshAgent = npcNavMeshAgent.GetComponent<NavMeshAgent>();
    npcAnimator = npcAnimator.GetComponent<Animator>();

    npcNavMeshAgent.updatePosition = true;
    npcNavMeshAgent.updateRotation = true;

    // wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
    wayPointInd = Random.Range(0, wayPoints.Length);

    state = NpcBasic.State.PATROL;

    alive = true;

    sm = SM();
    StartCoroutine(sm);

  }

  IEnumerator SM()
  {

    while (alive)
    {
      switch (state)
      {
        case State.PATROL:
          Patrol();
          break;
        case State.IDLE:
          Idle();
          break;
        case State.SHEARCHING:
          Shearching();
          break;
        case State.INTERACT:
          Interacting();
          break;
      }
      yield return null;
    }

  }

  void Patrol()
  {

    npcNavMeshAgent.speed = patrolSpeed;
    npcAnimator.SetBool("isWalking", true);
    npcAnimator.SetBool("isLooking", false);
    npcAnimator.SetBool("isStanding", false);
    npcAnimator.SetBool("isInteracting", false);
    if (Vector3.Distance(this.transform.position, wayPoints[wayPointInd].transform.position) >= 2)
    {
      npcNavMeshAgent.SetDestination(wayPoints[wayPointInd].transform.position);
      npcNavMeshAgent.destination = wayPoints[wayPointInd].transform.position;
    }
    else if (Vector3.Distance(this.transform.position, wayPoints[wayPointInd].transform.position) <= 2)
    {
      // wayPointInd += 1;
      wayPointInd = Random.Range(0, wayPoints.Length);
      if (wayPointInd >= wayPoints.Length)
      {
        wayPointInd = 0;
      }
    }
    else
    {
      npcNavMeshAgent.destination = npcTransform.transform.position;
    }

  }

  void Idle()
  {

    npcAnimator.SetBool("isWalking", false);
    npcAnimator.SetBool("isLooking", false);
    npcAnimator.SetBool("isStanding", true);
    npcAnimator.SetBool("isInteracting", false);

  }

  void Shearching()
  {

    npcAnimator.SetBool("isLooking", true);
    npcAnimator.SetBool("isWalking", false);
    npcAnimator.SetBool("isStanding", false);
    npcAnimator.SetBool("isInteracting", false);

  }

  void Interacting()
  {

    npcAnimator.SetBool("isInteracting", true);
    npcAnimator.SetBool("isLooking", false);
    npcAnimator.SetBool("isWalking", false);
    npcAnimator.SetBool("isStanding", false);

  }

}