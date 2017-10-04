using UnityEngine;
using UnityEngine.AI;

public class Interacting : IState
{

  private NavMeshAgent navMeshAgent;
  private Animator animator;
  private Transform newTransform;
  private Transform target;
  private float rotationSpeed;
  public Interacting(NavMeshAgent navMeshAgent, Animator animator, Transform newTransform, Transform target,
   float rotationSpeed)
  {

    this.navMeshAgent = navMeshAgent;
    this.animator = animator;
    this.newTransform = newTransform;
    this.target = target;
    this.rotationSpeed = rotationSpeed;

  }

  void IState.Enter()
  {

    animator.SetBool("isInteracting", true);

    Vector3 direction = (target.position - newTransform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(direction);
    newTransform.rotation = Quaternion.Slerp(newTransform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

  }

  void IState.Execute()
  {



  }

  void IState.Exit()
  {

    animator.SetBool("isInteracting", false);

  }
}
