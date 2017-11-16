using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

public class startJourneyCinematic : MonoBehaviour
{

  [SerializeField] private PlayableDirector timeLine;
  [SerializeField] private GameObject actor;

  Coroutine waitForTimeLineToFinish;

  public bool finishCinematic;

  void Awake()
  {

    timeLine = GetComponent<PlayableDirector>();
   
  }

  void Start()
  {

    
    actor.SetActive(true);
    finishCinematic = false;

  }

  public void StartTimeLine()
  {

    if (timeLine == null) return;
    timeLine.time = 0;
    timeLine.Play();

    if (waitForTimeLineToFinish != null)
      StopCoroutine(waitForTimeLineToFinish);

    waitForTimeLineToFinish = StartCoroutine(WaitForTimeLineToFinish());

  }

  public IEnumerator WaitForTimeLineToFinish()
  {

    float timeLineDuration = (float)timeLine.duration;

    yield return new WaitForSeconds(timeLineDuration);

    actor.SetActive(false);
    finishCinematic = true;
    StopCoroutine(waitForTimeLineToFinish);

  }

}
