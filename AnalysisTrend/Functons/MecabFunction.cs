using MeCab;

namespace AnalysisTrend.Functions;
class MecabFunction
{
    public static string ExtractionNoun(string text)
    {
        string resultText = "";

        var tagger = MeCabTagger.Create();
        foreach (var node in tagger.ParseToNodes(text))
        {
            if(0 < node.CharType)
            {
                if(node.Feature.Contains("名詞"))
                {
                    resultText += node.Surface + ",";
                }
            }
        }
        return resultText.TrimEnd(',');
    }
}