namespace ShoeStore.Helpers.ServerContexts
{ 
    internal static class RolesContext
    {
        private static readonly string _backendHostUrl;

        static RolesContext()
        {
            _backendHostUrl = ServerContext.BackendHostUrl;
        }
    }
}
