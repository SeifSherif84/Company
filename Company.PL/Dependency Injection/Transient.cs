namespace Company.PL.Dependency_Injection
{
    public class Transient : ITransient
    {
        public Guid Guid { get; set; }
        public Transient()
        {
            Guid = Guid.NewGuid();
        }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
