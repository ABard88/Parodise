using UnityEngine;
using System.Collections;

public class Quip : MonoBehaviour 
{
	public GameObject quip;

	void OnMouseOver()
	{
		quip.transform.localPosition = new Vector3 (0, -3, 0);
	}
	void OnMouseExit()
	{
		quip.transform.localPosition = new Vector3 (1000, 0, 0);	
	}
}
