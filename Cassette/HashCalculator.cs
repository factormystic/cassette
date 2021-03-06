﻿/*
 * Copyright 2015-2017 Drew Noakes
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.IO;
using System.Security.Cryptography;

namespace Cassette
{
    /// <summary>
    /// Computes hashes from data.
    /// </summary>
    public static class HashCalculator
    {
        private static readonly SHA1 _hashFunction = SHA1.Create();

        /// <summary>
        /// Compute the hash over the contents of <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path of a file to process.</param>
        /// <returns>The hash of <paramref name="path"/>'s contents.</returns>
        public static Hash Compute(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan))
                return Compute(fileStream);
        }

        /// <summary>
        /// Compute the hash over the contents of <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The stream to process.</param>
        /// <returns>The hash of <paramref name="stream"/>'s contents.</returns>
        public static Hash Compute(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            return Hash.FromBytes(_hashFunction.ComputeHash(stream));
        }
    }
}