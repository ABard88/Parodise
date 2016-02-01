using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(CardPress))]
[RequireComponent(typeof(DCardStackView))]
public class CleanGwent : MonoBehaviour
{
//----------------------Variables----------------
	public DCardStack player, playerfd, playercd;// player decks for selection
	public DCardStack p2, temp2, p2view;// temp2 for display which card is played.
	public DCardStack deck, deck2;// to deal out cards
	public Text Pscore1, Pscore2, endgame;//score display and messages
	public Button pass,D1,D2, D3, back, again;
	public static int num;
	float s1t=0;	float s2t=0;	float S1=0;	float S2=0;  float tempP1Score;float tempP2Score;//score management
	int fontsize=50;
	bool playerPass=false;	bool compPass=false; //passing mechanism
	public Image p2life1, p2life2, p1life1,p1life2; // skulls for round score indicator
	int R1=0;	int R2=0; // round scores
	bool select=true; bool yourTurn=true; bool compTurn=false;// to enable deck selection at the very start and avoid starting game before this
	int deckind;// to play according to the deck selected.
	bool Drac=false; bool Phatom=false; bool Buff=true; bool T1=false; bool T2=false; bool T3=false; bool T4=false;// activating special powers
	public GameObject fscreen; // background images
	int last=100; //bool swap=true;
//--------Varaiables for animating card movement----------

	public Transform startMarker;public Transform endMarker;
	public float speed=0.005f; public bool move=false;


//-------------End of Variables-----------------
	#region Public Methods

	public void Pass() // Player passes
	{
		playerPass = true;
		endgame.text="You passed.";
		compTurn = true;
		Debug.Log ("Passed");
		StartCoroutine (CompMove ());
	}
	public void DeckSelectFant()//selects fantasy deck for player
	{
		player = playerfd;
		GStartGame ();
		select = false;
		D1.transform.localPosition=new Vector3(-120,1500,0);D2.transform.localPosition=new Vector3(-120,-4000,0);
		D3.transform.localPosition=new Vector3(-120,1500,0);		
		fscreen.transform.localPosition = new Vector3 (0, 0, 0);
		deckind = 1;
	}
	public void DeckSelectPol()//selects pol deck for player
	{
		GStartGame ();
		select = false;
		D1.transform.localPosition=new Vector3(-120,1500,0);D2.transform.localPosition=new Vector3(-120,-4000,0);
		D3.transform.localPosition=new Vector3(-120,1500,0);
		fscreen.transform.localPosition = new Vector3 (0, 0, 0);
		deckind = 2;
	}
	public void DeckSelectCart()// selects cartoon deck for player
	{
		player = playercd;
		GStartGame ();
		select = false;
		D1.transform.localPosition=new Vector3(-120,1500,0);D2.transform.localPosition=new Vector3(-120,-4000,0);
		D3.transform.localPosition=new Vector3(-120,1500,0);
		fscreen.transform.localPosition = new Vector3 (0, 0, 0);
		deckind = 3;
	}


	public void RoundEval() // when both players pass checks who won the round and resets variables
	{
		if (S1 > S2)
		{
			R1 = R1 + 1;R2 = R2;
			endgame.text = "You win this round";System.Threading.Thread.Sleep (1000);
			S1 = 0;S2 = 0;s1t = 0;s2t = 0;playerPass = false; compPass = false;
			tempP1Score=0;tempP2Score=0; endgame.text="";
			T1=false;T2=false;T3=false;T4=false;
		}
		else if (S1 < S2) 
		{
			R1 = R1;R2 = R2 + 1;
			endgame.text = "Comp win this round";
			System.Threading.Thread.Sleep (1000);
			S1 = 0;S2 = 0;s1t = 0;s2t = 0;playerPass = false; compPass = false;
			tempP1Score=0;tempP2Score=0;endgame.text="";
			T1=false;T2=false;T3=false;T4=false;
		}
		else if (S1 == S2) 
		{
			R1 = R1 + 1;R2 = R2 + 1;
			endgame.text = "Both Lose.";System.Threading.Thread.Sleep (1000);	
			S1 = 0;S2 = 0;s1t = 0;s2t = 0;playerPass = false; compPass = false;
			tempP1Score=0;tempP2Score=0; endgame.text="";
			T1=false;T2=false;T3=false;T4=false;
		}
	}

