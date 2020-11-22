using System;
using System.IO.Compression;
using System.Net.Http;
using ContribSentry.Interface;
using Sentry.Extensibility;
using Sentry.Http;

namespace Sentry.Internal.Http
{
    /// <summary>
    /// Default Sentry HttpClientFactory
    /// </summary>
    /// <inheritdoc />
    internal class DefaultSentryHttpClientFactory : ISentryHttpClientFactory
    {
        /// <summary>
        /// Creates an <see cref="T:System.Net.Http.HttpClient" /> configure to call Sentry for the specified <see cref="T:Sentry.Dsn" />
        /// </summary>
        /// <param name="options">The HTTP options.</param>
        /// <returns></returns>
        public HttpClient Create(SentryOptions options)
        {
            using var a = Xunxo.Start("Ctor", "DefaultSentryHttpClientFactory");

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            using var b = Xunxo.Start("Ctor httpClientHandler", "DefaultSentryHttpClientFactory net2.1");
            var httpClientHandler = options.CreateHttpClientHandler?.Invoke() ?? new HttpClientHandler();
            if (options.HttpProxy != null)
            {
                httpClientHandler.Proxy = options.HttpProxy;
                options.DiagnosticLogger?.LogInfo("Using Proxy: {0}", options.HttpProxy);
            }
            b.Finish();


            // If the platform supports automatic decompression
            if (httpClientHandler.SupportsAutomaticDecompression)
            {
                // if the SDK is configured to accept compressed data
                httpClientHandler.AutomaticDecompression = options.DecompressionMethods;
            }
            else
            {
                options.DiagnosticLogger?.LogDebug("No response compression supported by HttpClientHandler.");
            }

            HttpMessageHandler handler = httpClientHandler;

            if (options.RequestBodyCompressionLevel != CompressionLevel.NoCompression)
            {
                if (options.RequestBodyCompressionBuffered)
                {
                    handler = new GzipBufferedRequestBodyHandler(handler, options.RequestBodyCompressionLevel);
                    options.DiagnosticLogger?.LogDebug("Using 'GzipBufferedRequestBodyHandler' body compression strategy with level {0}.", options.RequestBodyCompressionLevel);
                }
                else
                {
                    handler = new GzipRequestBodyHandler(handler, options.RequestBodyCompressionLevel);
                    options.DiagnosticLogger?.LogDebug("Using 'GzipRequestBodyHandler' body compression strategy with level {0}.", options.RequestBodyCompressionLevel);
                }
            }
            else
            {
                options.DiagnosticLogger?.LogDebug("Using no request body compression strategy.");
            }


            // Adding retry after last for it to run first in the pipeline
            using var c = Xunxo.Start("Ctor", "RetryAfterHandler");

            handler = new RetryAfterHandler(handler);

            c.Finish();

            using var d = Xunxo.Start("Ctor", "HttpClient");

            var client = new HttpClient(handler);

            d.Finish();

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            if (options.ConfigureClient is { } configureClient)
            {
                options.DiagnosticLogger?.LogDebug("Invoking user-defined HttpClient configuration action.");
                configureClient.Invoke(client);
            }

            return client;
        }
    }
}
