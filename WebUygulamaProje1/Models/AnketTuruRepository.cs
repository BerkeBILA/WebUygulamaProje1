using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Models
{
    public class AnketTuruRepository : Repository<AnketTuru>, IAnketTuruRepository
    {
        private  UygulamaDbContext _uygulamaDbContext;
        public AnketTuruRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext; 
        }

        public void Guncelle(AnketTuru anketTuru)
        {
            _uygulamaDbContext.Update(anketTuru);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
