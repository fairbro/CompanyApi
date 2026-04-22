namespace CompanyApi.Exceptions
{
    public class CompanyNotFoundException : Exception
    {
        public int Id { get; }

        public CompanyNotFoundException(int id)
            : base($"Company with id {id} does not exist")
        {
            Id = id;
        }
    }
}
