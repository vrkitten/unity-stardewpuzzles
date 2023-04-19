using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    [SerializeField] private GameController gameController;
    [SerializeField] private string functionOnClick;

    public void OnRestartButtonClicked()
    {
        gameController.RestartGame();
    }


}
