namespace WebUygulamaProje1.Models
{
    public interface IAnketRepository : IRepository<Anket>
    {
        void Guncelle(Anket anket);
        void Kaydet();
    }
}
