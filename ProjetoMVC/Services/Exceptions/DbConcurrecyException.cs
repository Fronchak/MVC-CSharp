namespace ProjetoMVC.Services.Exceptions
{
    public class DbConcurrecyException: ApplicationException
    {
        public DbConcurrecyException(string msg) : base(msg) { }
    }
}
