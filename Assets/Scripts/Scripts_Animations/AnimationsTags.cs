namespace ET
{
    public readonly struct AnimationsTags
    {
        public const string IDLE_ANIMATION = "Idle";

        public const string RUN = "Run";
        public const string WALK = "Walk";

        public const string SHOOTING = "Shooting";

        public const string BLOCK = "Block";

        public const string RELODING_WEAPONS = "RelodingWeapons";

        public const string ATTACK_1_TRIGGER = "Attack1";
        public const string ATTACK_2_TRIGGER = "Attack2";
        public const string ATTACK_3_TRIGGER = "Attack3";

        public const string CRAWL_TRIGGER = "Crawl";
        public const string FALLING_BACK_TRIGGER = "FallingBack";
        public const string STANDUP_TRIGGER = "StandUp";
        public const string HIT_TRIGGER = "Hit";
        public const string DEATH_TRIGGER = "Death";
    }

    public readonly struct Axis
    {
        public const string HORIZONTAL_AXIS = "Horizontal";
        public const string VERTICAL_AXIS = "Vertical";
    }

    public readonly struct Tags
    {
        public const string GROUND_TAG = "Ground";
        public const string PLAYER_TAG = "Player";
        public const string ENEMY_ZOMBIE_TAG = "EnemyZombie";
        public const string OBSTACLE_TAG = "Obstacle";

        public const string LEFT_ARM_TAG = "LeftArm";
        public const string LEFT_LEG_TAG = "LeftLeg";
        public const string RIGHT_LEG_TAG = "RightLeg";
        public const string UNTAGGED_TAG = "Untagged";
        public const string MAIN_CAMERA_TAG = "MainCamera";

        public const string PLAYER_HEALTH_VIEW = "PlayerHealthView";
        public const string PLAYER_AMMO_IMAGE_VIEW = "PlayerAmmoGroundView";
        public const string PLAYER_AMMO_TEXT_VIEW = "PlayerAmmoTextView";
        public const string STAMINA_VIEW = "StaminaView";
    }
}

