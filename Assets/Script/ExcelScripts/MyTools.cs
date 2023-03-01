
using UnityEngine;

using System.IO;
using System;
using System.Collections.Generic;

public class MyTools : MonoBehaviour {
    private void Start()
    {
        //ExportDataToCSV();
    }
    static int i = 0;

    //[MenuItem("My Tools/Add To Report %F1")]


    static void DEV_AppendToReport(string[] data)
    {

        //string[] s = new string[4] { ("" + i), "Omar" + i, "Ali" + i, ("20/02/2020" + i) };

        CSVManager.AppandToReport(data);
        //EditorApplication.Beep();
    }


    //[MenuItem("My Tools/Reset Report %F12")]
    static void DEV_ResetReport()
    {
        CSVManager.CreateReport();
        //EditorApplication.Beep();
    }


    string path, content;
    AnimationData animationData;

    void GetDataFromJSON()
    {

        path = Application.streamingAssetsPath + "/fusionData.json";
        content = File.ReadAllText(path);

        animationData = JsonUtility.FromJson<AnimationData>(content);

    }

    void ExportDataToCSV()
    {
        GetDataFromJSON();
        int cpt = 0;
        foreach (SquelleteData frame in animationData.squelleteData)
        {
            cpt++;
            int j = 0;

            string[] finalString = new string[61];

            finalString[0] = "Frame " + cpt;

            for (int i = 1; i<61; i+=3)
            {
                /*finalString[i] = ("" + frame.articuPosition[i - 1].x).Replace(".", ",");
                finalString[i + 1] = ("" + frame.articuPosition[i].y).Replace(".", ",");
                finalString[i + 2] = ("" + frame.articuPosition[i + 1].z).Replace(".", ",");*/
                finalString[i] = (""+frame.articuPosition[j].x);
                finalString[i+1] = ("" + frame.articuPosition[j].y); 
                finalString[i+2] = ("" + frame.articuPosition[j].z);
                j++;
            }
            DEV_AppendToReport(finalString);
        }
    }


    [Serializable]
    public class SquelleteData
    {
        public Vector3[] articuPosition;
        public Quaternion[] articuRotation;
        public Vector3[] scale;
        public double timeExec;
    }
    [Serializable]
    public class AnimationData
    {
        public List<SquelleteData> squelleteData;
    }



}
