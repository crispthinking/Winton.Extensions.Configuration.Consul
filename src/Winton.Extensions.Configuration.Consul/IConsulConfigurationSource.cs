// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Net.Http;
using System.Threading;
using Consul;
using Microsoft.Extensions.Configuration;
using Winton.Extensions.Configuration.Consul.Parsers;
using Winton.Extensions.Configuration.Consul.Parsers.Json;

namespace Winton.Extensions.Configuration.Consul
{
    /// <inheritdoc />
    /// <summary>
    ///     An <see cref="IConfigurationSource" /> for the ConsulConfigurationProvider.
    /// </summary>
    public interface IConsulConfigurationSource : IConfigurationSource
    {
        /// <summary>
        ///     Gets a <see cref="CancellationToken" /> that can be used to cancel any consul requests
        ///     either loading or watching via long polling.
        ///     It is recommended that this is cancelled during shut down.
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        ///     Gets or sets an <see cref="Action" /> to be applied to the <see cref="ConsulClientConfiguration" />
        ///     during construction of the <see cref="IConsulClient" />.
        ///     Allows the default config options for Consul to be overriden.
        /// </summary>
        Action<ConsulClientConfiguration> ConsulConfigurationOptions { get; set; }

        /// <summary>
        ///     Gets or sets an <see cref="Action" /> to be applied to the <see cref="HttpClientHandler" />
        ///     during construction of the <see cref="IConsulClient" />.
        ///     Allows the default HTTP client hander options for Consul to be overriden.
        /// </summary>
        Action<HttpClientHandler> ConsulHttpClientHandlerOptions { get; set; }

        /// <summary>
        ///     Gets or sets an <see cref="Action" /> to be applied to the <see cref="HttpClient" /> during
        ///     construction of the <see cref="IConsulClient" />.
        ///     Allows the default HTTP client options for Consul to be overriden.
        /// </summary>
        Action<HttpClient> ConsulHttpClientOptions { get; set; }

        /// <summary>
        ///     Gets the key in Consul where the configuration is located.
        /// </summary>
        string Key { get; }

        /// <summary>
        ///     Gets the portion of the Consul key to remove from the configuration keys.
        ///     By default, when the configuration is parsed, the keys are created by removing the root key in Consul
        ///     where the configuration is located.
        ///     If this property is set then this string is removed instead of the Consul root key.
        /// </summary>
        string KeyToRemove { get; }

        /// <summary>
        ///     Gets or sets an <see cref="Action" /> that is invoked when an exception is raised during config load.
        ///     Used by clients to handle the exception if possible and prevent it from being thrown.
        /// </summary>
        Action<ConsulLoadExceptionContext> OnLoadException { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="Func{ConsulWatchException, TimeSpan}" /> that is invoked when an exception is raised whilst watching.
        ///     The <see cref="TimeSpan"/> returned by the function is waited before trying again.
        /// </summary>
        /// <remarks>
        ///     This function is useful for implementing backoff strategies.
        ///     It also provides access to the <see cref="CancellationToken"/> which can be used to cancel the watch task.
        /// </remarks>
        Func<ConsulWatchExceptionContext, TimeSpan> OnWatchException { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the config is optional.
        /// </summary>
        bool Optional { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="IConfigurationParser" /> to use when parsing the config.
        ///     Allows different data formats to be stored in consul under the given key.
        ///     Defaults to <see cref="JsonConfigurationParser" />.
        /// </summary>
        IConfigurationParser Parser { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the source will be reloaded if the data in consul changes.
        /// </summary>
        bool ReloadOnChange { get; set; }
    }
}