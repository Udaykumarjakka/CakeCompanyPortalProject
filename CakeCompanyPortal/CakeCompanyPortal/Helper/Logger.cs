using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CakeCompanyPortal.Helper
{
    public sealed class Log
    {
        #region Constructor

        Log()
        { }

        #endregion

        #region Private Static Methods

        private static void LogToConsole(string message)
        {
            System.Console.WriteLine(string.Format("{0}  {1}", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"), message));
        }

        #endregion

        #region Public Static Methods

        public static void Create(string message)
        {
            try
            {
                Create(Category.Operational, message, Priority.Low, 0, TraceEventType.Information, string.Empty);
            }
            catch (Exception )
            {
                throw;
            }
        }

        public static void Create(string message, bool toConsole)
        {
            try
            {
                if (toConsole) LogToConsole(message);

                Create(message);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public static void Create(Category category, string message, Priority priority, Int32 eventId, TraceEventType severity, string title)
        {
            try
            {
                Logger.Write(
              new LogEntry(message, category.ToString(), (int)priority, eventId, severity, title, null)
          );
            }
            catch (Exception)
            {

                throw;
            }

        }

     

        public static void Create(Category category, string message, Priority priority, Int32 eventId, TraceEventType severity, string title, Exception exception)
        {
            Dictionary<string, object> messageAttributes = new Dictionary<string, object>();

            if (exception != null)
            {
                //Swati : 8 Dec : To check if item is already added into dictionary: Needs to change
                if (!messageAttributes.ContainsKey("Exception_StackTrace"))
                    messageAttributes.Add("Exception_StackTrace", exception.StackTrace);
            }

            Logger.Write
            (
                new LogEntry(message, category.ToString(), (int)priority, eventId, severity, title, messageAttributes)
            );
        }

        public static void Create(Exception exception, Int32 eventId)
        {
            Create(Category.Developer, exception.Message + Environment.NewLine+exception.StackTrace, Priority.High, eventId, TraceEventType.Error, string.Empty, exception);
        }

        public static void Create(Exception exception)
        {
            Create(exception, 0);
        }

        #endregion
    }

    public enum Priority : int
    {
        None,
        Low = 1,
        Medium = 3,
        High = 5
    }

    public enum Category
    {
        Developer,
        Operational,
        System,
        MPIArcGISCode
    }
}
