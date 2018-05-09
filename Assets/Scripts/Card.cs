using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public Vector2 thisPosition;
    public Vector2 mousePosition;
    public Vector2 objPosition;

    public int targetLine;
    public int presentLine;

    public bool isCorrect = false;

    public enum Shape { Heart, Diamond, Clova, Spade };
    public int shape;

    public bool color = false;	// red : false, black : true
    public int number;

    public Sprite White;
    // Use this for initialization
    void Start()
    {

    }

    public void setNumber(int shape, int number)
    {
        this.shape = shape;
        this.number = number;
        if (shape > 1)
        {
            color = true;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            thisPosition = transform.position;
            for (int i = 0; i < 7; i++)
            {
                if ((int)(thisPosition.x - GameController.playedMagnetPlaces[i].x) == 0)
                {
                    presentLine = i;
                }
            }
            Debug.Log(thisPosition);
        }
    }

    void OnMouseDrag()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        GetComponent<SpriteRenderer>().sortingOrder = 10;
        for (int j = 0; j < 4; j++)
        {
            transform.GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = 11;
        }

        transform.position = objPosition;
    }

    void OnMouseUp()
    {
        objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        for (int i = 0; i < 7; i++)
        {
            if ((int)((objPosition.x - GameController.playedMagnetPlaces[i].x - 1.0f) / 2.0f) == 0)
            {
                targetLine = i;
                Debug.Log(GameController.playedDeckList[targetLine].Peek().GetComponent<Card>().number + "," + (this.number));
                if (GameController.playedDeckList[targetLine].Peek().GetComponent<Card>().number == (this.number + 1) % 13
                && GameController.playedDeckList[targetLine].Peek().GetComponent<Card>().color != this.color)
                {
                    if (presentLine != targetLine)
                    {
                        isCorrect = true;
                        int order = GameController.playedDeck[i] + 1;
                        GetComponent<SpriteRenderer>().sortingOrder = order;
                        for (int j = 0; j < 4; j++)
                        {
                            transform.GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = order + 1;
                        }
                        transform.position = GameController.playedMagnetPlaces[i];
                        GameController.playedDeck[presentLine]--;
                        GameController.playedMagnetPlaces[presentLine].y -= GameController.y_offset;
                        GameController.playedDeck[targetLine]++;
                        GameController.playedMagnetPlaces[targetLine].y += GameController.y_offset;
                        GameController.playedDeckList[targetLine].Push(GameController.playedDeckList[presentLine].Pop());

                        GameObject CoveredCard = GameController.playedDeckList[presentLine].Peek();
                        CoveredCard.GetComponent<BoxCollider2D>().enabled = true;
                        for (int k = 0; k < 4; k++)
                        {
                            CoveredCard.transform.GetChild(k).GetComponent<Renderer>().enabled = true;
                            CoveredCard.transform.GetChild(k).GetComponent<SpriteRenderer>().sortingOrder =
                            CoveredCard.GetComponent<SpriteRenderer>().sortingOrder + 1;
                        }
                        CoveredCard.GetComponent<SpriteRenderer>().sprite = White;
                        Debug.Log("present" + GameController.playedMagnetPlaces[presentLine]);
                        Debug.Log("target" + GameController.playedMagnetPlaces[targetLine]);
                    }
                }
            }
        }

        if (!isCorrect)
        {
            int order = GameController.playedDeck[presentLine];
            GetComponent<SpriteRenderer>().sortingOrder = order;
            for (int j = 0; j < 4; j++)
            {
                transform.GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = order + 1;
            }
            transform.position = thisPosition;
        }

    }
}
