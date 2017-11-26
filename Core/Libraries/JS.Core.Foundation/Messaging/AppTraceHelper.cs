using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Messaging
{
    /// <summary>
    /// App Trace Helper
    /// </summary>
    public class AppTraceHelper : SingletonBase<AppTraceHelper>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppTraceHelper"/> class.
        /// </summary>
        public AppTraceHelper()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="traceLevel">The trace level.</param>
        /// <param name="message">The message.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns></returns>
        public IProcessResult TraceMessage(TraceLevels traceLevel, string message, string subject = null, string messageType = null, string stackTrace = null)
        {
            ILog log = LogManager.GetLogger("TraceLog");

            List<string> messageParts = new List<string>();

            if (!String.IsNullOrEmpty(messageType))
            {
                messageParts.Add(messageType);
            }

            if (!String.IsNullOrEmpty(subject))
            {
                messageParts.Add(subject);
            }

            if (!String.IsNullOrEmpty(message))
            {
                messageParts.Add(message);
            }

            string traceMessage = String.Join(" - ", messageParts);

            if (!String.IsNullOrEmpty(stackTrace))
            {
                traceMessage += String.Format("\n{0}", stackTrace);
            }

            switch (traceLevel)
            {
                case TraceLevels.Fatal:

                    log.Fatal(traceMessage);
                    break;
                case TraceLevels.Error:

                    log.Error(traceMessage);
                    break;
                case TraceLevels.Warning:

                    log.Warn(traceMessage);
                    break;
                case TraceLevels.Information:

                    log.Info(traceMessage);
                    break;
                case TraceLevels.Debug:

                    log.Debug(traceMessage);
                    break;
                default:
                    break;
            }

            return new ProcessResult(ResultCodes.Success);
        }
    }
}
