using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeleportPlayer : MonoBehaviour
{

  #region Variables
  [Header("Movement Properties")]
  [SerializeField] private float velocity;
  [SerializeField] private Color inactiveColor;
  [SerializeField] private Color gazedAtColor;
  [Range(0, .05f)] [SerializeField] private float outlineWidth;

  private new Transform transform;
  private Transform lastTarget;
  private Transform target;

  private bool gazedAt;

  private float outlineWidthInactive = 0;

  IEnumerator lerp;

#endregion

  private void Awake()
  {

    transform = GetComponent<Transform>();

  }

 #region Methods
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

    if(lerp != null)
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

  #endregion 
}
