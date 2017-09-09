using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeactivateAlarm : MonoBehaviour
{

  [SerializeField] private MonoBehaviour gvrAudioSource;
  [SerializeField] private GameObject imageEffect;
  [SerializeField] private float fillTime = 2f;
  [SerializeField] private Slider deactivareProgress;

  private Coroutine deactivateCoroutine;
  private float timer;
  private bool gazedAt;


  void Awake()
  {

    gvrAudioSource.enabled = true;
    gvrAudioSource.GetComponent<GvrAudioSource>().Play();
    imageEffect.GetComponent<ImageEffect>().enabled = true;
    imageEffect.GetComponent<Animator>().enabled = true;

  }

  public void PointerEnter()
  {

    gazedAt = true;

    if (imageEffect.GetComponent<ImageEffect>().enabled == false)
    {
      deactivareProgress.value = 1;
    }
    else
      deactivateCoroutine = StartCoroutine(DeactivateCoroutine());

  }

  public void PointerExit()
  {

    gazedAt = false;
    if (deactivateCoroutine != null)
    {
      StopCoroutine(deactivateCoroutine);
    }

    timer = 0f;
    deactivareProgress.value = 0;

    if (imageEffect.GetComponent<ImageEffect>().enabled == false)
    {
      deactivareProgress.value = 1;
    }

  }

  IEnumerator DeactivateCoroutine()
  {

    timer = 0;

    while (timer < fillTime)
    {
      timer += Time.deltaTime;

      deactivareProgress.value = timer / fillTime;

      yield return null;

      if (gazedAt)
        continue;

      timer = 0;
      deactivareProgress.value = 0;
      yield break;
    }

    StopAlarm();

  }

  private void StopAlarm()
  {

    gvrAudioSource.GetComponent<GvrAudioSource>().Stop();
    imageEffect.GetComponent<ImageEffect>().enabled = false;
    imageEffect.GetComponent<Animator>().enabled = false;

  }

}
