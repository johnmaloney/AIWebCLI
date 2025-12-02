using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Mime;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Repositories
{
    public class CommandRepository : ICommandRepository
    {
        #region Fields

        private Dictionary<string, PipelineInitializer> pipelineInitializers =
            new Dictionary<string, PipelineInitializer>();
        private readonly ConcurrentDictionary<string, Func<IContext, IPipe>> commandActions;

        #endregion

        #region Properties

        public IPipe this[IContext context]
        {
            get
            {
                return this.commandActions[context.Identifier.ToLowerInvariant()](context);
            }
        }

        #endregion

        #region Methods

        public CommandRepository()
        {
            commandActions = new ConcurrentDictionary<string, Func<IContext, IPipe>>();
        }

        public void AddCommandDelegate(string identifier, 
            Func<IContext, IPipe> actionDelegate)
        {
            commandActions.AddOrUpdate(
                identifier.ToLowerInvariant(), actionDelegate, 
                (cmdName, action) => action = actionDelegate);
        }

        #endregion

        #region Private Classes

        private class PipelineInitializer
        {
            public Func<IPipe> PipeInitializer { get; set; }

            public Func<
                string, 
                object, 
                string[], 
                Dictionary<string, object>, 
                IContext>
                ContextInitializer { get; set; }
        }

        #endregion
    }
}
