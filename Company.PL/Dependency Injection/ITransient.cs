namespace Company.PL.Dependency_Injection
{
    public interface ITransient
    {
        public Guid Guid { get; set; }
        public string GetGuid();
    }
}
