using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using Cinemachine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

  //public static LevelManager Instance { get; private set; }

  #region Variables

  public static int count;

  [SerializeField] private string sceneName;

  [SerializeField] private Canvas missionsCanvas;
  [SerializeField] private Text mission;
  [SerializeField] private Text missionDescription;
  [SerializeField] private GameObject teleportPoints;
  [SerializeField] private Text timerText;
  [SerializeField] private Text tiempoText;
  [Space(20f)]

  [Header("References")]
  [SerializeField]
  private GameObject imageEffect;
  [SerializeField] private TeleportPlayer playerMovement;
  [SerializeField] private GameObject reticle;
  [SerializeField] private GameObject alarm;
  [SerializeField] private GvrAudioSource gameOverSound;
  [SerializeField] private GvrAudioSource winOverSound;
  [SerializeField] private Timer timerCountDown;
  [SerializeField] private startJourneyCinematic cinematic;
  [SerializeField] private Animator camAnimator;
  [SerializeField] private Animator winAnimator;
  [SerializeField] private Animator tutorial;
  [SerializeField] private Camera cam;
  [SerializeField] private GameObject gameOver;
  [SerializeField] private GameObject toBeContinued;

  public bool isGameOver;
  public bool youWin;

  bool wakeup = false;

  #endregion

  void Awake()
  {

    count = 0;

    /* if (Instance == null)
     {
       Instance = null;
       DontDestroyOnLoad(gameObject);
     }
     else if (Instance != this)
       Destroy(gameObject);*/

    VRDevice.DisableAutoVRCameraTracking(cam, true);
    cinematic.StartTimeLine();
    playerMovement.GetComponent<TeleportPlayer>().enabled = false;

  }

  void Start()
  {

    missionsCanvas.enabled = false;
    timerText.enabled = false;
    tiempoText.enabled = false;
    teleportPoints.SetActive(false);

  }

  void Update()
  {

    if (cinematic.finishCinematic == true)
    {
      endCinematic();
    }

    if (!wakeup && cinematic.finishCinematic == true)
    {
      Invoke("WakeUp", .1f);
      wakeup = true;
    }

    if (isGameOver == true)
    {
      GameOver();
      isGameOver = false;
    }

    else if (youWin == true)
    {
      ToBeContinued();
    }
  }

  #region Methods

  void WakeUp()
  {

    tutorial.GetComponent<AnimatedDialog>().TipyingAnimation();
    mission.text = "Desactiva la alarma";
    missionDescription.text = "Desactiva la alarma en el tiempo determinado, para encontrarla sigue el sonido";
    cam.GetComponent<CinemachineBrain>().enabled = false;
     playerMovement.enabled = true;
    VRDevice.DisableAutoVRCameraTracking(cam, false);
    teleportPoints.SetActive(true);
    missionsCanvas.enabled = true;
    camAnimator.SetBool("MissionText", true);
    tutorial.SetBool("playAnimTuto", true);

  }

  void endCinematic()
  {

    timerText.enabled = true;
    tiempoText.enabled = true;
    timerCountDown.CountDownTimer();

  }

  void loadSceneMenu()
  {

    SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

  }

  void GameOver()
  {

    gameOverSound.Play();
    gameOver.GetComponent<AnimatedDialog>().TipyingAnimation();
    camAnimator.SetBool("isGameOver", isGameOver);
    playerMovement.enabled = false;
    reticle.SetActive(false);
    alarm.SetActive(false);
    StartCoroutine(LoadAsynchronously(sceneName));
  }

  void ToBeContinued()
  {

    youWin = false;
    winOverSound.Play();
    timerCountDown.StopCountDown();
    toBeContinued.GetComponent<AnimatedDialog>().TipyingAnimation();
    winAnimator.SetBool("ToBeContinued", true);
    playerMovement.enabled = false;
    reticle.SetActive(false);
    StartCoroutine(LoadAsynchronously(sceneName));

  }

  #endregion

  IEnumerator LoadAsynchronously(string sceneName)
  {

    yield return new WaitForSeconds(10f);
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

    while (!operation.isDone)
    {
      float progress = Mathf.Clamp01(operation.progress / .9f);

      Debug.Log(progress);

      yield return null;
    }

  }

}
