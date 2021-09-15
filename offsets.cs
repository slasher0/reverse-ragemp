using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheat
{
    //aktuellen offsets wenn game updated sind diese auf unknowncheats zu finden https://www.unknowncheats.me/forum/grand-theft-auto-v/144028-grand-theft-auto-reversal-structs-offsets-549.html
    class offsets
    {
        public static int WORLD_OFFSET = 8;

        //entity offsets
        public static UInt64 OFFSET_ENTITY_POSBASE = 0x30;			//base of a structure that contains entity coords
        public static UInt64 OFFSET_ENTITY_POSBASE_COS = 0x20;
        public static UInt64 OFFSET_ENTITY_POSBASE_SIN = 0x30;
        public static UInt64 OFFSET_ENTITY_POSBASE_POS = 0x50;			//vector3
        public static UInt64 OFFSET_ENTITY_POS = 0x90;			//vector3
        public static UInt64 OFFSET_ENTITY_HEALTHENTITY = 0x0280;			//entity health (except for vehicles); float cur, float max		OLD 0x280
        public static UInt64 OFFSET_ENTITY_HEALTH_MAX = 0x2A0;			//they moved this away from curHealth in 1.36 :(
        public static UInt64 OFFSET_ENTITY_ATTACKER = 0x2A8;			//base to a list of the last 3 entities that attacked the current entity
        public static UInt64 OFFSET_ENTITY_GOD = 0x189;		//godmode; on = 1, off = 0; byte
        public static UInt64 OFFSET_ENTITY_HEALTH = 0x280;			//	HelthH


        //player (entity) offsets
        public static UInt64 OFFSET_PLAYER_SUPERJUMP = 0x1F8;			// Super pulo offset!
        public static UInt64 OFFSET_PLAYER_ARMOR = 0x14E0; 			//armour OLD 0x14B0
        public static UInt64 OFFSET_PLAYER_INFO = 0x10C8;			//playerInfo struct												//#define OFFSET_PLAYER_INFO								0xCD0			// OLD 0x10B8			//playerInfo struct
        public static UInt64 OFFSET_PED_MODEL_INFO = 0x0020;			// Ped model info
        public static UInt64 OFFSET_PLAYER_ROCKSTAR_ID = 0x0070;			//	ROCKSTAR ID PLAYER
        public static UInt64 OFFSET_PLAYER_INFO_WANTED_CAN_CHANGE = 0x71C;			//fWantedCanChange
        public static UInt64 OFFSET_PLAYER_INFO_WANTED = 0x868;			//wanted level; DWORD											//#define OFFSET_PLAYER_INFO_WANTED							0x798			//wanted level; DWORD
        public static UInt64 OFFSET_PLAYER_INFO_RUN_SPD = 0x0CF0;			//run speed; def 1; float		OLD 0xE8 and 0x14C 
        public static UInt64 OFFSET_PLAYER_INFO_SWIM_SPD = 0x0170;			//swim speed; def 1; float		OLD 0x148
        public static UInt64 OFFSET_PLAYER_INFO_FRAMEFLAGS = 0x218;			//frame flags; DWORD			OLD 0x1F8	OLD 1.52/1.53 0x1F9						//#define OFFSET_PLAYER_INFO_FRAMEFLAGS						0x190			//frame flags; DWORD
        public static UInt64 OFFSET_PLAYER_INFO_STAMINA = 0x0CD4;			//fStamina, fStaminaMax			OLD 0xC00
        public static UInt64 OFFSET_PLAYER_MAX_STAMINA_REGEN = 0x0CD8;			//	MAX STAMINA
        public static UInt64 OFFSET_PLAYER_VEHICLE = 0xD30;			//ptr to last used vehicle  old 0xD28
        public static UInt64 OFFSET_PLAYER_NAME = 0x84;			//	OLD 0x7C
        public static UInt64 OFFSET_PLAYER_RAGDOLL = 0x10B8;			//byte; CPed.noRagdoll: 0x20 = off; 0x00/0x01 = on				//#define OFFSET_PLAYER_RAGDOLL								0x10A8			//byte; CPed.noRagdoll: 0x20 = off; 0x00/0x01 = on
        public static UInt64 OFFSET_PLAYER_SEATBELT = 0xC00;			//byte; CPed.seatBelt: 0xC8 = off; 0xC9 = on					//#define OFFSET_PLAYER_SEATBELT							0x13EC			//byte; CPed.seatBelt: 0xC8 = off; 0xC9 = on
        public static UInt64 OFFSET_PLAYER_INVEHICLE = 0x1477;																			//#define OFFSET_PLAYER_INVEHICLE								0x146B
        public static UInt64 OFFSET_PLAYER_WANTED = 0x868;			//	OLD 0x0848
        public static UInt64 OFFSET_PLAYER_STEALTH_WALK	= 0x016C;			//	STEALTH WALK SPEED		OLD 0x168
        public static UInt64 OFFSET_NET_PLAYER_INFO = 0xA8;
        public static UInt64 OFFSET_PLAYER_INFO_NAME = 0x84;
        public static UInt64 OFFSET_PLAYER_INFO_NPC_IGNORE = 0x850;			//npc ignore; DWORD; everyone = 0x450000;
        public static UInt64 OFFSET_PLAYER_WATER_PROOF = 0x188;			//water proof; DWORD; +0x1000000 = on
        public static UInt64 OFFSET_PLAYER_VEHICLE_DAMAGE_MP = 0xCFC;           //super punck/kick;float;


        //vehicle offsets
        public static UInt64 OFFSET_VEHICLE_HEALTH = 0x908;			//vehicle health; 0.f-1000.f									//#define OFFSET_VEHICLE_HEALTH								0x84C			
        public static UInt64 OFFSET_VEHICLE_HEALTH2 = 0x844;			//vehicle health2; 0.f-1000.f
        public static UInt64 OFFSET_VEHICLE_HANDLING = 0x938;																			//#define OFFSET_VEHICLE_HANDLING								0x878
        public static UInt64 OFFSET_VEHICLE_HANDLING_MASS = 0xC;			//fMass
        public static UInt64 OFFSET_VEHICLE_HANDLING_BUOYANCY = 0x40;			//fBuoyancy

        public static UInt64 OFFSET_VEHICLE_GRAVITY	= 0xC5C;			//fGravity														//#define OFFSET_VEHICLE_GRAVITY								0xB7C			
        public static UInt64 OFFSET_VEHICLE_BULLETPROOF_TIRES = 0x943;			//btBulletproofTires; (btBulletproofTires & 0x20) ? true : false//#define OFFSET_VEHICLE_BULLETPROOF_TIRES					0x883			//btBulletproofTires;  (btBulletproofTires & 0x20) ? true : false
        public static UInt64 OFFSET_VEHICLE_HANDLING_SUSPENSION_HEIGH = 0xD0;		//fSuspensionHeight
        public static UInt64 OFFSET_VEHICLE_HANDLING_COLISION_DAMAGE_MP	= 0xF0;         //fColisionDamageMult
        public static UInt64 OFFSET_VEHICLE_HANDLING_WEAPON_DAMAGE_MP = 0xF4;       //fWeaponDamageMult
        public static UInt64 OFFSET_VEHICLE_HANDLING_DOWNSHIFT = 0x5C;
        public static UInt64 OFFSET_VEHICLE_HANDLING_HANDBRAKEFORCE = 0x7C;			//fHandbrakeForce
        public static UInt64 OFFSET_VEHICLE_HANDLING_ENGINE_DAMAGE_MP =	0xFC;			//fEngineDamageMult
        public static UInt64 OFFSET_VEHICLE_HANDLING_ACCELERATION = 0x4C;
        public static UInt64 OFFSET_VEHICLE_HANDLING_BRAKEFORCE	= 0x6C;
        public static UInt64 OFFSET_VEHICLE_HANDLING_TRACTION_CURVE_MIN	= 0x90;         //fTractionCurveMin
        public static UInt64 OFFSET_VEHICLE_HANDLING_DEFORM_MULTIPLIER = 0xF8;      //fDeformationDamageMult
        public static UInt64 OFFSET_VEHICLE_HANDLING_UPSHIFT = 0x58;
        public static UInt64 OFFSET_VEHICLE_HANDLING_SUSPENSION_FORCE = 0xBC;           //fSuspensionForce 
        public static UInt64 OFFSET_VEHICLE_BOOST = 0x320;		//fBoost
        public static UInt64 OFFSET_VEHICLE_RECHARGE_SPEED = 0x324;			//fRocketRechargeSpeed
        public static UInt64 OFFSET_VEHICLE_MISSLES	= 0x1280;           //btVehicleMissles
        public static UInt64 OFFSET_VEHICLE_BOMBS = 0x1294;			//btAircraftBombs
        public static UInt64 OFFSET_VEHICLE_COUNTERMEASURES	= 0x1298;			//btAircraftCountermeasures
        public static UInt64 OFFSET_VEHICLE_MK2_MISSLES = 0x1284;			//btOppressorMK2Misseles
        public static UInt64 OFFSET_VEHICLE_TAMPA_MISSLES = 0x127C;			//btTampaMissles
        public static UInt64 OFFSET_VEHICLE_CUSTOM = 0x48;
        public static UInt64 OFFSET_VEHICLE_CUSTOM_EMS = 0x3D6;			//btEngineManagementSystem; 0x3 = max
        public static UInt64 OFFSET_VEHICLE_CUSTOM_BRAKES = 0x3D7;			//btBrakes; 0x6 = max
        public static UInt64 OFFSET_VEHICLE_CUSTOM_TRANSMISSION	= 0x3D8;			//btTransmission; 0x8 = max
        public static UInt64 OFFSET_VEHICLE_CUSTOM_SUSPENSION = 0x3DA;			//btSuspension; 0x1B = max
        public static UInt64 OFFSET_VEHICLE_CUSTOM_ARMOR = 0x3DB;			//btArmor; 0x1B = max
        public static UInt64 OFFSET_VEHICLE_CUSTOM_TURBO_TUNING	= 0x3DD;			//btTurboTuning; 0x1 = on
        public static UInt64 OFFSET_VEHICLE_CUSTOM_NEON_LIGHT_R	= 0x3A2;			//btNeonLightRed
        public static UInt64 OFFSET_VEHICLE_CUSTOM_NEON_LIGHT_G	= 0x3A1;			//btNeonLightGreen
        public static UInt64 OFFSET_VEHICLE_CUSTOM_NEON_LIGHT_B	= 0x3A0;			//btNeonLightBlue
        public static UInt64 OFFSET_VEHICLE_CUSTOM_NEON_LIGHTS_L = 0x402;			//btNeonLightLeft; 0x0 = off; 0x1 = on
        public static UInt64 OFFSET_VEHICLE_CUSTOM_NEON_LIGHTS_R = 0x403;			//btNeonLightRight; 0x0 = off; 0x1 = on
        public static UInt64 OFFSET_VEHICLE_CUSTOM_NEON_LIGHTS_F = 0x404;			//btNeonLightFront; 0x0 = off; 0x1 = on
        public static UInt64 OFFSET_VEHICLE_CUSTOM_NEON_LIGHTS_B = 0x405;			//btNeonLightBack; 0x0 = off; 0x1 = on
        public static UInt64 OFFSET_VEHICLE_CUSTOM_TYRE_SMOKE = 0x3DF;			//btTyreSmoke; 0x1 = on
        public static UInt64 OFFSET_VEHICLE_CUSTOM_TYRE_SMOKE_R	= 0x3FC;			//btTyreSmokeRed
        public static UInt64 OFFSET_VEHICLE_CUSTOM_TYRE_SMOKE_G	= 0x3FD;			//btTyreSmokeGreen
        public static UInt64 OFFSET_VEHICLE_CUSTOM_TYRE_SMOKE_B	= 0x3FE;			//btTyreSmokeBlue
        public static UInt64 OFFSET_VEHICLE_CUSTOM_LIMO_WINDOWS = 0x3FF;            //btLimoWindows; 0x1 = on



        //weapon offsets
        public static UInt64 OFFSET_WEAPON_MANAGER = 0x10D8;			//from playerbase												//#define OFFSET_WEAPON_MANAGER								0x10C8			
        public static UInt64 OFFSET_WEAPON_CURRENT = 0x20;			//from weapon manager
        public static UInt64 OFFSET_WEAPON_AMMOINFO = 0x60;			//from weaponbase												//#define OFFSET_WEAPON_AMMOINFO								0x48			//from weaponbase
        public static UInt64 OFFSET_WEAPON_AMMOINFO_MAX	= 0x28;			//ammoinfo
        public static UInt64 OFFSET_WEAPON_AMMOINFO_CUR_1 = 0x08;			//ptr lvl 1, ptr 1
        public static UInt64 OFFSET_WEAPON_AMMOINFO_CUR_2 = 0x00;			//ptr tr lvl 2, ptr 1
        public static UInt64 OFFSET_WEAPON_AMMOINFO_CURAMMO	 = 0x18;			//offset to cur ammo
        public static UInt64 OFFSET_WEAPON_AMMOINFO_TYPE = 0x0C;			//offset to projectile type?
        public static UInt64 OFFSET_WEAPON_SPREAD = 0x7C;			//float set to 0												//#define OFFSET_WEAPON_SPREAD								0x5C			
        public static UInt64 OFFSET_WEAPON_BULLET_DMG =	0xBC;			//float times 10 (so when 0, it will stay 0)					//#define OFFSET_WEAPON_BULLET_DMG							0x98			
        public static UInt64 OFFSET_WEAPON_RELOAD_MULTIPLIER = 0x134;			//float times 10												//#define OFFSET_WEAPON_RELOAD_MULTIPLIER						0x114			
        public static UInt64 OFFSET_WEAPON_RECOIL = 0x2F4;			//float set to 0												//#define OFFSET_WEAPON_RECOIL								0x2A4			
        public static UInt64 OFFSET_WEAPON_MODEL_HASH = 0x14;
        public static UInt64 OFFSET_WEAPON_NAME_HASH = 0x10;
        public static UInt64 OFFSET_WEAPON_RELOAD_VEHICLE = 0x130;																			//#define OFFSET_WEAPON_RELOAD_VEHICLE						0x110
        public static UInt64 OFFSET_WEAPON_RANGE = 0x28C;																			//#define OFFSET_WEAPON_RANGE									0x25C
        public static UInt64 OFFSET_WEAPON_SPINUP = 0x144;																			//#define OFFSET_WEAPON_SPINUP								0x124
        public static UInt64 OFFSET_WEAPON_SPIN = 0x148;																			//#define OFFSET_WEAPON_SPIN									0x128
        public static UInt64 OFFSET_WEAPON_BULLET_BATCH = 0x124;			//dwBulletInBatch												//#define OFFSET_WEAPON_BULLET_BATCH							0x100			
        public static UInt64 OFFSET_WEAPON_MUZZLE_VELOCITY = 0x11C;			//fMuzzleVelocity												//#define OFFSET_WEAPON_MUZZLE_VELOCITY						0xFC			
        public static UInt64 OFFSET_WEAPON_IMPACT_TYPE = 0x20;			//dwImpactType; 1: Fists,3; Bullets,5: Explosives
        public static UInt64 OFFSET_WEAPON_IMPACT_EXPLOSION	= 0x24;			//dwImpactExplosion
        public static UInt64 OFFSET_WEAPON_PENETRATION = 0x110;			//fPenetration
        public static UInt64 OFFSET_WEAPON_FORCE_ON_PED	= 0xDC;			//fForceOnPed
        public static UInt64 OFFSET_WEAPON_FORCE_ON_VEHICLE	= 0xE0;			//fForceOnVehicle(Bullet Mass)
        public static UInt64 OFFSET_WEAPON_FORCE_ON_HELI = 0xE4;			//fForceOnHeli
        public static UInt64 OFFSET_WEAPON_BATCH_SPREAD	= 0x104;			//fBatchSpread

        //tunable offsets
        public static UInt64 OFFSET_TUNABLE_RP_MULTIPLIER = 0x10;
        public static UInt64 OFFSET_TUNABLE_AP_MULTIPLIER =	0x30F80;
        public static UInt64 OFFSET_TUNABLE_MIN_MISSION_PAYOUT = 0x4BC8;			//fMinMissionPayout
        public static UInt64 OFFSET_TUNABLE_ANTI_IDLE_KICK1	= 0x2C0;			//AFK;DWORD;2000000000 = Anti idle kick
        public static UInt64 OFFSET_TUNABLE_ANTI_IDLE_KICK2	= 0x2C8;
        public static UInt64 OFFSET_TUNABLE_ANTI_IDLE_KICK3	= 0x2D0;
        public static UInt64 OFFSET_TUNABLE_ANTI_IDLE_KICK4 = 0x2D8;
        public static UInt64 OFFSET_TUNABLE_ORBITAL_CANNON_COOLDOWN	= 0x2C188;			//OrbitalCannonCooldown;DWORD
        public static UInt64 OFFSET_TUNABLE_BUNKER_RESEARCH = 0x29BB8;			//UnlockAllBunkerResearch;DWORD
        public static UInt64 OFFSET_ATTACKER_DISTANCE = 0x18;			//changed to 0x18, from 0x10

                //replay interface offsets
        public static UInt64 OFFSET_REPLAY_PED_INTERFACE = 0x18;
        public static UInt64 OFFSET_REPLAY_PICKUP_INTERFACE	= 0x20;
        public static UInt64 OFFSET_INTERFACE_LIST = 0x100;
        public static UInt64 OFFSET_INTERFACE_CUR_NUMS = 0x110;
        public static UInt64 OFFSET_REPLAY_PICKUP_HASH = 0x488;
    }
}
