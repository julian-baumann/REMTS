using RemoteExecuter;
using RemoteExecuter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Services
{
    class DataService
    {
        private static DataService _instance;

        public static DataService Instance
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
        }

        public RemotePcInfo[] ComputerList { get; set; }
    }
}
