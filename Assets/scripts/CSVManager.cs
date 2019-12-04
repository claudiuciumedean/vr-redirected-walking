using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class CSVManager
{
    private static string reportDirectoryName = Directory.GetCurrentDirectory();
    private static string reportFileName = "Report";
    private static string fileFormat = ".csv";
    private static string reportSeparator = ",";
    private static string[] reportHeaders = new string[4] { "ID", "Overlap level", "Distractor (yes/no)", "Impossible (yes/no)" };

    public static void setFileName(int id)
    {
        CSVManager.reportFileName = reportFileName + "_" + id;
    }

    #region Interactions
    public static void AppendToReport(string[] strings) {
        VerifyDirectory();
        VerifyFile();
        using (StreamWriter sw = File.AppendText(GetFilePath())) {
            string finalString = "";
            for (int i = 0; i < strings.Length; i++) {
                if (finalString != "") {
                    finalString += reportSeparator;
                }
                finalString += strings[i];
            }

            sw.WriteLine(finalString);
        }
    }

    public static void CreateReport() {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath())) {
            string finalString = "";

            for (int i = 0; i < reportHeaders.Length; i++) {
                if (finalString != "") {
                    finalString += reportSeparator;
                }
                finalString += reportHeaders[i];
            }
            sw.WriteLine(finalString);
        }
    }
    #endregion

    #region Operations

    static void VerifyDirectory() {
        string dir = GetDirectoryPath();

        if (!Directory.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }
    }

    static void VerifyFile() {
        string file = GetFilePath();
        if (!File.Exists(file))
        {
            CreateReport();
        }
        
    }

    #endregion

    #region Queries
    static string GetDirectoryPath() {
        return Directory.GetCurrentDirectory();
    }

    static string GetFilePath() {
        return GetDirectoryPath() + "/Reports/" + reportFileName + fileFormat;
    }
    #endregion
}
