using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Repositories
{
    public class CommandRepository : ICommandRepository
    {
        #region Fields

        private readonly ConcurrentDictionary<string, Func<IContext, IPipe>> commandActions;
        private readonly ConcurrentDictionary<string, IPipelineInitializer> pipelineInitializers;

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
            pipelineInitializers = new ConcurrentDictionary<string, IPipelineInitializer>();
        }

        public void AddCommandDelegate(string identifier, 
            Func<IContext, IPipe> actionDelegate,
            IPipelineInitializer initializer = null)
        {
            commandActions.AddOrUpdate(
                identifier.ToLowerInvariant(), actionDelegate, 
                (cmdName, action) => action = actionDelegate);

            if (initializer != null)
            {
                pipelineInitializers.AddOrUpdate(
                    identifier.ToLowerInvariant(), initializer,
                    (cmdName, existingInitializer) => initializer);
            }
        }

        public void AddCommandDelegate(string identifier, Func<IContext, IPipe> actionDelegate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
