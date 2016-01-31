using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(CardPress))]
[RequireComponent(typeof(DCardStackView))]
public class SabbacIIController : MonoBehaviour
{
	public DCardStack player, p2, temp, deck;
	public Button stickButton;
	public Text Pscore1, Pscore2, endgame;
	public static int num;
	bool turn; bool show;
	CardPress pressedCard;
	DCardStackView view;
	#region Public Methods
	
	public void DSticks()
	{
		stickButton.interactable = false;
	}
	
	void finalcards()
	{
		for(int i=0;i<p2.DCardCount;i++)
		{
			temp.DPush(p2.DPop());
		}		
		winner ();
	}
	void winner()
	{
		if ((player.DHandValue () >= temp.DHandValue ())&& player.DHandValue()<23) {endgame.text="You Win.";}
		else {endgame.text="Computer Wins.";}
	}
	void Play()
	{
		if (stickButton.interactable == true) 
		{
			num = CardPress.key;
			if (num != 0) {			
				int ind = player.cards.FindIndex (x => x == num);
				player.DRemove (ind);
				player.DPush(deck.DPop());
				int maxval = p2.cards.Max ();
				int index = p2.cards.FindIndex (x => x == maxval);
				p2.DRemove (index);
				p2.DPush(deck.DPop());
				int temp1 = player.DHandValue ();
				Pscore1.text = temp1.ToString ();
				var t2 = Pscore1.GetComponentInParent<ParticleSystem> ();
				t2.Play ();
			}
		}
		else{return;}
	}
	void Update()
	{	
		Play ();
		if(stickButton.interactable==false){finalcards ();}
	}
	
	void OnGUI()
	{
		/*GUIStyle Style = new GUIStyle();
		Style.normal.textColor = Color.red;
		Style.fontSize = 20;
		GUI.Label(new Rect(5, 530, 50, 28), player.DHandValue().ToString(),Style);*/ //use this to make on gui font styles
		if (stickButton.interactable == false)
		{	
			int temp2=temp.DHandValue();
			Pscore2.text=temp2.ToString();
		}
	}
	
	#endregion
	#region Unity messages
	void Start()
	{
		DStartGame();
	}
	#endregion
	void DStartGame()
	{
		for(int i=0; i<5;i++)
		{   			
			player.DPush(deck.DPop());
			p2.DPush(deck.DPop());
			
		}
	}
} 