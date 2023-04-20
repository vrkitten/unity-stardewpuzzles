using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuessesDisplay : MonoBehaviour
{

    [SerializeField] private MatchingGameController gameController;
    public Text text;
    private int countGuesses;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Guesses: " + gameController.countGuesses.ToString();
    }

}
