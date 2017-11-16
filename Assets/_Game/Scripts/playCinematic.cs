using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class playCinematic : MonoBehaviour {

	[SerializeField] private PlayableDirector timeLine;

	[HideInInspector] public bool cinematicEnded;

	Coroutine waitTimeLineToFinish;

	public void Awake () {
		timeLine = GetComponent<PlayableDirector> ();
		cinematicEnded = false;
	}

	public void playTimeLine () {
		timeLine.time = 0;
		timeLine.Play ();

		if (waitTimeLineToFinish != null)
			StopCoroutine (waitTimeLineToFinish);

		waitTimeLineToFinish = StartCoroutine (WaitTimeLineToFinish ());
	}

	public IEnumerator WaitTimeLineToFinish () {
		float timeLineDuration = (float) timeLine.duration;

		yield return new WaitForSeconds (timeLineDuration);

		cinematicEnded = true;
		// StopCoroutine (waitTimeLineToFinish);
	}
}