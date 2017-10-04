using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public class NpcDroid : MonoBehaviour
{

  private StateMachine stateMachine = new StateMachine();

  [SerializeField] private NavMeshAgent navMeshAgent;
  [SerializeField] private Animator droidAnimator;
  [SerializeField] private float patrolSpeed;
  [SerializeField] private GameObject[] wayPoints;
  [SerializeField] private Transform playerTransform;
  [SerializeField] private GvrAudioSource audioSource;
  [SerializeField] private AudioClip[] audioClips;
  private float rotationSpeed = 2;
  private Transform newtransform;
  private int wayPointInd = 0;

  public bool isStanding;
  public bool isInteracting;

  bool gazedAt;
  IEnumerator interactCourutine;
  int num;

  private void Start()
  {

    isStanding = false;
    isInteracting = false;
    this.newtransform = this.GetComponent<Transform>();
    this.navMeshAgent = this.GetComponent<NavMeshAgent>();
    this.droidAnimator = this.GetComponent<Animator>();
    this.audioSource = this.GetComponent<GvrAudioSource>();
    this.stateMachine.ChangeState(new Patrol(this.navMeshAgent, this.patrolSpeed,
          this.wayPoints, this.wayPointInd, this.transform, this.droidAnimator));

    navMeshAgent.updatePosition = true;
    navMeshAgent.updateRotation = true;

    navMeshAgent.speed = patrolSpeed;

  }

  private void Update()
  {

    while (isInteracting)
    {
      num = Random.Range(0, 1);
      audioSource.clip = audioClips[num];
      if (!audioSource.isPlaying)
      {
        audioSource.Play();
      }
      interactCourutine = Interaction();
      StartCoroutine(interactCourutine);
      this.stateMachine.ChangeState(new Interacting(this.navMeshAgent, this.droidAnimator,
            this.newtransform, this.playerTransform, this.rotationSpeed));
      return;
    }

    while (isStanding)
    {
      this.stateMachine.ChangeState(new Standing(this.droidAnimator));
      return;
    }

    this.stateMachine.ExecuteStateUpdate();

    if (!isStanding || !isInteracting)
    {
      this.stateMachine.SwitchToPreviousState();
    }

    this.stateMachine.ExecuteStateUpdate();

  }



  public void Interact()
  {

    isInteracting = true;
    isStanding = false;

  }

  void OnTriggerEnter(Collider other)
  {

  }

  public void PointerEnter()
  {

    gazedAt = true;
    isStanding = true;

  }

  public void PointerExit()
  {

    gazedAt = false;
    isStanding = false;

  }

  IEnumerator Interaction()
  {

    yield return new WaitForSeconds(3f);

    isInteracting = false;
    StopCoroutine(interactCourutine);

  }

}
