namespace audit.Repositories
{
    using audit.Models;

    public static class AuditObjectAdapter
    {
        public static AuditModel ToViewModel(this AuditObject obj)
        {
            return new();
        }

        public static AuditObject ToEntityModel(this AuditModel model)
        {
            return new();
        }
    }

}