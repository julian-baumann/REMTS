using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Test
{
    class Program
    {
        private static void Test()
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;

                ps.AddScript("Get-ExecutionPolicy");
                Collection<PSObject> executionPolicyResults = ps.Invoke();
                string executionPolicy = executionPolicyResults[0].ToString();

                ps.AddScript("Set-ExecutionPolicy Unrestricted");
                ps.AddScript("Import-Module DeliveryOptimization");
                ps.AddScript($"Set-ExecutionPolicy {executionPolicy}");
                ps.AddScript("Get-DeliveryOptimizationPerfSnap");

                Collection<PSObject> powerShellResult = ps.Invoke();

                if (ps.HadErrors)
                {
                    foreach(var error in ps.Streams.Error)
                    {
                        throw new Exception(error.ToString());
                    }
                }

                foreach (PSObject item in powerShellResult)
                {
                    foreach (PSMemberInfo member in item.Members)
                    {
                        if (member.MemberType == PSMemberTypes.Property)
                        {
                            Console.WriteLine($"{member.Name}: {member.Value}");
                        }
                    }
                }
            }

            runspace.Close();
        }

        static void Main(string[] args)
        {
            Test();
        }
    }
}
