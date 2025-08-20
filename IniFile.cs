using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HandleLeveler
{
    public class IniFile
    {
        private string _filePath;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string defualtValue,
            StringBuilder value, int size, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern bool WritePrivateProfileString(string section, string key, string value, string filePath);

        public IniFile(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(filePath))
            {
                try
                {
                    string directory = Path.GetDirectoryName(filePath) ?? string.Empty;

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    File.WriteAllText(filePath, string.Empty);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"INI 파일 생성 중 오류 : {ex.Message}");
                }
            }
        }

        public string ReadString(string section, string key, string defaultValue = "")
        {
            StringBuilder value = new StringBuilder(255);

            GetPrivateProfileString(section, key, defaultValue, value, 255, _filePath);

            return value.ToString();
        }

        public int ReadInteger(string section, string key, int defaultValue = 0)
        {
            string value = ReadString(section, key, defaultValue.ToString());

            if (int.TryParse(value, out int result))
                return result;

            return defaultValue;
        }

        public double ReadDouble(string section, string key, double defaultValue = 0.0)
        {
            string value = ReadString(section, key, defaultValue.ToString());

            if (double.TryParse(value, out double result))
                return result;

            return defaultValue;
        }

        public bool ReadBoolean(string section, string key, bool defaultValue = false)
        {
            string value = ReadString(section, key, defaultValue.ToString());

            if (bool.TryParse(value, out bool result))
                return result;

            return defaultValue;
        }

        public bool WriteValue(string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, _filePath);
        }
    }
}
