using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;
using System.IO;
using System;
using TMPro;
using UnityEngine.UI;


[Serializable]
public class PositionData
{
    public float [] x;
    public float [] y;
    public float [] z;
}

public class Ball : MonoBehaviour
{
    public int ballIndex;
    public float speed = 1;
    public TMP_Text valueText;
    public Slider slider;

    bool isMoving = false;
    bool isZero = false;
    IEnumerator coroutine;
    List<Vector3> positions;
    int index;
    float timerForDoubleClick = 0.0f;
    float delay = 0.3f;
    bool isDoubleClick = false;
    Vector3 initialPosition;


    private void Awake()
    {
        initialPosition = transform.position;
    }

    void Start()
    {       
        index = 0;
        DeSerializeJson();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        if (slider.value > 0)
        {
            valueText.text = slider.value.ToString();
            speed = slider.value * 10;
            if (isZero)
            {
                StartCoroutine(MoveObject());
                isZero = false;
            }            
        }
        else
        {
            valueText.text = slider.value.ToString();
            speed = slider.value * 10;
            isZero = true;
        }        
    }

    public void DeSerializeJson()
    {
        positions = new List<Vector3>();
        string textFile = "";
        switch (ballIndex)
        {
            case 1:
                textFile = File.ReadAllText(Application.dataPath + "/ball_path.json");
                break;
            case 2:
                textFile = File.ReadAllText(Application.dataPath + "/ball_path2.json");
                break;
            case 3:
                textFile = File.ReadAllText(Application.dataPath + "/ball_path3.json");
                break;
            case 4:
                textFile = File.ReadAllText(Application.dataPath + "/ball_path4.json");
                break;
            default:
                
                break;
        }
        
        PositionData posCollection = JsonUtility.FromJson<PositionData>(textFile);
        for (int i = 0; i < posCollection.x.Length; i++)
        {
            positions.Add(new Vector3(posCollection.x[i], posCollection.y[i], posCollection.z[i]));
        }
    }

    void OnMouseDown()
    {
        if (!isMoving)
        {
            gameObject.AddComponent<TrailRenderer>();
            GetComponent<TrailRenderer>().time = 1000;
            StartCoroutine(MoveObject());            
        }
        if (isDoubleClick == true && timerForDoubleClick < delay)
        {
            Debug.Log("Double Click");
            transform.position = initialPosition;            
            StopCoroutine(coroutine);
            isMoving = false;
            index = 0;
            Destroy(GetComponent<TrailRenderer>());
        }
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1") && isDoubleClick == false)
        {
            Debug.Log("Mouse clicked once");
            isDoubleClick = true;
        }
    }

    public IEnumerator MoveObject()
    {
        for (int i = index; i < positions.Count; i++)
        {
            index = i;
            if (speed <= 0)
            {
                isMoving = false;
                break;
            }
            else
            {                               
                isMoving = true;
                coroutine = Actions.Moving(gameObject.transform, i, positions, speed);
                yield return coroutine;
            }           
        }
    }

    void Update()
    {
        if (isDoubleClick == true)
        {
            timerForDoubleClick += Time.deltaTime;
        }
        if (timerForDoubleClick >= delay)
        {
            timerForDoubleClick = 0.0f;
            isDoubleClick = false;
        }
    }



}
