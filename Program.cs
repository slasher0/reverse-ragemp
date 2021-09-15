using System;
using System.Threading;
using System.Windows.Forms;
using System.Numerics;
using GameOverlay.Windows;
using GameOverlay.Drawing;
using System.Windows;
using System.Collections.Generic;

namespace Cheat
{
    class Program
    {
        public static Memory GTA;
        public static UInt64 Player;
        public static List<IntPtr> Friendlist = new List<IntPtr>();
        public static UInt64 ReplayInterface;
        public static UInt64 Weaponbase;
        public static UInt64 ViewMatrix;

        public static bool AimbotLocked = false;

        //Hotkeys

        public static string Aimbotkey = "E";
        public static string Healthkey = "B";
        public static string Armorkey = "B";


        static void Main(string[] args)
        {
            misc.ScreenX = Screen.PrimaryScreen.Bounds.Width;
            misc.ScreenY = Screen.PrimaryScreen.Bounds.Height;
            Thread mainthread = new Thread(cheat_mainthread);
            mainthread.Start();
        }

        public static void cheat_mainthread()
        {
            INIFile logini = new INIFile(@"C:\reversecheats\log.ini");
            INIFile configini = new INIFile(@"C:\reversecheats\config.ini");
            //configini.Write("Keys", "Aimbot_Key", "E");
            //configini.Write("Keys", "Health_Key", "B");
            //configini.Write("Keys", "Armor_Key", "B");
            string aimkey = configini.Read("Keys", "Aimbot_Key");
            Aimbotkey = aimkey;
            Console.WriteLine(aimkey);
            //misc.hwidcheck(misc.GetMachineGuid());
            //misc.checkVersion();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("REVERSE CHEATS starting...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[~]GTA 5 Process!: " + misc.obtainProcess());
            //wichtig
            if (misc.obtainProcess() != "none")
            {
                Console.WriteLine("[~] Functions was loaded!");
                GTA = new Memory(misc.obtainProcess());
                Thread.Sleep(2000);
                Console.WriteLine("[~] ProcID: " + GTA.GetProcessID().ToString("x8").ToUpper());
                Console.WriteLine("[~] Resolution: " + misc.ScreenX + "x" + misc.ScreenY);
                //48 8B 05 ? ? ? ? 45 ? ? ? ? 48 8B 48 08 48 85 C9 74 07 WorldSignature 1.1
                //48 8B 05 ? ? ? ? 48 8B 58 08 48 85 DB 74 32 WorldSignature  0.3.7
                UInt64 WorldFlirtPointer = GTA.PointerScan("48 8B 05 ? ? ? ? 48 8B 58 08 48 85 DB 74 32");
                UInt64 World = GTA.ReadRelativeAddress(WorldFlirtPointer);
                UInt64 BlipFlirtPointer = GTA.PointerScan("4C 8D 05 ? ? ? ? 0F B7 C1");
                UInt64 Blip = GTA.ReadRelativeAddress(BlipFlirtPointer);
                Player = GTA.Read<UInt64>(World, new int[] { offsets.WORLD_OFFSET });
                UInt64 PlayerInfo = GTA.Read<UInt64>(Player + offsets.OFFSET_PLAYER_INFO);
                Weaponbase = GTA.Read<UInt64>(Player + offsets.OFFSET_WEAPON_MANAGER);
                UInt64 ReplayInterfaceFlirtPointer = GTA.PointerScan("48 8D 0D ? ? ? ? 48 8B D7 E8 ? ? ? ? 48 8D 0D ? ? ? ? 8A D8 E8 ? ? ? ? 84 DB 75 13 48 8D 0D");
                ReplayInterface = GTA.ReadRelativeAddress(ReplayInterfaceFlirtPointer);
                UInt64 PlayerlistFlirtPointer = GTA.PointerScan("48 8B 0D ? ? ? ? E8 ? ? ? ? 48 8B C8 E8 ? ? ? ? 48 8B CF");
                UInt64 Playerlist = GTA.ReadRelativeAddress(PlayerlistFlirtPointer);
                UInt64 ViewMatrixFlirtPointer = GTA.PointerScan("48 8B 15 ? ? ? ? 48 8D 2D ? ? ? ? 48 8B CD");
                UInt64 BonePosFlirtPointer = GTA.PointerScan("41 81 E8 ? ? ? ? 0F 84 ? ? ? ? B8 ? ? ? ?");
                ViewMatrix = GTA.ReadRelativeAddress(ViewMatrixFlirtPointer);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[~] REVERSE CHEATS is ready! Press F11 to open it!");
                Console.ForegroundColor = ConsoleColor.White;
                //
                launchOverlay();

                while (true)
                {
                    //no recoil no spread
                    UInt64 weaponcurrent = GTA.Read<UInt64>(Weaponbase + offsets.OFFSET_WEAPON_CURRENT);
                    //
                    if (misc.IsKeyDown(Keys.B))
                    {
                        GTA.Write<Single>(Player + offsets.OFFSET_PLAYER_ARMOR, 200.0f);
                        GTA.Write<Single>(Player + offsets.OFFSET_ENTITY_HEALTH, 200.0f);
                    }
                    if (misc.IsKeyDown(Keys.PageUp))
                    {
                        GTA.Write<int>(weaponcurrent + offsets.OFFSET_WEAPON_NAME_HASH, 1119849093);
                    }
                    if (overlay.Godmode)
                    {
                        GTA.Write<Byte>(Player + offsets.OFFSET_ENTITY_GOD, 0x1);
                    }
                    else
                    {
                        GTA.Write<Byte>(Player + offsets.OFFSET_ENTITY_GOD, 0x0);
                    }
                    if (overlay.VehicleSpeed)
                    {
                        UInt64 curveh = GTA.Read<UInt64>(Player + offsets.OFFSET_PLAYER_VEHICLE);
                        GTA.Write<Single>(curveh + offsets.OFFSET_VEHICLE_GRAVITY, 80);
                    } 
                    else
                    {
                        UInt64 curveh = GTA.Read<UInt64>(Player + offsets.OFFSET_PLAYER_VEHICLE);
                        GTA.Write<Single>(curveh + offsets.OFFSET_VEHICLE_GRAVITY, 20);
                    }
                    if (overlay.Radgoll)
                    {
                        GTA.Write<Byte>(Player + offsets.OFFSET_PLAYER_RAGDOLL, 0x01);
                    }
                    if (overlay.NoReaload)
                    {
                        GTA.Write<Single>(weaponcurrent + offsets.OFFSET_WEAPON_RELOAD_MULTIPLIER, 10.0f);
                    } else
                    {
                        GTA.Write<Single>(weaponcurrent + offsets.OFFSET_WEAPON_RELOAD_MULTIPLIER, 1.0f);
                    }
                    if (overlay.Seatbalet)
                    {
                        GTA.Write<Byte>(Player + offsets.OFFSET_PLAYER_SEATBELT, 0xC9);
                    }
                    if (overlay.MaxDamage)
                    {
                        GTA.Write<Single>(weaponcurrent + offsets.OFFSET_WEAPON_BULLET_DMG, 1.0f);
                    }
                    if (overlay.AutoVehicleHeal)
                    {
                        UInt64 curveh = GTA.Read<UInt64>(Player + offsets.OFFSET_PLAYER_VEHICLE);
                        GTA.Write<Single>(curveh + offsets.OFFSET_VEHICLE_HEALTH, 1000f);
                        GTA.Write<Single>(curveh + offsets.OFFSET_VEHICLE_HEALTH2, 1000f);
                    }
                    if (overlay.NoColission)
                    {
                        UInt64 curveh = GTA.Read<UInt64>(Player + offsets.OFFSET_PLAYER_VEHICLE);
                        UInt64 handling = GTA.Read<UInt64>(curveh + offsets.OFFSET_VEHICLE_HANDLING);
                        GTA.Write<Single>(handling + offsets.OFFSET_VEHICLE_HANDLING_COLISION_DAMAGE_MP, 0.0f);
                    }
                    if (overlay.Recoil)
                    {
                        GTA.Write<Single>(weaponcurrent + offsets.OFFSET_WEAPON_RECOIL, 0.0f);
                    }
                    if (overlay.Spread)
                    {
                        GTA.Write<Single>(weaponcurrent + offsets.OFFSET_WEAPON_SPREAD, 0.0f);
                    }
                    if (overlay.RunSpeed)
                    {
                        GTA.Write<Single>(PlayerInfo + offsets.OFFSET_PLAYER_INFO_RUN_SPD, 3f);
                    } else
                    {
                        GTA.Write<Single>(PlayerInfo + offsets.OFFSET_PLAYER_INFO_RUN_SPD, 1f);
                    }
                    if (overlay.SwimSpeed)
                    {
                        GTA.Write<Single>(PlayerInfo + offsets.OFFSET_PLAYER_INFO_SWIM_SPD, 3f);
                    }
                    else
                    {
                        GTA.Write<Single>(PlayerInfo + offsets.OFFSET_PLAYER_INFO_SWIM_SPD, 1f);
                    }
                    /*if (overlay.SuperJump)
                    {
                        GTA.Write<Single>(Player + offsets.OFFSET_PLAYER_SUPERJUMP, 14);
                    }
                    else
                    {
                        GTA.Write<Single>(Player + offsets.OFFSET_PLAYER_SUPERJUMP, 1f);
                    }*/
                    /* am besten nicht benutzen
                    if (misc.IsKeyDown(Keys.End))
                    {
                        UInt64 curveh = GTA.Read<UInt64>(Player + offsets.OFFSET_PLAYER_VEHICLE);
                        UInt64 handling = GTA.Read<UInt64>(curveh + offsets.OFFSET_VEHICLE_HANDLING);
                        GTA.Write<Single>(handling + offsets.OFFSET_VEHICLE_HANDLING_ACCELERATION, 5000.0f);
                        GTA.Write<Single>(handling + offsets.OFFSET_VEHICLE_HANDLING_TRACTION_CURVE_MIN, 300.0f);
                    }
                    
                    vehicle speed + health 
                    if (misc.IsKeyDown(Keys.PageDown))
                    {
                        UInt64 curveh = GTA.Read<UInt64>(Player + offsets.OFFSET_PLAYER_VEHICLE);
                        UInt64 handling = GTA.Read<UInt64>(curveh + offsets.OFFSET_VEHICLE_HANDLING);
                        GTA.Write<Single>(handling + offsets.OFFSET_VEHICLE_HANDLING_COLISION_DAMAGE_MP, 0.0f);
                        GTA.Write<Single>(handling + offsets.OFFSET_VEHICLE_HANDLING_DEFORM_MULTIPLIER, 0.0f);
                        GTA.Write<Single>(handling + offsets.OFFSET_VEHICLE_HANDLING_ENGINE_DAMAGE_MP, 0.0f);
                        GTA.Write<Single>(curveh + offsets.OFFSET_VEHICLE_HEALTH, 1000f);
                        GTA.Write<Single>(curveh + offsets.OFFSET_VEHICLE_HEALTH2, 1000f);
                    }*/
                    Thread.Sleep(50);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("GTA 5 was not found!");
                Console.ReadLine();
            }
        }

        public static void drawesp(DrawGraphicsEventArgs e)
        {
            // esp stuff
            var replay = GTA.Read<IntPtr>(ReplayInterface);
            var pedlist = GTA.Read<IntPtr>((ulong)replay + 0x18);
            var objectlist = GTA.Read<IntPtr>((ulong)replay + 0x24);
            var localpos = GTA.Read<Vector3>(Player + offsets.OFFSET_ENTITY_POS);
            /* das auskomentierte sollte object esp werden funzt noch nicht ganz liegt warscheinlich an einem falschen offset (die objectliste ist aber die richtige)
            if (objectlist != IntPtr.Zero)
            {
                var llist_ptr = GTA.Read<IntPtr>((ulong)objectlist + 0x256);
                var mmax_ptr = GTA.Read<IntPtr>((ulong)objectlist + 0x272);
                for (uint i = 0; i <= (uint)mmax_ptr; i++)
                {
                    var objects = GTA.Read<IntPtr>((ulong)llist_ptr + i * 0x16);
                    var objectpos = GTA.Read<Vector3>((ulong)objects + 0x144);
                    if (misc.WorldToScreen(objectpos, out objectpos))
                    {
                        var objecthash = GTA.Read<int>((ulong)objects + 0x32 + 0x24);
                        Console.WriteLine(objectpos.ToString());
                        e.Graphics.DrawLine(overlay._brushes["green"], misc.ScreenX / 2, misc.ScreenY, objectpos.X, objectpos.Y, 0.5f);
                        e.Graphics.DrawText(e.Graphics.CreateFont("Arial", 12f, false, false, false), overlay._brushes["green"], objectpos.X, objectpos.Y + 20, objecthash.ToString());
                    }
                }
            }*/
            // esp hier werden alle spieler gefunden usw usw
            if (pedlist != IntPtr.Zero)
            {
                var list_ptr = GTA.Read<IntPtr>((ulong)pedlist + 0x100);
                var max_ptr = GTA.Read<IntPtr>((ulong)pedlist + 0x108);
                for (uint i = 0; i <= (uint)max_ptr; i++)
                {
                    var ped = GTA.Read<IntPtr>((ulong)list_ptr + i * 0x10);
                    Single HP = GTA.Read<Single>((ulong)ped + offsets.OFFSET_ENTITY_HEALTH);
                    HP = HP-100;

                    Single AM = GTA.Read<Single>((ulong)ped + offsets.OFFSET_PLAYER_ARMOR);

                    var ProgressHP1 = HP / 2;
                    var ProhressAM1 = AM / 2;

                    var pos = GTA.Read<Vector3>((ulong)ped + 144);

                    Vector2 v = new Vector2(localpos.X, localpos.Y);
                    Vector2 v2 = new Vector2(pos.X, pos.Y);

                    var distance = Vector2.Distance(v2, v);

                    if ((ulong)ped != Player)
                    {
                        if (HP > 0 && misc.WorldToScreen(pos, out pos))
                        {
                            //rockstarid für test zwecke braucht ihr eig nd
                            var rockstarid = GTA.Read<UInt64>((ulong)ped + offsets.OFFSET_PLAYER_ROCKSTAR_ID);
                            //add friend methode
                            if (misc.IsKeyDown(Keys.NumPad0) && get2dDistance(misc.ScreenX / 2, pos.X, misc.ScreenY / 2, pos.Y) < 40)
                            {
                                if (!Friendlist.Contains(ped))
                                {
                                    Friendlist.Add(ped);
                                }
                                else
                                {
                                    Friendlist.Remove(ped);
                                }
                                Thread.Sleep(100);
                            }
                            //aimbot
                            if (overlay.aimbot)
                            {
                                if (misc.IsKeyDown(Keys.E) && get2dDistance(misc.ScreenX / 2, pos.X, misc.ScreenY / 2, pos.Y) < 120)
                                {
                                    if (!Friendlist.Contains(ped))
                                    {
                                            misc.AimAtPos(pos.X, pos.Y);
                                    }
                                }
                            }
                            //hier wird der esp auf den spieler angezeigt wenn freund grün wenn nicht rot
                            if (Friendlist.Contains(ped))
                            {
                                e.Graphics.DrawCrosshair(overlay._brushes["green"], pos.X, pos.Y, 0.5f, 5, CrosshairStyle.Gap);
                                e.Graphics.DrawText(e.Graphics.CreateFont("Arial", 12f, false, false, false), overlay._brushes["green"], pos.X, pos.Y + 20,"HP: "+HP.ToString()+"\n A: "+ AM.ToString());
                              
                            }
                            else
                            {
                                if (distance < 200)
                                {
                                    var distmxp = distance;
                                    const float Game_Magic = 400f;
                                    const float playeraspectrot = 5.25f / 2.0f;
                                    float scale =(Game_Magic/distance)*(2/misc.ScreenY) ;
                                    float bx = 60 + scale;
                                    float by = 20 + scale ;
                                    if (distance > 2.5f && distance < 70)
                                    {
                                        float widthx = 120 /(distance*0.45f);
                                        float heighty = 300/(distance*0.45f);
                                        e.Graphics.DrawBox2D(overlay._brushes["green"], overlay._brushes["transparent"], pos.X + widthx, pos.Y + heighty, pos.X-widthx, pos.Y-heighty, 1.7f);
                                    }
                                    e.Graphics.DrawCrosshair(overlay._brushes["red"], pos.X, pos.Y, 1f, 5, CrosshairStyle.Gap);
                                    e.Graphics.DrawText(e.Graphics.CreateFont("Arial", 12f, false, false, false), overlay._brushes["white"], pos.X - 20, pos.Y + 20, "HP: " + HP.ToString() + "\n A: " + AM.ToString() + "\n Distance: " +(350/(distance*0.75f)).ToString());
                                    if (distance < 20)
                                    {
                                        //HP = 100;
                                        //A = 100;
                                        //e.Graphics.DrawLine(overlay._brushes["green"], (pos.X + ProgressHP1)-distmxp, pos.Y + 65, (pos.X - ProgressHP1)+distmxp, pos.Y + 65, strokethicknes(8,distance));
                                        //e.Graphics.DrawLine(overlay._brushes["blue"], (pos.X + ProhressAM1)-distmxp, pos.Y + 75, (pos.X - ProhressAM1)+distmxp, pos.Y + 75, strokethicknes(8,distance));
                                    }
                                }
                            }
                        }
                        
                    }
                }
            }
        }

        public static float strokethicknes(float thick,float distance)
        {
            if (distance <= 4)
            {
                return thick /4;
            }
            return 0;
        }
        //overlay wird hier gestartet (eif in ruhe lassen funzt perfekt)
        public static void launchOverlay()
        {
            new Thread(delegate ()
            {
                new overlay().Run();
            })
            {
                IsBackground = true
            }.Start();
        }

        //esp kacke sprich Matrix usw
        public static Matrix4x4 GetMatrix()
        {
            return GTA.Read<Matrix4x4>(ViewMatrix, new int[] { 588 });
        }

        //funzt nicht ganz gibt kacke aus
        public static double get2dDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Round(Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2))));
        }

        //sollte funktioniern falls ihr die passende boneid habt
        public static Vector3 get_bone_position(long ped_ptr, int bone_id)
        {
            Matrix4x4 matrix = GTA.Read<Matrix4x4>((ulong)ped_ptr, new int[] { 96 });
            return Vector3.Transform(GTA.Read<Vector3>((ulong)ped_ptr, new int[] { 1072 + bone_id * 16 }), matrix);
        }
    }
}