using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using RemoteExecuter;
using RemoteExecuter.Entities;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            var deliveryOptimization = new DeliveryOptimization();
            ExecutedCommandResult[] result = deliveryOptimization.GetDataFromRemotePC(new RemotePcInfo { IpAddress = "local" });

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name}: {item.Value}");
            }

        }
    }
}
