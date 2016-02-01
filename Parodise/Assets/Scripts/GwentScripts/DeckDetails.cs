using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class DeckDetails : MonoBehaviour 
{
	public GameObject Fant;
	void OnMouseOver()
	{
		Vector3 mousePosition = Input.mousePosition;
		Debug.Log (mousePosition);
		if(mousePosition.x<500)
		{Fant.transform.localPosition = new Vector3 (4, 0, -1);}
		else if(mousePosition.x>500)
		{Fant.transform.localPosition = new Vector3 (-4, 0, -1);}
	//	Fant.transform.localScale = new Vector3 (2f, 2f, 0f);

	}
	void OnMouseExit()
	{
		Fant.transform.localPosition = new Vector3 (0, 500, 0);
	}
}
