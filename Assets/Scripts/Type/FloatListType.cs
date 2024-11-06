using GoogleSheet.Type;

using System.Collections.Generic;

namespace Hamster.ZG.Type
{
    [Type(typeof(List<int>), new string[] { "list<float>", "List<float>" })]
    public class FloatListType : IType
    {
        public object DefaultValue => null;

        public object Read(string value)
        {
            var list = new List<float>();
            if (value == "[]") return list;

            var datas = ReadUtil.GetBracketValueToArray(value);
            if (datas != null)
            {
                foreach (var data in datas)
                    list.Add(float.Parse(data));
            }
            return list;
        }

        public string Write(object value)
        {
            var list = value as List<float>;
            return WriteUtil.SetValueToBracketArray(list);
        }
    }
}