
namespace Company.PL.Dependency_Injection
{
    public class Scoped : IScoped
    {
        public Guid Guid { get; set; }
        public Scoped()
        {
            Guid = Guid.NewGuid();
        }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
