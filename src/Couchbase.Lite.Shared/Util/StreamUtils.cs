﻿using System;
using Sharpen;
using System.IO;
using System.Collections.Generic;

namespace Couchbase.Lite.Util
{
    public static class StreamUtils
    {
        /// <exception cref="System.IO.IOException"></exception>
        internal static void CopyStreamsToFolder(IDictionary<String, Stream> streams, FilePath folder)
        {
            foreach (var entry in streams)
            {
                var filename = Path.GetFileNameWithoutExtension(entry.Key).ToUpperInvariant() + Path.GetExtension(entry.Key);
                var file = new FilePath(folder, filename);
                CopyStreamToFile(entry.Value, file);
            }
        }

        /// <exception cref="System.IO.IOException"></exception>
        internal static void CopyStreamToFile(Stream inStream, FilePath file)
        {
            var outStream = new FileStream(file.GetAbsolutePath(), FileMode.OpenOrCreate);
            var n = 0;
            var buffer = new byte[16384];
            while ((n = inStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                outStream.Write(buffer, 0, n);
            }
            outStream.Dispose();
            inStream.Dispose();
        }
    }
}

