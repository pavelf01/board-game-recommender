namespace DAL.Entity
{
    public interface BaseEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
