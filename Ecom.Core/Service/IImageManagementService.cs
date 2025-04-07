using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ecom.Core.Service
{
    public interface IImageManagementService
    {
        Task<List<string>> AddImageAsync(IFormFileCollection files, string Src);
        void DeleteImageAsync( string Src);
    }
}
