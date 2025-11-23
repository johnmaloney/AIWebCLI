using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCLI.Core.Contracts;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Pipes
{
    public abstract class APipeContext : IContext, IEnvironmentalContext
    {
        #region Fields

        protected List<string> messages = new List<string>();

        #endregion

        #region Properties

        public string Identifier { get; set; }

        public string ResponseType { get; set; }

        public List<string> Arguments { get; set; }

        public Dictionary<string, object> Options { get; set; }

        public virtual string[] Messages { get { return messages.ToArray(); } }

        public virtual object Response { get; set; }

        public IEnvironmentRepository Environments { get; protected set; }


        #endregion

        #region Methods

        public void AddMessage(params string[] messages)
        {
            foreach (var message in messages)
            {
                this.messages.Add(message);
            }
        }

        public bool TryGetArgument(string argumentName, out string argumentValue)
        {
            if (Arguments != null)
            {
                var argument = Arguments.FirstOrDefault(a =>
                {
                    if (!string.IsNullOrEmpty(a))
                    {
                        var leftside = a.LeftSideOf(":");
                        return leftside.ToLower() == argumentName.ToLower();
                    }
                    return false;
                });

                if (!string.IsNullOrEmpty(argument))
                {
                    // Only two supported arg types right now are letter:value and --help //
                    if (argument.Contains(":"))
                    {
                        argumentValue = argument.RightSideOf(":");
                        return true;
                    }
                    else
                    {
                        argumentValue = argument.RightSideOf("--");
                        return true;
                    }
                }
            }
            argumentValue = string.Empty;
            return false;
        }

        #endregion
    }
}
