using System.Linq.Expressions;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Models
{
    public class AnketRepository : Repository<Anket>, IAnketRepository
    {
        private UygulamaDbContext _uygulamaDbContext;
        public AnketRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Anket anket)
        {
            _uygulamaDbContext.Update(anket);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
