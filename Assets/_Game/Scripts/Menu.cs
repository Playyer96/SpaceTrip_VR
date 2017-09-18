using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

  [SerializeField] private float fillTime = 2.5f;
  [SerializeField] private float fillExitTime = 4f;
  [SerializeField] private Image startFill;
  [SerializeField] private Image exitFill;
  [SerializeField] private string sceneNameString;
  [SerializeField] private GameObject LoadingScreenObj;
  [SerializeField] private Slider slider;
  [SerializeField] private Text progressText;

  private Coroutine fillBarExitCoroutine;
  private Coroutine fillBarCoroutine;
  private float timer;
  private bool gazedAt;

  void Awake()
  {

    LoadingScreenObj.SetActive(false);

  }

  public void PointerEnter()
  {
    gazedAt = true;
    fillBarCoroutine = StartCoroutine(FillBar(sceneNameString));

  }

  public void PointerExit()
  {

    gazedAt = false;
    if (fillBarCoroutine != null)
    {
      StopCoroutine(fillBarCoroutine);
    }

    timer = 0f;
    startFill.fillAmount = 0;

  }

  public void PointerEnterEB()
  {
    gazedAt = true;
    fillBarExitCoroutine = StartCoroutine(FillBarEB());

  }

  public void PointerExitEB()
  {

    gazedAt = false;
    if (fillBarExitCoroutine != null)
    {
      StopCoroutine(fillBarExitCoroutine);
    }

    timer = 0f;
    exitFill.fillAmount = 0;

  }

  private IEnumerator FillBar(string sceneName)
  {

    timer = 0f;

    while (timer < fillTime)
    {
      timer += Time.deltaTime;

      startFill.fillAmount = timer / fillTime;

      yield return null;

      if (gazedAt)
        continue;

      timer = 0f;
      startFill.fillAmount = 0;
      yield break;

    }
    StartCoroutine(LoadAsynchronously(sceneName));
  }

  private IEnumerator FillBarEB()
  {

    timer = 0f;

    while (timer < fillExitTime)
    {
      timer += Time.deltaTime;

      exitFill.fillAmount = timer / fillExitTime;

      yield return null;

      if (gazedAt)
        continue;

      timer = 0f;
      exitFill.fillAmount = 0;
      yield break;

    }
    Exit();
  }

  private void Exit()
  {

    Application.Quit();

  }

  IEnumerator LoadAsynchronously(string sceneName)
  {

    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
    LoadingScreenObj.SetActive(true);

    while (!operation.isDone)
    {
      float progress = Mathf.Clamp01(operation.progress / .9f);

      slider.value = progress;
      progressText.text = progress * 100f + "%";
      Debug.Log(progress);

      yield return null;
    }

  }
}
