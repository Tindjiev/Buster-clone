using UnityEngine;

namespace InputNM
{

    public struct Inputstruct
    {
        private KeyCode[] _mainkeys; //at least 1 mainkey must be pressed
        private KeyCode[] _secondaryKeys; //all secondaries keys must be holded during the press of a mainkey
        public MethodTypes.boolfunctionKeycode InputMethod { get; private set; }
        public int TimesNeeded { get; private set; }
        
        private int _timesLeft;
        private float _lastTime;

        const float TIME_DIFFERENCE = 0.3f;

        //private string tostring;

        public bool CheckInput()
        {
            if (checkinputkey())
            {
                if (TimesNeeded == 1) //if it is a one-time press then don't bother to check other stuff
                {
                    return true;
                }
                //               Debug.Log(Time.time - _lastTime);
                if (_timesLeft == TimesNeeded) // this case checks if it's the first time pressed in a queue
                {
                    _timesLeft--;
                    _lastTime = Time.time;
                    return false;
                }
                else if (Time.time - _lastTime < TIME_DIFFERENCE) // if it's not the first time then check if it is on time
                {
                    if (_timesLeft <= 1)  // if it is on time and only 1 time was left then its the last of the queue so next time its pressed it should be treated as first time, thus _timesLeft=TimesNeeded
                    {                      // ideally its _timesLeft==1, but just in case...
                        if (_timesLeft != 1)
                        {
                            Debug.Break();
                            Debug.Log(_timesLeft + " times_left != 1");
                        }
                        _timesLeft = TimesNeeded;
                        _lastTime = Time.time;
                        return true;
                    }
                    else  //if _timesLeft is greater than 1 then move on normally by subbing 1 from _timesLeft
                    {
                        _timesLeft--;
                        _lastTime = Time.time;
                        return false;
                    }
                }
                else //fail to press again withing time means a new queue has started thus _timesLeft is 1 lower than TimesNeeded
                {
                    _timesLeft = TimesNeeded - 1;
                    _lastTime = Time.time;
                    return false;
                }
            }
            return false;
        }

        bool checkinputkey()
        {
            if (_secondaryKeys.Length == 0)
            {
                for (int i = 0; i < _mainkeys.Length; i++)
                {
                    if (InputMethod(_mainkeys[i]))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                bool tf = false;
                for (int i = 0; i < _mainkeys.Length; i++)
                {
                    if (InputMethod(_mainkeys[i]))
                    {
                        tf = true;
                    }
                }
                if (!tf)
                {
                    return false;
                }
            }
            for (int i = 0; i < _secondaryKeys.Length; i++)
            {
                if (!Input.GetKey(_secondaryKeys[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            string temp = "";
            for (int i = 0; i < _secondaryKeys.Length; i++)
            {
                temp += KeyToString(_secondaryKeys[i]) + " + ";
            }
            if (_mainkeys.Length != 0)
            {
                temp += KeyToString(_mainkeys[0]);
            }
            return temp;
        }

        static string KeyToString(KeyCode key)
        {
            if (KeyCode.Alpha0 <= key && key <= KeyCode.Alpha9)
            {
                return ((int)key - (int)KeyCode.Alpha0).ToString();
            }
            else if (KeyCode.Mouse0 <= key && key <= KeyCode.Mouse2)
            {
                switch (key)
                {
                    case KeyCode.Mouse0:
                        return "LClick";
                    case KeyCode.Mouse1:
                        return "RClick";
                    case KeyCode.Mouse2:
                        return "MClick";
                    default:
                        return "UknClick";
                }
                /*if (key == KeyCode.Mouse0)
                {
                    return "LClick";
                }
                else if (key == KeyCode.Mouse1)
                {
                    return "RClick";
                }
                else
                {
                    return "MClick";
                }*/
            }
            else if (key == KeyCode.Return || key == KeyCode.KeypadEnter)
            {
                return "Enter";
            }
            else if (key == KeyCode.LeftControl || key == KeyCode.RightControl)
            {
                return "Ctrl";
            }
            else if (key == KeyCode.LeftAlt || key == KeyCode.RightAlt)
            {
                return "Alt";
            }
            else
            {
                return key.ToString();
            }
        }

        public Inputstruct(MethodTypes.boolfunctionKeycode InputMethod)
        {
            this.InputMethod = InputMethod;
            this._mainkeys = new KeyCode[0];
            this._secondaryKeys = new KeyCode[0];
            this.TimesNeeded = 1;
            this._timesLeft = 1;
            this._lastTime = 0f;
        }

        public Inputstruct(MethodTypes.boolfunctionKeycode InputMethod, KeyCode[] keys) : this(InputMethod)
        {
            this._mainkeys = (KeyCode[])keys.Clone();
        }

        public Inputstruct(params KeyCode[] keys) : this(Input.GetKey, keys)
        {
        }

        public Inputstruct(MethodTypes.boolfunctionKeycode InputMethod, KeyCode[] _mainkeyss, KeyCode[] _secondaryKeys) : this(InputMethod, _mainkeyss)
        {
            this._secondaryKeys = (KeyCode[])_secondaryKeys.Clone();
        }

        public Inputstruct(MethodTypes.boolfunctionKeycode InputMethod, KeyCode key) : this(InputMethod, new KeyCode[] { key })
        {
        }

        public Inputstruct(KeyCode key) : this(Input.GetKey, key)
        {
        }

        public Inputstruct(MethodTypes.boolfunctionKeycode InputMethod, KeyCode Mainkey, KeyCode[] _secondaryKeys) : this(InputMethod, new KeyCode[] { Mainkey }, _secondaryKeys)
        {
        }

        public Inputstruct(MethodTypes.boolfunctionKeycode InputMethod, KeyCode key, int times) : this(InputMethod, key)
        {
            this._timesLeft = this.TimesNeeded = times;
        }

        public Inputstruct(MethodTypes.boolfunctionKeycode InputMethod, KeyCode[] keys, int times) : this(InputMethod, keys)
        {
            this._timesLeft = this.TimesNeeded = times;
        }

        public Inputstruct(MethodTypes.boolfunctionKeycode InputMethod, KeyCode Mainkey, KeyCode[] _secondaryKeys, int times) : this(InputMethod, Mainkey, _secondaryKeys)
        {
            this._timesLeft = this.TimesNeeded = times;
        }

        public Inputstruct(MethodTypes.boolfunctionKeycode InputMethod, KeyCode[] _mainkeys, KeyCode[] _secondaryKeys, int times) : this(InputMethod, _mainkeys, _secondaryKeys)
        {
            this._timesLeft = this.TimesNeeded = times;
        }
    }





}