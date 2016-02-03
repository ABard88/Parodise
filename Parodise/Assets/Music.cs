using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour 
{

	void Start()
	{
	}
	void Update()
	{
	}

	public static Music Instance;
	void Awake()
	{
		if (Instance) {
			DestroyImmediate (gameObject);
		} 
		else // continue playing music from where it left off
		{
			DontDestroyOnLoad (gameObject);
			Instance=this;
		}
	}
	void OnLevelWasLoaded(int level) // in these levels there is different music so stop playing old one
	{
		if(level==2 || level==3 || level==4)
		{
			DestroyImmediate (gameObject);
		}
	}

}
