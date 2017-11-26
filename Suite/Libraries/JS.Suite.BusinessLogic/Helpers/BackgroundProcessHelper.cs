using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace JS.Suite.BusinessLogic.Helpers
{
    /// <summary>
    /// Background Process Helper
    /// </summary>
    public class BackgroundProcessHelper : SingletonBase<BackgroundProcessHelper>
    {

        private Action<Action<CancellationToken>, CancellationToken> backgroundProcessAction = (childAction, cancellationToken) => 
        {
            try
            {
                childAction(cancellationToken);
            }
            catch (Exception e)
            {
                ExceptionHelper.Current.SendAppMessageForException(e);
            }
        };

        /// <summary>
        /// Triggers the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public IProcessResult Trigger(Action<CancellationToken> action)
        {
            if (!String.IsNullOrEmpty(System.Web.HttpRuntime.AppDomainAppId))
            {
                HostingEnvironment.QueueBackgroundWorkItem(action); 
            }
            else
            {
                Thread backgroundThread = new Thread(new ParameterizedThreadStart(obj => action((CancellationToken)obj)));

                backgroundThread.Start(new CancellationToken());
            }

            return new ProcessResult(ResultCodes.Success);
        }
    }
}
