using UnityEngine;
using System.Collections;

public class TeleportPlayer : MonoBehaviour
{

  [SerializeField] private float velocity;
  [SerializeField] private Color inactiveColor;
  [SerializeField] private Color gazedAtColor;
  [SerializeField] private float outlineWidth;
  [SerializeField] private float outlineWidthInactive;
  [SerializeField] private float lerpTime;

  private new Transform transform;
  private Transform target;
  private bool gazedAt;
  private Color lerpColor = Color.white;
  private float lerpOutline;
  private float timer;

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

  public void PointerExit(GameObject teleport)
  {

    gazedAt = false;
    teleport.GetComponent<Renderer>().material.color = gazedAt ? gazedAtColor : inactiveColor;    

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

    timer = 0;
    while (timer < velocity)
    {

      timer += Time.deltaTime;

      transform.position = Vector3.Lerp(transform.position, target.position, timer / velocity);

      yield return null;
        
    }

    StopCoroutine(lerp);

  }
}
