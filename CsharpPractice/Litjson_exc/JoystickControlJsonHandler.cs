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
            Print(string.Format("{0} cannot be found", ControllerPath));
            return null;
        }
        catch (Exception e2)
        {
            Print(string.Format("Error happened when read file from {0}, error is {1}", ControllerPath, e2.Message));
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
        return JsonMapper.ToObject(ReadJoystickControlJson());
    }

    public static List<string> GetJoystickNames()
    {
        List<string> rt = new List<string>();
        JsonData jd = GetJsonDatas();
        if (jd.IsArray == false)
        {
            rt.Add((string)jd["JoystickControllerName"]);
            return rt;
        }
        foreach (JsonData j in GetJsonDatas())
        {
            rt.Add((string)j["JoystickControllerName"]);
        }
        return rt;
    }

    public static JoystickControlKeyInfo GetJoystickContollerKeyInfoByName(string joystickName)
    {
        if (GetJoystickNames().Contains(joystickName) == false)
            return null;
        JsonData jd = null;
        foreach (JsonData j in GetJsonDatas())
        {
            if (((string)j["JoystickControllerName"]).Equals(joystickName))
            {
                jd = j;
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
        foreach(JsonData j in allJDs)
        {
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
        string conf = string.Empty;
#if UNITY_ANDROID && !UNITY_EDITOR
            conf = @"
            [{
                'JoystickControllerName': 'iJoy Controller',
                'KeyConfirm': {
                'keyAim': 1,
                'axisKey': 'JoystickButton0',
                'keyValue': 0
                },
                'KeyReturn': {
                'keyAim': 2,
                'axisKey': 'JoystickButton1',
                'keyValue': 0
                },
            'KeyUp': {
                'keyAim': 3,
                'axisKey': 'JoystickAxis1',
                'keyValue': 1
                },
            'KeyDown': {
                'keyAim': 4,
                'axisKey': 'JoystickAxis1',
                'keyValue': 2
                },
            'KeyLeft': {
                'keyAim': 5,
                'axisKey': 'JoystickAxis2',
                'keyValue': 2
                },
            'KeyRight': {
                'keyAim': 6,
                'axisKey': 'JoystickAxis2',
                'keyValue':1
                }
            }]
        ";
#endif
#if UNITY_IOS
        conf = @"[{
                'JoystickControllerName': 'iJoy Controller',
                'KeyConfirm': {
                'keyAim': 1,
                'axisKey': 'H',
                'keyValue': 0
                },
                'KeyReturn': {
                'keyAim': 2,
                'axisKey': 'ESC',
                'keyValue': 0
                },
            'KeyUp': {
                'keyAim': 3,
                'axisKey': 'W',
                'keyValue': 1
                },
            'KeyDown': {
                'keyAim': 4,
                'axisKey': 'X',
                'keyValue': 2
                },
            'KeyLeft': {
                'keyAim': 5,
                'axisKey': 'A',
                'keyValue': 2
                },
            'KeyRight': {
                'keyAim': 6,
                'axisKey': 'D',
                'keyValue':1
                }
            }]
        ";
#endif
        WriteJoystickJson(conf);
    }

    static void Print(string str)
    {
        System.Console.WriteLine(str);
    }
}
