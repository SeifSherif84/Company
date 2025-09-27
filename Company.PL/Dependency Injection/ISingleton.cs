namespace Company.PL.Dependency_Injection
{
    public interface ISingleton
    {
        public Guid Guid { get; set; }
        public string GetGuid();
    }
}
