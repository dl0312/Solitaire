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

    public int playedDeckLine;

    public int playedDectLineIndex;

    public bool color = false;	// red : false, black : true
    public int number;

    public GameObject thisObject;

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

    public void setPosition(int line, int lineIndex)
    {
        this.playedDeckLine = line;
        this.playedDectLineIndex = lineIndex;
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.playedDectLineIndex == GameController.playedDeck[this.playedDeckLine])
            {
                thisPosition = transform.position;
                for (int i = 0; i < 7; i++)
                {
                    if ((int)(thisPosition.x - GameController.playedMagnetPlaces[i].x) == 0)
                    {
                        presentLine = i;
                    }
                }
                thisObject = GameController.playedDeckList[presentLine][GameController.playedDeck[presentLine]];
                Debug.Log(thisPosition);
            }
            else
            {
                for (int i = this.playedDectLineIndex; i <= GameController.playedDeck[this.playedDeckLine]; i++)
                {
                    // GameController.playedDeckList[this.playedDeckLine]
                    // object1.transform.parent
                }
            }
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
            if ((int)((objPosition.x - GameController.playedMagnetPlaces[i].x + 1.0f) / 2.0f) == 0)
            {
                targetLine = i;
                Debug.Log("presentLine: " + this.presentLine);

                Debug.Log("GameController.playedDeck[this.presentLine]: " + GameController.playedDeck[this.presentLine]);

                Debug.Log("target line: " + targetLine + " targetLine state: " + GameController.playedDeck[targetLine]);
                GameObject targetObject = GameController.playedDeckList[targetLine][GameController.playedDeck[targetLine] - 1];
                Debug.Log("target number " + targetObject.GetComponent<Card>().number + ", present number " + (this.number));
                if (targetObject.GetComponent<Card>().number == (this.number + 1) % 13
                && targetObject.GetComponent<Card>().color != this.color)
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
                        if (GameController.playedDeck[presentLine] > 0)
                        {
                            Debug.Log("we are in coveredcard");
                            Debug.Log("GameController.playedDeck[this.presentLine]: " + GameController.playedDeck[this.presentLine]);
                            GameObject CoveredCard = GameController.playedDeckList[presentLine][GameController.playedDeck[this.presentLine] - 1];
                            CoveredCard.GetComponent<BoxCollider2D>().enabled = true;
                            for (int k = 0; k < 4; k++)
                            {
                                CoveredCard.transform.GetChild(k).GetComponent<Renderer>().enabled = true;
                                CoveredCard.transform.GetChild(k).GetComponent<SpriteRenderer>().sortingOrder =
                                CoveredCard.GetComponent<SpriteRenderer>().sortingOrder + 1;
                            }
                            CoveredCard.GetComponent<SpriteRenderer>().sprite = White;
                        }
                        transform.position = GameController.playedMagnetPlaces[i];
                        GameController.playedDeck[presentLine]--;
                        GameController.playedMagnetPlaces[presentLine].y += GameController.y_offset;
                        GameController.playedDeck[targetLine]++;
                        GameController.playedMagnetPlaces[targetLine].y -= GameController.y_offset;
                        this.setPosition(targetLine, GameController.playedDeck[targetLine] - 1);
                        Debug.Log("shifted to present " + presentLine + "target " + targetLine);
                        GameController.playedDeckList[targetLine].Add(thisObject);
                        GameController.playedDeckList[presentLine].Remove(thisObject);
                    }
                }
                break;
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
