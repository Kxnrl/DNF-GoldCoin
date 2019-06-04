using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DNF_Gold.Win32Api
{
    class Window
    {
        public const uint SW_HIDE = 0;
        public const uint SW_SHOW = 1;

        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        internal static extern bool ShowWindow(IntPtr hwnd, uint nCmdShow);
    }

    class Profile
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filepath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        public static string GetIniSectionValue(string file, string section, string key, string defaultVal = null)
        {
            var stringBuilder = new StringBuilder(1024);
            GetPrivateProfileString(section, key, defaultVal, stringBuilder, 1024, file);
            return stringBuilder.ToString();
        }

        public static bool SetIniSectionValue(string file, string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, file);
        }
    }
}
