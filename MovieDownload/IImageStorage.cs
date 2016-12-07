﻿namespace MovieDownload
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IImageStorage
    {
        Task DownloadAsync(string fileName, Stream outputStream, CancellationToken cancellationToken);
    }
}
