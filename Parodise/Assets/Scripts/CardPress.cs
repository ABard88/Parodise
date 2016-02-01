using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(CardModel))]
[RequireComponent(typeof(DCardStackView))]
public class CardPress : MonoBehaviour
{
	DCardStackView post;
	CardModel model;
	public static int key=1000;
	public bool isPressed = false;
	public GameObject cardPrefab;
	private Color startcolor;
	public Vector3 position;
	void OnMouseDown()
	{

		model = GetComponent<CardModel> ();
		key = model.cardIndex;
		isPressed = true;
		Debug.Log ("Key is:" + key);
		cardPrefab.GetComponent<AudioSource>().Play ();
		System.Threading.Thread.Sleep(250);
		//Destroy (cardPrefab);
	}
	void OnMouseOver()
	{
		//startcolor = GetComponent<Renderer>().material.color;
		//GetComponent<Renderer>().material.color = Color.white; // colors the whole card
		position = cardPrefab.transform.localPosition;
		Vector3 temp = position;
		cardPrefab.transform.localScale=new Vector3(0.75f,0.75f,0);
		cardPrefab.transform.localPosition = new Vector3(temp.x, temp.y,-5);

	}
	void OnMouseExit()
	{
		cardPrefab.transform.localScale=new Vector3(0.5f,0.5f,0);
		cardPrefab.transform.localPosition = position;
	}
}
