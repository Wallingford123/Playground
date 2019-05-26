using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public RectTransform panel;
    public GameObject slotInst;
    public RectTransform slideBar;
    public int rows, columns, tileWidth, edgeBuffer, slideBarBuffer, screenEdgeBuffer;
    bool isOpen = false;
    Scrollbar scroll;
    int totalRows;
    GameObject[,] buttons;
    int topRow = 0;
    float currentValue = 0;
    List<Item> items = new List<Item>();
    GameObject heldObject;
    Vector3 mousePos;
    public GameObject testObject;
    public GameObject empty;

    // Use this for initialization
    void Start() {
        scroll = slideBar.gameObject.GetComponent<Scrollbar>();
        totalRows = rows;
        int sizerW = (tileWidth * columns) / 2;
        int sizerH = (tileWidth * rows) / 2;
        heldObject = Instantiate(empty, panel.gameObject.transform);
        float width = Screen.width - screenEdgeBuffer*2;
        float height = Screen.height - screenEdgeBuffer*2;
        Debug.Log(height);
        Debug.Log(width);

        if (slideBarBuffer + edgeBuffer + tileWidth * columns < width)
        {
            panel.sizeDelta = new Vector2(slideBarBuffer + edgeBuffer + tileWidth * columns, panel.sizeDelta.y);
            panel.anchoredPosition = new Vector2(Screen.width/2 - sizerW - screenEdgeBuffer - slideBarBuffer, panel.anchoredPosition.y);
        }
        else
        {
            columns = Mathf.FloorToInt((width-slideBarBuffer+edgeBuffer) / tileWidth);
            panel.sizeDelta = new Vector2(width - (width - (tileWidth * columns)) + slideBarBuffer + edgeBuffer, panel.sizeDelta.y);
            panel.anchoredPosition = new Vector2(0, panel.anchoredPosition.y);
            Debug.Log(columns);
        }

        if (edgeBuffer + tileWidth * rows < height)
        {
            panel.sizeDelta = new Vector2(panel.sizeDelta.x, edgeBuffer + tileWidth * rows);
            panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, Screen.height/2 - sizerH - screenEdgeBuffer);
        }
        else
        {
            rows = Mathf.FloorToInt(height / tileWidth);
            panel.sizeDelta = new Vector2(panel.sizeDelta.x, height - (height - (tileWidth * rows)) + edgeBuffer);
            panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, 0);
        }

        sizerW = (tileWidth * columns) / 2;
        sizerH = (tileWidth * rows) / 2;
        buttons = new GameObject[totalRows, columns];

        for (int f = 0; f < rows; f++)
        {
            for (int i = 0; i < columns; i++)
            {
                buttons[f,i] = Instantiate(slotInst, panel.gameObject.transform);
                RectTransform rt = buttons[f,i].GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(tileWidth, tileWidth);
                rt.anchoredPosition = new Vector2(tileWidth / 2 - slideBarBuffer / 2 - sizerW + i * tileWidth, tileWidth / 2 + sizerH - (f+1) * tileWidth);
            }
        }
        for (int f = rows; f < totalRows; f++)
        {
            for (int i = 0; i < columns; i++)
            {
                buttons[f, i] = Instantiate(slotInst, panel.gameObject.transform);
                RectTransform rt = buttons[f, i].GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(tileWidth, tileWidth);
                //RectTransform p = buttons[f - 1, i].GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(tileWidth / 2 - slideBarBuffer / 2 - sizerW + i * tileWidth, tileWidth / 2 + sizerH - rows * tileWidth - (((f - rows)+1) * tileWidth));
                buttons[f, i].GetComponent<Image>().enabled = false;
            }
        }
        slideBar.anchoredPosition = new Vector2(sizerW + edgeBuffer / 4, 0);
        slideBar.sizeDelta = new Vector2(slideBarBuffer, tileWidth * rows);
        scroll.numberOfSteps = (totalRows - rows) + 1;
        float x = (1 / ((totalRows - rows) + 1f));
        scroll.size = x;
        scroll.onValueChanged.AddListener(Scrolled);
        foreach(GameObject o in buttons)
        {
            o.GetComponent<Button>().onClick.AddListener(delegate { buttonOnClick(o.GetComponent<buttonScript>()); });
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isOpen)
            {
                isOpen = true;
                panel.gameObject.SetActive(true);
                return;
            }
            if (isOpen)
            {
                isOpen = false;
                panel.gameObject.SetActive(false);
                if (heldObject.name != ("Empty")) Destroy(heldObject);
                heldObject = Instantiate(empty, panel.gameObject.transform);
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Destroy(heldObject);
            heldObject = Instantiate(testObject, panel.gameObject.transform);
        }
	}
    private void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 20;
        heldObject.transform.position = mousePos;
    }
    void Scrolled(float value)
    {
        float stepVal = 1f/(totalRows-rows);
        if (scroll.value > currentValue)
        {
            int numberOfSteps = Mathf.RoundToInt((scroll.value - currentValue) / stepVal);
            foreach (GameObject ob in buttons)
            {
                RectTransform obR = ob.GetComponent<RectTransform>();
                obR.anchoredPosition = new Vector2(obR.anchoredPosition.x, obR.anchoredPosition.y + tileWidth * numberOfSteps);
            }
            currentValue = scroll.value;
            for (int f = 0; f < numberOfSteps; f++)
            {
                for (int i = 0; i < columns; i++)
                {
                    buttons[topRow, i].GetComponent<Image>().enabled = false;
                }
                for (int i = 0; i < columns; i++)
                {
                    buttons[topRow + rows, i].GetComponent<Image>().enabled = true;
                }
                topRow++;
                Debug.Log("Moved");
            }
        }


        if (scroll.value < currentValue)
        {
            int numberOfSteps = Mathf.RoundToInt((currentValue - scroll.value) / stepVal);
            foreach (GameObject ob in buttons)
            {
                RectTransform obR = ob.GetComponent<RectTransform>();
                obR.anchoredPosition = new Vector2(obR.anchoredPosition.x, obR.anchoredPosition.y - tileWidth * numberOfSteps);
            }
            currentValue = scroll.value;
            for (int f = 0; f < numberOfSteps; f++)
            {
                for (int i = 0; i < columns; i++)
                {
                    buttons[topRow - 1, i].GetComponent<Image>().enabled = true;
                }
                for (int i = 0; i < columns; i++)
                {
                    buttons[topRow + rows - 1, i].GetComponent<Image>().enabled = false;
                }
                topRow--;
                Debug.Log("Moved");
            }
        }
    }
    void buttonOnClick(buttonScript b)
    {
        Debug.Log(isOpen);
        Debug.Log("clicked");
        GameObject buffer1, buffer2;
        buffer1 = b.currentObject;
        buffer2 = heldObject;

        buffer2.transform.SetParent(buffer1.transform.parent);
        buffer2.transform.position = buffer1.transform.parent.position;
        buffer1.transform.SetParent(panel.gameObject.transform);

        b.currentObject = buffer2;
        heldObject = buffer1;
    }
}
