using UnityEngine;
using GoogleSheet.Type;

namespace Hamster.ZG.Type
{
    [Type(typeof(Vector3), new string[] { "vector3", "Vector3", "vec3" })]
    public class Vector3Type : IType
    {
        public object DefaultValue => Vector3.zero;
        /// <summary>
        /// value는 스프레드 시트에 적혀있는 값
        /// </summary> 
        public object Read(string value)
        {
            // value : [1,2,3] 
            var values = ReadUtil.GetBracketValueToArray(value);
            float x = float.Parse(values[0]);
            float y = float.Parse(values[1]);
            float z = float.Parse(values[2]);
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// value write to google sheet
        /// </summary> 
        public string Write(object value)
        {
            Vector3 v = (Vector3)value;
            return $"[{v.x},{v.y},{v.z}]";
        }
    }
}
