namespace Company.PL.Dependency_Injection
{
    public interface IScoped
    {
        public Guid Guid { get; set; }
        public string GetGuid();
    }
}
