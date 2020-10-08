provides abstracts to build and consume data access scopes

samples:

    public interface IScopeAccessToDocumentDetails : IScopeAccessToData
    {
        Task<IDocumentDetail> GetDetailsFor(int documentID);

        Task<IReadOnlyCollection<IDocumentDetail>> GetDetailsFor(IReadOnlyCollection<int> documentIDs);

        Task<IReadOnlyCollection<IDocumentDetail>> GetDetailsFor(params int[] documentIDs);
    }

    internal sealed class DocumentDetailAccessScopeConfiguration :
        ContextScopeConfiguration<DocumentDetailAccessScope>
    {
        public override void LoadProvider(DbContextOptionsBuilder<DocumentDetailAccessScope> builder)
        {
// either
            builder.UseSqlServer(ConnectionDetails);
// or
            builder.UseSqlite(ConnectionDetails);
// but not both...
        }
    }

    public interface ICreateDocumentDetailAccessScopes :
        ICreateAccessScopes<IScopeAccessToDocumentDetails>
    {

    }

    internal sealed class DocumentDetailAccessScopeFactory :
        ContextScopeFactory<DocumentDetailAccessScope, IScopeAccessToDocumentDetails>,
        ICreateDocumentDetailAccessScopes
    {
        public DocumentDetailAccessScopeFactory(IConfigureContextScope configuration)
            : base(configuration)
        {
        }

        public override DocumentDetailAccessScope CreateScope(IConfigureContextScope configureContext) =>
            new DocumentDetailAccessScope(configureContext);
    }

    internal sealed class DocumentDetailAccessScope :
        DataAccessScope,
        IScopeAccessToDocumentDetails
    {
        public DocumentDetailAccessScope(IConfigureContextScope configuration)
            : base(configuration)
        {
            ...unless you inject more things into this class, no code here...
        }

        public async Task<IDocumentDetail> GetDetailsFor(int documentID)
        {
            ...your entity framework processing code here...
        }

        public async Task<IReadOnlyCollection<IDocumentDetail>> GetDetailsFor(IReadOnlyCollection<int> documentIDs)
        {
            ...your entity framework processing code here...
        }

        public async Task<IReadOnlyCollection<IDocumentDetail>> GetDetailsFor(params int[] documentIDs)
        {
            ...your entity framework processing code here...
        }
    }

you register your classes like this:

// Factories
[assembly: InternalRegistration(typeof(ICreateDocumentDetailAccessScopes), typeof(DocumentDetailAccessScopeFactory), TypeOfRegistrationScope.Singleton)]

// Configuration
[assembly: ConfigurationRegistration(typeof(IConfigureContextScope), typeof(DocumentDetailAccessScopeConfiguration), "your-app-settings-location")]
