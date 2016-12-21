using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
public class JoystickControlKeyInfo
{
    public enum KeyValue
    {
        NONE, Positive, Negative,
    }

    public enum KeyFunction
    {
        NONE,
        Confirm,
        Return,
        Up,
        Down,
        Left,
        Right,
    }
    /// <summary>
    /// Represent a key record
    /// keyAim would map to VRInput.cs's function
    /// </summary>
    public class JoystickKey
    {
        public KeyFunction keyAim; //which key to set
        public string axisKey; //which unity axis/key is mapped to this keyAim
        public KeyValue keyValue;//which key button to read, it's NONE if it's key
        public JoystickKey()
        {
            keyAim = KeyFunction.NONE;
            axisKey = "";
            keyValue = KeyValue.NONE;
        }
        public JoystickKey(KeyFunction func, string axis, KeyValue kv)
        {
            keyAim = func;
            axisKey = axis;
            keyValue = kv;
        }
        public void SetValues(KeyFunction func, string axis, KeyValue kv)
        {
            keyAim = func;
            axisKey = axis;
            keyValue = kv;
        }
        public void SetValues(JoystickKey jk)
        {
            keyAim = jk.keyAim;
            axisKey = jk.axisKey;
            keyValue = jk.keyValue;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("keyAim: {0} - axisKey {1} - keyValue {2}", keyAim, axisKey, keyValue));
            return sb.ToString();
        }
    }

    private string mJoystickControllerName = "Controller";
    public string JoystickControllerName
    {
        get { return mJoystickControllerName; }
        set { mJoystickControllerName = value; }
    }
    private JoystickKey mKeyConfirm = new JoystickKey();
    public JoystickKey KeyConfirm
    {
        get { return mKeyConfirm; }
        set { mKeyConfirm = value; }
    }
    private JoystickKey mKeyReturn = new JoystickKey();
    public JoystickKey KeyReturn
    {
        get { return mKeyReturn; }
        set { mKeyReturn = value; }
    }
    private JoystickKey mKeyUp = new JoystickKey();
    public JoystickKey KeyUp
    {
        get { return mKeyUp; }
        set { mKeyUp = value; }
    }
    private JoystickKey mKeyDown = new JoystickKey();
    public JoystickKey KeyDown
    {
        get { return mKeyDown; }
        set { mKeyDown = value; }
    }
    private JoystickKey mKeyLeft = new JoystickKey();
    public JoystickKey KeyLeft
    {
        get { return mKeyLeft; }
        set { mKeyLeft = value; }
    }
    private JoystickKey mKeyRight = new JoystickKey();
    public JoystickKey KeyRight
    {
        get { return mKeyRight; }
        set { mKeyRight = value; }
    }
    /// <summary>
    /// Given a axis, and kv, if the already exist in key info obj
    /// </summary>
    /// <param name="axis"></param>
    /// <param name="kv"></param>
    /// <returns></returns>
    public bool KeyAlreadyExist(string axis, KeyValue value)
    {
        if (KeyConfirm.axisKey.Equals(axis) && KeyConfirm.keyValue == value)
            return true;
        if (KeyDown.axisKey.Equals(axis) && KeyDown.keyValue == value)
            return true;
        if (KeyLeft.axisKey.Equals(axis) && KeyLeft.keyValue == value)
            return true;
        if (KeyReturn.axisKey.Equals(axis) && KeyReturn.keyValue == value)
            return true;
        if (KeyRight.axisKey.Equals(axis) && KeyRight.keyValue == value)
            return true;
        if (KeyUp.axisKey.Equals(axis) && KeyUp.keyValue == value)
            return true;
        return false;
    }

    /// <summary>
    /// Read from json string
    /// json example
    /*
        {
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
    }
    */
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static JoystickControlKeyInfo ReadFromString(string json)
    {
        JoystickControlKeyInfo ji = new JoystickControlKeyInfo();
        try
        {
            JsonData jd = JsonMapper.ToObject(json);
            ji.JoystickControllerName = jd[0].ToString();
            ji.KeyConfirm = JsonMapper.ToObject<JoystickKey>(jd["KeyConfirm"].ToJson());
            ji.KeyDown = JsonMapper.ToObject<JoystickKey>(jd["KeyDown"].ToJson());
            ji.KeyLeft = JsonMapper.ToObject<JoystickKey>(jd["KeyLeft"].ToJson());
            ji.KeyReturn = JsonMapper.ToObject<JoystickKey>(jd["KeyReturn"].ToJson());
            ji.KeyRight = JsonMapper.ToObject<JoystickKey>(jd["KeyRight"].ToJson());
            ji.KeyUp = JsonMapper.ToObject<JoystickKey>(jd["KeyUp"].ToJson());
            return ji;
        }
        catch (Exception e)
        {
            Print(e.Message);
            return null;
        }
    }
    
    public JsonData ToJsonData()
    {
        JsonReader jr = new JsonReader(ToJsonString());
        return JsonMapper.ToObject(jr);    
    }
    public string ToJsonString()
    {
        string rt = JsonMapper.ToJson(this).Trim();
        return rt;
    }
    
    public override string ToString()
    {
        return this.JoystickControllerName;
    }
    /// <summary>
    /// If keyConfirm and keyReturn is set, the key info is basically usable.
    /// </summary>
    /// <returns></returns>
    public bool BasicKeyReady()
    {
        bool ready = !KeyConfirm.axisKey.Equals("") && !KeyReturn.axisKey.Equals("");
        Print("Check joystick basic key ready:" + ready);

        return ready;
    }

    /// <summary>
    /// If confirm/return/left/right/up/down is ready
    /// </summary>
    /// <returns></returns>
    public bool AllKeyReady()
    {
        bool ready = !KeyConfirm.axisKey.Equals("")
            && !KeyReturn.axisKey.Equals("")
            && !KeyRight.axisKey.Equals("")
            && !KeyLeft.axisKey.Equals("")
            && !KeyUp.axisKey.Equals("")
            && !KeyDown.axisKey.Equals("");
        Print("Check joystick full key ready:" + ready);
        return ready;

    }
    static void Print(string str)
    {
        System.Console.WriteLine(str);
    }

}
