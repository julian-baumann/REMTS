using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using RemoteExecuter.Entities;

namespace RemoteExecuter
{
    public class DeliveryOptimization
    {
        public static ConsoleResultItem[] GetDataFromRemotePC(RemotePcInfo pc)
        {
            Runspace runspace;

            if (pc.IpAddress == "local")
            {
                runspace = RunspaceFactory.CreateRunspace();
            }
            else
            {
                WSManConnectionInfo connectionInfo = new WSManConnectionInfo();
                connectionInfo.ComputerName = pc.IpAddress;
                runspace = RunspaceFactory.CreateRunspace(connectionInfo);
            }

            runspace.Open();

            List<ConsoleResultItem> result = new List<ConsoleResultItem>();

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
                    foreach (var error in ps.Streams.Error)
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
                            result.Add(
                                new ConsoleResultItem
                                {
                                    Name = member.Name,
                                    Value = member.Value.ToString()
                                }
                            );
                        }
                    }
                }
            }

            runspace.Close();
            return result.ToArray();
        }

        public static IEnumerable<ConsoleResult> RunFromList(RemotePcInfo[] computers)
        {
            int index = 0;

            foreach (RemotePcInfo pc in computers)
            {
                ConsoleResult consoleResult = new ConsoleResult
                {
                    Index = index,
                    Pc = pc,
                    State = ConsoleResultStates.Done
                };

                yield return consoleResult;

                ConsoleResultItem[] items = GetDataFromRemotePC(pc);

                consoleResult.Data = items;
                consoleResult.State = ConsoleResultStates.Done;

                yield return consoleResult;

                index++;
            }
        }
    }
}
