using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlider : MonoBehaviour
{

	public TMP_Text valueText;
	public Ball ball;


	Slider slider;
	

	public void Start()
	{
		slider = GetComponent<Slider>();
		//Adds a listener to the main slider and invokes a method when the value changes.
		slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
	}


	// Invoked when the value of the slider changes.
	public void ValueChangeCheck()
	{		
		valueText.text = slider.value.ToString();
		ball.speed = slider.value * 10;

		StartCoroutine(ball.MoveObject());
	}
}
