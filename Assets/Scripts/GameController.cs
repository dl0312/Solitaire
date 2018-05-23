using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static List<GameObject>[] playedDeckList = new List<GameObject>[7];
    public static List<GameObject> preparedDeckList = new List<GameObject>();
    public static GameObject shownpreparedcard = null;

    public float time = 0;
    public int point = 0;
    public static int move_cnt = 0;
    public int[] deck = new int[52];
    public static Card[,] cards = new Card[4, 13];
    public GameObject card;
    public Sprite hidden;
    public Sprite White;
    public Sprite[] shape;
    public Sprite[] cardTextures;
    public Sprite[] Simbols;
    public Sprite[] smallSimbols;
    public Sprite[] RedNumbers;
    public Sprite[] BlackNumbers;
    public Vector2 preparedDeck = new Vector2(-7.3f, 3.2f);
    public Vector2[] playedDeckPlace = new Vector2[7];
    public Vector2[] shapeDeck = new Vector2[4];

    public static Vector2[] playedMagnetPlaces = new Vector2[7];
    public static int[] playedDeck = new int[7];

    public int[] shapeDeckStack = new int[4];
    public int cnt;
    public int played;
    public int decktmp = 0;



    public static float y_offset = 0.8f;

    // Use this for initialization


    public static void setchildrenSortingOrder(GameObject obj, int order)
    {
        for (int i = 0; i < 4; i++)
        {
            obj.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = order;
        }
    }

    public static void setchildrenSortingLayer(GameObject obj, string name)
    {
        for (int i = 0; i < 4; i++)
        {
            obj.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = name;
        }
    }

    void Start()
    {

        for (int i = 0; i < 7; i++)
        {
            playedDeckList[i] = new List<GameObject>();
        }
        for (int i = 0; i < 52; i++)
        {
            deck[i] = i;
        }
        for (int i = 0; i < 52; i++)
        {
            int temp = deck[i];
            int randomIndex = Random.Range(0, 51);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
            temp = deck[i];
        }
        GameObject tmp = Instantiate(card, preparedDeck, Quaternion.identity) as GameObject;
        tmp.name = "prepared Deck";

        tmp.GetComponent<SpriteRenderer>().sortingOrder = 1;
        for (int i = 0; i < 7; i++)
        {
            playedDeckPlace[i] = new Vector2(-7.3f + i * 2.0f, 0.5f);
            playedDeck[i] = i;
            for (int j = 0; j < i + 1; j++)
            {
                int tmpshape = deck[decktmp] / 13;
                int tmpnumber = deck[decktmp] % 13;
                GameObject temp = Instantiate(card, playedDeckPlace[i] + new Vector2(0, j * (-y_offset)), Quaternion.identity) as GameObject;
                switch (tmpshape)
                {
                    case (0):
                        temp.name = "Heart " + tmpnumber;
                        break;
                    case (1):
                        temp.name = "Dia    " + tmpnumber;
                        break;
                    case (2):
                        temp.name = "Spade " + tmpnumber;
                        break;
                    case (3):
                        temp.name = "Clova " + tmpnumber;
                        break;


                }

                if (j == i)
                {
                    playedDeckList[i].Add(temp);
                    temp.GetComponent<SpriteRenderer>().sprite = White;
                    temp.GetComponent<SpriteRenderer>().sortingOrder = 2 * i + 1;
                    temp.GetComponent<SpriteRenderer>().sortingLayerName = "discovered";
                    temp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Simbols[tmpshape];
                    temp.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = smallSimbols[tmpshape];
                    if (tmpshape < 2)
                    {
                        if (tmpnumber == 0)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[13];
                        }
                        else if (tmpnumber == 10)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[tmpnumber];
                            temp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = RedNumbers[0];
                        }
                        else
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[tmpnumber];
                        }
                    }
                    else
                    {
                        if (tmpnumber == 0)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[13];
                        }
                        else if (tmpnumber == 10)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[tmpnumber];
                            temp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = BlackNumbers[0];
                        }
                        else
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[tmpnumber];
                        }
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        temp.transform.GetChild(k).GetComponent<Renderer>().enabled = true;
                        temp.transform.GetChild(k).GetComponent<SpriteRenderer>().sortingOrder = 2 * i + 2;
                        temp.transform.GetChild(k).GetComponent<SpriteRenderer>().sortingLayerName = "discovered";

                    }
                    temp.GetComponent<BoxCollider2D>().enabled = true;
                    temp.GetComponent<Card>().setNumber(tmpshape, tmpnumber);
                    temp.GetComponent<Card>().setPosition(i, j);
                }
                else
                {
                    playedDeckList[i].Add(temp);
                    temp.GetComponent<SpriteRenderer>().sortingOrder = 2 * j + 1;
                    temp.GetComponent<SpriteRenderer>().sortingLayerName = "uncovered";
                    temp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Simbols[tmpshape];
                    temp.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = smallSimbols[tmpshape];

                    if (tmpshape < 2)
                    {
                        if (tmpnumber == 0)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[13];
                        }
                        else if (tmpnumber == 10)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[tmpnumber];
                            temp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = RedNumbers[0];
                        }
                        else
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[tmpnumber];
                        }
                    }
                    else
                    {
                        if (tmpnumber == 0)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[13];
                        }
                        else if (tmpnumber == 10)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[tmpnumber];
                            temp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = BlackNumbers[0];
                        }
                        else
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[tmpnumber];
                        }
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        temp.transform.GetChild(k).GetComponent<SpriteRenderer>().sortingOrder = 2 * j + 2;
                    }
                    temp.GetComponent<Card>().setNumber(tmpshape, tmpnumber);
                    temp.GetComponent<Card>().setPosition(i, j);

                }
                decktmp++;
            }
            playedMagnetPlaces[i] = playedDeckPlace[i] + new Vector2(0, (i + 1) * (-y_offset));
        }


        for (int i = 0; i < 4; i++)
        {
            shapeDeck[i] = new Vector2(-1.3f + i * 2.0f, 3.2f);
            GameObject temp = Instantiate(card, shapeDeck[i], Quaternion.identity) as GameObject;
            temp.GetComponent<SpriteRenderer>().sprite = shape[i];
            temp.GetComponent<SpriteRenderer>().sortingOrder = 1;
            shapeDeckStack[i] = 0;
        }

        played = 28;
        for (int i = played; i < 52; i++)
        {
            int tmpshape = deck[decktmp] / 13;
            int tmpnumber = deck[decktmp] % 13;
            GameObject temp = Instantiate(card, preparedDeck, Quaternion.identity) as GameObject;
            temp.GetComponent<SpriteRenderer>().sortingOrder = 1;
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "uncovered";
            temp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Simbols[tmpshape];
            temp.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = smallSimbols[tmpshape];

            if (tmpshape < 2)
            {
                if (tmpnumber == 0)
                {
                    temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[13];
                }
                else if (tmpnumber == 10)
                {
                    temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[tmpnumber];
                    temp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = RedNumbers[0];
                }
                else
                {
                    temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[tmpnumber];
                }
            }
            else
            {
                if (tmpnumber == 0)
                {
                    temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[13];
                }
                else if (tmpnumber == 10)
                {
                    temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[tmpnumber];
                    temp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = BlackNumbers[0];
                }
                else
                {
                    temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[tmpnumber];
                }
            }
            for (int k = 0; k < 4; k++)
            {
                temp.transform.GetChild(k).GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            temp.GetComponent<Card>().setNumber(tmpshape, tmpnumber);
            preparedDeckList.Add(temp);
            decktmp++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(preparedDeck.x + " " + preparedDeck.y);
            Debug.Log(Input.mousePosition.x + " " + Input.mousePosition.y);

            if (270 < Input.mousePosition.x
            && 350 > Input.mousePosition.x
            && 560 < Input.mousePosition.y
            && 660 > Input.mousePosition.y)
            {
                Debug.Log("prepareddeck!");
                showPreparedDeck();
            }
        }
    }

    void showPreparedDeck()
    {
        if (shownpreparedcard == null)
        {
            preparedDeckList[0].transform.position = new Vector2(-5.3f, 3.2f);
            shownpreparedcard = preparedDeckList[0];
            preparedDeckList.RemoveAt(0);
        }
        else
        {
            shownpreparedcard.transform.position = preparedDeck;
            preparedDeckList.Add(shownpreparedcard);
            shownpreparedcard = preparedDeckList[0];
            shownpreparedcard.transform.position = new Vector2(-5.3f, 3.2f);

        }
    }
}
