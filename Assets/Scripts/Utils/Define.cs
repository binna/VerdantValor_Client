using UnityEngine;

namespace Knight
{
    public static class Define
    {
        public enum MonsterState
        {
            Idle,
            Patrol,
            Trace,
            Attack
        }

        public enum ItemType
        {
            PotionHp,
            PotionAtk,
            Gold
        }

        public enum SceneType
        {
            Town = 0,
            Adventure,
            Intro
        }

        public enum SoundType
        {
            Bgm,
            Event
        }

        public enum UiName
        {
            HUD,
            Loading,
            Fade,
            Intro,
            Sign,
            ShopNpc,
            Shop,
            Setting,
            Inventory,
            Alarm
        }

        public enum Layer : int
        {
            Default = 0,
            TransparentFX = 1,
            IgnoreRaycast = 2,
            Weapon = 3,
            Water = 4,
            UI = 5,
            Ground = 6,
            Player = 7,
            Item = 8,
            Monster = 9
        }
        
        public enum MovingPlatformType
        {
            Horizontal,
            Vertical
        }
        
        public static class GameObjectName
        {
            public const string SOUND_MANAGER = "@SoundManager";
            public const string GAME_DATA_MANAGER = "@GameDataManager";
            public const string BGM_AUDIO = "BgmAudio";
            public const string EVENT_AUDIO = "EventAudio";
            public const string SPAWN_NAME = "Monsters";
        }
        
        public static class UiObjectName
        {
            public const string SLIDER_BGM_VOLUME = "Slider_BgmVolume";
            public const string TOGGLE_BGM_MUTE = "Toggle_BgmMute";
            
            public const string SLIDER_EVENT_VOLUME = "Slider_EventVolume";
            public const string TOGGLE_EVENT_MUTE = "Toggle_EventMute";
            
            public const string IMG_PROGRESS_BAR = "Img_ProgressBar";
            public const string IMG_BACKGROUND = "Img_Background";
            
            public const string EXIT_BUTTON = "Btn_Exit";
            public const string ENTER_BUTTON = "Btn_Enter";
            
            public const string IMG_ICON = "Img_Icon";
            
            public const string TXT_ID = "Txt_Id";
            public const string TXT_LEVEL = "Txt_Level";
            public const string TXT_GOLD = "Txt_Gold";
            public const string TXT_MESSAGE = "Txt_Message";
            
            public const string PLAY_GAME = "PlayGame";
            public const string MONSTERS = "Monsters";
            public const string ITEMS = "Items";
        }
        
        public static class Tag
        {
            public const string UNTAGGED = "Untagged";
            public const string RESPAWN = "Respawn";
            public const string FINISH = "Finish";
            public const string EDITOR_ONLY = "EditorOnly";
            public const string MAIN_CAMERA = "MainCamera";
            public const string PLAYER = "Player";
            public const string GAME_CONTROLLER = "GameController";
            public const string LADDER = "Ladder";
            public const string MONSTER = "Monster";
            public const string GROUND = "Ground";
            public const string ITEM_TRIGGER = "ItemTrigger";
        }
        
        public static class AnimatorParameter
        {
            public static readonly int isGround = Animator.StringToHash("IsGround");
            public static readonly int isCombo = Animator.StringToHash("IsCombo");
            public static readonly int hit = Animator.StringToHash("Hit");
            public static readonly int jump = Animator.StringToHash("Jump");
            public static readonly int attack = Animator.StringToHash("Attack");
            public static readonly int death = Animator.StringToHash("Death");
            public static readonly int positionX = Animator.StringToHash("PositionX");
            public static readonly int positionY = Animator.StringToHash("PositionY");
            public static readonly int isRun = Animator.StringToHash("IsRun");
            public static readonly int push = Animator.StringToHash("Push");
        }
        
        // Sounds
        public const string INTRO_BGM_PATH = "Sounds/IntroBGM";
        public const string TOWN_BGM_PATH = "Sounds/TownBGM";
        public const string ADVENTURE_BGM_PATH = "Sounds/AdventureBGM";
        public const string PORTAL_MOVE_PATH = "Sounds/Portal";
        public const string GAMEOVER_PATH = "Sounds/Gameover";
        public const string MONSTER_DEATH_PATH = "Sounds/MonsterDie";
        public const string LEVEL_UP_PATH = "Sounds/LevelUp";
        public const string ITEM_PICKUP_PATH = "Sounds/ItemPickup";
        
        // speed
        public const float DIALOG_TYPING_SPEED = 0.05f;
        
        // Path
        public const string IMAGES_PATH = "Images/";
        public const string PREFABS_PATH = "Prefabs/";

        // Use
        public const int INVNETORY_COUNT = 20;
        public const int RECOVERY_HP = 1;
        
        // Etc
        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL = "Vertical";
        
        // Monster
        public static string[] monsterType = { "FlyingEye", "Goblin", "Mushroom", "Skeleton" };
        public static MonsterSpawnArea[] monsterPositions =
        {
            new(-16, 16, -3),
            new(3, 18, 31),
            new(33, 50, 9),
            new(60, 100, 9),
            new(91, 102, 14),
            new(49, 72, 14),
            new(30, 42, 12)
        };
        public static int MONSTER_LIMIT = 15;
        public static readonly int monsterPosCnt = monsterPositions.Length;
        
        // Item
        public static int IMMEDIATE_PICKUP = 99;
    }
}
