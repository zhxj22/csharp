using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;
using System.IO;
namespace Litjson_exc
{
    class Program
    {
        static string mFileName = "joystick.json";
        static void Main(string[] args)
        {
            List<string> names = JoystickControlJsonHandler.GetJoystickNames();
            JoystickControlKeyInfo keyInfo = JoystickControlJsonHandler.GetJoystickContollerKeyInfoByName("iJoy Controller");
            string json = JoystickControlJsonHandler.GetJsonStrContent();
            JsonData jd = JoystickControlJsonHandler.GetJsonDatas();
            JsonData newJD = keyInfo.ToJsonData();
            newJD["KeyConfirm"] = "hello";
            JoystickControlJsonHandler.OverwriteJoystickConf(newJD);
            //string json = File.ReadAllText(mFileName).Replace("\r\n", "");
            //JoystickControlKeyInfo ji = new JoystickControlKeyInfo();

            //JsonData jd = JsonMapper.ToObject(json);
            //List<string> rt = new List<string>();
            //foreach (JsonData j in jd)
            //{
            //   rt.Add((string)j["JoystickControllerName"]);
            //}

            //JsonData data = jd[0];
            //ji.JoystickControllerName = (string)data["JoystickControllerName"];
            //ji.KeyConfirm = JsonMapper.ToObject<JoystickControlKeyInfo.JoystickKey>(data["KeyConfirm"].ToJson());
            //ji.KeyDown = JsonMapper.ToObject<JoystickControlKeyInfo.JoystickKey>(data["KeyDown"].ToJson());
            //ji.KeyLeft = JsonMapper.ToObject<JoystickControlKeyInfo.JoystickKey>(data["KeyLeft"].ToJson());
            //ji.KeyReturn = JsonMapper.ToObject<JoystickControlKeyInfo.JoystickKey>(data["KeyReturn"].ToJson());
            //ji.KeyRight = JsonMapper.ToObject<JoystickControlKeyInfo.JoystickKey>(data["KeyRight"].ToJson());
            //ji.KeyUp = JsonMapper.ToObject<JoystickControlKeyInfo.JoystickKey>(data["KeyUp"].ToJson());
            jd.Add(newJD);
            Print("xxxxxxxxxxxxxxxx");

            //JsonWriter writer1 = new JsonWriter(Console.Out);
        }

        static void Print(string str)
        {
            System.Console.WriteLine(str);
        }
    }
}
