using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateFillDropDown : MonoBehaviour {

    public GameObject dropDownElementDay;
    public GameObject dropDownElementMounth;
    public GameObject dropDownElementYear;


    public GameObject TableContentDay;
    public GameObject TableContentMounth;
    public GameObject TableContentYear;

    public string SelectedElementDay;
    public string SelectedElementMounth;
    public string SelectedElementYear;


    public GameObject tableDropDownDay;
    public GameObject tableDropDownMounth;
    public GameObject tableDropDownYear;


    public Text SelectedElementDayText;
    public Text SelectedElementMounthText;
    public Text SelectedElementYearText;


    // Use this for initialization
    void Start()
    {
        SetDropDownElementsDays();
        SetDropDownElementsMounths();
        SetDropDownElementsYears();
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    public void SetDropDownElementsDays()
    {
        for (int i = 1; i < 32; i++)
        {
            GameObject row = Instantiate(dropDownElementDay);
            row.SetActive(true);

            row.transform.SetParent(TableContentDay.transform, false);

            row.name = "row " + i;
            (row.GetComponent<Transform>().GetChild(0)).GetComponent<Text>().text = "" + i;
        }
    }
    public void SetDropDownElementsMounths()
    {
        for (int i = 1; i < 13; i++)
        {
            GameObject row = Instantiate(dropDownElementMounth);
            row.SetActive(true);

            row.transform.SetParent(TableContentMounth.transform, false);

            row.name = "row " + i;
            (row.GetComponent<Transform>().GetChild(0)).GetComponent<Text>().text = "" + i;
        }
    }

    public void SetDropDownElementsYears()
    {
        int currentYear = DateTime.Now.Year;
        for (int i = currentYear; i > 1930; i--)
        {
            GameObject row = Instantiate(dropDownElementYear);
            row.SetActive(true);

            row.transform.SetParent(TableContentYear.transform, false);

            row.name = "row " + i;
            (row.GetComponent<Transform>().GetChild(0)).GetComponent<Text>().text = "" + i;
        }
    }



    public void afficherDropDownTableDay()
    {
        if (tableDropDownDay.gameObject.active)
            tableDropDownDay.SetActive(false);
        else
            tableDropDownDay.SetActive(true);
        tableDropDownMounth.SetActive(false);
        tableDropDownYear.SetActive(false);
    }

    public void afficherDropDownTableMounth()
    {
        if (tableDropDownMounth.gameObject.active)
            tableDropDownMounth.SetActive(false);
        else
            tableDropDownMounth.SetActive(true);
        tableDropDownDay.SetActive(false);
        tableDropDownYear.SetActive(false);
    }

    public void afficherDropDownTableYear()
    {
        if (tableDropDownYear.gameObject.active)
            tableDropDownYear.SetActive(false);
        else
            tableDropDownYear.SetActive(true);
        tableDropDownDay.SetActive(false);
        tableDropDownMounth.SetActive(false);
    }

    public void SetSelectedElement(string dateDropDown, string value)
    {
        switch (dateDropDown)
        {
            case "Day":
                SelectedElementDay = value;
                SelectedElementDayText.text = value;
                break;
            case "Mounth":
                SelectedElementMounth = value;
                SelectedElementMounthText.text = value;
                break;
            case "Year":
                SelectedElementYear = value;
                SelectedElementYearText.text = value;
                break;
        }

    }
}
