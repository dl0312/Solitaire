using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static Stack<GameObject>[] playedDeckList = new Stack<GameObject>[7];

    public float time = 0;
    public int point = 0;
    public int move_cnt = 0;
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
    void Start()
    {

        for (int i = 0; i < 7; i++)
        {
            playedDeckList[i] = new Stack<GameObject>();
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
        tmp.GetComponent<SpriteRenderer>().sortingOrder = 1;
        for (int i = 0; i < 7; i++)
        {
            playedDeckPlace[i] = new Vector2(-7.3f + i * 2.0f, 0.5f);
            playedDeck[i] = i + 1;
            for (int j = 0; j < i + 1; j++)
            {
                int tmpshape = deck[decktmp] / 13;
                int tmpnumber = deck[decktmp] % 13;
                GameObject temp = Instantiate(card, playedDeckPlace[i] + new Vector2(0, j * (-y_offset)), Quaternion.identity) as GameObject;
                if (j == i)
                {
                    playedDeckList[i].Push(temp);
                    temp.GetComponent<SpriteRenderer>().sprite = White;
                    temp.GetComponent<SpriteRenderer>().sortingOrder = i + 1;
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
                        temp.transform.GetChild(k).GetComponent<SpriteRenderer>().sortingOrder = i + 2;
                    }
                    temp.GetComponent<BoxCollider2D>().enabled = true;
                    temp.GetComponent<Card>().setNumber(tmpshape, tmpnumber);
                }
                else
                {
                    playedDeckList[i].Push(temp);
                    temp.GetComponent<SpriteRenderer>().sortingOrder = j + 1;
                    temp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Simbols[tmpshape];
                    temp.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = smallSimbols[tmpshape];

                    if (tmpshape < 2)
                    {
                        if (tmpshape == 0)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[13];
                        }
                        else if (tmpshape == 10)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[tmpshape];
                            temp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = RedNumbers[0];
                        }
                        else
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = RedNumbers[tmpshape];
                        }
                    }
                    else
                    {
                        if (tmpshape == 0)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[13];
                        }
                        else if (tmpshape == 10)
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[tmpshape];
                            temp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = BlackNumbers[0];
                        }
                        else
                        {
                            temp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlackNumbers[tmpshape];
                        }
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        temp.transform.GetChild(k).GetComponent<SpriteRenderer>().sortingOrder = i + 2;
                    }
                    temp.GetComponent<Card>().setNumber(tmpshape, tmpnumber);
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
