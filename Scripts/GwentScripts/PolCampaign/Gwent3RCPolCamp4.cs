using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(Polcamplevel))]
[RequireComponent(typeof(CardPress))]
[RequireComponent(typeof(DCardStackView))]
public class Gwent3RCPolCamp4 : MonoBehaviour
{//-----------------Variables-----------------
	public GameObject p2life1, p2life2, p1life1, p1life2;// skull round indicators
	public DCardStack player, temp1, p2, p2l1, p2l2, p2l3, deck, p2view;
	public Text Pscore1, Pscore2, endgame;
	public Button Pass, back, again;
	public static int num, lev;
	float s1t=0;	float s2t=0;	float S1=0;	float S2=0; float tempP1Score, tempP2Score;
	int fontsize=50;
	int R1=0;	int R2=0; int last =100;
	bool pass1=false;	bool pass2=false; bool playerPass=false;bool compPass=false; bool yourTurn=true; bool compTurn=false;
	bool Buff=false; bool Drac=false; bool communismp1= false; bool communismp2=false; bool Buff2=false;
	CardPress pressedCard;
	Polcamplevel level;
//-----------------End of Variables------------
	#region Public Methods
	public void Pass1()
	{
		playerPass = true;
		endgame.text="You passed.";
		compTurn = true;
		Debug.Log ("Passed");
		StartCoroutine (CompMove ());
	}

	public void RoundEval() // when both players pass checks who won the round and resets variables
	{
		if (S1 > S2)
		{
			R1 = R1 + 1;R2 = R2;
			endgame.text = "You win this round";System.Threading.Thread.Sleep (1000);
			S1 = 0;S2 = 0;s1t = 0;s2t = 0;playerPass = false; compPass = false;
			tempP1Score=0;tempP2Score=0; endgame.text="";
		}
		else if (S1 < S2) 
		{
			R1 = R1;R2 = R2 + 1;
			endgame.text = "Comp win this round";
			System.Threading.Thread.Sleep (1000);
			S1 = 0;S2 = 0;s1t = 0;s2t = 0;playerPass = false; compPass = false;
			tempP1Score=0;tempP2Score=0;endgame.text="";
		}
		else if (S1 == S2) 
		{
			R1 = R1 + 1;R2 = R2 + 1;
			endgame.text = "Both Lose.";System.Threading.Thread.Sleep (1000);	
			S1 = 0;S2 = 0;s1t = 0;s2t = 0;playerPass = false; compPass = false;
			tempP1Score=0;tempP2Score=0; endgame.text="";
		}
	}
	
