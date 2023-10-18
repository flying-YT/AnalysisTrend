using AnalysisTrend;
using AnalysisTrend.Model;
using AnalysisTrend.Functions;

// Logfile setting
ContinuousLogger.directory = @"/home/log";
ContinuousLogger.fileName = @"AnalysisTrend";

ContinuousLogger.WriteLogInfo("Start Analysis");

NGWord ng = new NGWord();

var csvPath = @"/home/tmp";
var outputPath = @"/home/analysis";

string[] files = Directory.GetFiles(csvPath, "*.csv");

Dictionary<string, string> dict = new Dictionary<string, string>();
foreach (string file in files)
{
    ContinuousLogger.WriteLogInfo("file:" + file);
    List<string[]> list = FileFunction.ReadCSV(file);
    foreach(string[] strArray in list)
    {
        if(!dict.ContainsKey(strArray[0]))
        {
            string baseText = strArray[1];
            baseText = WordFunction.RemoveUnnecessaryChar(baseText);
            baseText = WordFunction.ConvertFullCharToHalfChar(baseText);
            baseText = WordFunction.ConvertFullwidthSpace(baseText);
            baseText = WordFunction.ConvertUnnecessaryDoubleByteHyphen(baseText);

            dict.Add(strArray[0], baseText);
            ContinuousLogger.WriteLogInfo("Dictionary add " + strArray[0]);
        }
    }
}

Dictionary<string, AggregateData> nounDict = new Dictionary<string, AggregateData>();
foreach(KeyValuePair<string, string> kvp in dict)
{
    var nounArray = MecabFunction.ExtractionNoun(kvp.Value).Split(",");
    foreach(string noun in nounArray)
    {
        if(!ng.CheckNG(noun))
        {
            if(!nounDict.ContainsKey(noun))
            {
                nounDict.Add(noun, new AggregateData{ Word = noun, Count = 0 });
            }
            else
            {
                //nounDict[noun].Count += 1;
                var aggregate = nounDict[noun];
                aggregate.Count++;
                nounDict[noun] = aggregate;
            }
        }
    }
}

DateTime dt = DateTime.Now;
List<string> maxNounList = new List<string>();
for(int i=1;i<=100;i++)
{
    var maxAggregateData = CommonFunction.GetMaxCountNoun(nounDict);
    nounDict.Remove(maxAggregateData.Word);

    maxNounList.Add(maxAggregateData.Word + "," + maxAggregateData.Count);
    ContinuousLogger.WriteLogInfo(i +":" + maxAggregateData.Word + " " + maxAggregateData.Count);
}

try
{
    FileFunction.WriteData(outputPath + "/" + dt.ToString("yyyyMMdd") + ".csv", maxNounList);
    ContinuousLogger.WriteLogInfo("Make csv file.");
}
catch(Exception e)
{
    ContinuousLogger.WriteLogError(e.ToString().Replace("\n", ""));
}
