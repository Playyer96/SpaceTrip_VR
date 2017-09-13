using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using Cinemachine;

public class LevelManager : MonoBehaviour {

  public static LevelManager Instance { get; private set; }

  [SerializeField] private Canvas missionsCanvas;
  [SerializeField] private Text mission;
  [SerializeField] private Text missionDescription;
  [SerializeField] private GameObject teleportPoints;

  private startJourneyCinematic cinematic;
  private Animator camAnimator;
  private Animator tutorial;
  private Camera cam;

  public bool isGameOver;
  public bool isDead;

  void Awake()
  {

    if (Instance == null)
    {
      Instance = null;
      DontDestroyOnLoad(gameObject);
    }
    else if (Instance != this)
      Destroy(gameObject);

    cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    camAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
    VRDevice.DisableAutoVRCameraTracking(cam, true);
    cinematic = GameObject.Find("TimeLine").GetComponent<startJourneyCinematic>();
    tutorial = GameObject.Find("Tutorial").GetComponent<Animator>();

  }

  void Start()
  {
    missionsCanvas.enabled = false;
    teleportPoints.SetActive(false);
    cinematic.StartTimeLine();

  }

  void Update()
  {

    WakeUp();

  }

  void WakeUp()
  {

    if (cinematic.finishCinematic == true)
    {
      mission.text = "Desactiva la alarma";
      missionDescription.text = "Encuentra el panel de desactivacion de la alarma, siguiendo el sonido";
      cam.GetComponent<CinemachineBrain>().enabled = false;
      VRDevice.DisableAutoVRCameraTracking(cam, false);
      teleportPoints.SetActive(true);
      missionsCanvas.enabled = true;
      camAnimator.SetBool("MissionText", true);
      tutorial.SetBool("playAnimTuto", true);
      tutorial.GetComponent<AnimatedDialog>().TipyingAnimation();
    }

  }

}
