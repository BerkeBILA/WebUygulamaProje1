namespace WebUygulamaProje1.Models
{
    public interface IAnketTuruRepository : IRepository<AnketTuru>
    {
        void Guncelle(AnketTuru anketTuru);
        void Kaydet();
    }
}
