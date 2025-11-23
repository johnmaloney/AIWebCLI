using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Models
{
    /// <summary>
    /// This is the meta data to generate command and query criteria
    /// See JSON files in the test projects
    /// </summary>
    public class Criteria : ICriteria
    {
        public string Identifier { get; set; }
        public string Command { get; set; }
        public string Descriptor { get; set; }
        public Dictionary<int, List<string>> CommandPipeGroups { get; set; }

       
        public IPipe GetPipeline(IPipe parentPipe)
        {
            // The first pipe will be the parent pipe and //
            // will hold all the other defined pipes //

            // The types are a dictionary of lists, based on the //
            // the numeric key (i.e. 1, 2, 3) that becomes the parent // 
            foreach (var commandPipeTypes in CommandPipeGroups)
            {
                foreach (var commandPipeType in commandPipeTypes.Value)
                {
                    if (parentPipe != null)
                        parentPipe.ExtendWith(commandPipeType.InstantiateObject<IPipe>());
                    else
                        parentPipe = commandPipeType.InstantiateObject<IPipe>();
                }               
            }
            return parentPipe;
        }

        public static Criteria CreateCriteria(string jsonData) 
        {
            return null;
        }
    }
}
