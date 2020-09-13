using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MEESEES2.Services
{
    public interface IInterstitialAds
    {
        Task Display(string adID);
    }
}
