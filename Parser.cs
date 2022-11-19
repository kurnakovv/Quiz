using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class is thread safe.
/// </summary>
public class Parser
{
    private string path;
    public async Task<string> GetPath()
    {
        return path;
    }
    public async Task SetPath(string p)
    {
        path = p;
    }
    public async Task<string> GetContent()
    {
        StreamReader sr = File.OpenText(path);
        string output = "";
        int data;
        while ((data = sr.Read()) > 0)
        {
            output += (char)data;
        }
        return output;
    }
    public async Task<string> GetContentWithoutUnicode()
    {
        StreamReader sr = File.OpenText(path);
        string output = "";
        int data;
        while ((data = sr.Read()) > 0)
        {
            if (data < 0x80)
            {
                output += (char)data;
            }
        }
        return output;
    }
    public void SaveContent(string content)
    {
        StreamWriter sw = File.CreateText(path);
        try
        {
            for (int i = 0; i < content.Length; i += 1)
            {
                sw.Write(content[i]);
            }
            sw.Close();
        }
        catch (IOException e)
        {
            Debug.WriteLine(e.Message + " " + e.StackTrace);
        }
    }
}