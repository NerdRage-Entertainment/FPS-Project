using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    public int baseHealth = 100;
   
    string characterName;

    //Animation characterAnim;

    //public bool isFalling;
    //public bool isAlive;
    //public bool isRunning;

    public string CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }

    public int CharacterHealth
    {
        get { return baseHealth; }
        set { baseHealth = value; }
    }

};
