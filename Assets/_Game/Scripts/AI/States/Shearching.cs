using UnityEngine;

public class Shearching : IState
{

  private Animator animator;

  public Shearching(Animator animator)
  {

    this.animator = animator;

  }

  void IState.Enter()
  {

    animator.SetBool("isLooking", true);

  }

  void IState.Execute()
  {



  }

  void IState.Exit()
  {

    animator.SetBool("isLooking", true);

  }
}
