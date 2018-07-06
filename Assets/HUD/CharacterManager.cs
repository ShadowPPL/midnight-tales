using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	public int numberOfUsers;

	//list of players
	public List<PlayerBase> players = new List<PlayerBase> ();

	// list of characters
	public List<CharacterBase> characterList = new List<CharacterBase> ();

	public static CharacterManager instance;

	public static CharacterManager GetInstance()
	{
		return instance;
	}

	//find character by id
	public CharacterBase GetCharByID(string id)
	{
		CharacterBase charFound = null;
		for (int i = 0; i < characterList.Count; i++) {
			if (string.Equals (characterList[i].charId,id)) {
				charFound = characterList [i];
				break;
			}
		}
		return charFound;
	}

	//find player by states
	public PlayerBase GetPlayerByStates(StateManager states)
	{
		PlayerBase playerFound = null;
		for (int i = 0; i < players.Count; i++) {
			if (players[i].playerStates == states) {
				playerFound = players [i];
				break;
			}
		}
		return playerFound;
	}

	// Use this for initialization
	void Awake () {
		instance = this;
		DontDestroyOnLoad (this.gameObject);
	}

}


[System.Serializable]
public class CharacterBase
{
	public string charId;
	public GameObject prefab;
}

[System.Serializable]
public class PlayerBase
{
	public string playerId;
	public string inputId;
	public PlayerType playerType;
	public bool	hasCharacter;
	public GameObject playerPrefab;
	public StateManager playerStates;
	public int score;

	public enum PlayerType
	{
		user, 	// human player
		ai,		// cpu
		simulation // future multiplay on line
	}
}
