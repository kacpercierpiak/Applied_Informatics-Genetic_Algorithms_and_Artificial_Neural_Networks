using MLDataLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace WTA.Services
{
    interface INetworkService
    {
        void Calc(WTAMLModel data);
    }
}
