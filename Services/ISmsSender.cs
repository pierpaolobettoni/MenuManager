using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clean_aspnet_mvc.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
