using System;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using System.IO;
using System.Text;

    public class JoystickControlJsonHandler
    {
        private static string ControllerPath = "joystick.json";
        private static object myLock = new object();
        private JoystickControlJsonHandler inst;
        private JoystickControlJsonHandler() { }
        public JoystickControlJsonHandler GetInstance()
        {
            lock (myLock)
            {
                if (inst == null)
                {
                    inst = new JoystickControlJsonHandler();
                }
                return inst;
            }
        }

        public static string ReadJoystickControlJson()
        {
            string rt = string.Empty;
            try
            {
                rt = File.ReadAllText(ControllerPath);
            }
            catch (IOException e)
            {
                return null;
            }
            catch (Exception e2)
            {
                return null;
            }
            return rt;
        }

        public static string GetJsonStrContent()
        {
            return ReadJoystickControlJson();
        }

        public static JsonData GetJsonDatas()
        {
            string jsonString = ReadJoystickControlJson();
            if (jsonString != null)
            {
                return JsonMapper.ToObject(jsonString);
            }
            return null;
        }

        public static List<string> GetJoystickNames()
        {
            List<string> rt = new List<string>();
            JsonData jd = GetJsonDatas();

            if (jd == null)
            {
                return null;
            }

            if (jd.IsArray == false)
            {
                rt.Add((string)jd["JoystickControllerName"]);
                return rt;
            }

            JsonData allJDs = GetJsonDatas();
            for (int i = 0; i < allJDs.Count; i++)
            {
                rt.Add((string)allJDs[i]["JoystickControllerName"]);
            }

            return rt;
        }

        public static JoystickControlKeyInfo GetJoystickContollerKeyInfoByName(string joystickName)
        {
            if (GetJoystickNames().Contains(joystickName) == false)
                return null;
            JsonData jd = null;
            JsonData allJDs = GetJsonDatas();
            for (int i = 0; i < allJDs.Count; i++)
            {
                if (((string)allJDs[i]["JoystickControllerName"]).Equals(joystickName))
                {
                    jd = allJDs[i];
                    break;
                }
            }

            return JoystickControlKeyInfo.ReadFromString(jd.ToJson());
        }

        /// <summary>
        /// Will over write with new jd
        /// </summary>
        /// <param name="jd"></param>
        public static void OverwriteJoystickConf(JsonData jd)
        {
            JsonData allJDs = GetJsonDatas();
            JsonData jdList = new JsonData();
            jdList.SetJsonType(JsonType.Array);
            jdList.Add(jd);
            for (int i = 0; i < allJDs.Count; i++)
            {
                JsonData j = (JsonData)allJDs[i];
                if (((string)j["JoystickControllerName"]).Equals((string)jd["JoystickControllerName"]))
                    continue;
                jdList.Add(j);
            }
            WriteJoystickJson(JsonMapper.ToJson(jdList));
        }

        public static void WriteJoystickJson(string alljson)
        {
            if (File.Exists(ControllerPath))
                File.Delete(ControllerPath);
            FileStream stream = new FileStream(ControllerPath, FileMode.CreateNew, FileAccess.Write);
            byte[] toBytes = Encoding.ASCII.GetBytes(alljson);
            stream.Write(toBytes, 0, toBytes.Length);
            stream.Flush();
            stream.Close();
        }
        /// <summary>
        /// The default joystick controller: iJoy Controller
        /// </summary>
        public static void WriteDefaultJoystickConfig()
        {
            if (File.Exists(ControllerPath))
                return;
            string conf = string.Empty;

        conf = @"[{
                'JoystickControllerName': 'iQIYI VR',
                'KeyConfirm': {
                'keyAim': 1,
                'axisKey': 'enter',
                'keyValue': 0
                },
                'KeyReturn': {
                'keyAim': 2,
                'axisKey': 'esc',
                'keyValue': 0
                },
            'KeyUp': {
                'keyAim': 3,
                'axisKey': 'up',
                'keyValue': 1
                },
            'KeyDown': {
                'keyAim': 4,
                'axisKey': 'down',
                'keyValue': 2
                },
            'KeyLeft': {
                'keyAim': 5,
                'axisKey': 'left',
                'keyValue': 2
                },
            'KeyRight': {
                'keyAim': 6,
                'axisKey': 'right',
                'keyValue':1
                }
            }]
        ";
            WriteJoystickJson(conf);
        }
    }
