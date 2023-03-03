using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
 using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    public delegate void DebugDelegate(string newText);

    // 2
    public DebugDelegate debug = Print;

    private string _state;

    public string labelText = "Collect all 4 items and win your freedom!";

    public Stack<string> lootStack = new Stack<string>();

    public int maxItems = 4;

    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    private int _itemsCollected = 0;

    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new
           InventoryList<string>();
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();

        var nextItem = lootStack.Peek();

        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem, nextItem);

        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
    }

    public void Initialize()
    {
        debug(_state);
        _state = "Manager initialized..";
        _state.FancyDebug();
        Debug.Log(_state);
        LogWithDelegate(debug);
        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        // 3
        del("Delegating the debug task...");
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;

            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";

                // 2
                showWinScreen = true;

                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems -
                   _itemsCollected) + " more to go!";
            }
        }
    }
    private int _playerHP = 3;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);
            if (_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... that's got hurt.";
            }
        }

    }

    // 3
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    void OnGUI()
    {
        {
            
            GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" +
                _playerHP);

            
            GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " +
               _itemsCollected);

           
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height -
               50, 300, 50), labelText);
        }
        

        
        if (showWinScreen)
        {
            
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
               Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel(0);
            }
        }
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
              Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                Utilities.RestartLevel();
            }
        }
    }
}

