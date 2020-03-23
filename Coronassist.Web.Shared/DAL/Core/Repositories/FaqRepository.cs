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
        public FaqRepository(DatabaseService databaseService) : base(databaseService)
        {
        }

        public async Task<Faq> AddFaq(Faq faq)
        {
            try
            {
                var _faq = await DatabaseService.accountContext.Faqs.FirstOrDefaultAsync(p => p.FaqId == faq.FaqId);
                if(_faq != null)
                {
                    _faq.Description = faq.Description;
                     DatabaseService.accountContext.Faqs.Update(_faq);
                    await DatabaseService.accountContext.SaveChangesAsync();
                    return _faq;
                }
                else
                {
                    var faqEntity = await DatabaseService.accountContext.Faqs.AddAsync(faq);
                    await DatabaseService.accountContext.SaveChangesAsync();
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
                var faq = await DatabaseService.accountContext.Faqs.FirstOrDefaultAsync();
                return faq;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
