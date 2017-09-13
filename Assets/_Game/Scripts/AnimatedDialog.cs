using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimatedDialog : MonoBehaviour {

  [SerializeField] private Text textArea;
  [SerializeField] private string dialog;
  [SerializeField] private float velocity = .1f;

  int stringIndex = 0;
  int characterIndex = 0;

  IEnumerator displayTimer;

  private void Start()
  {
    displayTimer = DisplayTimer();
  }

  IEnumerator DisplayTimer()
  {

     while(1 == 1)
    {
      yield return new WaitForSeconds(velocity);
      if (characterIndex > dialog.Length)
      {
        continue;
      }
      textArea.text = dialog.Substring(0, characterIndex);
      characterIndex++;
    }
  }

  public void TipyingAnimation()
  {

    StartCoroutine(displayTimer);

  }

  public void OnClick()
  {
    StopCoroutine(displayTimer);
    Destroy(gameObject);
    GetComponent<Animator>().SetBool("playAnimTuto", false);
  }

}
