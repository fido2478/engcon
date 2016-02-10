using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;


namespace SystemUtils
{
    class Registry
    {
        #region Constants and enums
        const int REG_NONE = 0;                         // No value type
        const int REG_SZ = 1;                           // Unicode nul terminated string
        const int REG_EXPAND_SZ = 2;                    // Unicode nul terminated string
        const int REG_BINARY = 3;                       // Free form binary
        const int REG_DWORD = 4;                        // 32-bit number
        const int REG_DWORD_LITTLE_ENDIAN = 4;          // 32-bit number (same as REG_DWORD)
        const int REG_DWORD_BIG_ENDIAN = 5;             // 32-bit number
        const int REG_LINK = 6;                         // Symbolic Link (unicode)
        const int REG_MULTI_SZ = 7;                     // Multiple Unicode strings
        const int REG_RESOURCE_LIST = 8;                // Resource list in the resource map
        const int REG_FULL_RESOURCE_DESCRIPTOR = 9;     // Resource list in the hardware description
        const int REG_RESOURCE_REQUIREMENTS_LIST = 10;
        public struct FILETIME
        {
        };

        // base registry keys
        public enum RootKey : uint
        {
            ClassesRoot = 0x80000000,
            CurrentUser = 0x80000001,
            LocalMachine = 0x80000002,
            Users = 0x80000003
        }
        #endregion

        class WinApi
        {
            #region Registry API imports

            [DllImport("coredll.dll")]
            public static extern int RegOpenKeyEx(
                uint hKey, string lpSubKey, int ulOptions,
                int samDesired, ref uint phkResult);

            [DllImport("coredll.dll")]
            public static extern int RegCloseKey(uint hKey);

            [DllImport("coredll.dll")]
            public static extern int RegQueryValueEx(
                uint hKey, string lpValueName, int lpReserved,
                out int lpType, byte[] lpData, ref int lpcbData);

            [DllImport("coredll.dll")]
            public static extern int RegEnumKeyEx(uint hKey, int dwIndex,
                string lpName, ref int lpcbName, int lpReserved,
                string lpClass, ref int lpcbClass, ref FILETIME lpftLastWriteTime);

            [DllImport("coredll.dll")]
            public static extern int RegEnumValue(uint hKey, int dwIndex, string
                lpValueName, ref int lpcbValueName, int lpReserved, ref int lpType, byte[]
                lpData, ref int lpcbData);

            #endregion
        }

        // static class
        private Registry()
        {
        }
  
        /// <summary>
        /// Read specified registry location. Returns data as a byte array
        /// and caller methods can convert to different types.
        /// </summary>
        static private byte[] GetValue(RootKey rootKey, string keyName, string valueName, out int type)
        {
            byte[] data = null;	// data that is returned
            uint hKey = 0;		// handle to reg key
            int dataType = 0;
            try
            {
                // open registry key
                if (WinApi.RegOpenKeyEx((uint)rootKey, keyName, 0, 0, ref hKey) == 0)
                {
                    // get the size of the data
                    int dataSize = 0;
                    WinApi.RegQueryValueEx(hKey, valueName, 0, out dataType, null, ref dataSize);

                    // allocate room for data and read value
                    if (dataSize != 0)
                    {
                        data = new byte[dataSize];
                        WinApi.RegQueryValueEx(hKey, valueName, 0, out dataType, data, ref dataSize);
                    }

                }
            }
            finally
            {
                if (hKey != 0)
                    WinApi.RegCloseKey(hKey);
            }
            // make sure to pass out the datatype for use by calling functions
            type = dataType;
            return data;

        }

        // Enumerate the values of the registry and return them as coma delimited string
        // caller methods can parse
        static public string EnumValues(RootKey rootKey, string keyName, bool Type)
        {
            string names = null;
            uint hKey = 0;

            try
            {
                // open registry key
                if (WinApi.RegOpenKeyEx((uint)rootKey, keyName, 0, 0, ref hKey) == 0)
                {
                    // loop through the enumerations (assume no more than 100 values, to limit the for loop
                    for (int i = 0; i < 100; i++)
                    {
                        // create 255 character buffer
                        string ValueName = new String(' ',255);

                        // initialize the needed variables
                        int ValueSize = 255;
                        int dataType = 0;
                        byte[] data = null;
                        int dataSize = 0;
                        int retval = 0;
                        string keyClass = null;
                        int keyClassSize = 0;
                        FILETIME lastWrite = new FILETIME();

                        // if Type = 0/false, enumerate values
                        // if Type = 1/true, enumerate keys
                        if (!Type)
                        {
                            retval = WinApi.RegEnumValue((uint)hKey, i, ValueName, ref ValueSize, 0, ref dataType, data, ref dataSize);
                        }
                        else
                        {
                            retval = WinApi.RegEnumKeyEx((uint)hKey, i, ValueName, ref ValueSize, 0, keyClass, ref keyClassSize, ref lastWrite);
                        }
                        // check to see if we got anything
                        if (retval == 0 || retval == 87)
                        {
                            // If the values were enumerated, then tag on their data
                            // else just add the name of the key
                            if (!Type)
                            {
                                // read the data and tag onto the name of the value as hex
                                int type = new int();
                                byte[] tempData = GetValue(RootKey.LocalMachine, keyName, ValueName.Substring(0, ValueSize), out type);
                                names += ValueName.Substring(0, ValueSize);
                                
                                // make sure to handle the datatype correctly
                                // for ints display the number, for strings display the converted text
                                switch (type)
                                {
                                    case REG_SZ:
                                        string result = UnicodeEncoding.Unicode.GetString(tempData, 0, tempData.GetLength(0)-1);
                                        names += "(" + result + "),";
                                        break;
                                    case REG_DWORD:
                                        int Num = System.BitConverter.ToInt32(tempData, 0);
                                        names += "(0x" + Num.ToString("X") + ")(d" + Num.ToString() + "),";
                                        //names += "(0x" + Num.ToString("X") + "),";
                                        break;
                                    default:
                                        break;
                                }

                            }
                            else
                            {
                                names += ValueName.Substring(0, ValueSize);
                                names += ",";
                            }

                        }
                        else
                        {
                            // set i to 100 to stop the looping
                            // if retval != 0 then we're done enumerating
                            i = 100;
                        }

                    }
                }
            }
            catch
            {
                // return empty string
                return "";
            }
            // close the key

            if (hKey != 0)
                WinApi.RegCloseKey(hKey);

            return names;
        }
    }
}
