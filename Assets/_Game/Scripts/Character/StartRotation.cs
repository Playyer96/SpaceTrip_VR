using UnityEngine;
using UnityEngine.VR;

public class StartRotation : MonoBehaviour
{

  [SerializeField] private Vector3 startRot;
  [SerializeField] private float m_RenderScale = 1f;

  void Start()
  {

    VRSettings.renderScale = m_RenderScale;

    Quaternion startQ = Quaternion.Euler(startRot);
    transform.rotation = startQ;

  }

}