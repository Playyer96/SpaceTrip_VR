using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

  private IState currentlyRunningState;
  public IState previousState;

  public void ChangeState(IState newState)
  {

    if (currentlyRunningState != null)
    {
      this.currentlyRunningState.Exit();
    }
    this.currentlyRunningState = newState;
    if (previousState == null)
    {
      this.previousState = this.currentlyRunningState;
    }
    this.currentlyRunningState.Enter();

  }

  public void ExecuteStateUpdate()
  {

    var runingState = this.currentlyRunningState;
    if (runingState != null)
    {
      runingState.Execute();
    }

  }

  public void SwitchToPreviousState()
  {
    if (currentlyRunningState != null)
    {
      this.currentlyRunningState.Exit();
    }
    this.currentlyRunningState = this.previousState;
    this.currentlyRunningState.Enter();

  }

}
