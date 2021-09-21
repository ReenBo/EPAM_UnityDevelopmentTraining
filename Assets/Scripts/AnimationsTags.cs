namespace ET
{
    public readonly struct AnimationsTags
    {
        public const string IDLE_ANIMATION = "Idle";

        public const string RUN = "Run";

        public const string SHOOTING = "Shooting";

        public const string BLOCK = "Block";

        public const string ATTACK_1_TRIGGER = "Attack1";
        public const string ATTACK_2_TRIGGER = "Attack2";
        public const string ATTACK_3_TRIGGER = "Attack3";

        public const string KNOCK_DOWN_TRIGGER = "KnockDown";
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
        public const string ENEMY_TAG = "Enemy";
        public const string OBSTACLE_TAG = "Obstacle";

        public const string LEFT_ARM_TAG = "LeftArm";
        public const string LEFT_LEG_TAG = "LeftLeg";
        public const string RIGHT_LEG_TAG = "RightLeg";
        public const string UNTAGGED_TAG = "Untagged";
        public const string MAIN_CAMERA_TAG = "MainCamera";

        public const string HEALTH_VIEW = "HealthView";
        public const string STAMINA_VIEW = "StaminaView";
    }
}