	public void RoundScore() // adds graphics to round score on the screen (skull icons)
	{
		if (R1 == 1) {p2life1.transform.localPosition = new Vector3 (-600, 60, 0);}
		else if (R1 == 2) {p2life2.transform.localPosition = new Vector3 (-575, 60, 0);	}
		else if (R2 == 1) {p1life1.transform.localPosition = new Vector3 (-600, -60, -5);}
		else if (R2 == 2) {p1life2.transform.localPosition = new Vector3 (-575, -60, -5);}
		System.Threading.Thread.Sleep (500);		
		endgame.text = "";
		if (R1==2) 
		{	endgame.text = "You Win the Match."; yourTurn=false; compTurn=false;
			back.transform.localPosition=new Vector3(-50,-20,0);again.transform.localPosition=new Vector3(50,-20,0);
			return;
		}
		else if (R2==2) 
		{
			endgame.text = "Computer Wins the Match.";yourTurn=false; compTurn=false;
			back.transform.localPosition=new Vector3(-50,-20,0);again.transform.localPosition=new Vector3(50,-20,0);
			return;}
	}
	public void PlayerMove() // Player's Move
	{	/*if(swap==false){*/num = CardPress.key;//}Takes input as to which card player selected
		if(playerPass!=true && num!= last)// to avoid constant repition of previous values since func is called every frame
		{	
			if(num!=999){last= num;}// exception so that the 1st move is not defaulted
			if(num!=999){tempP1Score = (num % 10);}
			Debug.Log ("My chance."+ tempP1Score);
			S1 = S1 + tempP1Score;// score update
			if(Buff==true){S1 = S1 + tempP1Score; Buff=false;}
			int ind = player.cards.FindIndex (x => x == num);//find card index in hand
			player.DRemove (ind);//remove card from hand
			if(deckind==2)// pol card power effects
			{
				if (num % 10 == 5) {player.DRemove(0); S2=S2-tempP2Score;tempP2Score=0;endgame.text="Your drone takes down enemy card. \n Unfortunately one of yours too in friendly fire.";}// Drone card effect
				else if (num % 10 == 7) {S2=S2-tempP2Score; S1=S1+tempP2Score;tempP2Score=0;endgame.text="Fundamentalists convert the opponents \n last card to your cause";}// Fundamentalism convert effect
				else if (num % 10 == 4) {S2=S2-(tempP2Score/2); S1=S1+(tempP2Score/2);tempP2Score=0;endgame.text="Corrupt leaders bribe opponent's last \n card you get half their points";}// Corrupt card effect
				else if (num==29) {player.DPush(deck.DPop());player.DPush(deck.DPop());endgame.text="Demographic boom. You get 2 extra cards";}// Xi gets 2 more grunts
				else if (num==28) {p2.DRemove(0);tempP2Score=0;endgame.text="Putin orders for assassination of opponent's cards";}//Vlad orders a hit.
			}
			if(deckind==1)//fantasy card power effects
			{
				if (num==14) {S2=S2-tempP2Score;tempP2Score=0; endgame.text="Medusa turns opponent to stone, you \n can take another turn";compTurn=false;return;}// Medusa comp doesn't score and missses turn
				else if (num==10) {S2=S2-tempP2Score; S1=S1+tempP2Score;tempP2Score=0;endgame.text="Imhotep converts the opponents \n last card to your cause";}// Fundamentalism convert effect Imhotep
				else if (num==28) {if(Drac==false){player.DPush(28); Drac=true; endgame.text="Regenerative card, you \n can play him twice";}}// Dracula regenerates once
				else if (num==25) {player.DPush(22);player.DPush(23);endgame.text="Gandalf brings cavalry to your rescue.(Imlerith & Horseman drawn)";}//Gandalf gets the cavalry
				else if (num==18) {p2.DRemove(0);tempP2Score=0;endgame.text="Lord Voldy kills one of your opponent's cards";}//Voldy assassinates
				else if (num==17) {tempP2Score=0; Buff=true; endgame.text="Yennefer decides to help. \n Select a card to buff.";compTurn=false;return;}
				else if (num==5 || num==15) {S1=S1-5;S2=S2+5; player.DPush(deck.DPop());endgame.text="Your spy lets you draw a card";}// Spies
			}
			if(deckind==3)// Cartoon card power effects
			{
				if (num==9) {if(Drac==false){player.DPush(9); Drac=true; endgame.text="Regenerative card, you \n can play him twice";}}// Dracula regenerates once
				else if (num==26) {if(Phatom==false){player.DPush(26); Phatom=true; endgame.text="Regenerative card, you \n can play him twice";}}// Dracula regenerates once
				else if (num==8) {S1=S1-8;S2=S2+8; player.DPush(deck.DPop());endgame.text="Your spy lets you draw a card";}// Spies
				else if (num==17) {player.DPush(3);player.DPush(13);endgame.text="Cobra commander calls his minions to your hand.";}// Cobra commander calls his minions
				else if (num==14) {p2.DRemove(0);tempP2Score=0; endgame.text="Kira assassinates an opponent's card";}//Kira orders a hit.
				else if (num==0 || num==10) {tempP2Score=0; Buff=true; endgame.text="Tsurara Decides to help \n Buff: Select a card to double";compTurn=false;return;}
				else if (num==21){T1=true;if(T1==true && T2==true){S1=S1+3; endgame.text="SwatKats team up for double effect.\n(2x points for both cards)"; T1=false;T2=false;}}
				else if (num==22){T2=true;if(T1==true && T2==true){S1=S1+3; endgame.text="SwatKats team up for double effect.\n(2x points for both cards)"; T1=false;T2=false;}}
				else if (num==5){T3=true;if(T3==true && T4==true){S1=S1+11; endgame.text="NinjaTurtles team up for double effect.\n(2x points for both cards)"; T3=false;T4=false;}}
				else if (num==6){T4=true;if(T3==true && T4==true){S1=S1+11; endgame.text="NinjaTurtles team up for double effect.\n(2x points for both cards)"; T3=false;T4=false;}}
			}
			compTurn=true;yourTurn=false; return;
		}
	}
	IEnumerator CompMove ()//Political Deck Moves for Computer.
	{	yield return new WaitForSeconds (2f);// delay
		if(compPass!=true && compTurn==true)
		{	
			if (S2 > S1 && playerPass == true) {compPass = true; endgame.text="Computer Passes";yield break;} // passing conditions
			else if(S2<S1+4 && playerPass == true){}
			else if (S2 < S1 + 12 && playerPass == true && R1 != 1) {compPass = true;endgame.text="Computer Passes";yield break;} 
			else if (S2 > S1 + 10 && p2.DCardCount < 9 && p2.DCardCount > 6) {compPass = true;endgame.text="Computer Passes";yield break;} 
			else if (p2.DCardCount == 0) {compPass = true;endgame.text="Computer Passes";yield break;}
			else if (p2.DCardCount <5 && R1==0 && R2==0) {compPass = true;endgame.text="Computer Passes";yield break;}
			else if (p2.DCardCount <3 && R1==0 && R2==1) {compPass = true;endgame.text="Computer Passes";yield break;} // end of passing conditions
			int maxval = p2.cards.Max ();// select card
			tempP2Score=(maxval%10);// extract score
			S2 = S2 + tempP2Score;// assign score
			if (maxval % 10 == 5) {p2.DRemove(0); S1=S1-tempP1Score;tempP1Score=0;endgame.text="Drone takes out your card. \n Opponent loses a card in friendly fire";}// Drone card effect
			else if (maxval % 10 == 7) {S2=S2+tempP1Score; S1=S1-tempP1Score;tempP1Score=0;Debug.Log("S1"+S1);endgame.text="Your last card has been fundamentalized \nand converted to the other side";}// Fundamentalism convert effect
			else if (maxval % 10 == 4) {S2=S2+(tempP1Score/2); S1=S1-(tempP1Score/2);tempP1Score=0;endgame.text="Corrupt leader bribes your forces. \n Last card gives half its points to the opponent.";}// Corrupt card effect
			else if (maxval==29) {p2.DPush(deck.DPop());p2.DPush(deck.DPop());endgame.text="Demographic Boom. Opponent draws 2 extra cards.";}// Xi gets 2 more grunts
			else if (maxval==28) {player.DRemove(0);endgame.text="Vlad orders a hit. You lose a card.";tempP1Score=0;}//Vlad orders a hit.
			else{endgame.text="";}
			int index = p2.cards.FindIndex (x => x == maxval);// find p2 cards index 
			p2view.DPush(maxval);// show card on screen
			p2.DRemove(index);// Delete card from hand
			yourTurn=true;
			if(playerPass==true){compTurn=true; move=true;}
			else{compTurn=false;}
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

	/*public void Swap()
	{
		int change = CardPress.key;
		int ind = player.cards.FindIndex (x => x == change);//find card index in hand
		player.DRemove (ind);
		player.DPush (deck.DPop ());
		CardPress.key = 1000;
		swap = false;

	}*/

	void Update()
	{
		//if(swap==true){Swap ();}
		Play ();
	}
	void OnGUI() // Running visual updates to screen(score) and checking for the winner.
	{
		if (select == false) 
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
	}
	#endregion
	#region Unity messages
	void Awake() // giving the player options to select the deck
	{
		if (select == true) 
		{
			D1.transform.localPosition=new Vector3(-350,0,0);
			D2.transform.localPosition=new Vector3(-150,0,0);
			D3.transform.localPosition=new Vector3(100,0,0);
		}
	}	
	#endregion
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