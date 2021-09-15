using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Numerics;
using GameOverlay.Windows;
using GameOverlay.Drawing;
using System.Drawing;

namespace Cheat
{
    class misc
    {
        //version (wichtig für versions check)
        public static string Version = "v0.1";
        public static int ScreenX;
        public static int ScreenY;

        [DllImport("user32.dll")]
        public static extern void SetWindowSize(int width, int height);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr SetFocus(HandleRef hWnd);

        [DllImport("user32.dll")]
        private static extern ushort GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);


        public static bool IsKeyDown(Keys key)
        {
            return 0 != (GetAsyncKeyState((int)key) & 0x8000);
        }
        public struct POINT
        {
            public static implicit operator System.Drawing.Point(POINT point)
            {
                return new System.Drawing.Point(point.X, point.Y);
            }

            public int X;

            public int Y;
        }

        //gibt die cursor position aus
        public static System.Drawing.Point GetCursorPosition()
        {
            POINT point;
            GetCursorPos(out point);
            return point;
        }

        //wird für den aimbot benutzt bewegt die maus auf dei angegebene position
        public static void AimAtPos(float x, float y)
        {
            float num = (x - 960f) / 2f;
            float num2 = (y - 540f) / 2f;
            float num3 = 0f;
            float num4 = 0f;
            /*Random random = new Random();
            new Random();
            if (random.Next(0, 3) >= 1)
            {
                num3 += (float)random.Next(0, 4);
                num4 += (float)random.Next(0, 7);
            }
            else
            {
                 num3 -= (float)random.Next(0, 4);
                 num4 -= (float)random.Next(0, 7);
            }*/
            mouse_event(1U, (uint)num + (uint)num3, (uint)num2 + (uint)num4, 0U, UIntPtr.Zero);
            Thread.Sleep(5);
        }

        //nimmt den richtigen prozess z.B FiveM oder GTA je nach dem 
        public static string obtainProcess()
        {
            string proc = "none";
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                if (process.ProcessName.Contains("GTA5") || process.ProcessName.Contains("FiveM_GTAProcess") || process.ProcessName.Contains("FiveM_b2189_GTAProcess") || process.ProcessName.Contains("GTAProcess"))
                {
                    proc = process.ProcessName;
                }
            }
            return proc;
        }

        //checkt ob das spiel läuft in dem fall gta
        public static bool isGameRunning(string title)
        {
            if (Process.GetProcessesByName(title).Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //gettet die hwid 
        public static string GetMachineGuid()
        {
            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";

            using (RegistryKey localMachineX64View =
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null)
                        throw new KeyNotFoundException(
                            string.Format("Key Not Found: {0}", location));

                    object machineGuid = rk.GetValue(name);
                    if (machineGuid == null)
                        throw new IndexOutOfRangeException(
                            string.Format("Index Not Found: {0}", name));

                    return machineGuid.ToString();
                }
            }
        }

        //versions check 
        public static void hwidcheck(string hwid)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string request = client.DownloadString("https://pastebin.com/raw/e2mztD1b");
                    if (!request.Contains(hwid))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Deine HWID ist nicht freigeschaltet! HWID:" + misc.GetMachineGuid());
                        Thread.Sleep(5000);
                        System.Environment.Exit(0);
                    }
                }
                catch (WebException e)
                {
                    System.Environment.Exit(0);
                    throw e;
                }
            }
        }

        public static void checkVersion()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string request = client.DownloadString("https://pastebin.com/raw/W2dpxh1c");
                    if (!request.Contains(Version))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Deine Version ist abgelaufen! Frag im Discord nach! https://discord.gg/6MkWrg3q97");
                        Thread.Sleep(5000);
                        System.Environment.Exit(0);
                    }
                }
                catch (WebException e)
                {
                    System.Environment.Exit(0);
                    throw e;
                }
            }
        }

        //für esp wichtig name erklärt alles
        public static bool WorldToScreen(Vector3 posIn, out Vector3 posOut, Matrix4x4 matrixArg = default(Matrix4x4))
		{
			posOut = default(Vector3);
			Matrix4x4 matrix;
			if (matrixArg == default(Matrix4x4))
			{
				matrix = Program.GetMatrix();
			}
			else
			{
				matrix = matrixArg;
			}
			Matrix4x4 matrix4x = Matrix4x4.Transpose(matrix);
			Vector4 vector = new Vector4
			{
				X = matrix4x.M41,
				Y = matrix4x.M42,
				Z = matrix4x.M43,
				W = matrix4x.M44
			};
			Vector4 vector2 = new Vector4
			{
				X = matrix4x.M21,
				Y = matrix4x.M22,
				Z = matrix4x.M23,
				W = matrix4x.M24
			};
			Vector4 vector3 = new Vector4
			{
				X = matrix4x.M31,
				Y = matrix4x.M32,
				Z = matrix4x.M33,
				W = matrix4x.M34
			};
			posOut.Z = vector.X * posIn.X + vector.Y * posIn.Y + vector.Z * posIn.Z + vector.W;
			posOut.X = vector2.X * posIn.X + vector2.Y * posIn.Y + vector2.Z * posIn.Z + vector2.W;
			posOut.Y = vector3.X * posIn.X + vector3.Y * posIn.Y + vector3.Z * posIn.Z + vector3.W;
			if (posOut.Z < 0.001f)
			{
				return false;
			}
			float num = 1f / posOut.Z;
			posOut.Y *= num;
			posOut.X *= num;
			int num2 = 1920;
			int num3 = 1080;
			int num4 = num2 / 2;
			int num5 = num3 / 2;
			num4 += (int)(0.5 * (double)posOut.X * (double)num2 + 0.5);
			num5 -= (int)(0.5 * (double)posOut.Y * (double)num3 + 0.5);
			posOut.X += (float)num4;
			posOut.Y = (float)num5;
			return true;
		}
    }
}
