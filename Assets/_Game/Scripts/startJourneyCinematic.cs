using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using Cinemachine;

public class startJourneyCinematic : MonoBehaviour
{

  [SerializeField] private PlayableDirector timeLine;
  [SerializeField] private GameObject actor;
  [SerializeField] private GameObject teleportPoints;
  [SerializeField] private GameObject camera;
  [SerializeField] private GameObject cameraStartPosition;
  [SerializeField] private GameObject cameraPostCinematicPosition;
  [SerializeField] private float smoothTime = 0.3F;

  Coroutine waitForTimeLineToFinish;
  Vector3 velocity = Vector3.zero;

  void Start()
  {

    timeLine = GetComponent<PlayableDirector>();
    camera.transform.position = cameraStartPosition.transform.position;
    camera.transform.rotation = cameraStartPosition.transform.rotation;
    teleportPoints.SetActive(false);
    actor.SetActive(true);
    StartTimeLine();


  }

  void Update()
  {

  }

  void StartTimeLine()
  {

    if (timeLine == null) return;
    timeLine.time = 0;
    timeLine.Play();
    if (waitForTimeLineToFinish != null) StopCoroutine(waitForTimeLineToFinish);
    waitForTimeLineToFinish = StartCoroutine(WaitForTimeLineToFinish());

  }

  IEnumerator WaitForTimeLineToFinish()
  {

    float timeLineDuration = (float)timeLine.duration;
    yield return new WaitForSeconds(timeLineDuration);
    teleportPoints.SetActive(true);
    actor.SetActive(false);

  }

}
