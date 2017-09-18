using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

  [SerializeField] private Text timerText;
  [SerializeField] private Text tiempoText;
  [SerializeField] private float startTime = 120;
  [SerializeField] private MonoBehaviour alarm;
  [SerializeField] private startJourneyCinematic cinematic;
  [SerializeField] private LevelManager isDead;
  IEnumerator countDown;

  void Awake()
  {

    alarm = alarm.GetComponent<ImageEffect>();
    cinematic = cinematic.GetComponent<startJourneyCinematic>();
    isDead = isDead.GetComponent<LevelManager>();
    countDown = CountDown();

  }

  public void CountDownTimer()
  {

    StartCoroutine(countDown);

  }

  public void StopCountDown()
  {

    StopCoroutine(countDown);

  }

  IEnumerator CountDown()
  {
    float t = 320;
    startTime = 320;

    while (startTime > 0)
    {
      startTime -= Time.deltaTime;

     t = startTime - Time.deltaTime;

      string minutes = ((int)t / 60).ToString();
      string seconds = (t % 60).ToString("f2");

      timerText.text = minutes + ":" + seconds;

      yield return new WaitForSeconds(t);

    }

    if (t <= 0 && alarm.enabled == true)
    {

      Debug.Log("Game Over");
      isDead.isGameOver = true;
      timerText.text = "0 : 00.00";
      StopCountDown();

    }

  }
}
