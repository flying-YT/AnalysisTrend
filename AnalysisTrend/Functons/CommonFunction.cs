using AnalysisTrend.Functions;
using AnalysisTrend.Model;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

class CommonFunction
{
    public static AggregateData GetMaxCountNoun(Dictionary<string, AggregateData> _nounDict)
    {
        AggregateData maxAggregateData = new AggregateData{ Word = "", Count = 0 };
        foreach(KeyValuePair<string, AggregateData> kvp in _nounDict)
        {
            if(maxAggregateData.Count < kvp.Value.Count)
            {
                maxAggregateData = new AggregateData { Word = kvp.Value.Word, Count = kvp.Value.Count};
            }
        }
        return maxAggregateData;
    }
}