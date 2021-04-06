using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using RemoteExecuter.Entities;
using RemoteExecuter;

namespace REMTS
{
    public class DataService
    {
        private DataService _instance;

        private DeliveryOptimization _deliveryOptimization;

        public DataService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataService();
                }

                return _instance;
            }
        }

        public DataService()
        {
            _deliveryOptimization = new DeliveryOptimization();
        }


        public RemotePcInfo[] ComputerList { get; set; }

        public void RunDeliveryOptimization()
        {
            foreach (RemotePcInfo pc in ComputerList)
            {
                DeliveryOptimizationItem[] items = _deliveryOptimization.GetDataFromRemotePC(pc);
            }

        }
    }
}
