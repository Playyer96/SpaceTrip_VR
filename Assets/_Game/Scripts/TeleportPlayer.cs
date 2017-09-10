using UnityEngine;
using System.Collections;

public class TeleportPlayer : MonoBehaviour
{

  [SerializeField] private float distanceGap = .125f;
  [SerializeField] private float velocity;
  [SerializeField] private Color inactiveColor;
  [SerializeField] private Color gazedAtColor;
  [Range(0, .5f)][SerializeField] private float outlineWidth;
  [SerializeField] private float lerpTime;


  private new Transform transform;
  private Transform lastTarget;
  private Transform target;
  private bool gazedAt;
  private Color lerpColor = Color.white;
  private float outlineWidthInactive;
  private float lerpOutline;

  IEnumerator lerp;

  private void Awake()
  {

    transform = GetComponent<Transform>();

  }

  void Update()
  {

    lerpColor = Color.Lerp(gazedAtColor, inactiveColor, Mathf.PingPong(Time.time, 1));
    lerpColor = Color.Lerp(inactiveColor, gazedAtColor, Mathf.PingPong(Time.time, 1));
    lerpOutline = Mathf.Lerp(outlineWidth, outlineWidthInactive, 5 * Time.deltaTime);
    lerpOutline = Mathf.Lerp(outlineWidthInactive, outlineWidth, 5 * Time.deltaTime);

  }

  public void PointerEnter(GameObject teleport)
  {

    gazedAt = true;
    if (inactiveColor != null && gazedAtColor != null)
    {
      teleport.GetComponent<Renderer>().material.color = gazedAt ? gazedAtColor : inactiveColor;

      return;
    }

  }

  public void PointerEnterOutline(GameObject outline)
  {
    gazedAt = true;
    if (inactiveColor != null)
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

    if(lerp != null)
    {
      StopCoroutine(lerp);
    }

    lerp = Lerp();
    StartCoroutine(lerp);

  }

  private IEnumerator Lerp()
  {

    while (transform.position != target.position)
    {

      target.GetComponentInChildren<Renderer>().material.SetFloat("_Outline", outlineWidthInactive);

      transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * velocity);

      yield return null;
        
    }

    StopCoroutine(lerp);
    Debug.Log("Finish");

     if(lastTarget != null)
    {
      lastTarget.gameObject.SetActive(true);
    }

    target.GetComponent<Renderer>().material.color = inactiveColor;
    lastTarget = target;
    lastTarget.gameObject.SetActive(false);

  }
}
