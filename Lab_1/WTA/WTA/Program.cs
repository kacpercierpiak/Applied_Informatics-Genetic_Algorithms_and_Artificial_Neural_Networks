using System;
using System.Collections.Generic;
using System.Linq;
using MLDataLib.Models;
using MLDataLib.Repositories;
using MLDataLib.Services;
using WTA.Controllers;
using WTA.Model;
using WTA.Services;

namespace WTA
{
    class Program
    {
        static void Main(string[] args)
        {
            var networkController = new NetworkController(new MLDataService(), new NetworkService( new OutputService()));
            networkController.WTA_Generate();
            Console.Read();
        }
    }
}
