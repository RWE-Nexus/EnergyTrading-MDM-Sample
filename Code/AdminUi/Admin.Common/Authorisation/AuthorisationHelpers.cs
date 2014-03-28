namespace Common.Authorisation
{
    public static class AuthorisationHelpers
    {
        public static bool HasEntityRights(string entity)
        {
            return true;
        }

        public static bool HasMappingRights(string entity, string system)
        {
            return true;
        }
    }
}