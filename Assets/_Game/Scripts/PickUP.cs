using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PickUP : MonoBehaviour {

  #region Variables
  [SerializeField] private float fillTime = 2f;
  [SerializeField] private GameObject fillProgress;
  [SerializeField] private Image deactivateProgress;
  [Range(0, .05f)] [SerializeField] private float outlineWidth;
  private Coroutine deactivateCoroutine;

  private float outlineWidthInactive = 0;
  private float timer;

  private bool gazedAt;

  IEnumerator pickUp;
  #endregion

  void Awake()
  {

    pickUp = PickUp();

  }

  public void PointerEnter(GameObject outline)
  {

    gazedAt = true;
    fillProgress.SetActive(true);
    StartCoroutine(pickUp);

    if (gazedAt)
    {

      outline.GetComponent<Renderer>().material.SetFloat("_Outline", outlineWidth);

      return;
    }
    else
      outline.GetComponent<Renderer>().material.SetFloat("_Outline", outlineWidthInactive);

  }

  public void PointerExit(GameObject outline)
  {

    gazedAt = false;
    if (pickUp != null)
    {
      StopCoroutine(pickUp);
    }

    timer = 0f;
    deactivateProgress.fillAmount = 0;
    fillProgress.SetActive(false);
    outline.GetComponent<Renderer>().material.SetFloat("_Outline", outlineWidthInactive);

  }

  void PickUpTool()
  {

    deactivateProgress.fillAmount = 0;
    gameObject.SetActive(false);
    fillProgress.SetActive(false);
    LevelManager.count++;
    Debug.Log(LevelManager.count);

  }

  private IEnumerator PickUp()
  {

    timer = 0;

    while (timer < fillTime)
    {
      timer += Time.deltaTime;

      deactivateProgress.fillAmount = timer / fillTime;

      yield return null;

      if (gazedAt)
        continue;

      timer = 0;
      deactivateProgress.fillAmount = 0;
      yield break;
    }

    PickUpTool();

  }
}
