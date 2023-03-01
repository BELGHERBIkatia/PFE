using UnityEngine;
using System.IO;

public static class CSVManager  {


    private static string reportDirectoryName = "Reports";
    private static string reportFileName = "report.csv";
    private static string reportSeperator = ";";
    private static string[] reportHeaders = new string[61];
    private static string[] headers = new string[20] {
        "HipCenter",
        "Spine",
        "ShoulderCenter",
        "Head",
        "ShoulderLeft",
        "ElbowLeft",
        "WristLeft",
        "HandLeft",
        "ShoulderRight",
        "ElbowRight",
        "WristRight",
        "HandRight",
        "HipLeft",
        "KneeLeft",
        "AnkleLeft",
        "FootLeft",
        "HipRight",
        "KneeRight",
        "AnkleRight",
        "FootRight" };

    private static string TimeStampHelper = "Time Stamp";


    #region Interactions

    public static void HeaterSet()
    {
        int j = 1;
        reportHeaders[0] = "Frames";

        for (int i = 0; i < headers.Length; i++)
        {
            reportHeaders[j] = headers[i] + " X";
            reportHeaders[j+1] = headers[i] + " Y";
            reportHeaders[j+2] = headers[i] + " Z";
            j += 3;
        }

    }
    public static void AppandToReport(string[] strings)
    {
        Debug.Log("  AppandToReport");
        Debug.Log("  AppandToReport strings =  "+ strings);
        reportFileName = "report"+newExamControlor.idExamen+".csv";

        VerifyDirectory();
        VerifyFile();

        using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < strings.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeperator;
                }
                finalString += strings[i];

            }
            finalString += reportSeperator+ GetTimeStamp();
            sw.WriteLine(finalString);

        }

    }

    public static void CreateReport()
    {
        Debug.Log("  CreateReport");
        VerifyDirectory();
        HeaterSet();
        using (StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < reportHeaders.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeperator;
                }
                finalString += reportHeaders[i];
  
            }
            finalString += reportSeperator + TimeStampHelper;
            sw.WriteLine(finalString);
        }
    }

#endregion

#region Operations

    static void VerifyDirectory()
    {
        Debug.Log("  VerifyDirectory");
        string dir = GetDirectoryPath();

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }


    }

    static void VerifyFile()
    {
        Debug.Log("  VerifyFile");
        string file = GetFilePath();
        if (!File.Exists(file))
        {
            CreateReport();
        }
    }

#endregion

#region Queries

    static string GetDirectoryPath()
    {
        return Application.dataPath + "/" + reportDirectoryName;
    }

    static string GetFilePath()
    {
        return GetDirectoryPath() + "/" + reportFileName;
    }

    static string GetTimeStamp()
    {
        return System.DateTime.UtcNow.ToString();
    }

#endregion
}
