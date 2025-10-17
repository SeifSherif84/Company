namespace Company.PL.Dependency_Injection
{
    public interface IScoped
    {
        public Guid Guid { get; set; }
        public string GetGuid();
    }
}


#region MyRegion
// https://chatgpt.com/share/68cdd6c3-1f14-8005-96d4-5db596ba34d7 
// https://chatgpt.com/share/68db9306-062c-800f-ae25-eccb225a7ae0
// https://chatgpt.com/share/68eba6a1-a91c-800f-85f1-597b09b33108
#endregion