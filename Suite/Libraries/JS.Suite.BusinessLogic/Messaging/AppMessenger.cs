using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.Helpers;
using JS.Core.Foundation.Messaging;
using JS.Suite.BusinessLogic.JSSupport;
using JS.Suite.BusinessLogic.Messaging.Email;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.DataAbstraction.JSSupport;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.Messaging
{
    /// <summary>
    /// App Messenger
    /// </summary>
    public class AppMessenger : SingletonBase<AppMessenger>
    {
        //CONSIDER: We may want to store the Messages in the Database in case the Server suffers an outage.
        Dictionary<int, List<AppMessage>> rollupMessageCache = null;

        /// <summary>
        /// Sends an Application Message
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="traceLevel">The trace level.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns></returns>
        public async Task<IProcessResult> Send(MessageTypes messageType, string message, string subject = null, TraceLevels traceLevel = TraceLevels.Information, string stackTrace = null)
        {
            return await Send(new AppMessage() { Subject = subject, Message = message, MessageType = messageType, TraceLevel = traceLevel, StackTrace = stackTrace});
        }

        /// <summary>
        /// Sends an Application Message
        /// </summary>
        /// <param name="appMessage">The application message.</param>
        public async Task<IProcessResult> Send(AppMessage appMessage)
        {
            List<MessageTypeConfiguration> messageConfigurations = await GetMessageConfigurations(appMessage);

            foreach (MessageTypeConfiguration messageConfiguration in messageConfigurations)
            {
                if (messageConfiguration.RollupMessageDelayMinutes == null)
                {
                    SendMessage(appMessage, messageConfiguration);
                }
                else
                {
                    AddMessageToMessageCache(appMessage, messageConfiguration);
                }
            }

            return new ProcessResult(ResultCodes.Success);
        }

        private void SendMessage(AppMessage appMessage, MessageTypeConfiguration messageConfiguration)
        {
            switch ((MessageHandlers)messageConfiguration.MessageHandlerId)
            {
                case MessageHandlers.Trace:

                    //TODO: ENUM TO DESCRIPTION ((MessageTypes)messageConfiguration.MessageTypeId).ToString()
                    AppTraceHelper.Current.TraceMessage(appMessage.TraceLevel, appMessage.Message, appMessage.Subject, EnumHelper.Current.GetName((MessageTypes)messageConfiguration.MessageTypeId), appMessage.StackTrace);
                    break;
                case MessageHandlers.ApplicationLog:

                    ApplicationLogBusinessManager.Current.Log(appMessage.TraceLevel, appMessage.Message, appMessage.Subject, (MessageTypes)messageConfiguration.MessageTypeId, appMessage.StackTrace);
                    break;
                case MessageHandlers.Email:
                    
                    if (!String.IsNullOrEmpty(appMessage.StackTrace))
                    {
                        appMessage.Message += String.Format("<h6>Stack Trace</h6><p>{0}</p>", appMessage.StackTrace); 
                    }

                    EmailHelper.Current.Send(EmailTypes.AppMessage, appMessage, messageConfiguration.EmailAddresses);

                    break;
                default:
                    break;
            }
        }

        private static async Task<List<MessageTypeConfiguration>> GetMessageConfigurations(AppMessage appMessage)
        {
            Task<List<MessageTypeConfiguration>> messageConfigurationsTask = MessageTypeConfigurationBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new MessageTypeConfiguration()
            {
                MessageTypeId = (int)appMessage.MessageType,
                IsDefaultFlag = false
            });

            //Gets Default Message Configurations
            Task<List<MessageTypeConfiguration>> defaultMessageConfigurationsTask = MessageTypeConfigurationBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new MessageTypeConfiguration()
            {
                IsDefaultFlag = true
            });

            //Combines the MessageTypeConfiguration Lists and Filters the TraceLevels
            List<List<MessageTypeConfiguration>> messageConfigurationLists = (await Task.WhenAll(messageConfigurationsTask, defaultMessageConfigurationsTask)).ToList();

            List<MessageTypeConfiguration> messageConfigurations = new List<MessageTypeConfiguration>();

            foreach (var messageConfigurationList in messageConfigurationLists)
	        {
                messageConfigurations.AddRange(messageConfigurationList);
	        }

            messageConfigurations = messageConfigurations.Where(m => m.TraceLevelId == null || m.TraceLevelId >= (int)appMessage.TraceLevel).ToList();

            return messageConfigurations;
        }

        #region Rollup Message Processing

        private void AddMessageToMessageCache(AppMessage appMessage, MessageTypeConfiguration messageConfiguration)
        {
            if (rollupMessageCache == null)
            {
                rollupMessageCache = new Dictionary<int, List<AppMessage>>();
            }

            if (rollupMessageCache.ContainsKey(messageConfiguration.MessageTypeConfigurationId))
            {
                rollupMessageCache[messageConfiguration.MessageTypeConfigurationId].Add(appMessage);
            }
            else
            {
                rollupMessageCache.Add(messageConfiguration.MessageTypeConfigurationId, new List<AppMessage>() { appMessage });

                Thread thread = new Thread(new ParameterizedThreadStart(BuildRollupAppMessageAndSend));

                thread.Start(messageConfiguration);
            }
        }

        private void BuildRollupAppMessageAndSend(object messageConfigurationObject)
        {
            MessageTypeConfiguration messageConfiguration = (MessageTypeConfiguration)messageConfigurationObject;

            //Sleep for the Rollup Period
            Thread.Sleep(new TimeSpan(0, messageConfiguration.RollupMessageDelayMinutes ?? 0, 0));

            //Build the Rollup Message
            List<AppMessage> messageParts = rollupMessageCache[messageConfiguration.MessageTypeConfigurationId];

            AppMessage rollupMessage = new AppMessage();

            rollupMessage.MessageType = (MessageTypes)messageConfiguration.MessageTypeId;

            rollupMessage.Subject = String.Format("({0}) {1}", messageParts.Count, messageParts.First().Subject);
            rollupMessage.Message = String.Join("", messageParts.Select(mp => mp.Message));

            //Send the Rollup Message
            SendMessage(rollupMessage, messageConfiguration);

            //Clear the Cache of the Sent Message
            rollupMessageCache.Remove(messageConfiguration.MessageTypeConfigurationId);
        } 

        #endregion
    }
}
