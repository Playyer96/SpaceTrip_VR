using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeleportPlayer : MonoBehaviour
{

  #region Variables
  [Header("Movement Properties")]
  [SerializeField]
  private float activateTarget = 1.75f;
  [SerializeField] private float velocity;
  [SerializeField] private Color inactiveColor;
  [SerializeField] private Color gazedAtColor;
  [Range(0, .05f)] [SerializeField] private float outlineWidth;
  [SerializeField] private GvrAudioSource footStepsAudioSource;
  [SerializeField] private GvrAudioSource breathingAudioSource;

  private new Transform transform;
  private Transform lastTarget;
  private Transform target;

  private bool gazedAt;
  private float timer;
  private float outlineWidthInactive = 0;

  IEnumerator lerp;
  IEnumerator clickTarget;

  float volumeMax = .15f;
  float volumeMin = .07f;

  #endregion

  private void Awake()
  {

    transform = GetComponent<Transform>();
    footStepsAudioSource = footStepsAudioSource.GetComponent<GvrAudioSource>();
    breathingAudioSource = breathingAudioSource.GetComponent<GvrAudioSource>();
  }

  void Start()
  {

    if (breathingAudioSource.isPlaying)
      breathingAudioSource.Stop();

  }

  void Update()
  {
    if (!footStepsAudioSource.isPlaying)
    {
      if (!breathingAudioSource.isPlaying)
      {
        breathingAudioSource.Play();
        breathingAudioSource.volume = Mathf.Lerp(volumeMax, volumeMin, 2 * Time.deltaTime);
      }
    }
    else
    {
      breathingAudioSource.volume = Mathf.Lerp(volumeMin, volumeMax, 2 * Time.deltaTime);
    }

  }

  #region Methods
  public void PointerEnter(Transform target)
  {

    gazedAt = true;
    this.target = target;
    if (clickTarget != null)
    {
      StopCoroutine(clickTarget);
    }

    clickTarget = ClickTarget();
    StartCoroutine(clickTarget);

    if (inactiveColor != null && gazedAtColor != null)
    {
      target.GetComponent<Renderer>().material.color = gazedAt ? gazedAtColor : inactiveColor;

      return;
    }

    lastTarget = target;

  }

  public void PointerEnterOutline(GameObject outline)
  {
    gazedAt = true;
    if (gazedAt)
    {

      outline.GetComponent<Renderer>().material.SetFloat("_Outline", outlineWidth);

      return;
    }
    else
      outline.GetComponent<Renderer>().material.SetFloat("_Outline", outlineWidthInactive);
  }

  public void PointerExit(GameObject teleport)
  {

    gazedAt = false;
    teleport.GetComponent<Renderer>().material.color = gazedAt ? gazedAtColor : inactiveColor;

  }

  public void PointerExitOutline(GameObject outline)
  {

    gazedAt = false;
    outline.GetComponent<Renderer>().material.SetFloat("_Outline", outlineWidthInactive);

  }

  public void OnTeleportClick(Transform target)
  {

    this.target = target;

    if (lerp != null)
    {
      StopCoroutine(lerp);
    }

    lerp = Lerp();
    StartCoroutine(lerp);

  }

  #endregion

  #region Coroutines
  private IEnumerator Lerp()
  {

    while (transform.position != target.position)
    {

      gazedAt = false;

      target.GetComponentInChildren<Renderer>().material.SetFloat("_Outline", outlineWidthInactive);

      transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * velocity);

      if (!footStepsAudioSource.isPlaying)
        footStepsAudioSource.Play();

      yield return null;

    }

    if (footStepsAudioSource.isPlaying)
      footStepsAudioSource.Stop();
    StopCoroutine(lerp);

    if (lastTarget != null)
    {
      lastTarget.gameObject.SetActive(true);
    }

    target.GetComponent<Renderer>().material.color = inactiveColor;
    lastTarget = target;
    lastTarget.gameObject.SetActive(false);

  }

  private IEnumerator ClickTarget()
  {

    timer = 0;

    while (timer < activateTarget)
    {
      timer += Time.deltaTime;

      yield return null;

      if (gazedAt)
        continue;

      timer = 0;

      yield break;
    }

    OnTeleportClick(target);

  }

  #endregion 
}
