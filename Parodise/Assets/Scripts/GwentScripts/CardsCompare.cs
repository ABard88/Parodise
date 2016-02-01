using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(CardModel))]
public class CardsCompare : MonoBehaviour
{
	public DCardStack player, playerfd, playercd, playersd;
	public Button prev1,prev2, next1, next2, replay, back, D1, D2 , D3, D4;
	bool select = true;
	int deckind; int i=0; int j=0;
	public GameObject cardprefab;
	CardModel model;

	public void DeckSelectFant()//selects fantasy deck for player
	{
		select = false;
		deckind = 1;
		StartShow ();
	}
	public void DeckSelectPol()//selects pol deck for player
	{
		player = playerfd;
		select = false;
		deckind = 2;
		StartShow ();
	}
	public void DeckSelectCart()//selects fantasy deck for player
	{
		select = false;
		deckind = 3;
		StartShowR ();
	}
	public void DeckSelectSci()//selects pol deck for player
	{
		playercd = playersd;
		Vector3 pos = playercd.transform.localPosition;
		select = false;
		deckind = 4;
		StartShowR ();
	}
	public void NextL()
	{
		player.DCreateDeck ();
		i++;
		StartShow ();
	}
	public void PrevL()
	{
		player.DCreateDeck ();
		i=i-1;// this works i goes down but things not pushed into the player
		StartShow ();
	}
	public void NextR()
	{
		playercd.DCreateDeck ();
		j++;
		StartShowR ();
	}
	public void PrevR()
	{
		playercd.DCreateDeck ();
		j=j-1;
		StartShowR ();
	}
	public void StartShow()
	{
		player.DPush (i);
	}
	
	public void StartShowR()
	{
		playercd.DPush (j);
	}

}