	public void RoundScore() // adds graphics to round score on the screen (skull icons)
	{
		if (R1 == 1) {p2life1.transform.localPosition = new Vector3 (-600, 60, 0);}
		else if (R1 == 2) {p2life2.transform.localPosition = new Vector3 (-575, 60, 0);	}
		else if (R2 == 1) {p1life1.transform.localPosition = new Vector3 (-600, -60, 0);}
		else if (R2 == 2) {p1life2.transform.localPosition = new Vector3 (-575, -60, 0);}
		System.Threading.Thread.Sleep (500);		
		endgame.text = "";
		if (R1==2) 
		{	endgame.text = "You Win the Match."; yourTurn=false; compTurn=false;
			back.transform.localPosition=new Vector3(-50,35,0);again.transform.localPosition=new Vector3(50,-35,0);
			return;
		}
		else if (R2==2) 
		{
			endgame.text = "Computer Wins the Match.";yourTurn=false; compTurn=false;
			back.transform.localPosition=new Vector3(-50,-35,0);again.transform.localPosition=new Vector3(50,-35,0);
			return;}
	}
	public void PlayerMove() // Player's Move
	{	num = CardPress.key;//Takes input as to which card player selected
		if(playerPass!=true && num!= last)// to avoid constant repition of previous values since func is called every frame
		{	
			if(num!=999){last= num;}// exception so that the 1st move is not defaulted
			if(num!=999){tempP1Score = (num % 10);}
			if(communismp1==true){S1=S1-tempP1Score+4; tempP1Score=4;}
			if(Buff==true){S1 = S1 + tempP1Score*2; Buff=false;}
			Debug.Log ("My chance."+ tempP1Score);
			S1 = S1 + tempP1Score;// score update
			int ind = player.cards.FindIndex (x => x == num);//find card index in hand
			player.DRemove (ind);//remove card from hand
			if(num==28){communismp1=true; endgame.text="You've adopted communism till end of round";}// Stalin
			else if (num==9 || num==25 || num==43) {player.DPush(deck.DPop());player.DPush(deck.DPop());endgame.text="Demographic boom: You get 2 extra cards.";}// Demo Boom Extra Cards
			else if (num==5 ||num==16 ||num==19||num==30) {player.DRemove(0); S2=S2-tempP2Score;tempP2Score=0;endgame.text="Your drone takes out opponent's card. \n You lose a card in friendly fire.";}// Drone card effect
			else if (num==7 || num==12 || num==17) {S2=S2-tempP2Score; S1=S1+tempP2Score;tempP2Score=0;endgame.text="You've converted the opponent's card to your side";}// Fundamentalism convert effect
			else if (num==3 || num==11|| num==20) {S2=S2-(tempP2Score/2); S1=S1+(tempP2Score/2);tempP2Score=0;endgame.text="Corruption: You've managed to bribe \n the opponent's card for half it's points.";}// Corrupt card effect
			else if (num==8) {p2.DRemove(0);tempP2Score=0;endgame.text="Vlad decrees a hit. You eliminate one of your opponent's cards.";}//Vlad orders a hit.
			else if (num==45) {p2.DRemove(0);tempP2Score=0;endgame.text="The plague destroys one of the opponent's cards.";}//Ebola casualty.
			else if (num==46) {player.DPush(40);endgame.text="You now have Rascism.";}// Demo Boom Extra Cards
			else if (num==6 || num==15) {tempP2Score=0; Buff=true; endgame.text="Bonus funds. \n Select a card to buff.";compTurn=false;return;}// buffing cards through IMF/Germany
			else if (num==31) {endgame.text="Cable company wastes' opponent's time. \n Take another turn";compTurn=false;return;}// Time warner makes opponent missses turn
			compTurn=true;yourTurn=false; return;
		}
	}
	IEnumerator CompMove ()//Political Deck Moves for Computer.
	{   yield return new WaitForSeconds (2);
		if(compPass!=true && compTurn==true)
		{	
			if (S2 > S1 && playerPass == true) {compPass = true; endgame.text="Computer Passes";yield break;} // passing conditions
			else if(S2<S1+4 && playerPass == true){}
			else if (S2 < S1 + 10 && playerPass == true && R1 != 1) {compPass = true;endgame.text="Computer Passes";yield break;} 
			else if (S2 > S1 + 10 && p2.DCardCount < 9 && p2.DCardCount > 6) {compPass = true;endgame.text="Computer Passes";yield break;} 
			else if (p2.DCardCount == 0) {compPass = true;endgame.text="Computer Passes";yield break;}
			else if (p2.DCardCount <4 && R1==0 && R2==0) {compPass = true;endgame.text="Computer Passes";yield break;}
			else if (p2.DCardCount <3 && R1==0 && R2==1) {compPass = true;endgame.text="Computer Passes";yield break;} // end of passing conditions
			if(communismp2==true){S2=S2-tempP1Score+4; tempP2Score=4;}
			if(Buff2==true){S2 = S2 + tempP2Score*2; Buff2=false;}
			int maxval = p2.cards.Max ();// select card
			tempP2Score=(maxval%10);// extract score
			S2 = S2 + tempP2Score;// assign score
			if(Buff==true){S2=S2+tempP2Score; Buff=false;}
			if(lev==4)
			{
				if(num==28){communismp2=true; endgame.text="You've adopted communism till end of round";}// Stalin
				else if (num==9 || num==25 || num==43) {player.DPush(deck.DPop());player.DPush(deck.DPop());endgame.text="Demographic boom: You get 2 extra cards.";}// Demo Boom Extra Cards
				else if (num==5 ||num==16 ||num==19||num==30) {player.DRemove(0); S2=S2-tempP2Score;tempP2Score=0;endgame.text="Your drone takes out opponent's card. \n You lose a card in friendly fire.";}// Drone card effect
				else if (num==7 || num==12 || num==17) {S2=S2-tempP2Score; S1=S1+tempP2Score;tempP2Score=0;endgame.text="You've converted the opponent's card to your side";}// Fundamentalism convert effect
				else if (num==3 || num==11|| num==20) {S2=S2-(tempP2Score/2); S1=S1+(tempP2Score/2);tempP2Score=0;endgame.text="Corruption: You've managed to bribe \n the opponent's card for half it's points.";}// Corrupt card effect
				else if (num==8) {p2.DRemove(0);tempP2Score=0;endgame.text="Vlad decrees a hit. You eliminate one of your opponent's cards.";}//Vlad orders a hit.
				else if (num==45) {p2.DRemove(0);tempP2Score=0;endgame.text="The plague destroys one of the opponent's cards.";}//Ebola casualty.
				else if (num==46) {player.DPush(40);endgame.text="You now have Rascism.";}// Demo Boom Extra Cards
				else if (num==6 || num==15) {tempP2Score=0; Buff2=true; endgame.text="Bonus funds. \n Select a card to buff.";compTurn=false;yield break;}// buffing cards through IMF/Germany
				else if (num==31) {endgame.text="Cable company wastes' opponent's time. \n Take another turn";compTurn=false;yield break;}// Time warner makes opponent missses turn
				else{endgame.text="";}
			}
			else if(lev==3)
			{
				if(num==28){communismp2=true; endgame.text="You've adopted communism till end of round";}// Stalin
				else if (num==9 || num==25 || num==43) {player.DPush(deck.DPop());player.DPush(deck.DPop());endgame.text="Demographic boom: You get 2 extra cards.";}// Demo Boom Extra Cards
				else if (num==5 ||num==16 ||num==19||num==30) {player.DRemove(0); S2=S2-tempP2Score;tempP2Score=0;endgame.text="Your drone takes out opponent's card. \n You lose a card in friendly fire.";}// Drone card effect
				else if (num==7 || num==12 || num==17) {S2=S2-tempP2Score; S1=S1+tempP2Score;tempP2Score=0;endgame.text="You've converted the opponent's card to your side";}// Fundamentalism convert effect
				else if (num==3 || num==11|| num==20) {S2=S2-(tempP2Score/2); S1=S1+(tempP2Score/2);tempP2Score=0;endgame.text="Corruption: You've managed to bribe \n the opponent's card for half it's points.";}// Corrupt card effect
				else if (num==8) {p2.DRemove(0);tempP2Score=0;endgame.text="Vlad decrees a hit. You eliminate one of your opponent's cards.";}//Vlad orders a hit.
				else if (num==45) {p2.DRemove(0);tempP2Score=0;endgame.text="The plague destroys one of the opponent's cards.";}//Ebola casualty.
				else if (num==46) {player.DPush(40);endgame.text="You now have Rascism.";}// Demo Boom Extra Cards
				else if (num==6 || num==15) {tempP2Score=0; Buff2=true; endgame.text="Bonus funds. \n Select a card to buff.";compTurn=false;yield break;}// buffing cards through IMF/Germany
				else if (num==31) {endgame.text="Cable company wastes' opponent's time. \n Take another turn";compTurn=false;yield break;}// Time warner makes opponent missses turn
				else{endgame.text="";}
			}
			else if(lev==2)
			{
				if(num==28){communismp2=true; endgame.text="You've adopted communism till end of round";}// Stalin
				else if (num==9 || num==25 || num==43) {player.DPush(deck.DPop());player.DPush(deck.DPop());endgame.text="Demographic boom: You get 2 extra cards.";}// Demo Boom Extra Cards
				else if (num==5 ||num==16 ||num==19||num==30) {player.DRemove(0); S2=S2-tempP2Score;tempP2Score=0;endgame.text="Your drone takes out opponent's card. \n You lose a card in friendly fire.";}// Drone card effect
				else if (num==7 || num==12 || num==17) {S2=S2-tempP2Score; S1=S1+tempP2Score;tempP2Score=0;endgame.text="You've converted the opponent's card to your side";}// Fundamentalism convert effect
				else if (num==3 || num==11|| num==20) {S2=S2-(tempP2Score/2); S1=S1+(tempP2Score/2);tempP2Score=0;endgame.text="Corruption: You've managed to bribe \n the opponent's card for half it's points.";}// Corrupt card effect
				else if (num==8) {p2.DRemove(0);tempP2Score=0;endgame.text="Vlad decrees a hit. You eliminate one of your opponent's cards.";}//Vlad orders a hit.
				else if (num==45) {p2.DRemove(0);tempP2Score=0;endgame.text="The plague destroys one of the opponent's cards.";}//Ebola casualty.
				else if (num==46) {player.DPush(40);endgame.text="You now have Rascism.";}// Demo Boom Extra Cards
				else if (num==6 || num==15) {tempP2Score=0; Buff2=true; endgame.text="Bonus funds. \n Select a card to buff.";compTurn=false;yield break;}// buffing cards through IMF/Germany
				else if (num==31) {endgame.text="Cable company wastes' opponent's time. \n Take another turn";compTurn=false;yield break;}// Time warner makes opponent missses turn
				else{endgame.text="";}
			}
			else if(lev==1)
			{
				if(num==28){communismp2=true; endgame.text="You've adopted communism till end of round";}// Stalin
				else if (num==9 || num==25 || num==43) {player.DPush(deck.DPop());player.DPush(deck.DPop());endgame.text="Demographic boom: You get 2 extra cards.";}// Demo Boom Extra Cards
				else if (num==5 ||num==16 ||num==19||num==30) {player.DRemove(0); S2=S2-tempP2Score;tempP2Score=0;endgame.text="Your drone takes out opponent's card. \n You lose a card in friendly fire.";}// Drone card effect
				else if (num==7 || num==12 || num==17) {S2=S2-tempP2Score; S1=S1+tempP2Score;tempP2Score=0;endgame.text="You've converted the opponent's card to your side";}// Fundamentalism convert effect
				else if (num==3 || num==11|| num==20) {S2=S2-(tempP2Score/2); S1=S1+(tempP2Score/2);tempP2Score=0;endgame.text="Corruption: You've managed to bribe \n the opponent's card for half it's points.";}// Corrupt card effect
				else if (num==8) {p2.DRemove(0);tempP2Score=0;endgame.text="Vlad decrees a hit. You eliminate one of your opponent's cards.";}//Vlad orders a hit.
				else if (num==45) {p2.DRemove(0);tempP2Score=0;endgame.text="The plague destroys one of the opponent's cards.";}//Ebola casualty.
				else if (num==46) {player.DPush(40);endgame.text="You now have Rascism.";}// Demo Boom Extra Cards
				else if (num==6 || num==15) {tempP2Score=0; Buff2=true; endgame.text="Bonus funds. \n Select a card to buff.";compTurn=false;yield break;}// buffing cards through IMF/Germany
				else if (num==31) {endgame.text="Cable company wastes' opponent's time. \n Take another turn";compTurn=false;yield break;}// Time warner makes opponent missses turn
				else{endgame.text="";}
			}
			int index = p2.cards.FindIndex (x => x == maxval);// find p2 cards index 
			p2view.DPush(maxval);// show card on screen
			p2.DRemove(index);// Delete card from hand
			yourTurn=true;compTurn=false;
			yield break;
		}
	}
	
