using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DCardStack : MonoBehaviour
{
	public List<int> cards; // index of cards from prefab
	public bool gameDeck;
	
	public bool HasCards
	{
		get { return cards != null && cards.Count > 0; }
	}
	
	public event CardEventHandler CardRemoved; // for removing cards from game object (Comp)
	public event CardEventHandler CardAdded;
	public int DCardCount
	{
		get
		{
			if(cards==null)
			{
				return 0;
			}
			else
			{
				return cards.Count;
			}
		}
	}
	
	internal IEnumerable<int> DPush()
	{
		throw new NotImplementedException();
	}
	
	public IEnumerable<int> DGetCards()   //Public access to cards through this.
	{
		foreach(int i in cards)
		{
			yield return i;
		}
	}
	public int DPop()
	{
		int temp = cards[0];
		if (CardRemoved != null) // checks condition and removed cards from game object
		{
			CardRemoved(this, new CardEventArgs(temp));
		}
		cards.RemoveAt(0); // this removes cards from original deck while dealing if CreateDeck() removed will not give a chance to redraw the same card
		//DCreateDeck(); // Recreates the deck and shuffles it.
		return temp;
	}

	public void DRemove(int index)
	{
		int temp = cards[index];
		if (CardRemoved != null) // checks condition and removed cards from game object
		{
			CardRemoved(this, new CardEventArgs(temp));
		}
		cards.RemoveAt(index); // this removes cards from original deck while dealing if CreateDeck() removed will not give a chance to redraw the same card
		//DCreateDeck(); // Recreates the deck and shuffles it.

	}

	public void DPush(int card)
	{
		cards.Add(card);
		if(CardAdded!=null)
		{
			CardAdded(this, new CardEventArgs(card));
		}
	}
	
	public int DHandValue()
	{
		int P1score=0;
		foreach(int card in DGetCards())
		{
			int cardRank = card % 10;
			P1score = P1score + cardRank;
		}
		
		return P1score;
		
	}
	
	public void DCreateDeck()
	{
		cards.Clear();
		for (int i =0; i<30;i++)
		{
			cards.Add(i);
		}
		int n = cards.Count;
		while(n>1)
		{
			n--;
			int k = UnityEngine.Random.Range(0, n + 1);
			int temp = cards[k];
			cards[k] = cards[n];
			cards[n] = temp;
		}
	}
	void Awake()
	{
		cards = new List<int>();
		if(gameDeck)
		{
			DCreateDeck();
		}
	}
	
	public static implicit operator GameObject(DCardStack v)
	{
		throw new NotImplementedException();
	}
}
