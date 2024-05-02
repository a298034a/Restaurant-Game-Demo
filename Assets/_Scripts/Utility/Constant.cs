namespace Restaurant
{
    public static class Constant
    {
        public enum CustomerIntention
        {
            None,
            WantOrder
        }

        #region Animate Name
        //Idle
        public const string ANIMATE_IDLE_UP = "IdleUp";
        public const string ANIMATE_IDLE_DOWN = "IdleDown";
        public const string ANIMATE_IDLE_RIGHT = "IdleRight";
        public const string ANIMATE_IDLE_LEFT = "IdleLeft";
        //Walk
        public const string ANIMATE_WALK_UP = "WalkUp";
        public const string ANIMATE_WALK_DOWN = "WalkDown";
        public const string ANIMATE_WALK_RIGHT = "WalkRight";
        public const string ANIMATE_WALK_LEFT = "WalkLeft";
        //Sit
        public const string ANIMATE_SIT_UP = "SitUp";
        public const string ANIMATE_SIT_DOWN = "SitDown";
        public const string ANIMATE_SIT_RIGHT = "SitRight";
        public const string ANIMATE_SIT_LEFT = "SitLeft";
        //Emotes
        public const string ANIMATE_EMOTE_EMPTY = "Empty";
        public const string ANIMATE_EMOTE_HAPPY = "Happy";
        public const string ANIMATE_EMOTE_ANGRY = "Angry";
        public const string ANIMATE_EMOTE_THINKING = "Thinking";
        public const string ANIMATE_EMOTE_NOTICE = "Notice";
        public const string ANIMATE_EMOTE_SATISFY = "Satisfy";
        #endregion
    }
}
