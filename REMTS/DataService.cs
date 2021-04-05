using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;

namespace REMTS
{
    class DataService
    {
        public string Test()
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            using PowerShell ps = PowerShell.Create();
            ps.Runspace = runspace;
            ps.AddScript("Get-Process");
            Collection<PSObject> powerShellResult = ps.Invoke();
            runspace.Close();

            String result;

            foreach (var item in powerShellResult)
            {
                result += $"\n{item.Members.ToString()}";
            }

            return powerShellResult.ToString();
        }
    }
}
