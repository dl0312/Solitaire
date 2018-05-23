using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public Vector2 thisPosition;
    public Vector2 mousePosition;
    public Vector2 objPosition;

    public int targetLine;

    public bool isCorrect = false;

    public enum Shape { Heart, Diamond, Clova, Spade };
    public int shape;

    public int playedDeckLine;

    public int playedDeckLineIndex;

    public bool color = false;	// red : false, black : true

    public bool isattached = false;
    public int number;

    public GameObject thisObject;

    public List<GameObject> attachedObejects;
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
        this.playedDeckLineIndex = lineIndex;
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("index" + this.playedDeckLine);
            Debug.Log("index" + this.playedDeckLineIndex + "index" + GameController.playedDeck[this.playedDeckLine]);
            thisPosition = transform.position;
            GetComponent<SpriteRenderer>().sortingLayerName = "choosen";
            GameController.setchildrenSortingLayer(this.gameObject, "choosen");
            thisObject = GameController.playedDeckList[playedDeckLine][playedDeckLineIndex];
            if (this.playedDeckLineIndex != GameController.playedDeck[this.playedDeckLine])
            {
                isattached = true;
                for (int i = this.playedDeckLineIndex + 1; i <= GameController.playedDeck[this.playedDeckLine]; i++)
                {
                    GameController.playedDeckList[playedDeckLine][i].GetComponent<SpriteRenderer>().sortingLayerName = "choosen";
                    GameController.setchildrenSortingLayer(GameController.playedDeckList[playedDeckLine][i], "choosen");
                    attachedObejects.Add(GameController.playedDeckList[playedDeckLine][i]);
                    Debug.Log(GameController.playedDeckList[playedDeckLine][i]);
                    GameController.playedDeckList[playedDeckLine][i].transform.parent = this.gameObject.transform;
                }
            }
        }
    }

    void OnMouseDrag()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

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
                Debug.Log("this.playedDeckLine: " + this.playedDeckLine);

                Debug.Log("GameController.playedDeck[this.this.playedDeckLine]: " + GameController.playedDeck[this.playedDeckLine]);

                Debug.Log("target line: " + targetLine + " targetLine state: " + GameController.playedDeck[targetLine]);
                if (GameController.playedDeck[targetLine] > 0)
                {
                    GameObject targetObject = GameController.playedDeckList[targetLine][GameController.playedDeck[targetLine]];
                    Debug.Log("target number " + targetObject.GetComponent<Card>().number + ", present number " + (this.number));
                    if (targetObject.GetComponent<Card>().number == (this.number + 1) % 13
                    && targetObject.GetComponent<Card>().color != this.color)
                    {
                        if (this.playedDeckLine != targetLine)
                        {
                            isCorrect = true;
                            int order = GameController.playedDeck[i] + 1;
                            GetComponent<SpriteRenderer>().sortingOrder = 2 * order + 1;
                            GetComponent<SpriteRenderer>().sortingLayerName = "discovered";
                            for (int j = 0; j < 4; j++)
                            {
                                transform.GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = 2 * order + 2;
                                transform.GetChild(j).GetComponent<SpriteRenderer>().sortingLayerName = "discovered";
                            }
                            if (GameController.playedDeck[this.playedDeckLine] > 0 || GameController.playedDeckList[this.playedDeckLine][this.playedDeckLineIndex - 1].GetComponent<SpriteRenderer>().sortingLayerName != "discovered")
                            {
                                Debug.Log("we are in coveredcard");
                                Debug.Log("GameController.playedDeck[this.this.playedDeckLine]: " + GameController.playedDeck[this.playedDeckLine]);
                                GameObject CoveredCard = GameController.playedDeckList[this.playedDeckLine][this.playedDeckLine - 1];
                                CoveredCard.GetComponent<BoxCollider2D>().enabled = true;
                                CoveredCard.GetComponent<SpriteRenderer>().sortingLayerName = "discovered";

                                for (int k = 0; k < 4; k++)
                                {
                                    CoveredCard.transform.GetChild(k).GetComponent<Renderer>().enabled = true;
                                    CoveredCard.transform.GetChild(k).GetComponent<SpriteRenderer>().sortingLayerName = "discovered";
                                }
                                CoveredCard.GetComponent<SpriteRenderer>().sprite = White;
                            }
                            transform.position = GameController.playedMagnetPlaces[i];
                            GameController.playedDeck[this.playedDeckLine]--;
                            GameController.playedMagnetPlaces[this.playedDeckLine].y += GameController.y_offset;
                            GameController.playedDeck[targetLine]++;
                            GameController.playedMagnetPlaces[targetLine].y -= GameController.y_offset;
                            Debug.Log("shifted to present " + this.playedDeckLine + "target " + targetLine);
                            GameController.playedDeckList[targetLine].Add(thisObject);
                            GameController.playedDeckList[this.playedDeckLine].Remove(thisObject);
                            this.setPosition(targetLine, GameController.playedDeck[targetLine]);

                            for (int k = 0; k < 7; k++)
                            {
                                Debug.Log("state" + k + " " + GameController.playedDeck[k]);
                                for (int u = 0; u < GameController.playedDeckList[k].Count; u++)
                                {
                                    Debug.Log("state" + k + " " + GameController.playedDeckList[k][u]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (this.number == 0)
                    {
                        isCorrect = true;
                        int order = 0;
                        GetComponent<SpriteRenderer>().sortingOrder = 2 * order + 1;
                        GetComponent<SpriteRenderer>().sortingLayerName = "discovered";
                        for (int j = 0; j < 4; j++)
                        {
                            transform.GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = 2 * order + 2;
                            transform.GetChild(j).GetComponent<SpriteRenderer>().sortingLayerName = "discovered";
                        }
                        if (GameController.playedDeck[this.playedDeckLine] > 0 || GameController.playedDeckList[this.playedDeckLine][this.playedDeckLineIndex - 1].GetComponent<SpriteRenderer>().sortingLayerName != "discovered")
                        {
                            Debug.Log("we are in coveredcard");
                            Debug.Log("GameController.playedDeck[this.this.playedDeckLine]: " + GameController.playedDeck[this.playedDeckLine]);
                            GameObject CoveredCard = GameController.playedDeckList[this.playedDeckLine][this.playedDeckLine - 1];
                            CoveredCard.GetComponent<BoxCollider2D>().enabled = true;
                            CoveredCard.GetComponent<SpriteRenderer>().sortingLayerName = "discovered";

                            for (int k = 0; k < 4; k++)
                            {
                                CoveredCard.transform.GetChild(k).GetComponent<Renderer>().enabled = true;
                                CoveredCard.transform.GetChild(k).GetComponent<SpriteRenderer>().sortingLayerName = "discovered";
                            }
                            CoveredCard.GetComponent<SpriteRenderer>().sprite = White;
                        }
                        transform.position = GameController.playedMagnetPlaces[i];
                        GameController.playedDeck[this.playedDeckLine]--;
                        GameController.playedMagnetPlaces[this.playedDeckLine].y += GameController.y_offset;
                        GameController.playedDeck[targetLine]++;
                        GameController.playedMagnetPlaces[targetLine].y -= GameController.y_offset;
                        Debug.Log("shifted to present " + this.playedDeckLine + "target " + targetLine);
                        GameController.playedDeckList[targetLine].Add(thisObject);
                        GameController.playedDeckList[this.playedDeckLine].Remove(thisObject);
                        this.setPosition(targetLine, GameController.playedDeck[targetLine]);

                    }
                }
                break;
            }
        }

        if (!isCorrect)
        {
            int order = GameController.playedDeck[this.playedDeckLine];
            GetComponent<SpriteRenderer>().sortingLayerName = "discovered";
            GameController.setchildrenSortingLayer(this.gameObject, "discovered");

            transform.position = thisPosition;
            for (int i = 0; i < attachedObejects.Count; i++)
            {
                attachedObejects[i].transform.parent = null;
                attachedObejects[i].GetComponent<SpriteRenderer>().sortingLayerName = "discovered";
                GameController.setchildrenSortingLayer(attachedObejects[i], "discovered");
            }
            attachedObejects.RemoveRange(0, attachedObejects.Count);
        }

    }
}
