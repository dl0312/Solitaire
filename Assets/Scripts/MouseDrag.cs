using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    public Vector2 thisPosition;
    public Vector2 mousePosition;
    public Vector2 objPosition;

    public int targetLine;
    public int presentLine;

    public bool isCorrect = false;
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
                if (!isCorrect)
                {
                    transform.position = thisPosition;
                }
                else
                {
                    targetLine = i;
                    int order = GameController.playedDeck[i] + 1;

                    isCorrect = true;
                    GetComponent<SpriteRenderer>().sortingOrder = order;
                    for (int j = 0; j < 4; j++)
                    {
                        transform.GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = order + 1;
                    }
                    if (presentLine != targetLine)
                    {
                        GetComponent<SpriteRenderer>().sortingOrder = order;
                        for (int j = 0; j < 4; j++)
                        {
                            if (j == 0)
                            {
                                transform.GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = order;
                            }
                            else { transform.GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = order + 1; }

                        }
                        transform.position = GameController.playedMagnetPlaces[i];
                        GameController.playedDeck[presentLine]--;
                        GameController.playedMagnetPlaces[presentLine].y -= GameController.y_offset;
                        GameController.playedDeck[targetLine]++;
                        GameController.playedMagnetPlaces[targetLine].y += GameController.y_offset;
                        Debug.Log("present" + GameController.playedMagnetPlaces[presentLine]);
                        Debug.Log("target" + GameController.playedMagnetPlaces[targetLine]);
                    }
                }
            }
        }

        if (!isCorrect)
        {
            transform.position = thisPosition;
        }

    }
}
