using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    class Program
    {
        public enum DownloadDefinition
        {
            P_LC,
            P_GQ,
            P_720,
            P_1080,
            P_4K
        };
        static Dictionary<string, DownloadDefinition> definitionDict = new Dictionary<string, DownloadDefinition>();
        static void InitDefinitionKeyValues()
        {
            definitionDict.Add("detaileDefinition1080P", DownloadDefinition.P_1080);
            definitionDict.Add("detaileDefinition4K", DownloadDefinition.P_4K);
            definitionDict.Add("detaileDefinition720P", DownloadDefinition.P_720);
            definitionDict.Add("detaileDefinitionGQ", DownloadDefinition.P_GQ);
            definitionDict.Add("detaileDefinitionLC", DownloadDefinition.P_LC);
        }

        static void Main(string[] args)
        {
            InitDefinitionKeyValues();
            string a = definitionDict.Where(keyvalue => keyvalue.Value == DownloadDefinition.P_1080).ElementAt(0).Key;
        }

    }
}
