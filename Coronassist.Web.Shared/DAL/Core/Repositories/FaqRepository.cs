using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class FaqRepository : BaseRepository, IFaqRepository
    {
        public FaqRepository(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        public async Task<Faq> AddFaq(Faq faq)
        {
            try
            {
                var _faq = await accountDbContext.Faqs.FirstOrDefaultAsync(p => p.FaqId == faq.FaqId);
                if(_faq != null)
                {
                    _faq.Description = faq.Description;
                     accountDbContext.Faqs.Update(_faq);
                    await accountDbContext.SaveChangesAsync();
                    return _faq;
                }
                else
                {
                    var faqEntity = await accountDbContext.Faqs.AddAsync(faq);
                    await accountDbContext.SaveChangesAsync();
                    return faqEntity.Entity;
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Faq> GetFaq()
        {
            try
            {
                var faq = await accountDbContext.Faqs.FirstOrDefaultAsync();
                return faq;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
