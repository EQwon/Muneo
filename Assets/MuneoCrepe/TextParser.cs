using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MuneoCrepe
{
    public class TextParser
    {
        public static List<string> Text2List(TextAsset data)
        {
            var returnList = new List<string>();

            var sr = new StringReader(data.text);
            var source = sr.ReadLine();     // 먼저 한줄을 읽는다.
            
            while (source != null)
            {
                returnList.Add(EnteredString(source));

                source = sr.ReadLine();    // 한줄 읽는다.
            }

            return returnList;
        }

        private static string EnteredString(string str)
        {
            var values = str.Split('\t');

            var sb = new StringBuilder();
            for (var i = 0; i < values.Length; i++)
            {
                sb.Append(values[i]);
                if (i != values.Length - 1)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
    }
}