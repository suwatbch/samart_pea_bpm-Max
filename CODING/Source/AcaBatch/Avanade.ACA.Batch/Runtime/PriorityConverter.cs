// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System.Diagnostics;
using System.Threading;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// Represents a utility class for converting
	/// a <see cref="ExecutionPriorityLevel"/> value
	/// to a Thread or Process Priority level.
	/// </summary>
	/// <remarks></remarks>
	public class PriorityConverter
	{
        /// <summary>
        /// Maps the specified <see cref="ExecutionPriorityLevel"/> value
        /// to a <see cref="ThreadPriority"/> value.
        /// </summary>
        /// <param name="priority">Avanade.ACA.Batch.ExecutionPriorityLevel</param>
        /// <remarks><table><tr><th><see cref="ExecutionPriorityLevel"/></th><th><see cref="ThreadPriority"/></th></tr><tr><td><see cref="ExecutionPriorityLevel.Low"/></td><td><see cref="ThreadPriority.Lowest"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.Lowest"/></td><td><see cref="ThreadPriority.Lowest"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.BelowNormal"/></td><td><see cref="ThreadPriority.BelowNormal"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.Normal"/></td><td><see cref="ThreadPriority.Normal"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.AboveNormal"/></td><td><see cref="ThreadPriority.AboveNormal"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.High"/></td><td><see cref="ThreadPriority.Highest"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.Highest"/></td><td><see cref="ThreadPriority.Highest"/></td></tr></table></remarks>
        /// <returns>System.Threading.ThreadPriority</returns>
		public static ThreadPriority
			ToThreadPriorityLevel(ExecutionPriorityLevel priority)
		{
			if (priority == ExecutionPriorityLevel.Highest)
			{
				return ThreadPriority.Highest;
			}
			else if (priority == ExecutionPriorityLevel.AboveNormal)
			{
				return ThreadPriority.AboveNormal;
			}
			else if (priority == ExecutionPriorityLevel.Normal)
			{
				return ThreadPriority.Normal;
			}
			else if (priority == ExecutionPriorityLevel.BelowNormal)
			{
				return ThreadPriority.BelowNormal;
			}
			else if (priority == ExecutionPriorityLevel.Lowest
				|| priority == ExecutionPriorityLevel.Low)
			{
				return ThreadPriority.Lowest;
			}

			return ThreadPriority.Normal;
		}

        /// <summary>
        /// Maps the specified <see cref="ExecutionPriorityLevel"/> value
        /// to a <see cref="ProcessPriorityClass"/> value.
        /// </summary>
        /// <param name="priority">Avanade.ACA.Batch.ExecutionPriorityLevel</param>
        /// <remarks><table><tr><th><see cref="ExecutionPriorityLevel"/></th><th><see cref="ProcessPriorityClass"/></th></tr><tr><td><see cref="ExecutionPriorityLevel.Lowest"/></td><td><see cref="ProcessPriorityClass.Idle"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.Low"/></td><td><see cref="ProcessPriorityClass.BelowNormal"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.BelowNormal"/></td><td><see cref="ProcessPriorityClass.BelowNormal"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.Normal"/></td><td><see cref="ProcessPriorityClass.Normal"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.AboveNormal"/></td><td><see cref="ProcessPriorityClass.AboveNormal"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.High"/></td><td><see cref="ProcessPriorityClass.High"/></td></tr><tr><td><see cref="ExecutionPriorityLevel.Highest"/></td><td><see cref="ProcessPriorityClass.RealTime"/></td></tr></table></remarks>
        /// <returns>System.Diagnostics.ProcessPriorityClass</returns>
		public static ProcessPriorityClass
			ToProcessPriorityClass(ExecutionPriorityLevel priority)
		{
			if (priority == ExecutionPriorityLevel.Highest)
			{
				return ProcessPriorityClass.RealTime;
			}
			else if (priority == ExecutionPriorityLevel.High)
			{
				return ProcessPriorityClass.High;
			}
			else if (priority == ExecutionPriorityLevel.AboveNormal)
			{
				return ProcessPriorityClass.AboveNormal;
			}
			else if (priority == ExecutionPriorityLevel.Normal)
			{
				return ProcessPriorityClass.Normal;
			}
			else if (priority == ExecutionPriorityLevel.BelowNormal
				|| priority == ExecutionPriorityLevel.Low)
			{
				return ProcessPriorityClass.BelowNormal;
			}
			else if(priority == ExecutionPriorityLevel.Lowest)
			{
				return ProcessPriorityClass.Idle;
			}
			
			return ProcessPriorityClass.Normal;
			
		}
	}
}
