// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections;

using Avanade.ACA.Batch.Instrumentation;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents a mapping from an exception
	/// type to an <see cref="IExceptionHandler"/>
	/// object.
	/// </summary>
	public class UnhandledExceptionHandlerCollection
	{
		private Hashtable						_handlers;
		private UnhandledExceptionEventHandler	_eventHandler;

        /// <summary>
        /// Creates a new instance of the <see cref="UnhandledExceptionHandlerCollection"/> class.
        /// </summary>
		public UnhandledExceptionHandlerCollection()
		{
			_eventHandler = new UnhandledExceptionEventHandler(OnUnhandledException);
			_handlers = new Hashtable();
		}

        /// <summary>
        /// Registers the current object as an event handler for
        /// any unhandled exception in the current AppDomain.
        /// </summary>
        /// <returns>void</returns>
		public void Enable()
		{
			AppDomain.CurrentDomain.UnhandledException += _eventHandler;
		}

        /// <summary>
        /// Removes the current object as an event handler
        /// for any unhandled exceptions in the current
        /// AppDomain.
        /// </summary>
        /// <returns>void</returns>
		public void Disable()
		{
			AppDomain.CurrentDomain.UnhandledException -= _eventHandler;
		}

        /// <summary>
        /// Adds the specified <see cref="IExceptionHandler"/>
        /// to the collection.
        /// </summary>
        /// <param name="handler">Avanade.ACA.Batch.IExceptionHandler</param>
        /// <returns>void</returns>
		public void Add(IExceptionHandler handler)
		{
			_handlers.Add(handler.ExceptionType, handler);
		}


        /// <summary>
        /// Maps the exception to an <see cref="IExceptionHandler"/> object
        /// and lets the IExceptionHandler handle it.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="args">System.UnhandledExceptionEventArgs</param>
        /// <returns>void</returns>
		protected virtual 
			void OnUnhandledException(object sender, 
			UnhandledExceptionEventArgs args)
		{
			if (args.ExceptionObject is Exception)
			{
				Exception e = (Exception)args.ExceptionObject;

				if (!(e is ACABatchException))
				{
                    BatchInstrumentationProvider.Instance.FireBatchArchitectureFailedEvent("Unhandled Exception occurred", e);
				}

				Type exceptionType = e.GetType();

				Type baseType	= exceptionType;
				Type objectType = typeof(object);
				IExceptionHandler handler = null;

				// loop through the 
				// exception type's inheritance
				// hierarchy, looking for 
				// matching handler.
				while (baseType != objectType)
				{
					handler = (IExceptionHandler)_handlers[baseType];

					if (handler != null)
					{
						break;
					}

					baseType = baseType.BaseType;
				}

				if (handler != null)
				{
					handler.Handle(e);
				}
			}
		}

	}
}
