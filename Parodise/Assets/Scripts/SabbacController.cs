using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(DCardStackView))]
public class SabbacController : MonoBehaviour
{
	public DCardStack player, p2, deck;
	public Button hitButton, stickButton;
	public Text Pscore1, Pscore2, endgame;
	int temp, temp1; int fontsize=50;
	#region Public Methods
	public void Hit()
	{
		player.DPush(deck.DPop());
		if(player.DHandValue()>23)
		{
			hitButton.interactable = false;
		}
	}
	public void Stick()
	{
		hitButton.interactable = false;
		stickButton.interactable = false;
		if (p2.DHandValue() < 20) {StartCoroutine (DealerPlay());}
	}

	IEnumerator DealerPlay()
	{	while (p2.DHandValue () < 20)
		{
			yield return new WaitForSeconds (1f);
			p2.DPush (deck.DPop ());
		}
		if (p2.DHandValue() >= 20) {Winner ();}
	}

	public void Winner()
	{
		if(hitButton.interactable==false && stickButton.interactable==false)
		{
			if (player.DHandValue() > 23)
			{
				endgame.text= "Bust: You Lose";
			}
			else if (p2.DHandValue() > 23)
			{
				endgame.text="You win.";
			}
			else if (player.DHandValue() > p2.DHandValue())
			{
				endgame.text="You win!";
			}
			else if (p2.DHandValue() >= player.DHandValue())
			{
				endgame.text="Computer Wins";
			}
		}
	}
	void OnGUI()
	{
		if (temp != player.DHandValue()) 
		{	
			if (Pscore1.fontSize != Pscore1.fontSize + 40) 
			{
				Pscore1.fontSize++;
			}
			if (temp < player.DHandValue()) 
			{
				temp++;
			} 
			else if(temp>player.DHandValue())
			{
				temp--;
			}
			if (Pscore1.fontSize != fontsize) 
			{
				Pscore1.fontSize--;
			}
		}	
		if (temp == player.DHandValue()) {Pscore1.fontSize = 50;}
		Pscore1.text = temp.ToString ();
		if (temp1 != p2.DHandValue()) 
		{	
			if (Pscore2.fontSize != Pscore2.fontSize + 40) 
			{
				Pscore2.fontSize++;
			}
			if (temp1 < p2.DHandValue()) 
			{
				temp1++;
			} 
			else if(temp1>p2.DHandValue())
			{
				temp1--;
			}
			if (Pscore2.fontSize != fontsize) 
			{
				Pscore2.fontSize--;
			}
		}	
		if (temp1 == p2.DHandValue()) {Pscore2.fontSize = 50;}
		Pscore2.text = temp1.ToString ();
		if(player.DHandValue()>23)
		{
			Stick();
		}
	}
	#endregion
	#region Unity messages
	void Start()
	{
		SStartGame();
	}
	#endregion
	void SStartGame()
	{
		for(int i=0; i<2;i++)
		{   	
			player.DPush(deck.DPop());
			p2.DPush(deck.DPop ());
		}
	}


}
