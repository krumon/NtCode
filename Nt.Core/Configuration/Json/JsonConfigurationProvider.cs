﻿using System;
using System.IO;
using System.Text.Json;

namespace Nt.Core.Configuration.Json
{
    /// <summary>
    /// A JSON file based <see cref="FileConfigurationProvider"/>.
    /// </summary>
    public class JsonConfigurationProvider : FileConfigurationProvider
    {
        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public JsonConfigurationProvider(JsonConfigurationSource source) : base(source) { }

        /// <summary>
        /// Loads the JSON data from a stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public override void Load(Stream stream)
        {
            try
            {
                Data = JsonConfigurationFileParser.Parse(stream);
            }
            catch (JsonException e)
            {
                throw new FormatException($"JSONParseError, {e}.");
            }
        }
    }
}
