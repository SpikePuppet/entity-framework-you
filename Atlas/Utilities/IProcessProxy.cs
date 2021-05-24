// -----------------------------------------------------------------------
// <copyright file="IProcessProxy.cs" company="Pooled Energy Pty Ltd">
//     Copyright (c) Copyright Pooled Energy Pty Ltd 2017
//     All rights reserved.
// </copyright>
// <author>Clint Irving</author>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Atlas.Utilities
{
    public interface IProcessProxy
    {
        /// <summary>
        /// Run a process with the given list of arguments
        /// </summary>
        /// <param name="path"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        string Run(string path, List<string> arguments);

        /// <summary>
        /// Run a process with the given list of arguments
        /// </summary>
        /// <param name="path"></param>
        /// <param name="arguments"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        string Run(string path, List<string> arguments, int timeout);

        /// <summary>
        /// Run a process with the given list of arguments
        /// </summary>
        /// <param name="path"></param>
        /// <param name="arguments"></param>
        /// <param name="timeout"></param>
        /// <param name="wrapParameters">Whether to autowrap parameters with " or "" or """</param>
        /// <returns></returns>
        string Run(string path, List<string> arguments, int timeout, ProcessProxyAutoWrapParameters wrapParameters);
    }
}
