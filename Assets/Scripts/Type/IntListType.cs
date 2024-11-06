using GoogleSheet.Type;

using System.Collections.Generic;

namespace Hamster.ZG.Type
{
    [Type(typeof(List<int>), new string[] { "list<int>", "List<int>" })]
    public class IntListType : IType
    {
        public object DefaultValue => null;

        public object Read(string value)
        {
            var list = new List<int>();
            if (value == "[]") return list;

            var datas = ReadUtil.GetBracketValueToArray(value);
            if (datas != null)
            {
                foreach (var data in datas)
                    list.Add(int.Parse(data));
            }
            return list;
        }

        public string Write(object value)
        {
            var list = value as List<int>;
            return WriteUtil.SetValueToBracketArray(list);
        }
    }
}