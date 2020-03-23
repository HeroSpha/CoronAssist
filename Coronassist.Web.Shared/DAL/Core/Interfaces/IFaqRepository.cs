using Coronassist.Web.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Interfaces
{
    public interface IFaqRepository
    {
        Task<Faq> AddFaq(Faq faq);
        Task<Faq> GetFaq();
        
    }
}
