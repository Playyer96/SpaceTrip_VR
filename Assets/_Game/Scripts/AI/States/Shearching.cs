using UnityEngine;

public class Shearching : IState
{

  private Animator animator;
  private Transform newTransform;
  private Transform lookAtTransform;
  private float rotationSpeed;

  public Shearching(Animator animator, Transform newTransform, Transform lookAtTransform, float rotationSpeed)
  {

    this.animator = animator;
    this.newTransform = newTransform;
    this.lookAtTransform = lookAtTransform;
    this.rotationSpeed = rotationSpeed;

  }

  void IState.Enter()
  {

    animator.SetBool("isLooking", true);

  }

  void IState.Execute()
  {

    Vector3 direction = (lookAtTransform.position + newTransform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(direction * Time.deltaTime);
    newTransform.rotation = Quaternion.Slerp(newTransform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

  }

  void IState.Exit()
  {

    animator.SetBool("isLooking", false);

  }
}
