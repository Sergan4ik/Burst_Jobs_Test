namespace Core
{
    public static class Tools
    {
        public static bool IsAlly(this Unit lhs, Unit rhs)
        {
            return lhs.teamId == rhs.teamId;
        }
        public static bool IsEnemy(this Unit lhs, Unit rhs)
        {
            return lhs.teamId != rhs.teamId;
        }
    }
}