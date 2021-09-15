using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Cheat
{
    public class overlay : IDisposable
	{
		private readonly GraphicsWindow _window;
		public static Dictionary<string, SolidBrush> _brushes = new Dictionary<string, SolidBrush>();
		public static Dictionary<string, Font> _fonts;
		public static Dictionary<string, Image> _images;
		public static bool guiOpen = false;
		public static bool drawable = false;
		public static bool mouseClick = false;
		public static bool canClick = true;
		//Other
		public static bool drawesp = false;
		public static bool aimbot = false;
		public static bool Info = false;
		//Player
		public static bool Godmode = false;
		public static bool RunSpeed = false;
		public static bool SwimSpeed = false;
		public static bool Seatbalet = false;
		public static bool Radgoll = false;
		public static bool SuperJump = false;
		//Vehicle
		public static bool VehicleSpeed = false;
		public static bool AutoVehicleHeal = false;
		public static bool NoColission = false;
		//Weapons
		public static bool Recoil = false;
		public static bool Spread = false;
		public static bool NoReaload = false;
		public static bool MaxDamage = false;

		public static Graphics drawGraphicsEvent;
		public overlay()
		{
			overlay._fonts = new Dictionary<string, Font>();
			overlay._images = new Dictionary<string, Image>();

			Graphics gfx = new Graphics()
			{
				MeasureFPS = true,
				PerPrimitiveAntiAliasing = true,
				TextAntiAliasing = true
			};

			this._window = new GraphicsWindow(0, 0, misc.ScreenX, misc.ScreenY, gfx)
			{
				FPS = 144,
				IsTopmost = true,
				IsVisible = true	
			};

			this._window.DestroyGraphics += _window_DestroyGraphics;
			this._window.DrawGraphics += _window_DrawGraphics;
			this._window.SetupGraphics += _window_SetupGraphics;
		}

		private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
		{
			//alle farben fonts usw
			Graphics graphics = e.Graphics;
			overlay._brushes["black"] = graphics.CreateSolidBrush(0, 0, 0, 255);
			overlay._brushes["blacktrans"] = graphics.CreateSolidBrush(0f, 0f, 0f, 0.08f);
			overlay._brushes["white"] = graphics.CreateSolidBrush(255, 255, 255, 255);
			overlay._brushes["red"] = graphics.CreateSolidBrush(255, 0, 0, 255);
			overlay._brushes["green"] = graphics.CreateSolidBrush(0, 255, 0, 255);
			overlay._brushes["blue"] = graphics.CreateSolidBrush(0, 0, 255, 255);
			overlay._brushes["lightblue"] = graphics.CreateSolidBrush(105, 105, 214);
			overlay._brushes["background"] = graphics.CreateSolidBrush(0f, 0f, 0f, 0.5f);
			overlay._brushes["white_background"] = graphics.CreateSolidBrush(255f, 255f, 255f, 0.5f);
			overlay._brushes["back"] = graphics.CreateSolidBrush(20f, 28f, 35f, 1f);
			overlay._brushes["grid"] = graphics.CreateSolidBrush(255f, 255f, 255f, 0.1f);
			overlay._brushes["random"] = graphics.CreateSolidBrush(0, 0, 0, 255);
			overlay._brushes["transparent"] = graphics.CreateSolidBrush(0, 0, 0, 0);
			overlay._brushes["bag"] = graphics.CreateSolidBrush(26, 26, 26, 255);
			overlay._fonts["arial"] = graphics.CreateFont("Arial", 20f, false, false, false);
			//overlay._images["Logo"] = graphics.CreateImage(ReadImageFile(@"C:\reversecheats\logo.png"));
		}

		private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
		{
			foreach (var pair in _brushes) pair.Value.Dispose();
			foreach (var pair in _fonts) pair.Value.Dispose();
			foreach (var pair in _images) pair.Value.Dispose();
		}
		private bool FindPoint(int x1, int y1, int x2, int y2, int x, int y)
		{
			if (x > x1 && x < x2 && y > y1 && y < y2)
				return true;

			return false;
		}

		bool TabPlayer = true;
		bool TabVehicle = false;
		bool TabWeapon = false;
		bool TabVisual = false;
		bool TabOther = false;
		private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			Graphics graphics = e.Graphics;
			overlay.drawGraphicsEvent = graphics;
			graphics.BeginScene();
			overlay.drawable = true;
			graphics.ClearScene();

			// ab hier menu rendern

			//das ist für das menu sprich auf / zu toggle
			if (misc.IsKeyDown(Keys.F11))
			{
				guiOpen = !guiOpen;
				Thread.Sleep(100);
			}
			//hier wird unter anderem das menu gerendert 
			if (guiOpen)
			{
				graphics.FillRoundedRectangle(overlay._brushes["bag"], (float)(0), (float)(1080), (float)(210), (float)(0), 3f);
				if (TabPlayer == true)
				{
					graphics.FillRoundedRectangle(overlay._brushes["bag"], (float)(misc.ScreenX / 2 - 230), (float)(misc.ScreenY / 4 + 300), (float)(misc.ScreenX / 2 + 330), (float)(misc.ScreenY / 4 + 30 + 5), 3f);
					if (this.drawCheckbox(e, 800, 350, "Aimbot", aimbot))
					{
						aimbot = !aimbot;
					}
					if (this.drawCheckbox(e, 800, 400, "Godmode", Godmode))
					{
						Godmode = !Godmode;
						//Program.setGodmode();
					}
					if (this.drawCheckbox(e, 800, 450, "Ragdoll", Radgoll))
					{
						Radgoll = !Radgoll;
					}
					if (this.drawCheckbox(e, 800, 500, "Run Speed", RunSpeed))
					{
						RunSpeed = !RunSpeed;
					}
					if (this.drawCheckbox(e, 950, 350, "Swim Speed", SwimSpeed))
					{
						SwimSpeed = !SwimSpeed;
					}
					/*if (this.drawCheckbox(e, 950, 400, "Super Jump", SuperJump))
					{
						SuperJump = !SuperJump;
						//Program.setGodmode();
					}*/
				} else if(TabVehicle == true)
                {
					graphics.FillRoundedRectangle(overlay._brushes["bag"], (float)(misc.ScreenX / 2 - 230), (float)(misc.ScreenY / 4 + 300), (float)(misc.ScreenX / 2 + 330), (float)(misc.ScreenY / 4 + 30 + 5), 3f);
					if (this.drawCheckbox(e, 800, 350, "Vehicle Speed", VehicleSpeed))
					{
						VehicleSpeed = !VehicleSpeed;
						//Program.setVehicleSpeed();
					}
					if (this.drawCheckbox(e, 800, 400, "Seatbalet", Seatbalet))
					{
						Seatbalet = !Seatbalet;
					}
					if (this.drawCheckbox(e, 800, 450, "Vehicle Autoheal", AutoVehicleHeal))
					{
						AutoVehicleHeal = !AutoVehicleHeal;
					}
					if (this.drawCheckbox(e, 800, 500, "No Colission", NoColission))
					{
						NoColission = !NoColission;
					}
				} else if(TabWeapon == true)
                {
					graphics.FillRoundedRectangle(overlay._brushes["bag"], (float)(misc.ScreenX / 2 - 230), (float)(misc.ScreenY / 4 + 300), (float)(misc.ScreenX / 2 + 330), (float)(misc.ScreenY / 4 + 30 + 5), 3f);
					if (this.drawCheckbox(e, 800, 350, "No Recoil", Recoil))
					{
						Recoil = !Recoil;
					}
					if (this.drawCheckbox(e, 800, 400, "No Spread", Spread))
					{
						Spread = !Spread;
					}
					if (this.drawCheckbox(e, 800, 450, "No Reload", NoReaload))
					{
						NoReaload = !NoReaload;
					}
					if (this.drawCheckbox(e, 800, 500, "Max Damage", MaxDamage))
					{
						MaxDamage = !MaxDamage;
					}
				} else if(TabVisual == true)
                {
					graphics.FillRoundedRectangle(overlay._brushes["bag"], (float)(misc.ScreenX / 2 - 230), (float)(misc.ScreenY / 4 + 300), (float)(misc.ScreenX / 2 + 330), (float)(misc.ScreenY / 4 + 30 + 5), 3f);
					if (this.drawCheckbox(e, 800, 350, "ESP", drawesp))
					{
						drawesp = !drawesp;
					}
				} else if(TabOther == true)
                {
					if (this.drawCheckbox(e, 800, 350, "Info", Info))
					{
						Info = !Info;
					}
				}
				//graphics.DrawImage(overlay._images["Logo"], 65, 25, 1);
				//Tabs
				if (this.drawCheckbox(e, 50, 150, "Player", TabPlayer))
				{
					TabPlayer = true;
					TabVehicle = false;
					TabWeapon = false;
					TabVisual = false;
					TabOther = false;
				}
				if (this.drawCheckbox(e, 50, 200, "Vehicle", TabVehicle))
				{
					TabVehicle = true;
					TabPlayer = false;
					TabWeapon = false;
					TabVisual = false;
					TabOther = false;
				}
				if (this.drawCheckbox(e, 50, 250, "Weapon", TabWeapon))
				{
					TabWeapon = true;
					TabPlayer = false;
					TabVehicle = false;
					TabVisual = false;
					TabOther = false;
				}
				if (this.drawCheckbox(e, 50, 300, "Visual", TabVisual))
				{
					TabVisual = true;
					TabPlayer = false;
					TabVehicle = false;
					TabWeapon = false;
					TabOther = false;
				}
				if (this.drawCheckbox(e, 50, 350, "Other", TabOther))
				{
					TabOther = !TabOther;
					TabPlayer = false;
					TabVehicle = false;
					TabWeapon = false;
					TabVisual = false;
				}
				//
				graphics.DrawCrosshair(overlay._brushes["white"], (float)misc.GetCursorPosition().X, (float)misc.GetCursorPosition().Y, 5, 1, CrosshairStyle.Cross);
			}
			else if (!guiOpen && drawesp)
			{
				Program.drawesp(e);
				graphics.DrawCrosshair(overlay._brushes["green"], misc.ScreenX / 2, misc.ScreenY / 2, 2, 0.5f, CrosshairStyle.Gap);
			}
			overlay.drawable = false;
			graphics.EndScene();
			Thread.Sleep(5);
		}
		public static byte[] ReadImageFile(string imageLocation)
		{
			System.Drawing.Image img = System.Drawing.Image.FromFile(imageLocation);
			byte[] bytes;
			using (MemoryStream ms = new MemoryStream())
			{
				img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
				bytes = ms.ToArray();
			}
			return bytes;
		}

		public void drawSlider(DrawGraphicsEventArgs e, int left, int top, string text, bool moduleActive)
        {
			Graphics graphics = e.Graphics;
			graphics.DrawBox2D(overlay._brushes["blue"], overlay._brushes["transparent"], (float)left, (float)top, (float)(left + 20), (float)(top + 20), 3);
		}
		public bool drawCheckbox(DrawGraphicsEventArgs e, int left, int top, string text, bool moduleActive)
		{
			Graphics graphics = e.Graphics;
			graphics.DrawRoundedRectangle(overlay._brushes["blue"], (float)left, (float)top, (float)(left + 20), (float)(top + 20), 1f, 2f);
			graphics.DrawText(overlay._fonts["arial"], 15f, overlay._brushes["white"], (float)(left + 30), (float)(top + 10) - graphics.MeasureString(overlay._fonts["arial"], 15f, text).Y / 2f, text);
			if (misc.GetCursorPosition().X >= left & misc.GetCursorPosition().X <= left + 25 & misc.GetCursorPosition().Y >= top & misc.GetCursorPosition().Y <= top + 25)
			{
				graphics.FillRectangle(overlay._brushes["background"], (float)left, (float)top, (float)(left + 20), (float)(top + 20));
				if (misc.IsKeyDown(Keys.LButton))
				{					
					return true;
					Thread.Sleep(500);
				}
			}
			if (moduleActive)
			{
				graphics.FillRectangle(overlay._brushes["blue"], (float)left, (float)top, (float)(left + 20), (float)(top + 20));
			}
			return false;
		}
		public void drawButton(DrawGraphicsEventArgs e, int left, int top, string text, Action method)
        {
			Graphics graphics = e.Graphics;
			graphics.FillRectangle(overlay._brushes["blue"], (float)left, (float)top, (float)(left + 60), (float)(top + 20));
			graphics.DrawText(overlay._fonts["arial"], 9f, overlay._brushes["white"], (float)(left + 20), (float)(top + 10) - graphics.MeasureString(overlay._fonts["arial"], 9f, text).Y / 2f, text);
			if (misc.GetCursorPosition().X >= left & misc.GetCursorPosition().X <= left + 65 & misc.GetCursorPosition().Y >= top & misc.GetCursorPosition().Y <= top + 25)
			{
				graphics.FillRectangle(overlay._brushes["lightblue"], (float)left, (float)top, (float)(left + 60), (float)(top + 20));
				graphics.DrawText(overlay._fonts["arial"], 9f, overlay._brushes["white"], (float)(left + 20), (float)(top + 10) - graphics.MeasureString(overlay._fonts["arial"], 9f, text).Y / 2f, text);
				if (misc.IsKeyDown(Keys.LButton))
                {
					method();
					Thread.Sleep(100);
				}
			}
		}
		public void Run()
		{
			_window.Create();
			_window.Join();
		}

		~overlay()
		{
			Dispose(false);
		}

		#region IDisposable Support
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				_window.Dispose();

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}