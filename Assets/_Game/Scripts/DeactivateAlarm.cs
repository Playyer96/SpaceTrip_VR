using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeactivateAlarm : MonoBehaviour
{

  #region Variables
  [SerializeField] private MonoBehaviour gvrAudioSource;
  [SerializeField] private GameObject imageEffect;
  [SerializeField] private float fillTime = 2f;
  [SerializeField] private Slider deactivateProgress;
  [SerializeField] private Text pickToolText;
  [SerializeField] private AnimatedDialog toolText;
  [SerializeField] private GameObject tools;
  [SerializeField] private GameObject uselessTools;
  [SerializeField] private LevelManager levelManager;

  private Coroutine deactivateCoroutine;

  private float timer;

  private bool gazedAt;

  bool firstTime;

  #endregion

  void Awake()
  {

    firstTime = true;
    gvrAudioSource.enabled = true;
    pickToolText.enabled = false;
    gvrAudioSource.GetComponent<GvrAudioSource>().Play();
    imageEffect.GetComponent<ImageEffect>().enabled = true;
    tools.SetActive(false);
     uselessTools.SetActive(true);

  }

  void Update()
  {

    if (firstTime == false)
    {
      tools.SetActive(true);
      uselessTools.SetActive(false);
    }

  }

  #region Methods
  public void PointerEnter()
  {
    gazedAt = true;

    if (LevelManager.count == 5)
    {

      if (imageEffect.GetComponent<ImageEffect>().enabled == false)
      {
        deactivateProgress.value = 1;
      }
      else
        deactivateCoroutine = StartCoroutine(DeactivateCoroutine());
    }

    if (firstTime == true)
    {
      firstTime = false;
      pickToolText.enabled = true;
      toolText.TipyingAnimation();

    }

  }

  public void PointerExit()
  {

    gazedAt = false;
    if (deactivateCoroutine != null)
    {
      StopCoroutine(deactivateCoroutine);
    }

    timer = 0f;
    deactivateProgress.value = 0;

    if (imageEffect.GetComponent<ImageEffect>().enabled == false)
    {
      deactivateProgress.value = 1;
    }

  }

  private void StopAlarm()
  {

    gvrAudioSource.GetComponent<GvrAudioSource>().Stop();
    imageEffect.GetComponent<ImageEffect>().enabled = false;
    imageEffect.GetComponent<Animator>().enabled = false;
  }

  #endregion

  IEnumerator DeactivateCoroutine()
  {

    timer = 0;

    while (timer < fillTime)
    {
      timer += Time.deltaTime;

      deactivateProgress.value = timer / fillTime;

      yield return null;

      if (gazedAt)
        continue;

      timer = 0;
      deactivateProgress.value = 0;
      yield break;
    }

    StopAlarm();
    levelManager.youWin = true;

  }
}
