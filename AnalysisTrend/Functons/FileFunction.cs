using System;
using System.Text;

namespace AnalysisTrend.Functions;
class FileFunction
{
    public static List<string[]> ReadCSV(string filePath)
    {
        List<string[]> _list = new List<string[]>();
        StreamReader sr = new StreamReader(filePath, Encoding.UTF8);
        while (sr.Peek() != -1)
        {
            string[] values = sr.ReadLine().Split(',');
            _list.Add(new string[] { values[0], values[1] });
        }
        sr.Close();
        return _list;
    }

    public static void WriteData(string _path, List<string> _list)
    {
        StreamWriter sw = new StreamWriter(_path, false, Encoding.UTF8);
        foreach (string data in _list)
        {
            sw.WriteLine(data);
        }
        sw.Close();
    }
}
