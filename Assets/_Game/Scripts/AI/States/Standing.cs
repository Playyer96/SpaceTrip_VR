using UnityEngine;

public class Standing : IState
{

  private Animator animator;


  public Standing(Animator animator)
  {

    this.animator = animator;

  }

  void IState.Enter()
  {

    animator.SetBool("isStanding", true);

  }

  void IState.Execute()
  {



  }

  void IState.Exit()
  {

    animator.SetBool("isStanding", false);

  }
}