	public void Play()// sequence of gameplay in turns
	{	
		if(compPass==true){endgame.text="Computer passes.";}
		if(	playerPass ==true){yourTurn=false; compTurn=true;}
		if(yourTurn==true){PlayerMove();} // calling player to play
		else if(compTurn==true){StartCoroutine (CompMove());} // calling comp to play
		if (playerPass == true && compPass == true) {RoundEval();RoundScore();}
		else if (playerPass != true && compPass == true) {PlayerMove();}
		else if (playerPass == true && compPass != true) {CompMove();}
	}

	void Update()
	{
		Play ();
	}
	void OnGUI() // Running visual updates to screen(score) and checking for the winner.
	{
			if (s1t != S1) 
			{	
				if (Pscore1.fontSize != Pscore1.fontSize + 40) 
				{
					Pscore1.fontSize++;
				}
				if (s1t < S1) 
				{
					s1t++;
				} 
				else if(s1t>S1)
				{
					s1t--;
				}
				if (Pscore1.fontSize != fontsize) 
				{
					Pscore1.fontSize--;
				}
			}	
			if (s1t == S1) {Pscore1.fontSize = 50;}
			Pscore1.text = s1t.ToString ();
			if (s2t != S2) 
			{	
				if (Pscore2.fontSize != Pscore2.fontSize + 40) 
				{
					Pscore2.fontSize++;
				}
				if (s2t < S2) 
				{
					s2t++;
				} 
				else if(s2t>S2)
				{
					s2t--;
				}
				if (Pscore2.fontSize != fontsize) 
				{
					Pscore2.fontSize--;
				}
			}	
			if (s2t == S2) {Pscore2.fontSize = 50;}
			Pscore2.text = s2t.ToString ();
	}
	#endregion
	void Start()
	{
		GStartGame ();
		lev = Polcamplevel.level;
		if (lev == 1) {p2=p2l1;}
		else if (lev == 2) {p2=p2l2;}
		else if (lev == 3) {p2=p2l3;}
		else {p2=p2;}
	}
	void GStartGame() // dealing out the cards to player and the computer
	{
		for(int i=0; i<10;i++)
		{   			
			player.DPush(deck.DPop());
			p2.DPush(deck.DPop());
			S1=0; S2=0;

		}	

	}

}