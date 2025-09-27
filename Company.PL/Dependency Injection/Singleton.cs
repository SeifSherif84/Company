namespace Company.PL.Dependency_Injection
{
    public class Singleton : ISingleton
    {
        public Guid Guid { get; set; }
        public Singleton()
        {
            Guid = Guid.NewGuid();
        }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
