using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateDropDown : MonoBehaviour {

    void Start()
    {
        Button b = this.GetComponent<Button>();
        string j = gameObject.transform.GetChild(0).GetComponent<Text>().text;
        string i = gameObject.name;
        b.onClick.AddListener(delegate { SelectElement(j); });
    }


    void SelectElement(string s)
    {
        GameObject grandParent = ((gameObject.transform.parent).parent).parent.parent.parent.parent.parent.gameObject;

        switch (grandParent.name)
        {
            case "Year":
                (grandParent.transform).GetChild(0).GetChild(0).GetComponent<Text>().text = s;
                ((gameObject.transform.parent).parent).parent.parent.parent.parent.gameObject.SetActive(false);
                ((gameObject.transform.parent).parent).parent.parent.parent.parent.parent.parent.gameObject.GetComponent<DateFillDropDown>().SelectedElementYear = s;

                break;
            case "Mounth":
                (grandParent.transform).GetChild(0).GetChild(0).GetComponent<Text>().text = s;
                ((gameObject.transform.parent).parent).parent.parent.parent.parent.gameObject.SetActive(false);
                ((gameObject.transform.parent).parent).parent.parent.parent.parent.parent.parent.gameObject.GetComponent<DateFillDropDown>().SelectedElementMounth = s;

                break;
            case "Day":
                (grandParent.transform).GetChild(0).GetChild(0).GetComponent<Text>().text = s;
                ((gameObject.transform.parent).parent).parent.parent.parent.parent.gameObject.SetActive(false);
                ((gameObject.transform.parent).parent).parent.parent.parent.parent.parent.parent.gameObject.GetComponent<DateFillDropDown>().SelectedElementDay = s;

                break;
        }

      
        Debug.Log("Hello s =" + s);
    }
}
