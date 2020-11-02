using System;
using System.Collections.Generic;
using System.Text;
using static MLDataLib.Services.MLDataService;

namespace MLDataLib.Services
{
    public interface IMLDataService
    {
        public dynamic GetMLData(MLRepos mLRepos);
    }
}
