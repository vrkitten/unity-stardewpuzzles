using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePieces : MonoBehaviour
{
    [SerializeField] private Transform puzzleField;
    public List<Sprite> puzzleSprites;
    public GameObject puzzleTilePrefab;

    // Start is called before the first frame update
    private void Start()
    {
        // Load the sprites from the Resources folder
        puzzleSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/ShuffleGame/PuzzleImages"));

        //shuffle the puzzle sprites
        Shuffle(puzzleSprites);

        // Calculate the tile size based on the canvas size
        float tileSize = GetComponent<RectTransform>().rect.width / Mathf.Sqrt(puzzleSprites.Count);

        int row = 0;
        int column = 0;






        // loop through each row and column of the puzzle grid
        for (int i = 0; i < puzzleSprites.Count; i++)
        {

            // create a new puzzle tile object from the prefab
            GameObject tileObject = Instantiate(puzzleTilePrefab, puzzleField);

            // Set the tile position and size
            RectTransform tileRectTransform = tileObject.GetComponent<RectTransform>();
            tileRectTransform.anchoredPosition = new Vector2(column * tileSize, -row * tileSize);
            tileRectTransform.sizeDelta = new Vector2(tileSize, tileSize);

            // set the tile image sprite
            Image tileImage = tileObject.GetComponent<Image>();
            tileImage.sprite = puzzleSprites[i];

            column++;

            if (column >= Mathf.Sqrt(puzzleSprites.Count))
            {
                row++;
                column = 0;
            }
        }


        // leave the last tile empty
        GameObject emptyTile = Instantiate(puzzleTilePrefab, puzzleField);
        emptyTile.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        Debug.Log("Number of sprites: " + puzzleSprites.Count);

    }


    // shuffle a list of sprites
    void Shuffle(List<Sprite> sprites)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            Sprite temp = sprites[i];
            int randomIndex = Random.Range(i, sprites.Count);
            sprites[i] = sprites[randomIndex];
            sprites[randomIndex] = temp;
        }
    }


}

