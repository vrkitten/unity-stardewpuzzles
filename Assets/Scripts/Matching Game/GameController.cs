using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;

    public int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    public Text guessCounterDisplay;


    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/townies");
        
    }


    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
    }


    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<UnityEngine.UI.Button>());
            btns[i].image.sprite = bgImage;
        }
    }


    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if(index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }


    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }


    public void PickAPuzzle()
        {
        if(!firstGuess)
        {
            firstGuess = true;

            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            btns[firstGuessIndex].interactable = false;

        } else if (!secondGuess)
        {
            secondGuess = true;

            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            btns[secondGuessIndex].interactable = false;
            btns[firstGuessIndex].interactable = false;

            countGuesses++;


            StartCoroutine(CheckIfThePuzzlesMatch());

        }
        }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(.5f);
        
        if (firstGuessPuzzle == secondGuessPuzzle)
        {

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();
        } else
        {

            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;

            btns[firstGuessIndex].interactable = true;
            btns[secondGuessIndex].interactable = true;
        }


        firstGuess = secondGuess = false;

    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished!");
            Debug.Log("It took you " + countGuesses + " guesses to finish the game");

            guessCounterDisplay.text = "Final Guesses: " + countGuesses.ToString();
        }
    }


    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void RestartGame()
    {
        // Reset game state
        firstGuess = secondGuess = false;
        countGuesses = countCorrectGuesses = 0;
        firstGuessIndex = secondGuessIndex = -1;
        firstGuessPuzzle = secondGuessPuzzle = null;
        countGuesses = 0;

        // Shuffle puzzles again
        Shuffle(gamePuzzles);

        // Update UI
        foreach (Button btn in btns)
        {
            btn.image.sprite = bgImage;
            btn.image.color = Color.white;
            btn.interactable = true;
        }
    }






}
