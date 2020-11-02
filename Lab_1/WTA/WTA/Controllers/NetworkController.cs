using MLDataLib.Models;
using MLDataLib.Services;
using System;
using System.Collections.Generic;
using System.Text;
using WTA.Services;

namespace WTA.Controllers
{
    class NetworkController
    {
        private IMLDataService _MLDataService { get; set; }
        private INetworkService _networkService { get; set; }
        public NetworkController(IMLDataService mLDataService, INetworkService networkService)
        {
            _MLDataService = mLDataService;
            _networkService = networkService;
        }
        public void WTA_Generate()
        {       
            WTAMLModel data = _MLDataService.GetMLData(MLDataService.MLRepos.Lab1);
            _networkService.Calc(data);
        }
    }
}
