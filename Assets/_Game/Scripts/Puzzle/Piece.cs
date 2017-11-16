using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

  [SerializeField] private float speed;
  [SerializeField] private PuzzleManager pz;

  public int[] values;

  float realRotation;
  void Start()
  {

    pz = GameObject.FindGameObjectWithTag("PuzzleController").GetComponent<PuzzleManager>();
    transform.rotation = Quaternion.Euler(0, 0, realRotation);

  }

  void Update()
  {

    if (transform.root.eulerAngles.z != realRotation)
    {
      transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, realRotation), speed);
    }
  }

  public void OnClick()
  {

    int difference = -pz.QuickSweep((int)transform.position.x, (int)transform.position.y);

    RotatePiece();

    difference += pz.QuickSweep((int)transform.position.x, (int)transform.position.y);

    pz.puzzle.curValue += difference;

    if (pz.puzzle.curValue == pz.puzzle.winValue)
      pz.Win();

  }

  public void RotatePiece()
  {

    realRotation += 90;

    if (realRotation == 360)
      realRotation = 0;

    RotateValues();

  }

  public void RotateValues()
  {

    int aux = values[0];

    for (int i = 0; i < values.Length - 1; i++)
    {
      values[i] = values[i + 1];
    }
    values[3] = aux;

  }

}